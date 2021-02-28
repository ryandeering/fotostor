// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using FourthYearProject.IDP.Areas.Identity.Data;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FourthYearProject.IDP
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<IEmailSender, DummyEmailSender>();

            //  services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, MyUserClaimsPrincipalFactory>();

            //// configures IIS out-of-proc settings (see https://github.com/aspnet/AspNetCore/issues/14882)
            //services.Configure<IISOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});

            //// configures IIS in-proc settings
            //services.Configure<IISServerOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            }).AddAspNetIdentity<ApplicationUser>();


            //// in-memory, code config

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto;
                // Only loopback proxies are allowed by default.
                // Clear that restriction because forwarders are enabled by explicit 
                // configuration.
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });



            //if (Environment.IsDevelopment())
            //{
            //    builder.AddInMemoryIdentityResources(Config.Ids);
            //    builder.AddInMemoryApiResources(Config.Apis);
            //    builder.AddInMemoryClients(Config.Clients);
            //} else
            //{
            //    builder.AddInMemoryIdentityResources(ConfigProd.Ids);
            //    builder.AddInMemoryApiResources(ConfigProd.Apis);
            //    builder.AddInMemoryClients(ConfigProd.Clients);
            //}


            if (!Environment.IsDevelopment())
            {
                builder.AddInMemoryIdentityResources(ConfigProd.Ids);
                builder.AddInMemoryApiResources(ConfigProd.Apis);
                builder.AddInMemoryClients(ConfigProd.Clients);
            }
            else
            {
                builder.AddInMemoryIdentityResources(Config.Ids);
                builder.AddInMemoryApiResources(Config.Apis);
                builder.AddInMemoryClients(Config.Clients);
            }







            //    // in-memory, code config
            //    if (Environment.IsProduction())
            //{
            //    builder.AddInMemoryIdentityResources(ConfigProd.Ids);
            //    builder.AddInMemoryApiResources(ConfigProd.Apis);
            //    builder.AddInMemoryClients(ConfigProd.Clients);
            //} else
            //{
            //    builder.AddInMemoryIdentityResources(Config.Ids);
            //    builder.AddInMemoryApiResources(Config.Apis);
            //    builder.AddInMemoryClients(Config.Clients);
            //}


            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            //// for demo purposes, allow cors requests
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.WithOrigins("https://localhost:44366/", "https://localhost:44304", "https://localhost:44333", "https://localhost:44340")
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials());
            //});

            services.AddSingleton<ICorsPolicyService>((container) =>
            {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
                return new DefaultCorsPolicyService(logger)
                {
                    AllowAll = true
                };
            });

            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders();

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}