namespace Funko_shop.DTOs;

public class ItemDto
{
  public int IdItem { get; set; }
  public string? Name { get; set; }
  public string? Image { get; set; }
  public double Price { get; set; }
  public string? Category { get; set; }
}

public class ItemDetailDto
{
  public int IdItem { get; set; }
  public string? ItemName { get; set; }
  public string? Image { get; set; }
  public double Price { get; set; }
  public string? Category { get; set; }
  public string? Description { get; set; }
  public int Stock { get; set; }
}

public class itemCartDto
{
  public int IdItem { get; set; }
  public int quantity { get; set; }
}

public class ItemIdDto
{
  public int IdItem { get; set; }
}

public class ItemsListDto
{
  public required List<ItemDataDto> Items {get; set;}
}