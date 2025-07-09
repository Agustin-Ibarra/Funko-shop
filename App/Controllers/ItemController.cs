using Funko_shop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Funko_shop.Controllers;

public class ItemController : Controller
{
  private readonly IItemRepository _itemRepository;
  public ItemController(IItemRepository repository)
  {
    _itemRepository = repository;
  }

  [HttpGet]
  [Route("/item")]
  public IActionResult Item()
  {
    return View();
  }

  [HttpGet]
  [Route("/item/details/{id}")]
  [OutputCache(Duration = 600, VaryByRouteValueNames = new[] { "id" })]
  public async Task<IActionResult> GetItemDetail(int id)
  {
    var item = await _itemRepository.GetItemDetail(id);
    if (item == null)
    {
      return StatusCode(404, "No se encontro el articulo");
    }
    else
    {
      return Ok(item);
    }
  }
}