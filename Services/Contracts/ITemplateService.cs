using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormV2Backend.Models;

namespace BrainstormV2Backend.Services.Contracts
{
  public interface ITemplateService
  {
    IEnumerable<Template> GetTemplates();
    Template GetTemplate();
    Template CreateTemplate();
    void UpdateTemplate();
    void DeleteTemplate();
  }
}