using BrainstormV2Backend.Models;
using BrainstormV2Backend.Services.Contracts;
using MongoDB.Driver;

namespace BrainstormV2Backend.Services
{
  public class TemplateService : ITemplateService
  {
    private readonly IMongoCollection<Template> _templateCollection;

    public TemplateService(IMongoClient mongoClient, IConfiguration configuration)
    {
      _templateCollection = mongoClient
        .GetDatabase(configuration["MongoDB:DatabaseName"])
        .GetCollection<Template>("Templates");
    }

    public async Task<IEnumerable<Template>> GetTemplates(TemplateFilter filter, string userId)
    {
      var query = _templateCollection.Find(x => x.UserId == userId);

      if (filter.Limit is not null)
      {
        query = query.Limit(filter.Limit);
      }

      return await query.ToListAsync();
    }

    public async Task<Template> GetTemplate(string templateId, string userId)
    {
      return await _templateCollection.Find(x => x.Id == templateId && x.UserId == userId).SingleOrDefaultAsync();
    }

    public async Task<Template> CreateTemplate(Template template, string userId)
    {
      var creation = new Template
      {
        UserId = userId,
        Name = template.Name,
        Fields = template.Fields
      };

      await _templateCollection.InsertOneAsync(creation);

      return creation;
    }

    public async Task UpdateTemplate(Template updates, string userId)
    {
      await _templateCollection.ReplaceOneAsync(x => x.Id == updates.Id && x.UserId == userId, updates);
    }

    public async Task DeleteTemplate(string templateId, string userId)
    {
      await _templateCollection.DeleteOneAsync(x => x.Id == templateId && x.UserId == userId);
    }
  }
}