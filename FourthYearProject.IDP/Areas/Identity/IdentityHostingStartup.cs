using FourthYearProject.IDP.Areas.Identity;
using FourthYearProject.IDP.Areas.Identity.Data;
using FourthYearProject.IDP.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DbContext = FourthYearProject.IDP.Areas.Identity.Data.DbContext;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace FourthYearProject.IDP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public IWebHostEnvironment Environment { get; }

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<DbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("dbContextConnection")));


                services.AddIdentity<ApplicationUser, IdentityRole>(
                        options =>
                        {
                            options.SignIn.RequireConfirmedAccount = true;
                            options.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                                new TokenProviderDescriptor(
                                    typeof(CustomEmailConfirmationTokenProvider<ApplicationUser>)));
                            options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                        })
                    .AddEntityFrameworkStores<DbContext>()
                    .AddDefaultTokenProviders();

                services.AddTransient<CustomEmailConfirmationTokenProvider<ApplicationUser>>();




            });





        }
    }
}