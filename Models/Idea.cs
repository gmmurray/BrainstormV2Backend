using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BrainstormV2Backend.Models
{
  public class Idea
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? UserId { get; set; }
    [Required]
    public string? Name { get; set; }
    [BsonIgnore]
    public Template? Template { get; set; }
    public string? TemplateId { get; set; }
    public IEnumerable<IdeaField> Fields { get; set; } = Enumerable.Empty<IdeaField>();
  }

  public class IdeaField : TemplateField
  {
    public string? Value { get; set; }
  }

  public class IdeaFilter
  {
    public int? Limit { get; set; }
    public string? TemplateId { get; set; }
  }
}