using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BrainstormV2Backend.Models;

public class Template
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string? UserId { get; set; }
  [Required]
  public string? Name { get; set; }
  public IEnumerable<TemplateField> Fields { get; set; } = Enumerable.Empty<TemplateField>();
}

public class TemplateField
{
  [Required]
  public string? Name { get; set; }
  public TemplateFieldType Type { get; set; }
}

public enum TemplateFieldType
{
  text,
  number,
  boolean,
}

public class TemplateFilter
{
  public int? Limit { get; set; }
  public IEnumerable<string> Ids { get; set; } = Enumerable.Empty<string>();
}