using System;
using System.IO;
using System.IO.Abstractions;
using Microsoft.Extensions.Options;

namespace EntityCreator;

public class Template(IOptions<Tpl> tpl, IFileSystem fileSystem) {

	public string GetContent(string templateName){
		string currentDirectory = fileSystem.Directory.GetCurrentDirectory();
		string? tplName = templateName switch {
							"BaseEntity" => tpl.Value.BaseEntity,
							var _ => throw new NotImplementedException($"{templateName} not implemented")
						};
		ArgumentException.ThrowIfNullOrWhiteSpace(tplName);
		string tplPath = Path.Combine(currentDirectory, tplName);
		if (!fileSystem.File.Exists(tplPath)){
			throw new FileNotFoundException($"Template file not found: {tplPath}");
		}
		return fileSystem.File.ReadAllText(tplPath);
	}

}