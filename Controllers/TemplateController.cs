using BrainstormV2Backend.Models;
using BrainstormV2Backend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormV2Backend.Controllers;

[Authorize]
[ApiController]
public class TemplateController : AuthenticatedController
{
  private readonly ILogger<TemplateController> _logger;
  private readonly ITemplateService _templateService;

  public TemplateController(ILogger<TemplateController> logger, ITemplateService templateService)
  {
    _logger = logger;
    _templateService = templateService;
  }

  [HttpGet("templates/{templateId}")]
  public async Task<IActionResult> GetTemplate(string templateId)
  {
    var result = await _templateService.GetTemplate(templateId, await GetTokenSub());

    if (result == null)
    {
      _logger.LogError("Error creating template");
      return NotFound();
    }

    return Ok(result);
  }

  [HttpGet("templates")]
  public async Task<IActionResult> GetTemplates([FromQuery] TemplateFilter filter)
  {
    var result = await _templateService.GetTemplates(filter, await GetTokenSub());

    return Ok(result);
  }

  [HttpPost("template")]
  public async Task<IActionResult> CreateTemplate(Template template)
  {
    var result = await _templateService.CreateTemplate(template, await GetTokenSub());

    if (result == null)
    {
      throw new Exception("Error creating template");
    }

    return Ok(result);
  }
}