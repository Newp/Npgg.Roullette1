using System;
using System.Collections.Generic;

namespace Roulette1
{
    
    public static class Number
    {
        public static readonly int N0 = 100;
        public static readonly int N00 = 10000;
        public static readonly int InFieldMin = 1;
        public static readonly int InFieldMax = 36;
        public static readonly int StreetCount = 12;

        public static readonly Column[] AllColumns = new Column[] { Column.C1, Column.C2, Column.C3 };

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

        public static Column GetColumn(int num)
        {
            if(IsInFieldNumber(num))
            {
                int remain = num % 3;
                switch (remain)
                {
                    case 1: return Column.C1;
                    case 2: return Column.C2;
                    default: return Column.C3; //3의 배수
                }
            }

            return Column.OutOfColumn;
        }

        static int[] EmptyNumbers = new int[0];

        public static int[] GetFactor(Column column)
        {
            if (column.IsAmoicColumn() == false)
                return EmptyNumbers;

            int[] result = new int[StreetCount];
            for (int i = 0; i < StreetCount; i++)
            {
                result[i] = (int)column + (3 * i);
            }
            return result;
        }

        public static OddEven GetOddEven(int num) => num % 2 == 0 ? OddEven.Even : OddEven.Odd;

        public static Street GetStreet(int num)
        {
            if(IsInFieldNumber(num))
            {
                int remain = (int)Math.Floor((num - 1d) / 3);
                return (Street)remain + 1;
            }
            
            return Street.OutOfStreet;
        }

        public static int[] GetFactor(Street row)
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
    }
    
    public enum OddEven
    {
        None,
        Odd,
        Even,
    }

    public enum Street
    {
        None = 0,
        S1,
        S4,
        S7,
        S10,
        S13,
        S16,
        S19,
        S22,
        S25,
        S28,
        S31,
        S34,
        OutOfStreet,
    }
}
