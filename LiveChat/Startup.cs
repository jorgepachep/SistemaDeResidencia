using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiveChat.Data;
using LiveChat.Data.Repository;
using LiveChat.Data.Repository.SQLRepository;
using LiveChat.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace LiveChat
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;

        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(
                dbContextOptions =>
                {
                    //dbContextOptions.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection"));
                    dbContextOptions.UseInMemoryDatabase(databaseName:"StatusProject");
                }    
            );

            services.AddIdentity<StoredUser,IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<ICopropietarioRepository,CopropietarioRepository>();
            services.AddAuthentication()
                .AddCookie(options=> {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/Forbidden";
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")
                ),
                RequestPath = new PathString("/node_modules")
            });

            app.UseAuthentication();

            app.UseMvc(options => options.MapRoute(
                "Default",
                "{controller}/{action}/{id?}",
                new {
                    Controller ="Home",
                    Action="Index"
                })
             );

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
