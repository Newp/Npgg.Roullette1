using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class CourtesyLineHitChecker : NumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.CourtesyLine;

        public CourtesyLineHitChecker()
        {
            this.AddHitNumber(new int[] { Number.N0, Number.N00});//고정픽
            this.AddHitNumber(Enumerable.Range(13, Number.InFieldMax - 12).ToArray());
            this.CheckValidate();
        }

        public static List<HitChecker> Gen() => new List<HitChecker>() { new CourtesyLineHitChecker() };

        //외부로부터 어떠한 인자도 받지 않기 때문에 유효성 검사가 필요없다.
        protected override void CheckValidate() { }
    }
}
