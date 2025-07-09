using Microsoft.AspNetCore.Mvc;
using Funko_shop.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;

namespace Funko_shop.Controllers;

public class AccountController : Controller
{

  private readonly IUserRepository _userRepository;
  public AccountController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
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