using FourthYearProject.IDP.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: HostingStartup(typeof(FourthYearProject.IDP.Areas.Identity.IdentityHostingStartup))]
namespace FourthYearProject.IDP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public IWebHostEnvironment Environment { get; }

        public void Configure(IWebHostBuilder builder )
        {

            builder.ConfigureServices((context, services) =>
            {


                services.AddDbContext<dbContext>(options =>
                  options.UseSqlServer(
                      context.Configuration.GetConnectionString("dbContextConnection")));


               // if (context.HostingEnvironment.IsDevelopment())
               // {
               //     services.AddDbContext<dbContext>(options =>
               //    options.UseSqlServer(
               //        context.Configuration.GetConnectionString("dbContextConnection")));
               // } else
               // {
               //     services.AddDbContext<dbContext>(options =>
               //options.UseSqlServer(
               //    context.Configuration.GetConnectionString("dbContextConnectionPROD")));

               // }

               

                //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //.AddEntityFrameworkStores<dbContext>();

                services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = true)
              .AddEntityFrameworkStores<dbContext>()
              .AddDefaultTokenProviders();
            });
        }
    }
}