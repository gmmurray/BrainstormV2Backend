using BrainstormV2Backend.Models;

namespace BrainstormV2Backend.Services.Contracts
{
  public interface ITemplateService
  {
    Task<IEnumerable<Template>> GetTemplates(TemplateFilter filter, string userId);
    Task<Template> GetTemplate(string templateId, string userId);
    Task<Template> CreateTemplate(Template template, string userId);
    void UpdateTemplate();
    void DeleteTemplate();
  }
}