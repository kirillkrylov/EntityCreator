using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EntityCreator.Columns;

public class ColumnFactory{
	public static BaseColumn? CreateColumn(DataValueType.ColumnType columnType, Guid schemaUid,
		string name, Guid packageId, bool? isRequired = null, bool? isIndexed = null) =>
		columnType switch {
			DataValueType.ColumnType.Text => CreateColumn<TextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Number => CreateColumn<NumberColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Web => CreateColumn<WebColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.MaxSizeText => CreateColumn<MaxSizeTextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.LongText => CreateColumn<LongTextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.MediumText => CreateColumn<MediumTextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.ShortText => CreateColumn<ShortTextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.EmailText => CreateColumn<EmailTextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Boolean => CreateColumn<BooleanColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Guid => CreateColumn<GuidColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Binary => CreateColumn<BinaryColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.DateTime => CreateColumn<DateTimeColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Date => CreateColumn<DateColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Time => CreateColumn<TimeColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Float => CreateColumn<FloatColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Float1 => CreateColumn<Float1Column>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Float2 => CreateColumn<Float2Column>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Float3 => CreateColumn<Float3Column>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Float4 => CreateColumn<Float4Column>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Float8 => CreateColumn<Float8Column>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Money => CreateColumn<MoneyColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.Phone => CreateColumn<PhoneColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.RichText => CreateColumn<RichTextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			DataValueType.ColumnType.SecureText => CreateColumn<SecureTextColumn>(schemaUid, name, packageId, isRequired, isIndexed),
			var _ => throw new ArgumentException($"{columnType} column type is not supported"),
		};

	public static T? CreateColumn<T>(Guid schemaUid, string name, Guid packageId, bool? isRequired,bool? isIndexed) where T : BaseColumn =>
		typeof(T) switch {
			not null when typeof(T) == typeof(TextColumn) => new TextColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(MaxSizeTextColumn) => new MaxSizeTextColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(LongTextColumn) => new LongTextColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(MediumTextColumn) => new MediumTextColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(ShortTextColumn) => new ShortTextColumn(schemaUid, name, packageId, isRequired, isIndexed)  as T,
			not null when typeof(T) == typeof(EmailTextColumn) => new EmailTextColumn(schemaUid, name, packageId, isRequired, isIndexed) {
				IsFormatValidated = true
			} as T,
			not null when typeof(T) == typeof(NumberColumn) => new NumberColumn(schemaUid, name, packageId,isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(BooleanColumn) => new BooleanColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(GuidColumn) => new GuidColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(WebColumn) => new WebColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(DateTimeColumn) => new DateTimeColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(DateColumn) => new DateColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(TimeColumn) => new TimeColumn(schemaUid, name, packageId, isRequired, isIndexed)  as T,
			not null when typeof(T) == typeof(FloatColumn) => new FloatColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(Float1Column) => new Float1Column(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(Float2Column) => new Float2Column(schemaUid, name, packageId, isRequired, isIndexed)as T,
			not null when typeof(T) == typeof(Float3Column) => new Float3Column(schemaUid, name, packageId, isRequired, isIndexed)  as T,
			not null when typeof(T) == typeof(Float4Column) => new Float4Column(schemaUid, name, packageId, isRequired, isIndexed)  as T,
			not null when typeof(T) == typeof(Float8Column) => new Float8Column(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(MoneyColumn) => new MoneyColumn(schemaUid, name, packageId, isRequired, isIndexed)  as T,
			not null when typeof(T) == typeof(BinaryColumn) => new BinaryColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(RichTextColumn) => new RichTextColumn(schemaUid, name, packageId, isRequired, isIndexed) {
				IsFormatValidated = true
			} as T,
			not null when typeof(T) == typeof(SecureTextColumn) => new SecureTextColumn(schemaUid, name, packageId, isRequired, isIndexed) as T,
			not null when typeof(T) == typeof(PhoneColumn) => new PhoneColumn(schemaUid, name, packageId, isRequired, isIndexed)  as T,
			var _ => throw new ArgumentException($"Invalid column type: {typeof(T).Name}")
		};
}

public class WebColumn : BaseColumn {
	public WebColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.WebTextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<WebColumn>(
		this, AppJsonSerializerContext.Default.WebColumn);
}
public class MaxSizeTextColumn : BaseColumn {
	public MaxSizeTextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.MaxSizeTextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<MaxSizeTextColumn>(
		this, AppJsonSerializerContext.Default.MaxSizeTextColumn);

}
public class MediumTextColumn : BaseColumn {
	public MediumTextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.MediumTextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<MediumTextColumn>(
		this, AppJsonSerializerContext.Default.MediumTextColumn);

}
public class LongTextColumn : BaseColumn {
	public LongTextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.LongTextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<LongTextColumn>(
		this, AppJsonSerializerContext.Default.LongTextColumn);

}
public class ShortTextColumn : BaseColumn {
	public ShortTextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.ShortTextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<ShortTextColumn>(
		this, AppJsonSerializerContext.Default.ShortTextColumn);

}
public class EmailTextColumn : BaseColumn {
	public EmailTextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.EmailTextDataValueTypeUId;
	}
	
	[JsonPropertyName("E25")]
	public required bool IsFormatValidated { get; init; }
	
	public override string ToJson() => JsonSerializer.Serialize<EmailTextColumn>(
		this, AppJsonSerializerContext.Default.EmailTextColumn);
}
public class TextColumn : BaseColumn {
	public TextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.TextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<TextColumn>(
			this, AppJsonSerializerContext.Default.TextColumn);
}
public class NumberColumn : BaseColumn {
	public NumberColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.IntegerDataValueTypeUId;
	}
	
	public override string ToJson() => JsonSerializer
		.Serialize<NumberColumn>(this, AppJsonSerializerContext.Default.NumberColumn);
}
public class BooleanColumn : BaseColumn {
	public BooleanColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.BooleanDataValueTypeUId;
	}
	
	public override string ToJson() => JsonSerializer
		.Serialize<BooleanColumn>(this, AppJsonSerializerContext.Default.BooleanColumn);
}
public class GuidColumn : BaseColumn {
	public GuidColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.GuidDataValueTypeUId;
	}
	
	public override string ToJson() => JsonSerializer
		.Serialize<GuidColumn>(this, AppJsonSerializerContext.Default.GuidColumn);
}
public class DateTimeColumn : BaseColumn {
	public DateTimeColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.DateTimeDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<DateTimeColumn>(
		this, AppJsonSerializerContext.Default.DateTimeColumn);
}
public class DateColumn : BaseColumn {
	public DateColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.DateDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<DateColumn>(
		this, AppJsonSerializerContext.Default.DateColumn);
}
public class TimeColumn : BaseColumn {
	public TimeColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.TimeDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<TimeColumn>(
		this, AppJsonSerializerContext.Default.TimeColumn);
}
public class FloatColumn : BaseColumn {
	public FloatColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.FloatDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<FloatColumn>(
		this, AppJsonSerializerContext.Default.FloatColumn);
}
public class Float1Column : BaseColumn {
	public Float1Column(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.Float1DataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<Float1Column>(
		this, AppJsonSerializerContext.Default.Float1Column);

}
public class Float2Column : BaseColumn {
	public Float2Column(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.Float2DataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<Float2Column>(
		this, AppJsonSerializerContext.Default.Float2Column);
}
public class Float3Column : BaseColumn {
	public Float3Column(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.Float3DataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<Float3Column>(
		this, AppJsonSerializerContext.Default.Float3Column);
}
public class Float4Column : BaseColumn {
	public Float4Column(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.Float4DataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<Float4Column>(
		this, AppJsonSerializerContext.Default.Float4Column);
}
public class Float8Column : BaseColumn {
	public Float8Column(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.Float8DataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<Float8Column>(
		this, AppJsonSerializerContext.Default.Float8Column);
}
public class MoneyColumn : BaseColumn {
	public MoneyColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.MoneyDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<MoneyColumn>(
		this, AppJsonSerializerContext.Default.MoneyColumn);
}
public class BinaryColumn : BaseColumn {
	public BinaryColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.BinaryDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<BinaryColumn>(
		this, AppJsonSerializerContext.Default.BinaryColumn);
}
public class PhoneColumn : BaseColumn {
	public PhoneColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.PhoneTextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<PhoneColumn>(
		this, AppJsonSerializerContext.Default.PhoneColumn);
}
public class SecureTextColumn : BaseColumn {
	public SecureTextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.SecureTextDataValueTypeUId;
	}
	public override string ToJson() => JsonSerializer.Serialize<SecureTextColumn>(
		this, AppJsonSerializerContext.Default.SecureTextColumn);
}
public class RichTextColumn : BaseColumn {
	public RichTextColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) 
		: base(schemaUid, name, packageId, isRequired, isIndexed) {
		DataValueTypeUId = DataValueType.RichTextDataValueTypeUId;
	}
	
	[JsonPropertyName("E25")]
	public required bool IsFormatValidated { get; init; }
	
	public override string ToJson() => JsonSerializer.Serialize<RichTextColumn>(
		this, AppJsonSerializerContext.Default.RichTextColumn);
}
public abstract class BaseColumn(Guid schemaUid, string name, Guid packageId, bool? isRequired, bool? isIndexed) {

	[JsonPropertyName("UId")]
	public Guid Uid { get; init; } = Guid.NewGuid();
	
	[JsonPropertyName("A2")]
	public string Name { get; init; } = name;
	
	[JsonPropertyName("A3")]
	public Guid CreatedInSchemaUId { get; init; } = schemaUid;
	
	[JsonPropertyName("A4")]
	public Guid ModifiedInSchemaUId { get; init; } = schemaUid;
	
	[JsonPropertyName("A5")]
	public Guid CreatedInPackageId { get; init; } = packageId;
	
	[JsonPropertyName("S2")]
	public Guid DataValueTypeUId { get; protected init;}
	
	[JsonPropertyName("E2")]
	public int RequirementType { get; protected init; } = isRequired.HasValue && isRequired.Value ? 1 : 0;
	
	[JsonPropertyName("E5")]
	public bool IsValueCloneable { get; init; }
	
	[JsonPropertyName("E6")]
	public bool IsIndexed { get; init; } = isIndexed.HasValue && isIndexed.Value;
	
	public abstract string ToJson();
}
