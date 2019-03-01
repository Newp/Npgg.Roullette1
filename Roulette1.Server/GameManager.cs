using Microsoft.AspNetCore.SignalR;
using Proto;
using Proto.Mailbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette1.Server
{
    public class GameManager : IActor
    {
        Dictionary<string, User> _users = new Dictionary<string, User>();
        IHubContext<RouletteHub> _hub;
        Dictionary<string, BettingChecker> _hitChecker = new Dictionary<string, BettingChecker>();

        RandomBox<int> randomBox = new RandomBox<int>();
        public GameManager(IHubContext<RouletteHub> hub)
        {
            this._hub = hub;
            foreach (var hc in HitChecker.MakeHitChecker())
            {
                _hitChecker.Add(hc.ToString(), new BettingChecker()
                {
                    Betting =new List<BettingInfo>()
                    ,HitChecker = hc
                });
            }

            foreach(var num in Number.GetAllNumbers())
                randomBox.Add(100, num);

            gameWatch.Start();
            broadcastWatch.Start();
        }

        Stopwatch gameWatch = new Stopwatch();
        Stopwatch broadcastWatch = new Stopwatch();
        async void GameUpdate()
        {
            if(broadcastWatch.ElapsedMilliseconds > 100)
            {
                GameState bs = new GameState();
                bs.LeftMillisec = 10000 - (int)gameWatch.ElapsedMilliseconds;
                bs.TotalBetting = this.totalBetting;

                _hub.Clients.All.SendAsync("OnGameState", bs);

                broadcastWatch.Restart();
            }

            if (gameWatch.ElapsedMilliseconds < 10000)
            {
                return;
            }
            gameWatch.Restart();

            int pickNumber = randomBox.Pick(); //룰렛이 결정된다.

            var hitList = _hitChecker.Where(kvp => kvp.Value.HitChecker.IsHit(pickNumber));

            foreach(var kvp in hitList)
            {
                int odds = kvp.Value.HitChecker.Odds + 1;
                foreach (var betting in kvp.Value.Betting)
                {
                    MoneyChange(betting.UserId, "WIN : " + kvp.Key, betting.Amount * odds);
                }
            }

            foreach(var hit in _hitChecker)
            {
                hit.Value.Betting.Clear();
            }

            ulong totalMoney = 0;
            foreach(var user in _users.Values)
            {
                totalMoney += (ulong)user.Money;
            }

            string msg = string.Format("game finished => elapsed : {0}ms, total money : {1}", gameWatch.ElapsedMilliseconds, totalMoney);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(msg);
            Console.ResetColor();
            totalBetting = 0;
            gameWatch.Restart();
        }

        ulong totalBetting;
        int frame = 0;

        public void MoneyChange(string userId, string why, int amount)
        {
            var user = this._users[userId];
            user.Money += amount;
            MoneyChanged mc = new MoneyChanged()
            {
                Amount = amount,
                Why = why,
                Result = user.Money
            };

            _hub.Clients.Client(user.UserId).SendAsync("OnMoneyChanged", mc);
        }

        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;

            if (msg is Update)
            {
                frame++;
                GameUpdate();
                context.Respond(0);
            }
            else if (msg is GetFrame)
            {
                context.Respond(++frame);
                frame = 0;
            }
            else if (msg is BettingInfo betting)
            {
                frame++;
                if (_hitChecker.TryGetValue(betting.BettingType, out var checker) == false)
                {
                    context.Respond(ApiResult.InvalidBetting);
                }
                
                checker.Betting.Add(betting);
                totalBetting += (ulong)betting.Amount;

                this.MoneyChange(betting.UserId, "betting:" + betting.BettingType, -betting.Amount);

                context.Respond(ApiResult.Success);
            }
            else if (msg is RequestNewUser newUser)
            {
                frame++;
                if (_users.TryGetValue(newUser.UserId, out var user) == false)
                {
                    user = new User()
                    {
                        UserId = newUser.UserId,
                        Money = 10000,
                    };
                    _users.Add(user.UserId, user);
                }
                _hub.Clients.Client(newUser.UserId).SendAsync("OnLogin", user);

                context.Respond(user);
            }
            else if (msg is SystemMessage) { }
            else
            {

            }

            return Actor.Done;
        }
        
        
    }

    public class RequestNewUser
    {
        public string UserId { get; set; }
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

    public class BettingChecker
    {
        public HitChecker HitChecker { get; set; }
        public List<BettingInfo> Betting { get; set; }
    }

}
