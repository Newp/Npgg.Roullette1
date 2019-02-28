using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Roulette1.Server
{
    public class RouletteHub : Hub
    {
        GameManager _gameManager;
        public RouletteHub(GameManager gameManager)
        {
            Console.WriteLine("game manager initialized");
            this._gameManager = gameManager;
        }

        public async void Betting(object[] obj)
        {
            await Clients.Caller.SendAsync("bettingRespond");
        }
        public async void Send2(string msg)
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
            var msg = new SessionMessage()
            {
                SessionEvent= SessionEvent.OnConnect,
                ConnectionId = Context.ConnectionId
            };

            _gameManager.SessonRequest<int>(msg);
            
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
