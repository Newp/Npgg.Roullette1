using System;
using System.Linq;
using System.Collections.Generic;
using Npgg;
using System.Numerics;

namespace Roulette1
{
    public class RouletteBoard
    {   
        Dictionary<string, HitChecker> _hitChecker = new Dictionary<string, HitChecker>();
        public RouletteBoard()
        {
            this._hitChecker = HitChecker.MakeHitChecker().ToDictionary(v => v.ToString());
        }

        public ApiResult Betting(User user, string key, int amount)
        {
            if(this._hitChecker.TryGetValue(key, out var hitChecker)==false)
            {
                return ApiResult.InvalidBetting;
            }

            return ApiResult.Success;
        }
    }


    public class RouletteLogic
    {
        
    }
    




}
