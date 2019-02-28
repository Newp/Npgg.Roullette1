using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette1
{
    public static class Extensions
    {
        public static bool IsAmoicRow(this Street row)
        {
            if (row == Street.None || row == Street.OutOfStreet)
                return false;
            return true;
        }

        public static bool IsAmoicColumn(this Column column)
        {
            if (column == Column.None || column == Column.OutOfColumn)
                return false;
            return true;
        }


        public static EvenOdd GetEvenOdd(this int num) => num % 2 == 0 ? EvenOdd.Even : EvenOdd.Odd;

        public static NumberColor GetColor(this int num)
        {
            if (Number.RedNumbers.Contains(num))
                return NumberColor.Red;

            if (Number.BlackNumbers.Contains(num))
                return NumberColor.Black;

            return NumberColor.None;
        }

        //public static bool IsInFieldNumber(this Row row)
        //{
        //    if (row == Row.None || row == Row.InvalidRow)
        //        return false;
        //    return true;
        //}
    }
}
