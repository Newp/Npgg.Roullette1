using System.Collections.Generic;

namespace Roulette1
{
    public class StraightHitChecker : HitChecker
    {
        public readonly int HitNumber = -1;

        public override BettingType BettingType => BettingType.Straight;

        public StraightHitChecker(int num)
        {
            this.HitNumber = num;
            this.CheckValidate();
        }

        public override bool IsHit(int number) => this.HitNumber == number;

        public static List<HitChecker> Gen()
        {
            List<HitChecker> result = new List<HitChecker>();

            foreach (int num in NumberHelper.GetAllNumbers())
            {
                var hit = new StraightHitChecker(num);
                
                result.Add(hit);
            }

            return result;
        }

        protected override void CheckValidate()
        {
            if (NumberHelper.IsAtomicNumber(HitNumber) == false)
            {
                this.Throw(this.HitNumber);
            }
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} ( {this.HitNumber} )";
        }
    }


}
