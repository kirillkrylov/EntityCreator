using System.Collections.Generic;
using System.Text.Json.Serialization;
using EntityCreator.Columns;

namespace EntityCreator;

[JsonSourceGenerationOptions(
	WriteIndented = true,
	PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
)]
[JsonSerializable(typeof(DbSettings))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(AppSettings))]
[JsonSerializable(typeof(PackageSettings))]
[JsonSerializable(typeof(Tpl))]
[JsonSerializable(typeof(Properties))]
[JsonSerializable(typeof(PropertiesWrapper))]
[JsonSerializable(typeof(Descriptor))]
[JsonSerializable(typeof(DescriptorWrapper))]
[JsonSerializable(typeof(Parent))]
[JsonSerializable(typeof(BaseColumn))]
[JsonSerializable(typeof(TextColumn))]
[JsonSerializable(typeof(NumberColumn))]
[JsonSerializable(typeof(WebColumn))]
[JsonSerializable(typeof(LongTextColumn))]
[JsonSerializable(typeof(MediumTextColumn))]
[JsonSerializable(typeof(ShortTextColumn))]
[JsonSerializable(typeof(EmailTextColumn))]
[JsonSerializable(typeof(MaxSizeTextColumn))]
[JsonSerializable(typeof(BooleanColumn))]
[JsonSerializable(typeof(GuidColumn))]
[JsonSerializable(typeof(DateTimeColumn))]
[JsonSerializable(typeof(DateColumn))]
[JsonSerializable(typeof(TimeColumn))]
[JsonSerializable(typeof(FloatColumn))]
[JsonSerializable(typeof(Float1Column))]
[JsonSerializable(typeof(Float2Column))]
[JsonSerializable(typeof(Float3Column))]
[JsonSerializable(typeof(Float4Column))]
[JsonSerializable(typeof(Float8Column))]
[JsonSerializable(typeof(MoneyColumn))]
[JsonSerializable(typeof(BinaryColumn))]
[JsonSerializable(typeof(PhoneColumn))]
[JsonSerializable(typeof(SecureTextColumn))]
[JsonSerializable(typeof(RichTextColumn))]

public partial class AppJsonSerializerContext : JsonSerializerContext { }