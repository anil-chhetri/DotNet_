using Catalog.DTOs;
using Catalog.Models;

namespace Catalog.Extensions
{
    public static class ItemExtensions
    {
        public static ItemDto AsItemDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}