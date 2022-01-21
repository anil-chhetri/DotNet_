using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace commanderGQL.Models
{
    [GraphQLDescription(@"Represent the command and howto perform those command.")]
    public class Command
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string HowTo { get; set; }

        [Required]
        [GraphQLDescription("Command to run on command line interface.")]
        public string CommandText { get; set; }

        [Required]
        public int PlatformId { get; set; }

        public Platform Platform { get; set; }

    }
}