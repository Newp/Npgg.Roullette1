using System.Collections.Generic;

namespace Roulette1
{
    public class User
    {
        public string UserId { get; set; }
        public int Money { get; set; }
    }


    public class BettingInfo
    {
        public string UserId { get; set; }
        public string BettingType { get; set; }
        public int Amount { get; set; }
    }



    public struct MoneyChanged
    {
        public string Why { get; set; }
        public int Amount { get; set; }
        public int Result { get; set; }
    }

    public class GameState
    {
        public int LeftMillisec { get; set; }
        public ulong TotalBetting { get; set; }
    }
}
