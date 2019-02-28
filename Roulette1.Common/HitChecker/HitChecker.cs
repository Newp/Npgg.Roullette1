using System.Linq;
using System.Collections.Generic;
using System;

namespace Roulette1
{
    public abstract class HitChecker
    {
        public bool Validated { get; private set; }

        public abstract BettingType BettingType { get; }

        public abstract int Odds { get; }

        public abstract bool IsHit(int number);
        protected abstract void CheckValidate();

        protected void Throw(int num, string msg = null)
        {
            throw new InvalidHitInfoException(this.BettingType, num, msg);
        }

        public static Type[] GetAllHitCheckerType()
        {
            var checkerType = typeof(HitChecker);
            Type[] allCheckers = checkerType.Assembly.GetTypes().Where(type => type.IsAbstract == false && checkerType.IsAssignableFrom(type)).ToArray();

            return allCheckers;
        }
        public static List<HitChecker> MakeHitChecker()
        {
            List<HitChecker> hitCheckers = new List<HitChecker>();

            foreach (var checker in GetAllHitCheckerType())
            {
                var m = checker.GetMethods().Where(method => method.IsStatic && method.Name == "Gen").First();
                var checkerList = (List<HitChecker>)m.Invoke(null, null);
                hitCheckers.AddRange(checkerList);
            }

            return hitCheckers;
        }

    }


}
