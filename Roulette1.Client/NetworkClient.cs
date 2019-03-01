using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Roulette1.Client
{
    class MonitorNetworkClient : NetworkClient
    {
        public GameState gameState { get; private set; }
        public MonitorNetworkClient(string url) : base(url)
        {

        }
        public override void OnGameState(GameState gameState)
        {
            this.gameState = gameState;
            Console.WriteLine("game state => total betting: {0}, game start will {1}ms", gameState.TotalBetting, gameState.LeftMillisec);
            base.OnGameState(gameState);
        }
        //public override void OnMoneyChanged(MoneyChanged mc)
        //{
        //    int before = this.User.Money;
        //    base.OnMoneyChanged(mc);
        //    int after = this.User.Money;
        //    Console.WriteLine("money changed=>{0}, amount:{1},{2},{3},{4}", mc.Why, mc.Amount,before, after, mc.Result);
        //}
    }

    class NetworkClient
    {
        public int Frame { get; set; }
        HubConnection _connection;
        public bool Connected => _connection.State == HubConnectionState.Connected;
        public NetworkClient(string url)
        {
            this._connection = new HubConnectionBuilder().WithUrl(url).Build();
            _connection.On<User>("OnLogin", OnLogin);
            _connection.On<MoneyChanged>("OnMoneyChanged", OnMoneyChanged);
            _connection.On<GameState>("OnGameState", OnGameState);

            _connection.Closed += _connection_Closed;
        }

        private Task _connection_Closed(Exception arg)
        {
            Console.WriteLine("disconnected : {0}", this.User.UserId);
            return Task.FromResult(0);
        }

        public void Betting(string bettingType, int amount)
        {
            _connection.SendAsync("Betting", bettingType, amount);
            Frame++;
        }

        public bool Connect()
        {
            try
            {
                _connection.StartAsync().Wait();
                Frame++;
                Login();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Login()
        {
            _connection.SendAsync("Login", "dummy");
            Frame++;
        }

        public virtual void OnMoneyChanged(MoneyChanged mc)
        {  
            if (this.User.Money + mc.Amount != mc.Result)
            {
            }
            this.User.Money += mc.Amount;
            
            Frame++;
        }

        public void OnRespond(string action)
        {
            Console.WriteLine("server push : {0}", action);
            Frame++;
        }
        //public void OnBetting(BettingInfo betting)
        //{
        //    if (betting.UserId != this.User.UserId)
        //    {
        //    }
        //    this.User.CurrentBetting.Add(betting);
        //    Frame++;
        //}
        public virtual void OnGameState(GameState gameState)
        {
            Frame++;
        }

        public User User { get; private set; }
        public void OnLogin(User user)
        {
            this.User = user;
            Frame++;
        }
    }
}
