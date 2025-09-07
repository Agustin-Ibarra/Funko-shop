using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FunkoShop.Aplication.Controllers;

[EnableRateLimiting("fixedWindows")]
public class HomeController : Controller
{

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

}
