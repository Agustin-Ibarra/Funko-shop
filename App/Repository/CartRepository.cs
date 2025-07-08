using Funko_shop.Data;
using Funko_shop.DTOs;
using Funko_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Funko_shop.Repository;

public interface ICartRepository
{
  Task<List<CartItemDto>> GetItemsToCart(int idUser);
  Task CreateItemCart(Cart cart);
  Task DeleteItem(int idCart);
  Task ClearCart(int idUser);
}
public class CartRepository : ICartRepository
{
  private readonly AppDbContext _context;
  public CartRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<CartItemDto>> GetItemsToCart(int idUser)
  {
    var items = await _context.Carts
    .OrderBy(item => item.id_cart)
    .Include(item => item.CustomerFk)
    .Include(item => item.ItemFk)
    .Where(item => item.customer == idUser)
    .Select(item => new CartItemDto
    {
      IdCart = item.id_cart,
      Item = item.ItemFk != null ? item.ItemFk.name : "sin nombre",
      IdItem = item.item,
      Price = item.ItemFk != null ? item.ItemFk.unit_price : 0.0,
      Image = item.ItemFk != null ? item.ItemFk.image_1 : "sin foto",
      Category = item.ItemFk != null ? item.ItemFk.categoryFk.name_category : "sin categoria",
      Quantity = item.quantity,
      TotalCart = item.ItemFk != null ? item.ItemFk.unit_price * item.quantity : 0.0,
    })
    .Skip(0)
    .Take(15)
    .ToListAsync();
    return items;
  }

  public async Task CreateItemCart(Cart cartItem)
  {
    _context.Carts.Add(cartItem);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteItem(int idCart)
  {
    var itemCart = await _context.Carts
    .Where(cart => cart.id_cart == idCart)
    .FirstOrDefaultAsync() ?? throw new Exception($"No se encotnro el articulo con el id: {idCart}");
    _context.Carts.Remove(itemCart);
    await _context.SaveChangesAsync();
  }

  public async Task ClearCart(int idUser)
  {
    await _context.Database.ExecuteSqlRawAsync("DELETE FROM carts WHERE customer = {0}", idUser);
  }

}