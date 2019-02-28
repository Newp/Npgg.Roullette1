using System.Linq;

namespace Roulette1
{
    public abstract class HitChecker
    {
        public bool Validated { get; private set; }

        public abstract BettingType BettingType { get; }
        public abstract int Odds { get; }

        public abstract bool IsHit(int number);
        protected abstract void CheckValidate();

        protected void Throw(int num, string msg = null)
        {
            throw new InvalidHitInfoException(this.BettingType, num, msg);
        }

        
    }


}
