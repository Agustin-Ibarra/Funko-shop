using FunkoShop.Aplication.DTOs;
using FunkoShop.Aplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;

namespace FunkoShop.Aplication.Controllers;

[EnableRateLimiting("fixedWindows")]
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
  [Route("/api/item/details/{id}")]
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

  [Authorize]
  [HttpPatch]
  [Route("/api/item")]
  public async Task<IActionResult> UpdateItemStock([FromBody] ItemsListDto itemsList)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
      return BadRequest(new { StatusCode = 400, error = errors });
    }
    else
    {
      List<ItemCartDto> ItemsRejected = [];
      List<ItemCartDto> ItemsUpdatetd = [];
      try
      {
        foreach (var item in itemsList.Items)
        {
          var flag = await _itemRepository.UpdateStock(item.IdItem, item.Quantity);
          if (flag == false)
          {
            ItemsRejected.Add(item);
          }
          else
          {
            ItemsUpdatetd.Add(item);
          }
        }
        if (ItemsRejected.Count > 0)
        {
          foreach (var item in ItemsUpdatetd)
          {
            await _itemRepository.RestoreStock(item.IdItem, item.Quantity);
          }
          return BadRequest(new { error = "No se pudo reservar los articulos", rejectedList = ItemsRejected });
        }
        else
        {
          return Created();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return StatusCode(503, new { error = "Ocurrio un error en la base de datos" });
      }
    }
  }
}