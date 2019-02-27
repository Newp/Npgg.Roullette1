using System;
using System.Collections.Generic;

namespace Roulette1
{
    public static class NumberHelper
    {
        public static readonly int N0 = 100;
        public static readonly int N00 = 10000;
        public static readonly int InFieldMin = 1;
        public static readonly int InFieldMax = 36;
        public static readonly int RowCount = 12;

        public static IEnumerable<int> GetAllNumbers()
        {
            yield return N0;
            yield return N00;
            for(int i = InFieldMin; i<= InFieldMax; i++)
            {
                yield return i;
            }
            yield break;
        }
        public static IEnumerable<int> GetInFieldNumbers()
        {
            for (int i = InFieldMin; i <= InFieldMax; i++)
            {
                yield return i;
            }
            yield break;
        }

        public static bool IsAtomicNumber(int value)
        {
            if (IsOutFieldNumber(value))
                return true;

            if(IsInFieldNumber(value))
                return true;

            return false;
        }

        public static Column GetColumn(int value)
        {
            if (IsAtomicNumber(value) == false)
                return Column.InvalidColumn;

            if (IsOutFieldNumber(value))
                return Column.OutOfColumn;

            int remain = value % 3;
            switch (remain)
            {
                case 1: return Column.C1;
                case 2: return Column.C2;
                default : return Column.C3; //3의 배수
            }
        }

        static int[] EmptyNumbers = new int[0];

        public static int[] GetColumnFactor(Column column)
        {
            if (column.IsAmoicColumn() == false)
                return EmptyNumbers;

            int[] result = new int[RowCount];
            for (int i = 0; i < RowCount; i++)
            {
                result[i] = (int)column + (3 * i);
            }
            return result;
        }

        public static Street GetStreet(int value)
        {
            if (IsAtomicNumber(value) == false)
                return Street.InvalidStreet;

            if (IsOutFieldNumber(value))
                return Street.OutOfStreet;

            int remain = (int)Math.Floor((value-1d) / 3);
            return (Street)remain + 1;
        }

        public static int[] GetStreetFactor(Street row)
        {
            if (row.IsAmoicRow() == false)
                return EmptyNumbers;

            int[] result = new int[3];
            int start = ((int)row * 3) - 2;
            for(int i =0;i<result.Length;i++)
            {
                result[i] = start + i;
            }
            return result;
        }

        public static bool IsInFieldNumber(int value)=> InFieldMin <= value && value <= InFieldMax;
        public static bool IsOutFieldNumber(int value)=>Is0(value) || Is00(value);

        public static bool Is0(int value) => value == N0;
        public static bool Is00(int value) => value == N00;
    }

    public enum Column
    {
        None = 0,
        C1,
        C2,
        C3,
        OutOfColumn,
        InvalidColumn = 4444
    }

    public enum Street
    {
        None = 0,
        Street1,
        Street4,
        Street7,
        Street10,
        Street13,
        Street16,
        Street19,
        Street22,
        Street25,
        Street28,
        Street31,
        Street34,
        OutOfStreet,
        InvalidStreet = 4444
    }
}
