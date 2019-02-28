using Microsoft.AspNetCore.SignalR;
using Proto;
using Proto.Mailbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{

    public class UserManager : IActor
    {
        Dictionary<string, User> _users = new Dictionary<string, User>();
        IHubContext<RouletteHub> _hub;
        public UserManager(IHubContext<RouletteHub> hub)
        {
            this._hub = hub;
        }

        int idseq = 1;

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;

            if (msg is Action<User> userAction)
            {
                if (context.Headers.TryGetValue("userid", out string userId))
                {
                    var user = this._users[userId];
                    userAction(user);
                }
                else
                {
                    //활성화 상태에 대한 처리는 일단 미룬다.
                    foreach (var user in _users.Values)
                    {
                        userAction(user);
                    }
                }
                context.Respond(0);
            }
            else if (msg is RequestNewUser newUser)
            {

                if(_users.TryGetValue(newUser.UserId, out var user) == false)
                {
                    user = new User()
                    {
                        UserId = newUser.UserId,
                        Money = 100000,
                    };
                    _users.Add(user.UserId, user);
                }
                user.ConnectedId = newUser.ConnectedId;

                context.Respond(user);
            }
            else if (msg is SystemMessage)
            {
            }
            else
            {

            }

            return Actor.Done;
        }
        
    }

    public class RequestNewUser
    {
        public string UserId { get; set; }
        public string ConnectedId { get; set; }
    }
    
    public enum SessionEvent
    {
        None,
        OnLogin,
    }
    public class SessionMessage
    {
        public SessionEvent SessionEvent { get; set; }
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
