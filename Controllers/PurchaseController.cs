using System.Security.Claims;
using Funko_shop.DTOs;
using Funko_shop.Models;
using Funko_shop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Funko_shop.Controllers;

public class PurchaseController : Controller
{
  private readonly PurchaseRepository _purchaseRepository;
  private readonly CartRepository _cartRepository;
  public PurchaseController(PurchaseRepository purchaseRepository, CartRepository cartRepository)
  {
    _purchaseRepository = purchaseRepository;
    _cartRepository = cartRepository;
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
  [Route("/purchase")]
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
  [Route("/purchase/{idPurchase}")]
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
  [Route("/purchase")]
  public async Task<IActionResult> CreatePurchase([FromBody] PayDataDto payData)
  {
    int idUser = Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier));
    var purchaseOrder = new PurchaseOrder(DateTime.Now, "FunkoShop", idUser, payData.Total);
    int IdPurchase = await _purchaseRepository.CreatePurchaseOrder(purchaseOrder);
    if (payData.Items != null)
    {
      foreach (var item in payData.Items)
      {
        var purchaseDetail = new PurchaseDetail
        {
          id_purchase_order = IdPurchase,
          item = item.IdItem,
          item_price = item.IdItem,
          quantity = item.Quantity,
          subtotal = item.Quantity * item.unitPrice,
          PurchaseOrderFk = purchaseOrder
        };
        await _purchaseRepository.CreatePurchaseDetails(purchaseDetail);
      }
    }
    await _cartRepository.ClearCart(idUser);
    return Created($"/purchase/{IdPurchase}", new { id = IdPurchase });
  }

}