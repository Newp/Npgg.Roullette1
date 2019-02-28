using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class SixNumberHitChecker : SmallChoiceNumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.SixNumber;

        public static List<HitChecker> Gen() => Gen<SixNumberHitChecker>(Allowed);
        public override int[] AllowedChoiceNumber => Allowed;

        //모든 마지막 Street를 제외한 C1요소를 가져온다.
        public static readonly int[] Allowed = Column.C1.GetFactor().Take(Number.StreetCount - 1).ToArray();
        
       

        public SixNumberHitChecker(int num)
        {
            this.AddHitNumber(Enumerable.Range(num, 6).ToArray());
            this.CheckValidate();
        }

        
    }
}
