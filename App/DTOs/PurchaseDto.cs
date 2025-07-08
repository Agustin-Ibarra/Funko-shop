namespace Funko_shop.DTOs;

public class PurchaseDetailDto
{
  public string? ItemName { get; set; }
  public double ItemPrice { get; set; }
  public int Quantity { get; set; }
  public double Subtotal { get; set; }
}

public class PurchaseOrderDto
{
  public int IdPurchase { get; set; }
  public DateTime DatePurchase { get; set; }
  public double Total { get; set; }
  public List<PurchaseDetailDto>? PurchaseDetail { get; set; }
  public string? IdPurchaseFormat { get; set; }
}