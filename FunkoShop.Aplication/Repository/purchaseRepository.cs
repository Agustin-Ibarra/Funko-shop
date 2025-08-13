using FunkoShop.Aplication.Data;
using FunkoShop.Aplication.DTOs;
using FunkoShop.Aplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FunkoShop.Aplication.Repository;

public class PurchaseRepository
{
  private readonly AppDbContext _context;
  public PurchaseRepository(AppDbContext context)
  {
    _context = context;
  }


  public async Task<List<PurchaseOrderDto>> GetPurchaseByCustomer(int idUser)
  {
    var purchase = await _context.PurchaseOrders
    .OrderBy(purchase => purchase.id_purchase)
    .Where(purchase => purchase.customer == idUser)
    .Select(purchase => new PurchaseOrderDto
    {
      IdPurchase = purchase.id_purchase,
      DatePurchase = purchase.date_purchase,
      Total = purchase.total
    })
    .Skip(0)
    .Take(15)
    .ToListAsync();
    return purchase;
  }
  public async Task<List<PurchaseDetailDto>> GetPurchaseDetail(int IdPurchase)
  {
    var purchaseDetail = await _context.PurchaseDetails
    .Where(purchase => purchase.id_purchase_order == IdPurchase)
    .Select(purchase => new PurchaseDetailDto
    {
      ItemName = purchase.itemNameFk != null ? purchase.itemNameFk.name : "Articulo sin nombre",
      ItemPrice = purchase.ItemPriceFk != null ? purchase.ItemPriceFk.unit_price : 0.00,
      Quantity = purchase.quantity,
      Subtotal = purchase.ItemPriceFk != null ? purchase.ItemPriceFk.unit_price * purchase.quantity : 0.00,
    })
    .Skip(0)
    .Take(15)
    .ToListAsync();
    return purchaseDetail;
  }

  public async Task<PurchaseOrderDto> GetPurchase(int idPurchase)
  {
    var purchase = await _context.PurchaseOrders
    .Where(purchase => purchase.id_purchase == idPurchase)
    .Select(purchase => new PurchaseOrderDto
    {
      IdPurchase = purchase.id_purchase,
      DatePurchase = purchase.date_purchase,
      Total = purchase.total
    })
    .FirstOrDefaultAsync() ?? throw new Exception("No se encontro la orden de compra");
    return purchase;
  }

  public async Task<int> CreatePurchaseOrder(PurchaseOrder purchase)
  {
    _context.PurchaseOrders.Add(purchase);
    await _context.SaveChangesAsync();
    return purchase.id_purchase;
  }

  public async Task CreatePurchaseDetails(PurchaseDetail purchaseDetail)
  {
    _context.PurchaseDetails.Add(purchaseDetail);
    await _context.SaveChangesAsync();
  }
}