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
        public NetworkClient(string url)
        {
            this._connection = new HubConnectionBuilder().WithUrl(url).Build();
            _connection.On<string>("broadcastMessage", OnRespond);

            _connection.Closed += _connection_Closed;
        }

        private Task _connection_Closed(Exception arg)
        {
            Console.WriteLine("disconnected");
            return Task.FromResult(0);
        }

        public void Send(string msg)
        {
            _connection.SendAsync("Send", msg);
            _connection.SendAsync("Send2", msg);
        }

        public bool Connect(string id)
        {
            try
            {
                _connection.StartAsync().Wait();

                _connection.SendAsync("Login", id);
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

        User user = null;
        public void OnLogon(User user)
        {
            this.user = user;
        }
    }
}
