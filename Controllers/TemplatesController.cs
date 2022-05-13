using BrainstormV2Backend.Models;
using BrainstormV2Backend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormV2Backend.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class TemplatesController : AuthenticatedController
{
  private readonly ILogger<TemplatesController> _logger;
  private readonly ITemplateService _templateService;

  public TemplatesController(ILogger<TemplatesController> logger, ITemplateService templateService)
  {
    _logger = logger;
    _templateService = templateService;
  }

  [HttpGet("{templateId}")]
  public async Task<IActionResult> Get(string templateId)
  {
    var template = await _templateService.GetTemplate(templateId, await GetTokenSub());

    if (template is null)
    {
      _logger.LogError("Error creating template");
      return NotFound();
    }

    return Ok(template);
  }

  [HttpGet]
  public async Task<IActionResult> Get([FromQuery] TemplateFilter filter)
  {
    var templates = await _templateService.GetTemplates(filter, await GetTokenSub());

    return Ok(templates);
  }

  [HttpPost]
  public async Task<IActionResult> Create(Template template)
  {
    var created = await _templateService.CreateTemplate(template, await GetTokenSub());

    return CreatedAtAction(nameof(Get), new { templateId = created.Id }, created);
  }

  [HttpPut("{templateId}")]
  public async Task<IActionResult> Update(string templateId, Template updates)
  {
    var userId = await GetTokenSub();
    var template = await _templateService.GetTemplate(templateId, userId);

    if (template is null)
    {
      return NotFound();
    }

    updates.Id = template.Id;

    await _templateService.UpdateTemplate(updates, userId);

    return Ok();
  }

  [HttpDelete("{templateId}")]
  public async Task<IActionResult> Delete(string templateId)
  {
    var userId = await GetTokenSub();
    var template = await _templateService.GetTemplate(templateId, userId);

    if (template is null)
    {
      return NotFound();
    }

    await _templateService.DeleteTemplate(templateId, userId);

    return NoContent();
  }
}