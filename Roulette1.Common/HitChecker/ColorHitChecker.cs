using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{

    public class ColorHitChecker : SmallChoiceNumberListedHitChecker
    {
        public override BettingType BettingType => BettingType.Color;
        public override int Odds => 1;

        public static List<HitChecker> Gen() => Gen<ColorHitChecker>(Allowed);
        public override int[] AllowedChoiceNumber => Allowed;


        public static readonly int[] Allowed = new int[] { 1, 2 };

        public ColorHitChecker(int num)
        {
            NumberColor color = num.GetColor();
            this.AddHitNumber(Number.GetFactor(color));
            this.CheckValidate();
        }
    }
}
