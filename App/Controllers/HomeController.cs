using Microsoft.AspNetCore.Mvc;

namespace Funko_shop.Controllers;

public class HomeController : Controller
{

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

  [HttpGet]
  [Route("/shop")]
  public IActionResult Shop()
  {
    return View();
  }

  [HttpGet]
  [Route("/item")]
  public IActionResult Item()
  {
    return View();
  }

}
