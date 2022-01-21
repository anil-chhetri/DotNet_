using System.ComponentModel.DataAnnotations;

namespace commanderGQL.Models
{
    public record Platform 
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string LicenseKey { get; set; }

    }
}