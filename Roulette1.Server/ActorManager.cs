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
            this._user = _context.Spawn(Props.FromProducer(() => provider.GetService<UserManager>()));
        }

        void Update()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int frame = 0;
            long elpsedms = 0;
            while(true)
            {
                frame++;
                elpsedms += watch.ElapsedMilliseconds;

                if(elpsedms > 1000)
                {
                    Console.WriteLine("frame:{0}", frame);
                    frame = 0;
                    elpsedms = 0;
                }

                watch.Restart();
            }
        }

        public Task<T> SessonRequest<T>(object obj) => _context.RequestAsync<T>(_user, obj);

        public Task<ApiResult> Betting(BettingInfo req)
        {
            return _context.RequestAsync<ApiResult>(_user, req);
        }
    }
}