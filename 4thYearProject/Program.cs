using System;
using System.Net.Http;
using System.Threading.Tasks;
using _4thYearProject.Server.MessageHandlers;
using _4thYearProject.Server.Services;
using _4thYearProject.Server.Services.Identity;
using _4thYearProject.Server.Services.Interfaces;
using _4thYearProject.Shared;
using Blazored.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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


            var APIDevelop = builder.Configuration["APIDevelop"];
            var APIProduction = builder.Configuration["APIProduction"];


            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            builder.Services.AddBlazoredModal();

            if (builder.HostEnvironment.IsProduction())
            {
                builder.Services
                    .AddHttpClient<IPostDataService, PostDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IUserDataService, UserDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IFollowingDataService, FollowingDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ICommentDataService, CommentDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ILikeDataService, LikeDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IShoppingCartService, ShoppingCartDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IStripePaymentService, StripePaymentService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IStripePaymentService, StripePaymentService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IHashTagDataService, HashTagDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISuggestionsDataService, SuggestionsDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISearchDataService, SearchDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();


                builder.Services.AddOidcAuthentication(options =>
                {
                    builder.Configuration.Bind("OidcConfigurationProduction", options.ProviderOptions);
                });
            }
            else
            {
                builder.Services
                    .AddHttpClient<IPostDataService, PostDataService
                    >(client => client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IUserDataService, UserDataService>(
                        client => client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IFollowingDataService, FollowingDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ICommentDataService, CommentDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ILikeDataService, LikeDataService>(
                        client => client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IShoppingCartService, ShoppingCartDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IStripePaymentService, StripePaymentService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IHashTagDataService, HashTagDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISuggestionsDataService, SuggestionsDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISearchDataService, SearchDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectAPIAuthorizationMessageHandler>();


                builder.Services.AddOidcAuthentication(options =>
                {
                    builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
                });
            }


            builder.Services.AddScoped<IUserService, UserService>();

            await builder.Build().RunAsync();
        }
    }
}