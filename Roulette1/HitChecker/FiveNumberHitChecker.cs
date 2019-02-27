using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class FiveNumberHitChecker : NumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.Street;

        public FiveNumberHitChecker(int num)
        {
            var street = NumberHelper.GetStreet(num);
            this.AddHitNumber(NumberHelper.GetFactor(street));
            this.CheckValidate();
        }

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

    }
}
