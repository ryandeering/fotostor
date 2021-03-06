using System;
using System.Net.Http;
using System.Threading.Tasks;
using FourthYearProject.Blazor.MessageHandlers;
using FourthYearProject.Blazor.Services;
using FourthYearProject.Blazor.Services.Identity;
using FourthYearProject.Blazor.Services.Interfaces;
using FourthYearProject.Shared;
using Blazored.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FourthYearProject.Blazor
{
#pragma warning disable S1118 // Utility classes should not have public constructors
    public class Program
#pragma warning restore S1118 // Utility classes should not have public constructors
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");


            builder.Services.AddTransient<FourthYearProjectApiAuthorizationMessageHandler>();

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
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IUserDataService, UserDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IFollowingDataService, FollowingDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ICommentDataService, CommentDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ILikeDataService, LikeDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IShoppingCartService, ShoppingCartDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IStripePaymentService, StripePaymentService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IStripePaymentService, StripePaymentService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IHashTagDataService, HashTagDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISuggestionsDataService, SuggestionsDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISearchDataService, SearchDataService>(client =>
                        client.BaseAddress = new Uri(APIProduction))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();


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
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IUserDataService, UserDataService>(
                        client => client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IFollowingDataService, FollowingDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ICommentDataService, CommentDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ILikeDataService, LikeDataService>(
                        client => client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IShoppingCartService, ShoppingCartDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IStripePaymentService, StripePaymentService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<IHashTagDataService, HashTagDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISuggestionsDataService, SuggestionsDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();
                builder.Services
                    .AddHttpClient<ISearchDataService, SearchDataService>(client =>
                        client.BaseAddress = new Uri(APIDevelop))
                    .AddHttpMessageHandler<FourthYearProjectApiAuthorizationMessageHandler>();


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