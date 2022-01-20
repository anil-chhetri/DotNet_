using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DTOs;
using Catalog.Extensions;
using Catalog.Models;
using Catalog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }


        [HttpGet("", Name = nameof(GetItems))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemDto>))]
        public IActionResult GetItems() => Ok(itemRepository.GetAll().Select(item => item.AsItemDto()));



        [HttpGet("{Id}", Name = nameof(GetItemsById))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetItemsById(Guid Id)
        {
            var fromdb = itemRepository.GetById(Id);

            if (fromdb == null)
            {
                return NotFound("Invalid Id Passed.");
            }

            return Ok(fromdb.AsItemDto());
        }


        // [HttpGet("{Name}")]
        // [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Item))]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public IActionResult GetItemsByName(string Name)
        // {
        //     var fromdb = itemRepository.GetByName(Name);

        //     if(fromdb == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(fromdb);
        // }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddItemDtos))]
        public IActionResult AddItems([FromBody] AddItemDtos newItem)
        {
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = newItem.Name,
                Price = newItem.price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            itemRepository.AddItem(item);
            itemRepository.Save();

            return CreatedAtAction(nameof(GetItemsById), new { Id = item.Id }, item.AsItemDto());
        }



        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateItem(Guid Id, [FromBody] UpdateItemDtos items)
        {
            var fromdb = itemRepository.GetById(Id);
            if (fromdb == null)
            {
                return NotFound();
            }

            var updatedItem = fromdb with
            {
                Name = items.Name,
                Price = items.Price
            };

            itemRepository.UpdateItem(updatedItem);
            itemRepository.Save();

            return AcceptedAtAction(nameof(GetItemsById), new { Id = Id }, items);
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteItem(Guid Id)
        {
            var item = itemRepository.GetById(Id);

            if(item == null)
            {
                return NotFound();
            }

            itemRepository.Delete(item);
            itemRepository.Save();

            return NoContent();
        }

    }
}