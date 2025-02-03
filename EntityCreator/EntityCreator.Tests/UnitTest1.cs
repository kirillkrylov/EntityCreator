using EntityCreator.Columns;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace EntityCreator.Tests;

[TestFixture]
public class ColumnFactory_Tests {

	
	[Test]
	[Ignore("Used as launcher")]
	public void TextColumns(){
	
		const string columnName = "AtfName";
		Guid schemaUId = Guid.NewGuid();
		Guid packageId = Guid.NewGuid();
		
		TextColumn? textColumn = ColumnFactory.CreateColumn<TextColumn>(schemaUId, columnName, packageId, false, false);
		BaseColumn tc = ColumnFactory.CreateColumn(DataValueType.ColumnType.Text, schemaUId, columnName, packageId);
		tc.DataValueTypeUId.Should().Be(textColumn.DataValueTypeUId);
		
		var tcJson = tc.ToJson();
		var textColumnJson = textColumn.ToJson();
		
		
		WebColumn? webColumn = ColumnFactory
			.CreateColumn<WebColumn>(schemaUId, columnName, packageId, false, false);
		string jsonWeb = webColumn?.ToJson() ?? "";
		
	}
	
	
	[Test]
	[Ignore("Used as launcher")]
	public void Create(){
		
		var tpl = Substitute.For<Template>(null, null);
		
		Entity entity = new (tpl, new LoggerFactory()) {
			Name = "AtfRepository",
			PackageId = Guid.NewGuid()
		};
		entity.AddColumn("AtfName", DataValueType.ColumnType.Text);
		entity.AddColumn("AtfWeb", DataValueType.ColumnType.Web);
		string metadata = entity.GenerateMetadataText();
		var a = "";
	}
}