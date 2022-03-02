using HotChocolate;

namespace Graphql2.Query
{
    public class test 
    {
        [GraphQLDeprecated("applicable for only testing.")]
        public string Messages { get; set; } = "Working";
    }
}