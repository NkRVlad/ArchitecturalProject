using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using BusinessLogicLayer;
using BusinessLogicLayer.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogicLayer.ProductService;
using BusinessLogicLayer.FilterService;
using System.Linq;
using DataAccessLayer.Entities;

namespace Task_1
{
    public class Startup
    {
        private Settings _Settings { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _Settings = new Settings();
            Configuration.Bind(_Settings);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration["Data:ConnectionString"],
                    migrations => migrations.MigrationsAssembly("DataAccessLayer")
                );
            });
            
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           
            }).AddJwtBearer(opt =>{

                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = _Settings.EnvironmentSettings.ValidateIssuer,
                    ValidateAudience = _Settings.EnvironmentSettings.ValidateAudience,
                    ValidateLifetime = _Settings.EnvironmentSettings.ValidateLifetime,
                    ValidIssuer = _Settings.EnvironmentSettings.Host,
                    ValidAudience = _Settings.EnvironmentSettings.Host,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Settings.EnvironmentSettings.SecretKey))
                };
            });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ApiRequestFilter));
            });

            services.AddControllersWithViews();
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ApiRequestFilter>();
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            SeedDefaultData(app);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void SeedDefaultData(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (dbContext.Users.FirstOrDefault(u => u.Login == "Vlad") == null)
                {
                    User vlad = new User
                    {
                        Login = "Vlad",
                        Password = "123456"
                    };

                    User oleg = new User
                    {
                        Login = "Oleg",
                        Password = "123456"
                    };

                    dbContext.Users.Add(vlad);
                    dbContext.Users.Add(oleg);

                    dbContext.SaveChanges();
                }

                if (dbContext.Products.FirstOrDefault(u => u.Name == "Tomato") == null)
                {
                    Product tomato = new Product
                    {
                        Name = "Tomato",
                        Count = 124,
                        Price = 23.4
                    };

                    Product potato = new Product
                    {
                        Name = "Potato",
                        Count = 611,
                        Price = 123
                    };

                    Product carrot = new Product
                    {
                        Name = "Carrot",
                        Count = 447,
                        Price = 12
                    };

                    Product bow = new Product
                    {
                        Name = "Bow",
                        Count = 63,
                        Price = 25
                    };

                    dbContext.Products.Add(tomato);
                    dbContext.Products.Add(potato);
                    dbContext.Products.Add(carrot);
                    dbContext.Products.Add(bow);

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
