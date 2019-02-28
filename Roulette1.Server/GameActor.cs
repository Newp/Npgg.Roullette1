using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public class GameActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            throw new NotImplementedException();
        }
    }



    public class SessionActor : IActor
    {
        
        public Task ReceiveAsync(IContext context)
        {
            throw new NotImplementedException();
        }

        
    }
    public enum SessionEvent
    {
        None,
        OnConnect,
    }
    public class SessionMessage
    {
        public SessionEvent SessionEvent { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
    }
}
