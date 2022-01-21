using System.Linq;
using commanderGQL.Data;
using commanderGQL.Models;
using HotChocolate;

namespace commanderGQL.GraphQL
{
    public class Query
    {
        public IQueryable<Platform> GetPlatforms([Service] ApplicationDbContext context)
        {
            return context.Platforms;
        }

        
    }
}