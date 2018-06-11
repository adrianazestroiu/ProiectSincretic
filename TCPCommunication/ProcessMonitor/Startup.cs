using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRChat.Hubs;
using ProcessMonitor.DAL;
using Microsoft.EntityFrameworkCore;
using Server;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(ProcessMonitor.Startup))]
namespace ProcessMonitor
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
            services.AddSignalR();
            services.AddMvc();

            var connection = @"Server=DESKTOP-5SHQ6DG\SQLEXPRESS;Database=Entries;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<EntriesContext>(options => options.UseSqlServer(connection));

            //var tcp = new AsyncServer();
            //var thread = new Thread(new ThreadStart(AsyncServer.StartListening));
            //thread.IsBackground = true;
            //thread.Start();
            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(AsyncServer.StopClient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
