using System;
using System.Linq;
using commanderGQL.Data;
using commanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace commanderGQL.GraphQL.Platforms
{

    public class CommandTypes : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor
                .Field(x => x.Platform)
                .ResolveWith<CommandResolver>(c => c.GetPlatform(default, default))
                .UseDbContext<ApplicationDbContext>();


            // descriptor.Field(c => c.PlatformId).UseFiltering();    
        }

        private class CommandResolver
        {
            public Platform GetPlatform([Parent] Command command, [ScopedService] ApplicationDbContext context)
            {
                return context.Platforms.FirstOrDefault(c => c.Id == command.PlatformId);
            }
        }

    }


}