using Proto;
using Proto.Mailbox;
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
        Dictionary<string, UserAsset> _logonUser = new Dictionary<string, UserAsset>();

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            if (msg is SessionMessage sm)
            {
                switch (sm.SessionEvent)
                {
                    case SessionEvent.OnConnect:
                        break;
                    case SessionEvent.OnLogin:
                        this._logonUser[sm.ConnectionId] = new UserAsset()
                        {
                            UserId = sm.UserId,
                            Money = 10000000,
                        };

                        context.Respond(0);
                        break;
                    case SessionEvent.OnDisconnect:
                        break;
                    default:
                        throw new Exception("invalid session event");
                }
            }
            else if (msg is SystemMessage)
            {
            }

            return Actor.Done;
        }
        
    }
    public enum SessionEvent
    {
        None,
        OnConnect,
        OnLogin,
        OnDisconnect,
    }
    public class SessionMessage
    {
        public SessionEvent SessionEvent { get; set; }
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
