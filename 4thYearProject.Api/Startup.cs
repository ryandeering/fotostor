namespace _4thYearProject.Api
{
    using _4thYearProject.Api.CloudStorage;
    using _4thYearProject.Api.Controllers.Identity;
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Stripe;
    using System.Linq;

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
            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "_4thYearProject"));

            var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .Build();   //change this later as we let posts become publicly visible



            services.AddHttpContextAccessor();


            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });


            if (Environment.IsDevelopment())
            {
                services.AddAuthentication(
            IdentityServerAuthenticationDefaults.AuthenticationScheme)
        .AddIdentityServerAuthentication(options =>
        {
            options.Authority = "https://localhost:44333/";
            options.ApiName = "_4thyearprojectapi";
        });

            }
            else
            {
                services.AddAuthentication(
IdentityServerAuthenticationDefaults.AuthenticationScheme)
.AddIdentityServerAuthentication(options =>
{
    options.Authority = "https://fourthyrprojidp.azurewebsites.net";
    options.ApiName = "_4thyearprojectapi";
});
            }







            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFollowingRepository, FollowingRepository>();
            services.AddScoped<IUserDataRepository, UserDataRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //necessary?
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddControllers(configure =>
            configure.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["ApiKey"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseCors("Open");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
