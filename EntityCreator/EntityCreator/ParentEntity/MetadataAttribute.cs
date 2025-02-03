using System;

namespace EntityCreator.ParentEntity;

[AttributeUsage(AttributeTargets.Property)]
public class MetadataAttribute(string name, MetadataAction metadataAction, bool isFirst = false) : Attribute {
	public string Name { get; set; } = name;
	
	public MetadataAction MetadataAction { get; set; } = metadataAction;
	public bool IsFirst { get; set; } = isFirst;

}


public enum MetadataAction {

	Set, Add, Remove, Merge

}

