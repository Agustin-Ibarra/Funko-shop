using Microsoft.AspNetCore.Mvc;

namespace Funko_shop.Controllers;

public class HomeController : Controller
{

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

}
