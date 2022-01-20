using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record AddItemDtos
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,10000)]
        public decimal price { get; set; }
        
    }
}