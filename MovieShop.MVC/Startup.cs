using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Repositories;
using AutoMapper;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using MovieShop.MVC.MiddleWares;

namespace MovieShop.MVC
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
            services.AddControllersWithViews();
            //telling our container what class it needs to inject in the consturcture for the interface
            //registration of service for interface
            //autofac third party ioc
            //service.addscoped<if controllername has"home", then use MovieServiceTest, else use MoiveService
            services.AddAutoMapper(typeof(Startup), typeof(MovieShopMappingProfile));
            services.AddScoped<IMovieService, MovieService>();
            // services.AddScoped<IMovieServices, MovieServiceTest>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();
            //   services.AddAutoMapper(profileAssembly1, profileAssembly2 /*, ...*/);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Name = "MovieShopAuthCookie";
                   options.ExpireTimeSpan = TimeSpan.FromHours(2);
                   options.LoginPath = "/Account/Login";
               });
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddDbContext<MovieShopDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection"));
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseMovieShopExceptionMiddleware();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseLoggerMiddleware();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
