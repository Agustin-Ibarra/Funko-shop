using System.Security.Claims;
using Funko_shop.DTOs;
using Funko_shop.Models;
using Funko_shop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Funko_shop.Controllers;

public class CartController : Controller
{
  private readonly CartRepository _cartRepository;
  private readonly ItemRepository _itemRepository;
  private readonly UserRepository _userRepository;
  public CartController(CartRepository repository, ItemRepository itemRepository, UserRepository userRepository)
  {
    _cartRepository = repository;
    _itemRepository = itemRepository;
    _userRepository = userRepository;
  }

  [HttpGet]
  [Authorize]
  [Route("/cart")]
  public IActionResult Cart()
  {
    return View();
  }


  [Authorize]
  [HttpGet]
  [Route("/cart/items")]
  public async Task<IActionResult> GetItemCartItems()
  {
    int idUser = Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier));
    double total = 0.0;
    int quantityTotal = 0;
    var itemsCart = await _cartRepository.GetItemsToCart(idUser);
    if (itemsCart.Count < 1)
    {
      return NotFound(new { error = "No se encontro resultado para este cliente" });
    }
    else
    {
      foreach (var item in itemsCart)
      {
        total += item.Price * item.Quantity;
        quantityTotal += item.Quantity;
      }
      return Ok(new { itemsCart, totalToPay = total, items = quantityTotal });
    }
  }

  [Authorize]
  [HttpPost]
  [Route("/cart/items")]
  public async Task<IActionResult> CreateItemCart([FromBody] Cart cartItem)
  {
    cartItem.customer = Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier));
    var itemModel = await _itemRepository.GetItemModel(cartItem.item);
    var userModel = await _userRepository.GetUserModel(cartItem.customer);
    var itemId = await _itemRepository.GetItemId(cartItem.item);
    cartItem.ItemFk = itemModel;
    cartItem.CustomerFk = userModel;
    cartItem.item = itemId.IdItem;
    await _cartRepository.CreateItemCart(cartItem);
    return Created("/cart/items",new {message = "Articulo agregado al carrito"});
  }

  [HttpDelete]
  [Authorize]
  [Route("/cart/items/{idCart}")]
  public async Task<IActionResult> DeleteItemCart(int idCart)
  {
    try
    {
      await _cartRepository.DeleteItem(idCart);
    }
    catch (Exception ex)
    {
      return BadRequest(new { error = ex.Message });
    }
    return NoContent();
  }

}