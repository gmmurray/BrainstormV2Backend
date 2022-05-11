using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BrainstormV2Backend.Models;
using BrainstormV2Backend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace BrainstormV2Backend.Controllers;

[Authorize]
[ApiController]
public class TemplateController : ControllerBase
{
  private readonly ILogger<TemplateController> _logger;
  private readonly ITemplateService _templateService;

  public TemplateController(ILogger<TemplateController> logger, ITemplateService templateService)
  {
    _logger = logger;
    _templateService = templateService;
  }

  [HttpGet("template")]
  public Template GetTemplate()
  {
    return _templateService.GetTemplate();
  }

  [HttpGet("templates")]
  public IEnumerable<Template> GetTemplates(object request)
  {
    Console.WriteLine(request);
    return _templateService.GetTemplates();
  }
}