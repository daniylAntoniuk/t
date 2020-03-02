using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Repositories;
using GeekStore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<DbUser, DbRole>(options => options.Stores
            .MaxLengthForKeys = 128)
                     .AddEntityFrameworkStores<DBContext>()
                     .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                })
            .AddGoogle(options =>
             {
                 options.ClientId = "354708562295-fr8s6uvlsvk6ti2ulvdev3ogsa330u8t.apps.googleusercontent.com";
                 options.ClientSecret = "FxUhNXe-cNh7Cw5PoadN8mzy";
             })
            .AddTwitter(twitterOptions =>
            {                
                twitterOptions.ConsumerKey = "AQaIJJLSZsqQHLxtx8OpzCK5g";
                twitterOptions.ConsumerSecret = "LDzEQmKd5ccV9bhTcBkZJyuHWGAlZsFSqG9Xq9SLBpYYdqn9kC";
                twitterOptions.RetrieveUserDetails = true;
            })
            //.AddFacebook(options =>
            //{
            //    options.AppId = "2959980590699969";
            //    options.AppSecret = "68d4dab82efab4d079a1c57716243644";
            //})
            ;



            services.AddTransient<ISubcategory, SubcategoryRepository>();
            services.AddTransient<ICategory, CategoryRepository>();
            services.AddTransient<IProduct, ProductRepository>();
            services.AddTransient<IProductImages, ProductImageRepository>();
            services.AddTransient<IComment, CommentRepository>();
            services.AddTransient<IOrder, OrderRepository>();
            services.AddTransient<ICart, CartRepository>();
            services.AddTransient<IUserProfile, UserProfileRepository>();
            //services.AddTransient<IEmailSender, EmailSender>();


            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Seed.SeedData(app.ApplicationServices, env, this.Configuration);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseAuthentication();
      
            

            app.UseSession(
                );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    "404-PageNotFound",
                    "{*url}",
                    new { controller = "Home", action = "Error" }
                    );
                routes.MapRoute(
                   name: "seeProduct",
                   template: "Product/SeeProduct/{id}"
                   );
                routes.MapRoute(
                   name: "categoryProducts",
                   template: "Product/CategoryProducts/{id}"
                   );
                routes.MapRoute(
                  name: "cartProducts",
                  template: "Product/SeeCart/{id}"
                  );
                routes.MapRoute(
                  name: "cartCheckout",
                  template: "Product/CartCheckout/{id}"
                  );

                routes.MapRoute(
                  name: "seeProfile",
                  template: "Account/SeeProfile/{id}"
                  );
                routes.MapRoute(
                  name: "productDisable",
                  template: "Admin/ProductDisable/{id}"
                  );
                routes.MapRoute(
                  name: "productEnable",
                  template: "Admin/ProductEnable/{id}"
                  );
                routes.MapRoute(
                  name: "addToCart",
                  template: "Product/AddToCart/{id}"
                  );

            });
        }
    }
}

