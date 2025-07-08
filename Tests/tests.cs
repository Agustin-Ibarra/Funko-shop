using Funko_shop.Controllers;
using Funko_shop.DTOs;
using Funko_shop.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace tests;

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
      Image = "image.jpg",
      Price = 10.00,
      Category = "category"
    };
    var itemRepositoryMock = new Mock<IItemRepository>();
    itemRepositoryMock
    .Setup(repository => repository.GetItems(1))
    .ReturnsAsync([item]);
    var controller = new ShopController(itemRepositoryMock.Object);
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
      Image = "image.jpg",
      Price = 10.00,
      Category = "Item category",
      Description = "Item descirption",
      Stock = 1
    };
    var itemRepositoryMock = new Mock<IItemRepository>();
    itemRepositoryMock
    .Setup(repository => repository.GetItemDetail(1))
    .ReturnsAsync(itemDetail);
    var controller = new ShopController(itemRepositoryMock.Object);
    // Act
    var request = await controller.GetItemDetail(1);
    // Assert
    var response = Assert.IsType<OkObjectResult>(request);
    Assert.Equal(200, response.StatusCode);
  }
}