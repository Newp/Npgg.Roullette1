using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{
    public class StreetHitChecker : HitChecker
    {
        public readonly List<int> HitNumbers;

        public override BettingType BettingType => BettingType.Street;

        public StreetHitChecker(int num)
        {
            var street = NumberHelper.GetStreet(num);
            this.HitNumbers = new List<int>(NumberHelper.GetFactor(street));
            this.CheckValidate();
        }

        public override bool IsHit(int number) => this.HitNumbers.Contains(number);

        public static List<HitChecker> Gen()
        {
            List<HitChecker> result = new List<HitChecker>();
            foreach (int num in NumberHelper.GetFactor(Column.C1))
            {
                var hit = new StreetHitChecker(num);
                result.Add(hit);
            }

            return result;
        }

        protected override void CheckValidate()
        {
            int min = HitNumbers.Min();
            var col = NumberHelper.GetColumn(min);

            if (col != Column.C1)
                Throw(min, "street hit can only C1");
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} ( {string.Join(", ", this.HitNumbers)} )";
        }
    }
}
