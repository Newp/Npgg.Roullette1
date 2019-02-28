using Microsoft.AspNetCore.SignalR;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public class ActorManager
    {
        RootContext _context = new RootContext();
        PID _user = null;

        public ActorManager(IHubContext<RouletteHub> hub)
        {
            this._user = _context.Spawn(Props.FromProducer(() => new UserManager(hub)));
        }

        public Task<T> SessonRequest<T>(object obj) => _context.RequestAsync<T>(_user, obj);
    }
    
}
