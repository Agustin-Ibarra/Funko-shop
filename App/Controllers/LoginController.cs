using Funko_shop.Repository;
using Microsoft.AspNetCore.Mvc;
using Funko_shop.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Funko_shop.Controllers;

public class LoginController : Controller
{
  private readonly IUserRepository _userRepository;
  public LoginController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpGet]
  [Route("/login")]
  public IActionResult Login()
  {
    return View();
  }

  [Route("/login")]
  [HttpPost]
  public async Task<IActionResult> ApiLogin([FromBody] LoginDto model)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
      return BadRequest(new { StatusCode = 400, error = errors });
    }
    else
    {
      var userData = await _userRepository.GetUser(model.Email);
      if (userData != null)
      {
        if (BCrypt.Net.BCrypt.Verify(model.Password, userData.Password) == true)
        {
          var claims = new[]
          {
            new Claim(ClaimTypes.Role,"customer"),
            new Claim(ClaimTypes.NameIdentifier,userData.IdUser.ToString())
          };
          var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
          var principal = new ClaimsPrincipal(identity);
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
          return Ok();
        }
        else
        {
          return Unauthorized(new { error = "El correo o la contraseña son incorrectos" });
        }
      }
      else
      {
        return Unauthorized(new { error = "El correo o la contraseña son incorrectos" });
      }
    }
  }
}