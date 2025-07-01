namespace Funko_shop.DTOs;

public class CartItemDto
{
  public int IdCart { get; set; }
  public int IdItem { get; set; }
  public string? Item { get; set; }
  public string? Image { get; set; }
  public string? Category { get; set; }
  public double Price { get; set; }
  public int Quantity { get; set; }
  public double TotalCart { get; set; }
}

public class ItemDataDto
{
  public int IdItem { get; set; }
  public int Quantity { get; set; }
  public double unitPrice { get; set; }
}
public class PayDataDto
{
  public double Total { get; set; }
  public List<ItemDataDto>? Items { get; set; }
}