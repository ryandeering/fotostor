using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using _4thYearProject.Server.Services;
using _4thYearProject.Server.MessageHandlers;
using _4thYearProject.Shared;
using _4thYearProject.Server.Services.Identity;

namespace _4thYearProject.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient<FourthYearProjectAPIAuthorizationMessageHandler>();

            builder.Services.AddTransient(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });


            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
            });


            //if (builder.HostEnvironment.IsDevelopment())
            //{
            //  builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"));
            //  builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"));
            // builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"));
            //
            //  }

            builder.Services.AddHttpClient<IPostDataService, PostDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/")).AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"))
            .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"))
            .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"))
            .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();



            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //  builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://4thyearprojectapi.azurewebsites.net/"));
            // builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri("https://4thyearprojectapi.azurewebsites.net/"));
            //builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri("https://4thyearprojectapi.azurewebsites.net/"));


            builder.Services.AddScoped<IUserService, UserService>();

            await builder.Build().RunAsync();
        }
    }
}
