using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CookiesBasedAuth.Data;
using CookiesBasedAuth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CookiesBasedAuth
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("default")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 2;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.SignIn.RequireConfirmedEmail = true;
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/identity/login";
                options.AccessDeniedPath = "/identity/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
            });

            services.Configure<SmtpSettings>(Configuration.GetSection(nameof(SmtpSettings)));
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            //     app.Use(async (context, next) =>
            //   {
            //       Console.WriteLine("checking migrations...");
            //       var dbcontext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
            //       if (dbcontext.Database.GetPendingMigrations().Any())
            //       {
            //           dbcontext.Database.Migrate();
            //       }

            //       await next();
            //       Console.WriteLine("done checking Mirations");
            //   });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //     app.Use(async (context, next) =>
            //   {
            //       var dbcontext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
            //       if (dbcontext.Database.GetPendingMigrations().Any())
            //       {
            //           dbcontext.Database.Migrate();
            //       }

            //       await next();
            //   });

        }

    }
}
