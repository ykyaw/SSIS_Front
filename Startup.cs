using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSIS_FRONT.Components.JWT.Impl;
using SSIS_FRONT.Components.JWT.Interfaces;
using SSIS_FRONT.Extensions;

namespace SSIS_FRONT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:5000");
                });
            });
            services.AddControllersWithViews();
            services.AddScoped<IAuthService, JWTService>();

            services.AddSession();
            //inject the service
            services.AddScoped<HttpClient>();
            //services.AddScoped<DBUser>();
            //inject dbcontext
            //services.AddDbContext<UserContext>(opt =>
            //   opt.UseLazyLoadingProxies()
            //   .UseSqlServer(Configuration.GetConnectionString("DbConn")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();


            app.UseSession();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseMiddlewareExtensions();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
