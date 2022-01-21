using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace commanderGQL.Models
{
    [GraphQLDescription(@"
    Represent any software or services that has command line interface.
    Can varies from old to new.
    ")]
    public record Platform 
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string LicenseKey { get; set; }

        public IEnumerable<Command> Commands { get; set; } = new List<Command>();
    }
}