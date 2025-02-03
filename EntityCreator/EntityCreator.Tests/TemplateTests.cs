using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace EntityCreator.Tests;

[TestFixture]
public class TemplateTests {

	private MockFileSystem _mockFileSystem;
	private Tpl _tpl;
	
	[SetUp]
	public void Setup(){
		_mockFileSystem = new MockFileSystem();
		MockDriveData driveData = new () {
			DriveType = DriveType.Fixed, 
			IsReady = true
		};
		_mockFileSystem.AddDrive("T", driveData);
		_tpl = new Tpl {
			BaseEntity = "Tpl/BaseEntity.json.tpl",
		};
	}
	
	
	[Test]
	[Ignore("Used as launcher")]
	public async Task MetadataContent_ShouldStartWith(){

		//Arrange
		string currentDirectory = Path.Combine("T:", "EntityCreator");
		string tplDirectory  = Path.Combine(currentDirectory, "Tpl");
		string baseEntityTemplatePath = Path.Combine(tplDirectory, "BaseEntity.json.tpl");
		_mockFileSystem.Directory.SetCurrentDirectory(currentDirectory);
		
		MockFileData mockFileData = await GetFileContentAsync("Tpl/BaseEntity.json.tpl");
		_mockFileSystem.AddFile(baseEntityTemplatePath, mockFileData);
		
		IOptions<Tpl> tplOption = Options.Create(_tpl);
		Template sut = new (tplOption, _mockFileSystem);
		//Act
		string content = sut.GetContent("BaseEntity");
		
		//Assert
		const string l1 = @"= MetaData.Schema.UId";
		content.Should().StartWith(l1);
	}
	
	
	private async Task<MockFileData> GetFileContentAsync(string path){
		string projectDirectory =
		Path.Combine(Directory.GetCurrentDirectory(), "..","..","..","..","EntityCreator");
		
		var bytes = await File.ReadAllBytesAsync(Path.Combine(projectDirectory, path));
		return new MockFileData(bytes);
	}

}