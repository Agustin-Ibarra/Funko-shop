using Funko_shop.Controllers;
using Funko_shop.DTOs;
using Funko_shop.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests;

public class ShopControllerTast
{
  [Fact]
  public async Task GetItemTest()
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
    .Setup(repository => repository.GetItems(1))
    .ReturnsAsync([item]);
    var controller = new ItemController(itemRepositoryMock.Object);
    // Act
    var request = await controller.ApiItems(1);
    // Assert
    var response = Assert.IsType<OkObjectResult>(request);
    Assert.Equal(200, response.StatusCode);
  }

  [Fact]
  public async Task GetItemDetailTest()
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
    .Setup(repository => repository.GetItemDetail(1))
    .ReturnsAsync(itemDetail);
    var controller = new ItemController(itemRepositoryMock.Object);
    // Act
    var request = await controller.GetItemDetail(1);
    // Assert
    var response = Assert.IsType<OkObjectResult>(request);
    Assert.Equal(200, response.StatusCode);
  }
}