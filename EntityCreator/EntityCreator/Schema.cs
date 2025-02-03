using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using EntityCreator.ParentEntity;
using Microsoft.Extensions.Logging;

namespace EntityCreator;

public interface ISchemaItem {
	
	public string Name { get; init; }
	public Guid UId { get; init; }
	public Guid PackageId { get; init; }
	public string ManagerName { get; }
	
}


public abstract class SchemaItem : ISchemaItem {

	protected readonly ILoggerFactory LoggerFactory;

	protected SchemaItem(ILoggerFactory loggerFactory){
		LoggerFactory = loggerFactory;
	}

	[Metadata("MetaData.Schema.A2", MetadataAction.Set)]
	public required string Name { get; init; }

	[Metadata("MetaData.Schema.UId", MetadataAction.Set, true)]
	public Guid UId { get; init; } = Guid.NewGuid();

	[Metadata("MetaData.Schema.B6", MetadataAction.Set)]
	public required Guid PackageId { get; init; }
	
	public abstract string ManagerName { get; }

	[Metadata("MetaData.Schema.B8", MetadataAction.Set)]
	public string CreatedInVersion { get;} = "7.7.0.0";
	
} 


[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
public class MetadataContext<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>  {
	public static string GetMetadataText(T instance){
		StringBuilder sb = new();
		var properties = typeof(T)
						.GetProperties()
						.OrderByDescending(meta => meta.GetCustomAttribute<MetadataAttribute>()?.IsFirst ?? false);
								
		foreach (PropertyInfo property in properties) {
			MetadataAttribute? metadataAttribute = property.GetCustomAttribute<MetadataAttribute>();
			if (metadataAttribute != null) {
				switch (metadataAttribute.MetadataAction) {
					case MetadataAction.Set:
						sb.AppendLine($"= {metadataAttribute.Name} \"{property.GetValue(instance)}\"");
						break;
					case MetadataAction.Add:
						sb.AppendLine($"+ {metadataAttribute.Name}+ \"{property.GetValue(instance)}\"");
						break;
					case MetadataAction.Remove:
						sb.AppendLine($"- {metadataAttribute.Name}- \"{property.GetValue(instance)}\"");
						break;
					case MetadataAction.Merge:
						sb.AppendLine($"~ {metadataAttribute.Name}+ \"{property.GetValue(instance)}\"");
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
		return sb.ToString();
	}
}

public interface ISchema<out T> where T : ISchemaItem {

	public Descriptor Descriptor {get;}
	public string Metadata { get; set; }
	public Properties Properties { get; set; }
	public T SchemaItem { get; }
}


public class Schema<T> : ISchema<T> where T : ISchemaItem, new() {
	public Schema(string name, Guid packageId){
		SchemaItem = new T {
			Name = name,
			PackageId = packageId
		};
	}
	
	public Descriptor Descriptor => CreateDescriptor();
	public required string Metadata { get; set; }
	public required Properties Properties { get; set; }

	public T SchemaItem { get; }
	
	private Descriptor CreateDescriptor() => new(
			UId:SchemaItem.UId,
			Name: SchemaItem.Name,
			ModifiedOnUtc: DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
			ManagerName : SchemaItem.ManagerName,
			Caption : SchemaItem.Name,
			DependsOn : [],
			Parent : new Parent (SchemaItem.PackageId, "Package")
		);
}

public record PropertiesWrapper(
	[property:JsonPropertyName("Properties")] Properties Properties
);

public record Properties(
	[property:JsonPropertyName("AdministratedByColumns")] string AdministratedByColumns,
	[property:JsonPropertyName("AdministratedByOperations")] string AdministratedByOperations,
	[property:JsonPropertyName("AdministratedByRecords")] string AdministratedByRecords,
	[property:JsonPropertyName("CreatedInVersion")] string CreatedInVersion,
	[property:JsonPropertyName("IsSSPAvailable")] string IsSSPAvailable,
	[property:JsonPropertyName("IsTrackChangesInDB")] string IsTrackChangesInDB,
	[property:JsonPropertyName("IsVirtual")] string IsVirtual,
	[property:JsonPropertyName("UseLiveEditing")] string UseLiveEditing
);


public record DescriptorWrapper(
	[property:JsonPropertyName("Descriptor")] Descriptor Descriptor
);

public record Descriptor(
	[property:JsonPropertyName("UId")] Guid UId,
	[property:JsonPropertyName("Name")] string Name,
	[property:JsonPropertyName("ModifiedOnUtc")]string ModifiedOnUtc,
	[property:JsonPropertyName("Parent")] Parent Parent,
	[property:JsonPropertyName("ManagerName")] string ManagerName,
	[property:JsonPropertyName("Caption")] string Caption,
	[property:JsonPropertyName("DependsOn")] object[] DependsOn
);

public record Parent(
	[property:JsonPropertyName("UId")] Guid UId,
	[property:JsonPropertyName("Name")] string Name
);

