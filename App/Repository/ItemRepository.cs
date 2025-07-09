using Funko_shop.Data;
using Funko_shop.DTOs;
using Funko_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Funko_shop.Repository;

public interface IItemRepository
{
  Task<List<ItemDto>> GetItems(int offset);
  Task<List<ItemDto>> GetItemsByPriceOrderAsc(int offset);
  Task<List<ItemDto>> GetItemsByPriceOrderDesc(int offset);
  Task<List<ItemDto>> GetItemsByFilter(string filter, int offset);
  Task<List<ItemDto>> GetItemsByFilterOrderAsc(string filter, int offset);
  Task<List<ItemDto>> GetItemsByFilterOrderDesc(string filter, int offset);
  Task<ItemDetailDto> GetItemDetail(int id);
  Task<Item> GetItemModel(int id);
}
public class ItemRepository : IItemRepository
{
  private readonly AppDbContext _context;
  public ItemRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<ItemDto>> GetItems(int offset)
  {
    var items = await _context.Items
    .OrderBy(item => item.id_item)
    .Include(item => item.categoryFk)
    .Select(item => new ItemDto
    {
      IdItem = item.id_item,
      Name = item.name,
      Image = item.image_1,
      Price = item.unit_price,
      Category = item.categoryFk.name_category
    })
    .Skip(offset)
    .Take(15)
    .ToListAsync();
    return items;
  }

  public async Task<List<ItemDto>> GetItemsByPriceOrderAsc(int offset)
  {
    var items = await _context.Items
    .OrderBy(items => items.unit_price)
    .Include(item => item.categoryFk)
    .Select(item => new ItemDto
    {
      IdItem = item.id_item,
      Name = item.name,
      Price = item.unit_price,
      Category = item.categoryFk.name_category,
      Image = item.image_1
    })
    .Skip(offset)
    .Take(15)
    .ToListAsync();
    return items;
  }

  public async Task<List<ItemDto>> GetItemsByPriceOrderDesc(int offset)
  {
    var items = await _context.Items
    .OrderByDescending(items => items.unit_price)
    .Include(item => item.categoryFk)
    .Select(item => new ItemDto
    {
      IdItem = item.id_item,
      Name = item.name,
      Price = item.unit_price,
      Category = item.categoryFk.name_category,
      Image = item.image_1
    })
    .Skip(offset)
    .Take(15)
    .ToListAsync();
    return items;
  }

  public async Task<List<ItemDto>> GetItemsByFilter(string filter, int offset)
  {
    var items = await _context.Items
    .OrderBy(item => item.id_item)
    .Include(item => item.categoryFk)
    .Where(item => item.name == filter || item.categoryFk.name_category == filter)
    .Select(item => new ItemDto
    {
      IdItem = item.id_item,
      Name = item.name,
      Price = item.unit_price,
      Category = item.categoryFk.name_category,
      Image = item.image_1
    })
    .Skip(offset)
    .Take(15)
    .ToListAsync();
    return items;
  }

  public async Task<List<ItemDto>> GetItemsByFilterOrderAsc(string filter, int offset)
  {
    var items = await _context.Items
    .OrderBy(item => item.unit_price)
    .Include(item => item.categoryFk)
    .Where(item => item.name == filter || item.categoryFk.name_category == filter)
    .Select(item => new ItemDto
    {
      IdItem = item.id_item,
      Name = item.name,
      Price = item.unit_price,
      Category = item.categoryFk.name_category,
      Image = item.image_1
    })
    .Skip(offset)
    .Take(15)
    .ToListAsync();
    return items;
  }
  public async Task<List<ItemDto>> GetItemsByFilterOrderDesc(string filter, int offset)
  {
    var items = await _context.Items
    .OrderByDescending(item => item.unit_price)
    .Include(item => item.categoryFk)
    .Where(item => item.name == filter || item.categoryFk.name_category == filter)
    .Select(item => new ItemDto
    {
      IdItem = item.id_item,
      Name = item.name,
      Price = item.unit_price,
      Category = item.categoryFk.name_category,
      Image = item.image_1
    })
    .Skip(offset)
    .Take(15)
    .ToListAsync();
    return items;
  }

  public async Task<ItemDetailDto> GetItemDetail(int id)
  {
    var item = await _context.Items
    .Where(item => item.id_item == id)
    .Select(item => new ItemDetailDto
    {
      IdItem = item.id_item,
      ItemName = item.name,
      Image = item.image_1,
      Price = item.unit_price,
      Category = item.categoryFk.name_category,
      Description = item.description,
      Stock = item.stock
    })
    .FirstOrDefaultAsync() ?? throw new Exception($"No existe articulo con el id: {id}");
    return item;
  }

  public async Task<Item> GetItemModel(int id)
  {
    var item = await _context.Items
    .Where(item => item.id_item == id)
    .FirstOrDefaultAsync() ?? throw new Exception($"No existe articulo con el id: {id}");
    return item;
  }

}