using FunkoShop.Aplication.Controllers;
using FunkoShop.Aplication.DTOs;
using FunkoShop.Aplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests;

public class ShopControllerTast
{
  [Fact]
  public async Task GetItems_ReturnsOk()
  {
    // Averrage
    var item = new ItemDto
    {
      IdItem = 1,
      Name = "Item test",
      Price = 10.00,
    };
    var itemRepositoryMock = new Mock<IItemRepository>();
    itemRepositoryMock
    .Setup(item => item.GetItems(1))
    .ReturnsAsync([item]);
    var controller = new ShopController(itemRepositoryMock.Object);
    // Act
    var request = await controller.ApiItems(1);
    // Assert
    var response = Assert.IsType<OkObjectResult>(request);
    Assert.Equal(200, response.StatusCode);
  }
}