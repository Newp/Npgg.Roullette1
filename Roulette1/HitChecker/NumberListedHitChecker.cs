using System.Collections.Generic;

namespace Roulette1
{
    public abstract class NumberListedHitChecker : HitChecker
    {
        public readonly List<int> HitNumbers = new List<int>();

        public override bool IsHit(int number)=>this.HitNumbers.Contains(number);

        public override abstract BettingType BettingType { get; }

        protected override abstract void CheckValidate();

        protected void AddHitNumber(int[] hits) => this.HitNumbers.AddRange(hits);

        public override string ToString()
        {
            return $"{this.GetType().Name} ( {string.Join(", ", this.HitNumbers)} )";
        }
    }
}
