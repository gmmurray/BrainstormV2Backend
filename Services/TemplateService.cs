using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormV2Backend.Services.Contracts;
using BrainstormV2Backend.Models;

namespace BrainstormV2Backend.Services
{
  public class TemplateService : ITemplateService
  {
    public IEnumerable<Template> GetTemplates()
    {
      return Enumerable.Empty<Template>();
    }

    public Template GetTemplate()
    {
      return null;
    }

    public Template CreateTemplate()
    {
      return null;
    }

    public void UpdateTemplate()
    {
      return;
    }

    public void DeleteTemplate()
    {
      return;
    }
  }
}