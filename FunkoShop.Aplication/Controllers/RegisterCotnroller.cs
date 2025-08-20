using FunkoShop.Aplication.Repository;
using Microsoft.AspNetCore.Mvc;
using FunkoShop.Aplication.Models;
using Microsoft.EntityFrameworkCore;
using FunkoShop.Aplication.DTOs;

namespace FunkoShop.Aplication.Controllers;

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
  public async Task<IActionResult> Register([FromBody] RegisterDto dto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
      return BadRequest(new { StatusCode = 400, error = errors });
    }
    else
    {
      dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
      var NewUser = new User
      {
        name = dto.Name,
        last_name = dto.LastName,
        email = dto.Email,
        user_password = dto.Password,
        user_role = 1,
        create_time = DateTime.Now
      };
      try
      {
        await _userRepository.CreateUser(NewUser);
      }
      catch (DbUpdateException)
      {
        return Conflict(new { error = "Ya existe un usuario registrado con ese correo" });
      }
      return Created("/account", new { message = "usuario creado" });
    }
  }
}