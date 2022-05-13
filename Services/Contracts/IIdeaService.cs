using BrainstormV2Backend.Models;

namespace BrainstormV2Backend.Services.Contracts
{
  public interface IIdeaService
  {
    Task<Idea?> GetIdea(string ideaId, string userId);
    Task<IEnumerable<Idea>> GetIdeas(IdeaFilter filter, string userId);
    Task<Idea> CreateIdea(Idea idea, string userId);
    Task UpdateIdea(Idea updates, string userId);
    Task DeleteIdea(string ideaId, string userId);
  }
}