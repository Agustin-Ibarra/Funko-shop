using System.Security.Claims;
using Funko_shop.Controllers;
using Funko_shop.DTOs;
using Funko_shop.Models;
using Funko_shop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests;

public class CartControllerTest
{
  [Fact]
  public async Task GetCartItems_ReturnsOk()
  {
    // Averrage
    var itemCart = new CartItemDto
    {
      IdCart = 1,
      IdItem = 2,
    };
    var claims = new[] { new Claim(ClaimTypes.NameIdentifier,"1") };
    var identity = new ClaimsIdentity(claims, "testauth");
    var user = new ClaimsPrincipal(identity);
    var itemRepositoryMock = new Mock<IItemRepository>();
    var userRepositoryMock = new Mock<IUserRepository>();
    var cartRepositoryMock = new Mock<ICartRepository>();

    cartRepositoryMock
    .Setup(item => item.GetItemsToCart(1))
    .ReturnsAsync([itemCart]);

    var controller = new CartController(cartRepositoryMock.Object, itemRepositoryMock.Object, userRepositoryMock.Object);
    controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
    // Act
    var request = await controller.GetCartItems();
    // Assert
    var response = Assert.IsType<OkObjectResult>(request);
    Assert.Equal(200, response.StatusCode);
  }

  [Fact]
  public async Task GetCartItems_ReturnsBadRequest()
  {
    // Averrage
    var claims = new[] { new Claim(ClaimTypes.NameIdentifier,"1") };
    var identity = new ClaimsIdentity(claims, "testauth");
    var user = new ClaimsPrincipal(identity);
    var itemRepositoryMock = new Mock<IItemRepository>();
    var userRepositoryMock = new Mock<IUserRepository>();
    var cartRepositoryMock = new Mock<ICartRepository>();

    cartRepositoryMock
    .Setup(item => item.GetItemsToCart(1)).ReturnsAsync([]);

    var controller = new CartController(cartRepositoryMock.Object, itemRepositoryMock.Object, userRepositoryMock.Object);
    controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
    // Act
    var request = await controller.GetCartItems();
    // Assert
    var response = Assert.IsType<NotFoundObjectResult>(request);
    Assert.Equal(404, response.StatusCode);
  }

  [Fact]
  public async Task CreateItemCart_ReturnOk()
  {
    // Averrage
    var claims = new[] { new Claim(ClaimTypes.NameIdentifier,"1") };
    var identity = new ClaimsIdentity(claims, "testauth");
    var user = new ClaimsPrincipal(identity);
    var itemRepositoryMock = new Mock<IItemRepository>();
    var userRepositoryMock = new Mock<IUserRepository>();
    var cartRepositoryMock = new Mock<ICartRepository>();

    itemRepositoryMock
    .Setup(item => item.GetItemModel(1))
    .ReturnsAsync(new Item
    {
      name = "item",
      categoryFk = new Category { id_category = 1 }
    });

    userRepositoryMock
    .Setup(user => user.GetUserModel(2))
    .ReturnsAsync(new User { RoleFk = new Role { role = "customer" } });

    cartRepositoryMock
    .Setup(cart => cart.CreateItemCart(new Cart { id_cart = 10 }));

    var controller = new CartController(cartRepositoryMock.Object, itemRepositoryMock.Object, userRepositoryMock.Object);
    controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
    // Act
    var request = await controller.CreateItemCart( new Cart {id_cart = 10});
    // Assert
    var response = Assert.IsType<CreatedResult>(request);
    Assert.Equal(201, response.StatusCode);
  }
}