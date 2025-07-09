using Microsoft.AspNetCore.Mvc;
using Funko_shop.Repository;
using Funko_shop.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Funko_shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;

namespace Funko_shop.Controllers;

public class UserController : Controller
{

  private readonly IUserRepository _userRepository;
  public UserController(IUserRepository repository)
  {
    _userRepository = repository;
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

  [HttpGet]
  [Route("/register")]
  public IActionResult Register()
  {
    return View();
  }

  [HttpPost]
  [Route("/register")]
  public async Task<IActionResult> Register([FromBody] User model)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
      return BadRequest(new { StatusCode = 400, error = errors });
    }
    else
    {
      var role = await _userRepository.GetRole();
      model.user_password = BCrypt.Net.BCrypt.HashPassword(model.user_password);
      model.create_time = DateTime.Now;
      model.user_role = role.id_role;
      try
      {
        await _userRepository.CreateUser(model);
      }
      catch (DbUpdateException)
      {
        return Conflict(new { error = "Ya existe un usuario registrado con ese correo" });
      }
      return Created("/account", new { message = "usuario creado" });
    }
  }

  [HttpGet]
  [Authorize]
  [Route("/account")]
  public IActionResult Account()
  {
    return View();
  }

  [Authorize]
  [HttpGet]
  [Route("/account/profile")]
  [OutputCache(Duration = 600)]
  public async Task<IActionResult> Profile()
  {
    int idUser = Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier));
    try
    {
      var profile = await _userRepository.GetProfile(idUser);
      return Ok(profile);
    }
    catch (Exception ex)
    {
      return BadRequest(new { error = ex.Message });
    }
  }

  [HttpPost]
  [Route("/account/logout")]
  public async Task<IActionResult> LogOut()
  {
    await HttpContext.SignOutAsync();
    Response.Cookies.Delete(".AspNetCore.Cookies");
    return Redirect("Login");
  }
}