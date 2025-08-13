using System.Security.Claims;
using FunkoShop.Aplication.Controllers;
using FunkoShop.Aplication.DTOs;
using FunkoShop.Aplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests;

public class ItemControllerTest
{
  [Fact]
  public async Task GetItemDetail_ReturnsOk()
  {
    // Averrage
    var itemDetail = new ItemDetailDto
    {
      IdItem = 1,
      ItemName = "Item test",
      Price = 10.00,
    };
    var itemRepositoryMock = new Mock<IItemRepository>();
    itemRepositoryMock
    .Setup(item => item.GetItemDetail(1))
    .ReturnsAsync(itemDetail);
    var controller = new ItemController(itemRepositoryMock.Object);
    // Act
    var request = await controller.GetItemDetail(1);
    // Assert
    var response = Assert.IsType<OkObjectResult>(request);
    Assert.Equal(200, response.StatusCode);
  }

  [Fact]
  public async Task UpdateStock_ReturnsNoContent()
  {
    // Averrage
    var itemData = new ItemDataDto { IdItem = 1, Quantity = 1 };
    var items = new ItemsListDto { Items = [itemData] };
    var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
    var identity = new ClaimsIdentity(claims, "testauth");
    var user = new ClaimsPrincipal(identity);
    var itemRepositoryMock = new Mock<IItemRepository>();
    itemRepositoryMock
    .Setup(item => item.UpdateStock(1, 1))
    .Returns(Task.CompletedTask);
    var controller = new ItemController(itemRepositoryMock.Object);
    // Act
    var request = await controller.UpdateItemStock(items);
    // Assert
    var response = Assert.IsType<NoContentResult>(request);
    Assert.Equal(204, response.StatusCode);
  }
}