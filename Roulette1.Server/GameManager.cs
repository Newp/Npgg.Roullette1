using Microsoft.AspNetCore.SignalR;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public class GameManager
    {
        RootContext _context = new RootContext();
        PID _session = null;

        IHubContext<RouletteHub> _hub = null;
        public GameManager(IHubContext<RouletteHub> hub)
        {
            this._session = _context.Spawn(Props.FromProducer(() => new SessionActor()));
            this._hub = hub;
        }

        public Task<T> SessonRequest<T>(object obj) => _context.RequestAsync<T>(_session, obj);
    }
    
}
