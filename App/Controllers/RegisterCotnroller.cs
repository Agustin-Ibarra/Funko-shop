using Funko_shop.Repository;
using Microsoft.AspNetCore.Mvc;
using Funko_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Funko_shop.Controllers;

public class RegisterController : Controller
{
  private readonly IUserRepository _userRepository;
  public RegisterController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
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
}