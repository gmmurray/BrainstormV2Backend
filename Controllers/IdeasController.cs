using BrainstormV2Backend.Models;
using BrainstormV2Backend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormV2Backend.Controllers
{
  [Authorize]
  [ApiController]
  [Route("/api/[controller]")]
  public class IdeasController : AuthenticatedController
  {
    private readonly IIdeaService _ideaService;

    public IdeasController(IIdeaService ideaService)
    {
      _ideaService = ideaService;
    }

    [HttpGet("{ideaId}")]
    public async Task<IActionResult> Get(string ideaId)
    {
      var idea = await _ideaService.GetIdea(ideaId, await GetTokenSub());

      if (idea is null)
      {
        return NotFound();
      }

      return Ok(idea);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] IdeaFilter filter)
    {
      var ideas = await _ideaService.GetIdeas(filter, await GetTokenSub());

      return Ok(ideas);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Idea idea)
    {
      var created = await _ideaService.CreateIdea(idea, await GetTokenSub());

      return CreatedAtAction(nameof(Get), new { ideaId = created.Id }, created);
    }

    [HttpPut("{ideaId}")]
    public async Task<IActionResult> Update(string ideaId, Idea updates)
    {
      var userId = await GetTokenSub();
      var idea = await _ideaService.GetIdea(ideaId, userId);

      if (idea is null)
      {
        return NotFound();
      }

      updates.Id = idea.Id;

      await _ideaService.UpdateIdea(updates, userId);

      return Ok();
    }

    [HttpDelete("{ideaId}")]
    public async Task<IActionResult> Delete(string ideaId)
    {
      var userId = await GetTokenSub();
      var idea = await _ideaService.GetIdea(ideaId, userId);

      if (idea is null)
      {
        return NotFound();
      }

      await _ideaService.DeleteIdea(ideaId, userId);

      return NoContent();
    }
  }
}