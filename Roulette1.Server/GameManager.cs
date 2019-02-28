using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public class GameManager// :Singleton<GameManager>
    {
        public static readonly GameManager Instance = new GameManager();

        RootContext _context = new RootContext();
        PID _session = null;

        private GameManager()
        {
            this._session = _context.Spawn(Props.FromProducer(() => new SessionActor()));
        }

        public static Task<T> SessonRequest<T>(object obj) => context.RequestAsync<T>(_session, obj);
    }
    
}
