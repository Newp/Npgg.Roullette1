using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class EvenOddHitChecker : SmallChoiceNumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.EvenOdd;

        public static List<HitChecker> Gen() => Gen<EvenOddHitChecker>(Allowed);
        public override int[] AllowedChoiceNumber => Allowed;

        public static readonly int[] Allowed = new int[] { 2, 1 };//Even이 먼저, Odd가 뒤

        public EvenOddHitChecker(int num)
        {
            for(int i =0;i<= Number.InFieldMax / 2; i++)
            {
                this.AddHitNumber(num + (i * 2));
            }
            this.CheckValidate();
        }

        
    }
}
