using Microsoft.AspNetCore.SignalR;
using Proto;
using Proto.Mailbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public class GameManager : IActor
    {
        IHubContext<RouletteHub> _hub;
        ActorManager _actorManager;

        Dictionary<string, HitChecker> _hitChecker = new Dictionary<string, HitChecker>();

        public GameManager(IHubContext<RouletteHub> hub, ActorManager actorManager)
        {
            this._hub = hub;
            this._actorManager = actorManager;

            this._hitChecker = HitChecker.MakeHitChecker().ToDictionary(v => v.ToString());
        }
        

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;

            if (msg is Betting betting)
            {
                
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

    public class BettingChecker
    {
        public HitChecker HitChecker { get; set; }
    }

    public class Betting
    {
        public string BettingType { get; set; }
        public int Amount { get; set; }
    }

    public enum GameBroadcast
    {
        NewGame,
        GameUpdate,

    }
}
