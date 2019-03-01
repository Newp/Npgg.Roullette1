using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Roulette1.Client
{
    class NetworkClient
    {
        HubConnection _connection;
        public bool Connected => _connection.State == HubConnectionState.Connected;
        public NetworkClient(string url)
        {
            this._connection = new HubConnectionBuilder().WithUrl(url).Build();
            _connection.On<User>("OnLogin", OnLogin);
            _connection.On<BettingInfo>("OnBetting", OnBetting);

            _connection.On<string>("broadcastMessage", OnRespond);
            _connection.Closed += _connection_Closed;
        }

        private Task _connection_Closed(Exception arg)
        {
            Console.WriteLine("disconnected");
            return Task.FromResult(0);
        }

        public void Betting(string bettingType, int amount)
        {
            _connection.SendAsync("Betting", bettingType, amount);
        }

        public void Send(string msg)
        {
            _connection.SendAsync("Send", msg);
            _connection.SendAsync("Send2", msg);
        }

        public bool Connect()
        {
            try
            {
                _connection.StartAsync().Wait();
                _connection.SendAsync("Login", "dummy");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void OnRespond(string action)
        {
            Console.WriteLine("server push : {0}", action);
        }
        public void OnBetting(BettingInfo betting)
        {
            if(betting.UserId != this.user.UserId)
            {
            }
            this.user.CurrentBetting.Add(betting);
        }

       User user = null;
        public void OnLogin(User user)
        {
            this.user = user;
            Console.WriteLine("server push : logon");
        }
    }
}
