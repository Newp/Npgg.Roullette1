using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Roulette1.Server
{
    public class RouletteHub : Hub
    {
        ActorManager _gameManager;
        public RouletteHub(ActorManager gameManager)
        {
            this._gameManager = gameManager;
        }

        public async void Login(string userId)
        {
            RequestNewUser request = new RequestNewUser()
            {
                UserId = userId,
                ConnectedId = Context.ConnectionId
            };
            var user = await _gameManager.SessonRequest<User>(request);
        }

        public async void Betting(object[] obj)
        {
            await Clients.Caller.SendAsync("bettingRespond");
        }

        public async void Send2(CommandType commandType, string msg)
        {
            Console.WriteLine("send string msg");
        }

        public async void Send(object[] obj)
        {
            Console.WriteLine("send object[] obj");

            string name = obj[0].ToString();
            string message = obj[1].ToString();
            await Clients.All.SendAsync("broadcastMessage", name, message);
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
