using System.Linq;
using commanderGQL.Data;
using commanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace commanderGQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<PlatformResolver>(c => c.GetCommands(default, default))
                .UseDbContext<ApplicationDbContext>();
        }
    }

    public class PlatformResolver
    {
        public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] ApplicationDbContext context)
        {
            return context.Commands.Where(x => x.PlatformId == platform.Id);
        }
    }
}