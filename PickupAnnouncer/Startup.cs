using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickupAnnouncer.Helpers;
using PickupAnnouncer.Hubs;
using PickupAnnouncer.Interfaces;
using PickupAnnouncer.Mappings;
using PickupAnnouncer.Services;

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
            services.AddRazorPages();
            services.AddSignalR();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<PickupHub>("/pickup");
            });
        }
    }
}
