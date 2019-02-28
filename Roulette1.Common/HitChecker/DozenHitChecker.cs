using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class DozenHitChecker : SmallChoiceNumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.Dozen;
        public override int Odds => 2;

        public static List<HitChecker> Gen() => Gen<DozenHitChecker>(Allowed);
        public override int[] AllowedChoiceNumber => Allowed;

        public static readonly int[] Allowed = new int[] { 1, 13, 25 };

        public DozenHitChecker(int num)
        {
            this.AddHitNumber(Enumerable.Range(num, 12).ToArray());
            this.CheckValidate();
        }
        


    }
}
