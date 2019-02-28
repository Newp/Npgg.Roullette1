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
            string userid = context.Headers.GetOrDefault("userid");
            var msg = context.Message;

            if (msg is Action<User> userAction)
            {
                var user = this._users[userid];
                userAction(user);
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
                        OnBoard = 0
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
