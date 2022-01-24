using System.Linq;
using commanderGQL.Data;
using commanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace commanderGQL.GraphQL.Platforms
{
    public class PlTypes : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(x => x.GetCommands(default, default))
                .UseDbContext<ApplicationDbContext>()
                .Description("Commands available in this platforms");
        }


    }

    public class Resolvers
    {
        public IQueryable<Command> GetCommands([Parent]Platform platform, [ScopedService] ApplicationDbContext context)
        {
            return context.Commands.Where(x => x.PlatformId == platform.Id);
        }
    }
}