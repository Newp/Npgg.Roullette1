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
            Task.Factory.StartNew(FrameUpdate);
        }

        async void FrameUpdate()
        {
            Console.WriteLine("ActorManager Update Started");
            Stopwatch frameWatch = new Stopwatch();
            frameWatch.Start();
            var update = new Update();
            var getframe = new GetFrame();
            while (true)
            {
                await _context.RequestAsync<int>(_user, update);

                if (frameWatch.ElapsedMilliseconds > 1000)
                {
                    int frame  = await _context.RequestAsync<int>(_user, getframe); ;
                    frameWatch.Stop();

                    Console.WriteLine("frame:{0} on {1}", frame, frameWatch.ElapsedMilliseconds);
                    frameWatch.Restart();
                }
            }
        }

        public Task<T> SessonRequest<T>(object obj) => _context.RequestAsync<T>(_user, obj);

        public Task<int> Betting(BettingInfo req)
        {
            return _context.RequestAsync<int>(_user, req);
        }
    }

    public class Update { }
    public class GetFrame { }
}