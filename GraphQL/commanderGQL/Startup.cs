using commanderGQL.Data;
using commanderGQL.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GraphQL.Server.Ui.Voyager;
using commanderGQL.GraphQL.Platforms;

namespace commanderGQL
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //this services provide error if we run multiples queries simualtanesouly.
            // services.AddDbContext<ApplicationDbContext>(options => {
            //         options
            //         .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            //         .UseSqlServer(Configuration.GetConnectionString("default"));
            // });

            services.AddPooledDbContextFactory<ApplicationDbContext>(options => {
                options
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(Configuration.GetConnectionString("default"));
            });


            // Adding GraphQL services.

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<PlatformType>()
                .AddType<CommandTypes>()
                // .AddType<PlTypes>()
                // .AddProjections()  //use to add UseProjection in qurey.cs
                .AddFiltering()
                .AddSorting()
                .AddMutationType<Mutations>()
                ;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //adding endpoints to graphQL
                endpoints.MapGraphQL();

                // endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World!");
                // });
            });

            app.UseGraphQLVoyager(new VoyagerOptions() {
                GraphQLEndPoint="/graphql"
                
            }, path: "/graphql-voyager");

            
        }
    }
}