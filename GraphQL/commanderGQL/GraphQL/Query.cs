using System.Linq;
using commanderGQL.Data;
using commanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace commanderGQL.GraphQL
{
    public class Query
    {

        //added to support DbcontextPool
        [UseDbContext(typeof(ApplicationDbContext))]
        [UseFiltering]
        [UseSorting]
        // [UseProjection]  // used to get the foreign key refrenced tables data.
        public IQueryable<Platform> GetPlatforms([ScopedService] ApplicationDbContext context)
            //creating the scoped Dependency Imjection services
        {
            return context.Platforms;
        }

        
        [UseDbContext(typeof(ApplicationDbContext))]
        // [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommands([ScopedService] ApplicationDbContext context)
        {
            return context.Commands;
        }

    }
}