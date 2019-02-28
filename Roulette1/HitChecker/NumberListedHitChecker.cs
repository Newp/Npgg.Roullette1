using System;
using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{
    public abstract class NumberListedHitChecker : HitChecker
    {
        public readonly List<int> HitNumbers = new List<int>();


        

        public override bool IsHit(int number)=>this.HitNumbers.Contains(number);

        public override abstract BettingType BettingType { get; }

        protected override abstract void CheckValidate();

        protected void AddHitNumber(params int[] hits) => this.HitNumbers.AddRange(hits);

        public override string ToString()
        {
            return $"{this.GetType().Name} ( {string.Join(", ", this.HitNumbers)} )";
        }
    }

    public abstract class SmallChoiceNumberListedHitChecker : NumberListedHitChecker
    {
        public abstract int[] AllowedChoiceNumber { get; }
        protected override void CheckValidate()
        {
            int min = HitNumbers.Min();
            if( this.AllowedChoiceNumber.Contains(min) == false)
            {
                Throw(min, "choice is not allowed");
            }
        }

        public static List<HitChecker> Gen<T>(int [] AllowedNumber) where T : HitChecker
        {
            List<HitChecker> result = new List<HitChecker>();
            foreach(int num in AllowedNumber)
            {
                var created = Activator.CreateInstance(typeof(T), new object[] { num });
                result.Add((T)created);
            }
            return result;
        }
    }
}
