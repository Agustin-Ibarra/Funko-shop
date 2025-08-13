using FunkoShop.Aplication.DTOs;
using FunkoShop.Aplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace FunkoShop.Aplication.Controllers;

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

  [HttpPatch]
  [Authorize]
  [Route("/item")]

  public async Task<IActionResult> UpdateItemStock([FromBody] ItemsListDto itemsList)
  {
    foreach (var item in itemsList.Items)
    {
      try
      {
        await _itemRepository.UpdateStock(item.IdItem, item.Quantity);
      }
      catch (Exception)
      {
        return BadRequest(new { error = $"No se puedo actualizar el articulo {item.IdItem}" });
      }
    }
    return NoContent();
  }
}