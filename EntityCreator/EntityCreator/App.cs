using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotMake.CommandLine;
using EntityCreator.Commands;
using EntityCreator.PgSql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EntityCreator;

[CliCommand(
	Description = "Add entity to Creatio from db table", 
	Parent = typeof(RootCliCommand),
	Name = "add-entity",
	Aliases = ["ae"]
	)]
public class App {

	[CliArgument(Description = "Entity name in Creatio", 
		Required = true, 
		Name = "entity",
		ValidationMessage = $"Entity name is required to be longer than 3 characters, can contain [A-Z a-z 0-9 _]",
		ValidationPattern = @"^[A-Za-z0-9_]{3,}$"
		)]
	public string EntityName { get; set; } = string.Empty;

	[CliArgument(Description = "Table to fetch columns from", Required = true, Name = "from")]
	public string? TableName { get; set; }
	
	[CliOption(Description = "Verbose logging",Required = false, Name = "verbose", HelpName = "Log level", Arity = CliArgumentArity.ExactlyOne)]
	public bool IsVerbose { get; set; }
	
	[CliOption(Description = "PackageId",Required = false, 
		Name = "package-id", 
		HelpName = "Package Id", 
		Arity = CliArgumentArity.ExactlyOne,
		ValidationMessage = $"Entity name is required to be longer than 3 characters, can contain [A-Z a-z 0-9 _]",
		ValidationPattern = @"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$" //GUID
		)]
	public string PackageId { get; set; }
	
	[CliOption(Description = "PackagePath",
		Required = false, 
		Name = "package-path", 
		HelpName = "Package path", 
		Arity = CliArgumentArity.ExactlyOne,
		ValidationMessage = "Package path must point to an existing package folder",
		ValidationRules = CliValidationRules.ExistingDirectory
		)]
	public string PackagePath { get; set; }
	
	private readonly PackageSettings _packageSetting;
	private readonly Writer _writer;
	private readonly Template _tpl;
	private readonly IPgsql _pgsql;
	private readonly ILogger<App> _logger;
	private readonly ILoggerFactory _loggerFactory;

	public App(IOptions<PackageSettings> setting, Writer writer, Template tpl, IPgsql pgsql, ILogger<App> logger, ILoggerFactory loggerFactory){
		_writer = writer;
		_tpl = tpl;
		_pgsql = pgsql;
		_logger = logger;
		_loggerFactory = loggerFactory;
		_packageSetting = setting.Value;
	}

	public async Task<int> RunAsync(){
		bool isGuid = Guid.TryParse(PackageId, out Guid pId);
		Entity entity = new (_tpl, _loggerFactory) {
			Name = EntityName,
			PackageId = isGuid ? pId : _packageSetting.PackageUId ?? Guid.NewGuid()
		};
		
		List<pColumn> columns = await _pgsql.GetColumnsAsync(TableName!);
		columns.ForEach(c => AddColumn(ref entity, c));
		
		if(!string.IsNullOrWhiteSpace(PackagePath) && Directory.Exists(PackagePath)){
			_packageSetting.SetPackageFolderPath(PackagePath);
		}
		
		_writer.CreateFolderIfNotExists(entity.Name);
		
		string metadata = entity.GenerateMetadataText();
		await _writer.WriteMetadataAsync(metadata, entity.Name);
		
		string descriptor = entity.GenerateDescriptorText();
		await _writer.WriteDescriptorAsync(descriptor,entity.Name);
		
		string properties = entity.GeneratePropertiesText();
		await _writer.WritePropertiesAsync(properties,entity.Name);
		
		string resource = entity.GenerateResourceXml();
		await _writer.WriteResourceAsync(resource, entity.Name);
		await _writer.WriteCSharpAsync(string.Empty, entity.Name);
		
		_logger.LogInformation(new EventId(200, "EntityCreated"),"Entity {entityName} created successfully.",entity.Name);
		return 0;
	}
	
	
	/// <summary>
	/// Adds a column to the specified entity based on the provided column information.
	/// </summary>
	/// <param name="entity">The entity to which the column will be added.</param>
	/// <param name="column">The column information used to determine the column type and name.</param>
	/// <remarks>
	/// This method determines the column type based on the data type received from pgsql and other properties of the provided column.
	/// It then adds the column to the entity, ensuring the column name is unique if it is a reserved name.
	/// </remarks>
	private static void AddColumn(ref Entity entity, pColumn column){
		DataValueType.ColumnType columnType = column.DataType switch {
			//"character varying" => DataValueType.ColumnType.MaxSizeText,
			"character varying" => column.CharacterMaximumLength switch {
							>0 and <=50 => DataValueType.ColumnType.ShortText,
							>50 and <=250  => DataValueType.ColumnType.MediumText,
							>251 and <=500 => DataValueType.ColumnType.LongText,
							var _ => DataValueType.ColumnType.MaxSizeText,
						},
			"character" => column.CharacterMaximumLength switch {
								>0 and <=50 => DataValueType.ColumnType.ShortText,
								>50 and <=250  => DataValueType.ColumnType.MediumText,
								>251 and <=500 => DataValueType.ColumnType.LongText,
								var _ => DataValueType.ColumnType.MaxSizeText,
							},
			"text" => column.CharacterMaximumLength switch {
							>0 and <=50 => DataValueType.ColumnType.ShortText,
							>50 and <=250  => DataValueType.ColumnType.MediumText,
							>251 and <=500 => DataValueType.ColumnType.LongText,
							var _ => DataValueType.ColumnType.MaxSizeText,
						},
			"integer" => DataValueType.ColumnType.Number,
			"smallint" => DataValueType.ColumnType.Number,
			"bigint" => DataValueType.ColumnType.Float,
			"numeric" => column.NumericScale switch {
							0 => DataValueType.ColumnType.Float,
							1 or 2 => DataValueType.ColumnType.Float2,
							3 => DataValueType.ColumnType.Float3,
							4 => DataValueType.ColumnType.Float4,
							var _ => DataValueType.ColumnType.Float8
						},
			"boolean" => DataValueType.ColumnType.Boolean,
			"date" => DataValueType.ColumnType.Date,
			"timestamp without time zone" => DataValueType.ColumnType.DateTime,
			"time without time zone" => DataValueType.ColumnType.Time,
			"uuid" => DataValueType.ColumnType.Guid,
			var _ => DataValueType.ColumnType.MaxSizeText
		};
		entity.AddColumn(EnsureUniqueColumnName(column.ColumnName), columnType);
	}
	
	/// <summary>
	/// Create base entity has a number of reserved column names (Id, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, ProcessListeners)
	/// This method checks that the column name is not in the reserved list and if it is, it renames it by adding suffix Original.
	/// </summary>
	/// <param name="originalName"></param>
	/// <returns></returns>
	private static string EnsureUniqueColumnName(string originalName){
		string[] reservedColumns = ["Id", "CreatedOn", "CreatedBy", "ModifiedOn", "ModifiedBy", "ProcessListeners"];

		return reservedColumns.Contains(originalName) 
			? $"{originalName}Original" 
			: originalName;
	}

}
