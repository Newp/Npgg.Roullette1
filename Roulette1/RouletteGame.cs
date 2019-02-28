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

        public ApiResult Betting(UserAsset user, string key, int amount)
        {
            if(this._hitChecker.TryGetValue(key, out var hitChecker)==false)
            {
                return ApiResult.InvalidBetting;
            }

            return ApiResult.Success;
        }
    }

    public enum ApiResult
    {
        None,
        Success,
        InvalidBetting,
        NotEnoughMoney,
    }

    public class UserAsset
    {
        public string UserId { get; set; }
        public int Money { get; set; }
        public int OnBoard { get; set; }
    }

    public class RouletteLogic
    {
        
    }
    




}
