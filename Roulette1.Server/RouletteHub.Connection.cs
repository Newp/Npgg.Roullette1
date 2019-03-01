using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public partial class RouletteHub
    {
        public string UserId => Context.ConnectionId;
        ActorManager _gameManager;
        public RouletteHub(ActorManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
