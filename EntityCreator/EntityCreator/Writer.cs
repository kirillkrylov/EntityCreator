using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EntityCreator;

public class Writer(IFileSystem fileSystem, IOptions<PackageSettings> packageSettings) {

	#region Constants: Private

	private const string DescriptorFileName = "descriptor.json";
	private const string MetadataFileName = "metadata.json";
	private const string PropertiesFileName = "properties.json";
	private const string ResourceFileName = "resource.en-US.xml";
	private const string ResourcesFolderName = "Resources";
	private const string SchemasFolderName = "Schemas";

	#endregion

	#region Fields: Private

	private IDirectoryInfo PackageFolderPath 
		=>fileSystem.DirectoryInfo.New(packageSettings.Value.PackageFolderPath ?? string.Empty);

	#endregion

	#region Methods: Public

	public void CreateFolderIfNotExists(string entityName){
		if (PackageFolderPath.GetDirectories(SchemasFolderName, SearchOption.TopDirectoryOnly).Length == 0) {
			IDirectoryInfo diSchema = PackageFolderPath.CreateSubdirectory(SchemasFolderName);
			if (diSchema.GetDirectories(entityName, SearchOption.TopDirectoryOnly).Length == 0) {
				diSchema.CreateSubdirectory(entityName);
			}
		} else {
			IDirectoryInfo diSchema = PackageFolderPath.GetDirectories(SchemasFolderName, SearchOption.TopDirectoryOnly).First();
			if (diSchema.GetDirectories(entityName, SearchOption.TopDirectoryOnly).Length == 0) {
				diSchema.CreateSubdirectory(entityName);
			}
		}

		if (PackageFolderPath.GetDirectories(ResourcesFolderName, SearchOption.TopDirectoryOnly).Length == 0) {
			PackageFolderPath.CreateSubdirectory(ResourcesFolderName);
			IDirectoryInfo diResource = PackageFolderPath.CreateSubdirectory(ResourcesFolderName);
			if (diResource.GetDirectories(entityName, SearchOption.TopDirectoryOnly).Length == 0) {
				diResource.CreateSubdirectory($"{entityName}.Entity");
			}
		} else {
			IDirectoryInfo diResource = PackageFolderPath.CreateSubdirectory(ResourcesFolderName);
			if (diResource.GetDirectories(entityName, SearchOption.TopDirectoryOnly).Length == 0) {
				diResource.CreateSubdirectory($"{entityName}.Entity");
			}
		}
	}

	public Task WriteCSharpAsync(string content, string entityName){
		string resourceFilePath
			= Path.Join(PackageFolderPath.FullName, SchemasFolderName, entityName, $"{entityName}.cs");
		return fileSystem.File.WriteAllTextAsync(resourceFilePath, content);
	}

	public Task WriteDescriptorAsync(string content, string entityName){
		string descriptorFilePath
			= Path.Join(PackageFolderPath.FullName, SchemasFolderName, entityName, DescriptorFileName);
		return fileSystem.File.WriteAllTextAsync(descriptorFilePath, content);
	}

	public Task WriteMetadataAsync(string content, string entityName){
		string metadataFilePath
			= Path.Join(PackageFolderPath.FullName, SchemasFolderName, entityName, MetadataFileName);
		return fileSystem.File.WriteAllTextAsync(metadataFilePath, content);
	}

	public Task WritePropertiesAsync(string content, string entityName){
		string propertiesFilePath
			= Path.Join(PackageFolderPath.FullName, SchemasFolderName, entityName, PropertiesFileName);
		return fileSystem.File.WriteAllTextAsync(propertiesFilePath, content);
	}

	public Task WriteResourceAsync(string content, string entityName){
		string resourceFilePath = Path.Join(PackageFolderPath.FullName, ResourcesFolderName, $"{entityName}.Entity",
			ResourceFileName);
		return fileSystem.File.WriteAllTextAsync(resourceFilePath, content);
	}

	#endregion

}