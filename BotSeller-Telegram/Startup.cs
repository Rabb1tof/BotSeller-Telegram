using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BotSeller_Telegram.Database;
using BotSeller_Telegram.Extentions;
using Microsoft.Extensions.Configuration;

namespace BotSeller_Telegram
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public readonly IConfiguration Configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTelegramBot();
            services.AddDbContext<Context>(ctx =>
                ctx.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddBag<long>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
