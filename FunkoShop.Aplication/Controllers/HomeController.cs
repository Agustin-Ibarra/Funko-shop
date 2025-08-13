using Microsoft.AspNetCore.Mvc;

namespace FunkoShop.Aplication.Controllers;

public class HomeController : Controller
{

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

}
