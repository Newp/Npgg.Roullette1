using System;

namespace Roulette1
{
    public class InvalidHitInfoException : Exception
    {
        public readonly BettingType BettingType;
        public readonly int Number;
        public readonly string DetailMessage;

        public InvalidHitInfoException(BettingType bettingType, int number, string message)
            : base($"{bettingType}, {number} => {message}")
        {
            this.BettingType = bettingType;
            this.Number = number;
            this.DetailMessage = message;
        }
    }


}
