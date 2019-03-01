using System.Collections.Generic;

namespace Roulette1
{
    public class User
    {
        public string UserId { get; set; }
        public int Money { get; set; }

        public List<BettingInfo> CurrentBetting { get; set; }
    }


    public class BettingInfo
    {
        public string UserId { get; set; }
        public string BettingType { get; set; }
        public int Amount { get; set; }
    }
}
