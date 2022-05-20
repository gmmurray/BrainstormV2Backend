using BrainstormV2Backend.Models;
using BrainstormV2Backend.Services.Contracts;
using MongoDB.Driver;

namespace BrainstormV2Backend.Services
{
  public class IdeaService : IIdeaService
  {
    private readonly IMongoCollection<Idea> _ideaCollection;
    private readonly ITemplateService _templateService;

    public IdeaService(IMongoClient mongoClient, IConfiguration configuration, ITemplateService templateService)
    {
      _ideaCollection = mongoClient
          .GetDatabase(configuration["MongoDB:DatabaseName"])
          .GetCollection<Idea>("Ideas");
      _templateService = templateService;
    }

    public async Task<Idea?> GetIdea(string ideaId, string userId)
    {
      var idea = await _ideaCollection.Find(x => x.Id == ideaId && x.UserId == userId).SingleOrDefaultAsync();

      if (idea?.TemplateId is not null)
      {
        idea.Template = await _templateService.GetTemplate(idea.TemplateId, userId);
      }

      return idea;
    }

    public async Task<IEnumerable<Idea>> GetIdeas(IdeaFilter filter, string userId)
    {
      var filterBuilder = Builders<Idea>.Filter;

      var filterDefinition = filterBuilder.Eq("UserId", userId);

      if (filter?.TemplateId is not null)
      {
        filterDefinition &= filterBuilder.Eq("TemplateId", filter.TemplateId);
      }

      var query = _ideaCollection.Find(filterDefinition);

      if (filter?.Limit is not null)
      {
        query = query.Limit(filter.Limit);
      }

      var ideas = await query.ToListAsync();

      var templates = await _templateService.GetTemplates(new TemplateFilter
      {
        Ids = ideas.Where(x => x.TemplateId is not null).Select(x => x.TemplateId!),
      }, userId);

      var templateDict = templates.ToDictionary(x => x.Id!, x => x);

      foreach (var idea in ideas)
      {
        if (idea.TemplateId is not null)
        {
          if (templateDict.ContainsKey(idea.TemplateId))
          {
            idea.Template = templateDict[idea.TemplateId];
          }
          else
          {
            idea.Template = null;
            idea.TemplateId = null;
          }
        }
      }

      return ideas;
    }

    public async Task<Idea> CreateIdea(Idea idea, string userId)
    {
      var creation = new Idea
      {
        UserId = userId,
        Name = idea.Name,
        TemplateId = idea.TemplateId,
        Fields = idea.Fields
      };

      await _ideaCollection.InsertOneAsync(creation);

      if (creation.TemplateId is not null)
      {
        creation.Template = await _templateService.GetTemplate(creation.TemplateId, userId);
      }

      return creation;
    }

    public async Task UpdateIdea(Idea updates, string userId)
    {
      await _ideaCollection.ReplaceOneAsync(x => x.Id == updates.Id && x.UserId == userId, updates);
    }

    public async Task DeleteIdea(string ideaId, string userId)
    {
      await _ideaCollection.DeleteOneAsync(x => x.Id == ideaId && x.UserId == userId);
    }
  }
}