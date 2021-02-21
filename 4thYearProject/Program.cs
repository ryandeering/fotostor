using _4thYearProject.Server.MessageHandlers;
using _4thYearProject.Server.Services;
using _4thYearProject.Server.Services.Identity;
using _4thYearProject.Shared;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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


            string APIDevelop = builder.Configuration["APIDevelop"];
            string APIProduction = builder.Configuration["APIProduction"];


            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            if (builder.HostEnvironment.IsProduction())
            {
                builder.Services.AddHttpClient<IPostDataService, PostDataService>(client => client.BaseAddress = new Uri(APIProduction)).AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IUserDataService, UserDataService>(client => client.BaseAddress = new Uri(APIProduction))
           .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IFollowingDataService, FollowingDataService>(client => client.BaseAddress = new Uri(APIProduction))
          .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<ICommentDataService, CommentDataService>(client => client.BaseAddress = new Uri(APIProduction))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<ILikeDataService, LikeDataService>(client => client.BaseAddress = new Uri(APIProduction))
               .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IShoppingCartService, ShoppingCartDataService>(client => client.BaseAddress = new Uri(APIProduction))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();

                builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri(APIProduction))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri(APIProduction))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri(APIProduction))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                


                builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfigurationProduction", options.ProviderOptions);
            });



            }
            else
            {

                builder.Services.AddHttpClient<IPostDataService, PostDataService>(client => client.BaseAddress = new Uri(APIDevelop)).AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IUserDataService, UserDataService>(client => client.BaseAddress = new Uri(APIDevelop))
           .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IFollowingDataService, FollowingDataService>(client => client.BaseAddress = new Uri(APIDevelop))
          .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<ICommentDataService, CommentDataService>(client => client.BaseAddress = new Uri(APIDevelop))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<ILikeDataService, LikeDataService>(client => client.BaseAddress = new Uri(APIDevelop))
               .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IShoppingCartService, ShoppingCartDataService>(client => client.BaseAddress = new Uri(APIDevelop))
               .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();

                builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri(APIDevelop))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri(APIDevelop))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri(APIDevelop))
                .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();

			builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
            });



            }


            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //  builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://4thyearprojectapi.azurewebsites.net/"));
            // builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri("https://4thyearprojectapi.azurewebsites.net/"));
            //builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri("https://4thyearprojectapi.azurewebsites.net/"));


            builder.Services.AddScoped<IUserService, UserService>();

            await builder.Build().RunAsync();
        }
    }
}
