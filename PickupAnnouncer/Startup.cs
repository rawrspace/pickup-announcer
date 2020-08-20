using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickupAnnouncer.Helpers;
using PickupAnnouncer.Hubs;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Mappings;
using PickupAnnouncer.Services;
using System;

namespace PickupAnnouncer
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
            services.AddTransient<IDbHelper, DbHelper>();
            services.AddTransient<IDbService, DbService>(builder => new DbService(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IStudentHelper, StudentHelper>();
            services.AddTransient<IMapper, Mapper>(builder =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new BaseProfile());
                });
                return new Mapper(config);
            });
            services.AddTransient<IRegistrationFileHelper, RegistrationFileHelper>();

            services.AddControllers();
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Admin");
            }).AddNToastNotifyToastr();
            services.AddSignalR();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseNToastNotify();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapHub<PickupHub>("/pickup");
            });
        }
    }
}
