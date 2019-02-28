using System;
using System.Collections.Generic;
using Npgg;

namespace Roulette1
{
    public class RouletteGame
    {
    }

    public class RouletteLogic
    {
        private RouletteTable _rtable = new RouletteTable();

        public RouletteLogic()
        {
            
        }

        
        
        VaryDictionary<string> _betting = new VaryDictionary<string>();
        

        public void Betting(string id, int amounts)
        {
            _betting.Increase(id, amounts);
        }
    }

    public class BettingKey
    {
        public BettingType BettingType { get; set; }
        public int[] Numbers { get; set; }
    }

    public class RouletteTable : RandomBox<string>
    {

    }




}
