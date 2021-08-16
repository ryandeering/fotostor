using System;
using System.Linq;
using _4thYearProject.Api.CloudStorage;
using _4thYearProject.Api.Controllers.Identity;
using _4thYearProject.Api.DataSeeding;
using _4thYearProject.Api.Emailing;
using _4thYearProject.Api.Models;
using _4thYearProject.Api.Models.Interfaces;
using _4thYearProject.Shared;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Stripe;

namespace _4thYearProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build(); //change this later as we let posts become publicly visible

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();


            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] {"application/octet-stream"});
            });


            if (Environment.IsDevelopment())
                services.AddAuthentication(
                        IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "https://localhost:44333/";
                        options.ApiName = "_4thyearprojectapi";
                    });
            else
                services.AddAuthentication(
                        IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "https://fotostopidp.azurewebsites.net";
                        options.ApiName = "_4thyearprojectapi";
                    });


            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IFollowingRepository, FollowingRepository>();
            services.AddScoped<IUserDataRepository, UserDataRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IHashTagRepository, HashTagRepository>();
            services.AddScoped<ISuggestionsRepository, SuggestionsRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddTransient<IEmailSender, MailKitEmailSender>();
            services.Configure<MailKitEmailSenderOptions>(options =>
            {
                options.Host_Address = Configuration["ExternalProviders:MailKit:SMTP:Address"];
                options.Host_Port = Convert.ToInt32(Configuration["ExternalProviders:MailKit:SMTP:Port"]);
                options.Host_Username = Configuration["ExternalProviders:MailKit:SMTP:Account"];
                options.Host_Password = Configuration["ExternalProviders:MailKit:SMTP:Password"];
                options.Sender_EMail = Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
                options.Sender_Name = Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "4thYearProject.API", Version = "v1"});
            });


            if (Environment.IsDevelopment())
                services.AddCors(options =>
                {
                    options.AddPolicy("Open",
                        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                });
            else
                services.AddCors(options =>
                {
                    options.AddPolicy("Open",
                        builder =>
                        {
                            builder.WithOrigins("https://fotostopapi.azurewebsites.net",
                                    "https://fotostopidp.azurewebsites.net",
                                    "https://red-pebble-0ad568c03.azurestaticapps.net")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });


            services.AddControllers(configure =>
                configure.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }




            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["ApiKey"];
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "4thYearProject.API V1"); });
            }

            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseCors("Open");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}