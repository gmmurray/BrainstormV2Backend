using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormV2Backend.Controllers
{
  public class AuthenticatedController : ControllerBase
  {
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public AuthenticatedController()
    {
      _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    [NonAction]
    public async Task<string> GetTokenSub()
    {
      var token = await HttpContext.GetTokenAsync("access_token");
      return _jwtSecurityTokenHandler.ReadJwtToken(token).Subject;
    }
  }
}