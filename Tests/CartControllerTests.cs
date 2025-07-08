using System.Security.Claims;
using Funko_shop.Controllers;
using Funko_shop.DTOs;
using Funko_shop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests;

public class CartControllerTest
{
  [Fact]
  public async Task GetItemCartItems_ReturnsOk()
  {
    // Averrage
    var itemCart = new CartItemDto
    {
      IdCart = 1,
      IdItem = 2,
    };
    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier,"1")
    };
    var itemRepositoryMock = new Mock<IItemRepository>();
    var userRepositoryMock = new Mock<IUserRepository>();
    var cartRepositoryMock = new Mock<ICartRepository>();
    cartRepositoryMock
    .Setup(repository => repository.GetItemsToCart(1))
    .ReturnsAsync([itemCart]);
    var identity = new ClaimsIdentity(claims, "testauth");
    var user = new ClaimsPrincipal(identity);
    var controller = new CartController(cartRepositoryMock.Object, itemRepositoryMock.Object, userRepositoryMock.Object);
    controller.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext
      {
        User = user
      }
    };
    // Act
    var request = await controller.GetItemCartItems();
    // Assert
    var response = Assert.IsType<OkObjectResult>(request);
    Assert.Equal(200, response.StatusCode);
  }

  [Fact]
    public async Task GetItemCartItems_ReturnsBadRequest()
  {
    // Averrage
    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier,"1")
    };
    var itemRepositoryMock = new Mock<IItemRepository>();
    var userRepositoryMock = new Mock<IUserRepository>();
    var cartRepositoryMock = new Mock<ICartRepository>();
    cartRepositoryMock
    .Setup(repository => repository.GetItemsToCart(1))
    .ReturnsAsync([]);
    var identity = new ClaimsIdentity(claims, "testauth");
    var user = new ClaimsPrincipal(identity);
    var controller = new CartController(cartRepositoryMock.Object, itemRepositoryMock.Object, userRepositoryMock.Object);
    controller.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext
      {
        User = user
      }
    };
    // Act
    var request = await controller.GetItemCartItems();
    // Assert
    var response = Assert.IsType<NotFoundObjectResult>(request);
    Assert.Equal(404, response.StatusCode);
  }
}