using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{
    public class SquareHitChecker : NumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.Square;
        public override int Odds => 8;

        public SquareHitChecker(int num)
        {
            this.AddHitNumber(new int[]
            {
                num,
                num+1,
                num+3,
                num+4,
            });
            this.CheckValidate();
        }

        public static List<Column> AllowedColumns = new List<Column>() { Column.C1, Column.C2 };
        public static List<Street> DeniedStreets = new List<Street>() { Street.None, Street.OutOfStreet, Street.S34 };

        public static List<HitChecker> Gen()
        {
            List<HitChecker> result = new List<HitChecker>();
            foreach(int num in Number.InFieldNumbers)
            {
                var column = Number.GetColumn(num);
                var street = num.GetStreet();

                if(AllowedColumns.Contains(column) == false || DeniedStreets.Contains(street))
                    continue; //조건에 허용되지 않음.

                var hit = new SquareHitChecker(num);
                result.Add(hit);
            }

            return result;
        }

        protected override void CheckValidate()
        {
            int min = HitNumbers.Min();
            var col = Number.GetColumn(min);

            if (col != Column.C1 && col != Column.C2)
                Throw(min, "square hit can only C1, C2");
        }
    }
}
