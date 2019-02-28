using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class StreetHitChecker : NumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.Street;

        public StreetHitChecker(int num)
        {
            var street = num.GetStreet();
            this.AddHitNumber(street.GetFactor());
            this.CheckValidate();
        }

        public static List<HitChecker> Gen()
        {
            List<HitChecker> result = new List<HitChecker>();
            foreach (int num in Column.C1.GetFactor())
            {
                var hit = new StreetHitChecker(num);
                result.Add(hit);
            }

            return result;
        }

        protected override void CheckValidate()
        {
            int min = HitNumbers.Min();
            var col = Number.GetColumn(min);

            if (col != Column.C1)
                Throw(min, "street hit can only C1");
        }

    }
}
