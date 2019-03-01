using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Proto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public class ActorManager
    {
        RootContext _context = new RootContext();
        PID _user = null;

        public ActorManager(IHubContext<RouletteHub> hub, IServiceProvider provider)
        {
            this._user = _context.Spawn(Props.FromProducer(() => provider.GetService<GameManager>()));
            Task.Factory.StartNew(Update);
        }

        async void Update()
        {
            Console.WriteLine("ActorManager Update Started");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var update = new Update();
            var getframe = new GetFrame();
            while (true)
            {
                await _context.RequestAsync<int>(_user, update);

                if (watch.ElapsedMilliseconds > 1000)
                {
                    int frame  = await _context.RequestAsync<int>(_user, getframe); ;
                    Console.WriteLine("frame:{0}", frame);
                    watch.Restart();
                }

            }
        }

        public Task<T> SessonRequest<T>(object obj) => _context.RequestAsync<T>(_user, obj);

        public Task<ApiResult> Betting(BettingInfo req)
        {
            return _context.RequestAsync<ApiResult>(_user, req);
        }
    }

    public class Update { }
    public class GetFrame { }
}