namespace BrainstormV2Backend.Models;

public class Template
{
  public string UserId { get; set; }
  public string Name { get; set; }
  public IEnumerator<TemplateField> Fieldds { get; set; }
}

public class TemplateField
{
  public string Name { get; set; }
  public TemplateFieldType Type { get; set; }
}

public enum TemplateFieldType
{
  text,
  number,
  boolean,
}