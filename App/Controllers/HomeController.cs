using Microsoft.AspNetCore.Mvc;
using Funko_shop.Repository;

namespace Funko_shop.Controllers;

public class HomeController : Controller
{
  private readonly ItemRepository _itemRepository;
  private readonly ILogger<HomeController> _logger;

  public HomeController(ILogger<HomeController> logger, ItemRepository repository)
  {
    _logger = logger;
    _itemRepository = repository;

  }

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
