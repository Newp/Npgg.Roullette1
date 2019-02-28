using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class ColumnHitChecker : SmallChoiceNumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.Column;

        public static List<HitChecker> Gen() => Gen<ColumnHitChecker>(Allowed);
        public override int[] AllowedChoiceNumber => Allowed;

        public static readonly int[] Allowed = new int[] { 1, 2, 3 };

        public ColumnHitChecker(int num)
        {
            var column = Number.GetColumn(num);
            this.AddHitNumber(column.GetFactor());
            this.CheckValidate();
        }

        
    }
}
