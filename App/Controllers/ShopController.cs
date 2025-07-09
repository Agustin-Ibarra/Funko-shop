using Funko_shop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Funko_shop.Controllers;

public class ShopController : Controller
{
  private readonly IItemRepository _itemRepository;
  public ShopController(IItemRepository itemRepository)
  {
    _itemRepository = itemRepository;
  }

  [HttpGet]
  [Route("/shop")]
  public IActionResult Shop()
  {
    return View();
  }

  [HttpGet]
  [Route("/shop/items/{offset}")]
  [OutputCache(Duration = 300, VaryByRouteValueNames = new[] { "offset" })]
  public async Task<IActionResult> ApiItems(int offset)
  {
    var items = await _itemRepository.GetItems(offset);
    return Ok(items);
  }

  [HttpGet]
  [Route("/shop/items/{order}/{offset}")]
  [OutputCache(Duration = 300, VaryByRouteValueNames = new[] { "order", "offset" })]
  public async Task<IActionResult> ApiItemsOrderByprice(string order, int offset)
  {
    if (order == "asc")
    {
      var result = await _itemRepository.GetItemsByPriceOrderAsc(offset);
      return Ok(result);
    }
    else
    {
      var result = await _itemRepository.GetItemsByPriceOrderDesc(offset);
      return Ok(result);
    }
  }

  [HttpGet]
  [Route("/shop/items/filter/{filter}/{offset}")]
  [OutputCache(Duration = 300, VaryByRouteValueNames = new[] { "filter", "offset" })]
  public async Task<IActionResult> ApiItemsByFilter(string filter, int offset)
  {
    var items = await _itemRepository.GetItemsByFilter(filter, offset);
    if (items.Count < 1)
    {
      return NotFound(new { error = $"No se encontro un resultado para '{filter}'" });
    }
    else
    {
      return Ok(items);
    }
  }

  [HttpGet]
  [Route("/shop/items/filter/{filter}/{order}/{offset}")]
  [OutputCache(Duration = 300, VaryByRouteValueNames = new[] { "filter", "order", "offset" })]
  public async Task<IActionResult> ApiItemsByFilterAndOrder(string filter, string order, int offset)
  {
    if (order == "asc")
    {
      var items = await _itemRepository.GetItemsByFilterOrderAsc(filter, offset);
      if (items.Count < 1)
      {
        return NotFound(new { error = $"No se encontro un resultado para '{filter}'" });
      }
      else
      {
        return Ok(items);
      }
    }
    else
    {
      var items = await _itemRepository.GetItemsByFilterOrderDesc(filter, offset);
      if (items.Count < 1)
      {
        return NotFound(new { error = $"No se encontro un resultado para '{filter}'" });
      }
      else
      {
        return Ok(items);
      }
    }
  }
}