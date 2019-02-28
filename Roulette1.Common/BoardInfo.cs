using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Roulette1
{
    public class BoardInfo
    {
        public DateTime ExecuteTime { get; set; }
        public ulong BettedAmount { get; set; }
    }

    public class BoardUpdateInfo
    {
        public int PlayerCount { get; set; }
        public DateTime ExecuteTime { get; set; }
        public ulong BettedAmount { get; set; }
    }
}
