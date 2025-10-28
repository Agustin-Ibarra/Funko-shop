using System.ComponentModel.DataAnnotations;

namespace FunkoShop.Aplication.DTOs;

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

public class ItemCartDto
{
  [Range(1, 100, ErrorMessage = "El id del articulo esta no existe o esta fuera del rango")]
  public int IdItem { get; set; }

  [Range(1, 5, ErrorMessage = "El rango de cantidad es de minimo 1 y maximo 5")]
  public int Quantity { get; set; }
}

public class ItemPriceDto
{
  public double ItemUnitPrice { get; set; }
}

public class ItemsListDto
{
  [MinLength(1, ErrorMessage = "La lista debe tener almenos un articulo")]
  [MaxLength(10, ErrorMessage = "La lista solo admite un maximo de 10 articulos")]
  public required List<ItemCartDto> Items { get; set; }
}