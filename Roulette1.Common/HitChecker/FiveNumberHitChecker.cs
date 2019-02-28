using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class FiveNumberHitChecker : NumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.FiveNumber;
        public override int Odds => 6;

        public FiveNumberHitChecker()
        {
            this.AddHitNumber(new int[] { Number.N0, Number.N00, 1,2,3 });//고정픽
            this.CheckValidate();
        }

        public static List<HitChecker> Gen() => new List<HitChecker>() { new FiveNumberHitChecker() };

        //외부로부터 어떠한 인자도 받지 않기 때문에 유효성 검사가 필요없다.
        protected override void CheckValidate() { }
    }
}
