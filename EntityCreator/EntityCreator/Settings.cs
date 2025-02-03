using System;
using System.Text.Json.Serialization;

namespace EntityCreator;


public class AppSettings {

	[JsonPropertyName("DbSettings")]
	public DbSettings? DbSettings { get; set; }
	
	[JsonPropertyName("PackageSettings")]
	public PackageSettings? PackageSettings { get; set; }
	
	[JsonPropertyName("Tpl")]
	public Tpl? Tpl { get; set; }
	

}

public class DbSettings {
	

	[JsonPropertyName("connectionString")]
	public string? ConnectionString { get; set; }

}

public class PackageSettings {

	[JsonPropertyName("packageFolderPath")]
	public string? PackageFolderPath { get; set; }

	[JsonPropertyName("packageUId")]
	public Guid? PackageUId { get; set; }
	
	public void SetPackageUId(Guid packageUId){
		PackageUId = packageUId;
	}
	public void SetPackageFolderPath(string packageFolderPath){
		PackageFolderPath = packageFolderPath;
	}

}


public class Tpl {

	[JsonPropertyName("BaseEntity")]
	public string? BaseEntity { get; set; }

}