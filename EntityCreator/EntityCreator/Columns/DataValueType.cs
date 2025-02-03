using System;
using System.Collections.Generic;

namespace EntityCreator;


/// <summary>
/// Copies data value types as described in <c>Terrasoft.Core.DataValueType</c>.
/// </summary>
public class DataValueType {

	public enum ColumnType {
		Binary,
		Boolean,
		DateTime,
		Date,
		Time,
		Float,
		Float1,
		Float2,
		Float3,
		Float4,
		Float8,
		Money,
		Phone,
		RichText,
		SecureText,
		Guid,
		Number,
		Lookup,
		
		/// <summary>
		/// Unlimited length text
		/// </summary>
		MaxSizeText,
		
		/// <summary>
		/// Text 500 characters.
		/// </summary>
		LongText,
		
		/// <summary>
		/// Text 250 characters.
		/// </summary>
		MediumText,
		
		/// <summary>
		/// Text 50 characters.
		/// </summary>
		ShortText,
		
		EmailText,
		
		Text,
		
		/// <summary>
		/// Web column, limited to 250 characters
		/// </summary>
		Web
		
	}
	
	#region Properties: Public

	/// <summary>
	///  Binary data value type Id.
	/// </summary>
	public static Guid BinaryDataValueTypeUId { get; } = new("{B7342B7A-5DDE-40de-AA7C-24D2A57B3202}");

	/// <summary>
	///  Boolean data value type Id.
	/// </summary>
	public static Guid BooleanDataValueTypeUId { get; } = new("{90B65BF8-0FFC-4141-8779-2420877AF907}");

	/// <summary>
	///  Gets unique identifier of the color data value type.
	/// </summary>
	public static Guid ColorDataValueTypeUId { get; } = new("{DAFB71F9-EE9F-4e0b-A4D7-37AA15987155}");

	/// <summary>
	///  Gets unique identifier of the composite object data value type.
	/// </summary>
	public static Guid CompositeObjectDataValueTypeUId { get; } = new("632E4371-0A7F-46CD-A284-A623B3933027");

	/// <summary>Returns the composite object list data value type identifier.</summary>
	/// <value>The composite object list data value type identifier.</value>
	public static Guid CompositeObjectListDataValueTypeUId { get; } = new("651EC16F-D140-46DB-B9E2-825C985A8AC2");

	/// <summary>
	///  Date data value type Id.
	/// </summary>
	public static Guid DateDataValueTypeUId { get; } = new("{603D4960-A1A2-45e9-B232-206A54421B01}");

	/// <summary>
	///  DateTime data value type Id.
	/// </summary>
	public static Guid DateTimeDataValueTypeUId { get; } = new("{D21E9EF4-C064-4012-B286-FA1A8171DA44}");

	/// <summary>
	///  DbObjectName data value type Id.
	/// </summary>
	public static Guid DbObjectNameDataValueTypeUId { get; } = new("{0EAAA70F-2A5A-444e-BDF1-98B37895C820}");

	/// <summary>
	///  Email data value type Id.
	/// </summary>
	public static Guid EmailTextDataValueTypeUId { get; } = new("{66CBA64C-DAF1-4F36-B8EA-73C0D695D90C}");

	/// <summary>
	///  EntityCollection data value type Id.
	/// </summary>
	public static Guid EntityCollectionDataValueTypeUId { get; } = new("{51FB23BA-3EB2-11E2-B7D5-B0C76188709B}");

	/// <summary>
	///  EntityColumnMappingCollection data value type Id.
	/// </summary>
	public static Guid EntityColumnMappingCollectionDataValueTypeUId { get; } = new("{B53EAA2A-4BB7-4A6B-9F4F-58CCAB293E31}");

	/// <summary>
	///  Gets unique identifier of the file locator data value type.
	/// </summary>
	public static Guid FileLocatorDataValueTypeUId { get; } = new("A33C9252-D401-453E-949D-169157067ED9");

	/// <summary>
	///  Float1 data value type Id.
	/// </summary>
	public static Guid Float1DataValueTypeUId { get; } = new("{07BA84CE-0BF7-44B4-9F2C-7B15032EB98C}");

	/// <summary>
	///  Float2 data value type Id.
	/// </summary>
	public static Guid Float2DataValueTypeUId { get; } = new("{5CC8060D-6D10-4773-89FC-8C12D6F659A6}");

	/// <summary>
	///  Float3 data value type Id.
	/// </summary>
	public static Guid Float3DataValueTypeUId { get; } = new("{3F62414E-6C25-4182-BCEF-A73C9E396F31}");

	/// <summary>
	///  Float4 data value type Id.
	/// </summary>
	public static Guid Float4DataValueTypeUId { get; } = new("{FF22E049-4D16-46EE-A529-92D8808932DC}");

	/// <summary>
	///  Float8 data value type Id.
	/// </summary>
	public static Guid Float8DataValueTypeUId { get; } = new("{A4AAF398-3531-4A0D-9D75-A587F5B5B59E}");

	/// <summary>
	///  Float data value type Id.
	/// </summary>
	public static Guid FloatDataValueTypeUId { get; } = new("{57EE4C31-5EC4-45FA-B95D-3A2868AA89A8}");

	/// <summary>
	///  Guid data value type Id.
	/// </summary>
	public static Guid GuidDataValueTypeUId { get; } = new("{23018567-A13C-4320-8687-FD6F9E3699BD}");

	/// <summary>
	///  HashText data value type Id.
	/// </summary>
	public static Guid HashTextDataValueTypeUId { get; } = new("{ECBCCE18-2A17-4ead-829A-9D02FA9578A4}");

	/// <summary>
	///  Integer data value type Id.
	/// </summary>
	public static Guid IntegerDataValueTypeUId { get; } = new("{6B6B74E2-820D-490E-A017-2B73D4CCF2B0}");

	/// <summary>
	///  LocalizableParameterValuesList data value type Id.
	/// </summary>
	public static Guid LocalizableParameterValuesListDataValueTypeUId { get; } = new("{CFFC4762-C5C7-44BC-8CC6-CB55ABA6E06B}");

	/// <summary>
	///  Localizable string data value type Id.
	/// </summary>
	public static Guid LocalizableStringDataValueTypeUId { get; } = new("{95C6E6C4-2CC8-46BE-A1CB-96F942655F86}");

	/// <summary>
	///  LongText data value type Id.
	/// </summary>
	public static Guid LongTextDataValueTypeUId { get; } = new("{5CA35F10-A101-4C67-A96A-383DA6AFACFC}");

	/// <summary>
	///  Lookup data value type Id.
	/// </summary>
	public static Guid LookupDataValueTypeUId { get; } = new("{B295071F-7EA9-4e62-8D1A-919BF3732FF2}");

	/// <summary>
	///  MaxSizeText data value type Id.
	/// </summary>
	public static Guid MaxSizeTextDataValueTypeUId { get; } = new("{C0F04627-4620-4bc0-84E5-9419DC8516B1}");

	/// <summary>
	///  MediumText data value type Id.
	/// </summary>
	public static Guid MediumTextDataValueTypeUId { get; } = new("{DDB3A1EE-07E8-4D62-B7A9-D0E618B00FBD}");

	/// <summary>
	///  MetaDataText data value type Id.
	/// </summary>
	public static Guid MetaDataTextDataValueTypeUId { get; } = new("{394E160F-C8E0-46FA-9C0D-75D97E9E9169}");

	/// <summary>
	///  Money data value type Id.
	/// </summary>
	public static Guid MoneyDataValueTypeUId { get; } = new("{969093E2-2B4E-463B-883A-3D3B8C61F0CD}");

	/// <summary>Returns the object list data value type identifier.</summary>
	/// <value>The object list data value type identifier.</value>
	public static Guid ObjectListDataValueTypeUId { get; } = new("4B51A8B5-1EE9-4437-9D58-F35E083CBCDF");

	/// <summary>
	///  Phone data value type Id.
	/// </summary>
	public static Guid PhoneTextDataValueTypeUId { get; } = new("{26CBA63C-DAF1-4F36-B2EA-73C0D675D90C}");

	/// <summary>
	///  Rich text data value type Id.
	/// </summary>
	public static Guid RichTextDataValueTypeUId { get; } = new("{79BCCFFA-8C8B-4863-B376-A69D2244182B}");

	/// <summary>
	///  SecureText data value type Id.
	/// </summary>
	public static Guid SecureTextDataValueTypeUId { get; } = new("{3509B9DD-2C90-4540-B82E-8F6AE85D8248}");

	/// <summary>
	///  ShortText data value type Id.
	/// </summary>
	public static Guid ShortTextDataValueTypeUId { get; } = new("{325A73B8-0F47-44A0-8412-7606F78003AC}");

	/// <summary>
	///  Text data value type Id.
	/// </summary>
	public static Guid TextDataValueTypeUId { get; } = new("{8B3F29BB-EA14-4ce5-A5C5-293A929B6BA2}");

	/// <summary>
	///  Time data value type Id.
	/// </summary>
	public static Guid TimeDataValueTypeUId { get; } = new("{04CC757B-8F06-482c-8A1A-0C0E171D2410}");

	/// <summary>
	///  Web data value type Id.
	/// </summary>
	public static Guid WebTextDataValueTypeUId { get; } = new("{26CBA64C-DAF1-4F36-B2EA-73C0D695D90C}");

	#endregion

}