using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class HighLowHitChecker : SmallChoiceNumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.HighLow;

        public static List<HitChecker> Gen() => Gen<HighLowHitChecker>(Allowed);
        public override int[] AllowedChoiceNumber => Allowed;

        public static readonly int[] Allowed = new int[] { 1, 19 };

        public HighLowHitChecker(int num)
        {
            this.AddHitNumber(Enumerable.Range(num, 18).ToArray());
            this.CheckValidate();
        }

        
    }
}
