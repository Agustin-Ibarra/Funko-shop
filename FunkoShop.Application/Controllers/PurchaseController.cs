using System.Security.Claims;
using FunkoShop.Aplication.DTOs;
using FunkoShop.Aplication.Models;
using FunkoShop.Aplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FunkoShop.Aplication.Controllers;

[EnableRateLimiting("fixedWindows")]
public class PurchaseController : Controller
{
  private readonly PurchaseRepository _purchaseRepository;
  private readonly ICartRepository _cartRepository;
  private readonly IItemRepository _itemRepository;
  public PurchaseController(PurchaseRepository purchaseRepository, ICartRepository cartRepository, IItemRepository itemRepository)
  {
    _purchaseRepository = purchaseRepository;
    _cartRepository = cartRepository;
    _itemRepository = itemRepository;
  }

  [HttpGet]
  [Authorize]
  [Route("/purchases")]
  public IActionResult Purchase()
  {
    return View();
  }

  [HttpGet]
  [Authorize]
  [Route("/api/purchase")]
  public async Task<IActionResult> GetPurchaseByUser()
  {
    int idUser = Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier));
    var purchases = await _purchaseRepository.GetPurchaseByCustomer(idUser);
    if (purchases.Count < 1)
    {
      return NotFound(new { error = "No se encontraron ordenes de compras" });
    }
    else
    {
      foreach (var purchase in purchases)
      {
        var purchaseDetail = await _purchaseRepository.GetPurchaseDetail(purchase.IdPurchase);
        purchase.IdPurchaseFormat = $"{2.ToString("D4")}{purchase.IdPurchase.ToString("D8")}";
        purchase.PurchaseDetail = purchaseDetail;
      }
      return Ok(purchases);
    }
  }

  [HttpGet]
  [Authorize]
  [Route("/api/purchase/{idPurchase}")]
  public async Task<IActionResult> GetPurchase(int idPurchase)
  {
    var purchaseOrder = await _purchaseRepository.GetPurchase(idPurchase);
    var purchaseDeatils = await _purchaseRepository.GetPurchaseDetail(idPurchase);
    purchaseOrder.PurchaseDetail = purchaseDeatils;
    purchaseOrder.IdPurchaseFormat = $"{2.ToString("D4")}{purchaseOrder.IdPurchase.ToString("D8")}";
    return Ok(purchaseOrder);
  }

  [HttpPost]
  [Authorize]
  [Route("/api/purchase")]
  public async Task<IActionResult> CreatePurchase([FromBody] ItemsListDto itemsListDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
      return BadRequest(new { StatusCode = 400, error = errors });
    }
    else
    {
      double total = 0;
      double subtotal = 0;
      foreach (var item in itemsListDto.Items)
      {
        var itemPrice = await _itemRepository.GetItemPrice(item.IdItem); // se obtiene el precio del articulo por cada iteracion
        if (itemPrice != null)
        {
          total += itemPrice.ItemUnitPrice * item.Quantity; // los recargos o descuentos se aplicarian aqui
          subtotal += itemPrice.ItemUnitPrice * item.Quantity;
        }
        else
        {
          return BadRequest(new { error = $"No se encontro el articulo con id: {item.IdItem}" });
        }
      }
      var purchaseOrder = new PurchaseOrder
      {
        customer = Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier)),
        date_purchase = DateTime.Now,
        seller = "Funko shop",
        total = total
      };
      await _purchaseRepository.CreatePurchaseOrder(purchaseOrder);
      foreach (var item in itemsListDto.Items)
      {
        // se crea un registro del detalle de la orden de compra por cada articulo 
        // con el id de la orden de compra creada antes del bucle for
        var itemUnitPrice = await _itemRepository.GetItemPrice(item.IdItem);
        var purchaseDatail = new PurchaseDetail
        {
          id_purchase_order = purchaseOrder.id_purchase,
          item = item.IdItem,
          item_price = itemUnitPrice != null ? itemUnitPrice.ItemUnitPrice : 0,
          quantity = item.Quantity,
          subtotal = subtotal
        };
        await _purchaseRepository.CreatePurchaseDetails(purchaseDatail);
      }
      return Created("/api/purchase",new {idPurchase = purchaseOrder.id_purchase});
    }
  }
}