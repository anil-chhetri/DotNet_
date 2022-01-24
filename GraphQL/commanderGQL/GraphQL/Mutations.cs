using System.Threading.Tasks;
using commanderGQL.Data;
using commanderGQL.GraphQL.commands;
using commanderGQL.GraphQL.Platforms;
using commanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace commanderGQL.GraphQL
{
    public class Mutations
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<PlatformPayload> AddPlatformAsync(PlatformInput input,
                    [ScopedService] ApplicationDbContext context)
        {
            var platform = new Platform()
            {
                Name = input.Name
            };

            await context.Platforms.AddAsync(platform);
            await context.SaveChangesAsync();

            return new PlatformPayload(platform);
        }


        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<commandPayload> AddCommandAsync(commandInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var command = new Command() {
                HowTo = input.howto,
                CommandText = input.commandtext,
                PlatformId = input.platformId
            };

            await context.Commands.AddAsync(command);
            await context.SaveChangesAsync();

            return new commandPayload(command);
        }

    }

}