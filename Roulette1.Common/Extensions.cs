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

        static int[] EmptyNumbers = new int[0];

        public static int[] GetFactor(this Street row)
        {
            if (row.IsAmoicRow() == false)
                return EmptyNumbers;

            int[] result = new int[3];
            int start = ((int)row * 3) - 2;
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = start + i;
            }
            return result;
        }

        public static int[] GetFactor(this Column column)
        {
            if (Number.ColumnFactors.TryGetValue(column, out var result))
            {
                return result;
            }
            return EmptyNumbers;
        }

        public static Street GetStreet(this int num)
        {
            if (Number.IsInFieldNumber(num))
            {
                int remain = (int)Math.Floor((num - 1d) / 3);
                return (Street)remain + 1;
            }
            return Street.OutOfStreet;
        }

        //public static bool IsInFieldNumber(this Row row)
        //{
        //    if (row == Row.None || row == Row.InvalidRow)
        //        return false;
        //    return true;
        //}
    }
}
