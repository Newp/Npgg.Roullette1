using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Proto;

namespace Roulette1.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<RouletteHub>("/chat");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }


    public class RouletteHub : Hub
    {
        RootContext context = new RootContext();
        Props gameActor = Props.FromProducer(() => new GameActor());
        PID pid = null;

        public RouletteHub()
        {
            
            this.pid = context.Spawn(gameActor);
        }

        public async void Betting(object[] obj)
        {
            await Clients.Caller.SendAsync("bettingRespond");
        }

        public async void Send(object[] obj)
        {
            string name = obj[0].ToString();
            string message = obj[1].ToString();
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
