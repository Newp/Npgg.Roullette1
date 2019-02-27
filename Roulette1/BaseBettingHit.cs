using System;
using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{
    public enum BettingType
    {
        Straight,
        Split,
        Street,
        Square,
    }

    public abstract class BettingHit
    {
        public bool Validated { get; private set; }
        
        public abstract BettingType BettingType { get; }
        
        public abstract bool IsHit(int number);
        protected abstract void CheckValidate();

        protected void Throw(int num, string msg = null)
        {
            throw new InvalidHitInfoException(this.BettingType, num, msg);
        }
    }

    public class StraightHit : BettingHit
    {
        public readonly int HitNumber = -1;

        public override BettingType BettingType => BettingType.Straight;

        public StraightHit(int num)
        {
            this.HitNumber = num;
            this.CheckValidate();
        }

        public override bool IsHit(int number) => this.HitNumber == number;

        public static List<BettingHit> Gen()
        {
            List<BettingHit> result = new List<BettingHit>();

            foreach (int num in NumberHelper.GetAllNumbers())
            {
                var hit = new StraightHit(num);
                
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

    public class SplitHit : BettingHit
    {
        public readonly int HitNumber1;
        public readonly int HitNumber2;
        public readonly bool IsVertical;

        public override BettingType BettingType => BettingType.Split;

        public SplitHit(int num, bool isVertical)
        {
            if(num == 100)
            {
                HitNumber1 = 100;
                HitNumber2 = 10000;
                return;
            }

            IsVertical = isVertical;

            this.HitNumber1 = num;
            this.HitNumber2 = IsVertical ? num + 3 : num + 1;

            this.CheckValidate();
        }

        protected override void CheckValidate()
        {
            int small = Math.Min(this.HitNumber1, this.HitNumber2);
            int big = Math.Max(this.HitNumber1, this.HitNumber2);


            if (IsVertical)
            {
                Row row = NumberHelper.GetRow(small);
                if (DeniedRows.Contains(row))
                    Throw(small, "허용되지 않는 vertial split 의 row");
            }
            else
            {
                Column col = NumberHelper.GetColumn(small);
                if(AllowedColumns.Contains(col) == false)
                {
                    Throw(small, "허용되지 않는 horizonal split 의 column");
                }
            }

            int diff = big - small;

            if (diff != 1 && diff != 3 && diff != 100) //100은 0+00 split
            {
                this.Throw(small, "인접하지 않은 숫자");
            }

            if (NumberHelper.IsAtomicNumber(big) == false
                || NumberHelper.IsAtomicNumber(small) == false)
                this.Throw(small, "허용되지 않은 숫자");
        }


        public override bool IsHit(int number) => this.HitNumber1 == number || this.HitNumber2 == number;

        public static List<Column> AllowedColumns = new List<Column>() { Column.C1, Column.C2 };
        public static List<Row> DeniedRows = new List<Row>() { Row.None, Row.InvalidRow, Row.OutOfRow, Row.R34 };

        public static List<BettingHit> Gen()
        {
            List<BettingHit> result = new List<BettingHit>();
            
            foreach (int num in NumberHelper.GetAllNumbers())
            {
                Column col = NumberHelper.GetColumn(num);
                Row row = NumberHelper.GetRow(num);

                if (NumberHelper.Is0(num) || AllowedColumns.Contains(col))
                {
                    var horizontalHit = new SplitHit(num, false);
                    result.Add(horizontalHit);
                }
                if (DeniedRows.Contains(row) == false)
                {
                    var verticalHit = new SplitHit(num, true);
                    result.Add(verticalHit);
                }
                
            }

            return result;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} ( {this.HitNumber1}, {this.HitNumber2} )";
        }
    }


}
