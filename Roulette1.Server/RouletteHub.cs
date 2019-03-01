using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Roulette1.Server
{
    public partial class RouletteHub : Hub
    {
        public async void Login(string notused)
        {
            RequestNewUser request = new RequestNewUser()
            {
                UserId = Context.ConnectionId,
            };
            var user = await _gameManager.SessonRequest<User>(request);
            
            //await Clients.Caller.SendAsync("OnLoggon", null);
        }


        public async void Betting(string bettingType, int amount)
        {
            BettingInfo req = new BettingInfo()
            {
                UserId = this.UserId,
                BettingType = bettingType,
                Amount = amount,
            };

            var result = await _gameManager.Betting(req);
        }
        





    }

    
}



//public async void Send2(CommandType commandType, string msg)
//{
//    Console.WriteLine("send string msg");
//}

//public async void Send(object[] obj)
//{
//    Console.WriteLine("send object[] obj");

//    string name = obj[0].ToString();
//    string message = obj[1].ToString();
//    await Clients.All.SendAsync("broadcastMessage", name, message);
//}