using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData.Abstracts;
using ODataExamples.Data;
using Microsoft.AspNetCore.OData;

namespace ODataExamples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ODataDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default")));

            //services.AddOData(options =>
            //        options.Filter().Expand().Select().OrderBy().AddModel("odata", GetEdmModel()));

            services.AddControllers()
                .AddOData(opt => opt.Filter().Expand().Select().OrderBy().AddRouteComponents("odata", GetEdmModel())); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            //name of the controller is passed.
            builder.EntitySet<Customer>("Customers");

            return builder.GetEdmModel();
        }
    }
}
