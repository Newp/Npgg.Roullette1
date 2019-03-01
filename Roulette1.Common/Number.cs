using System;
using System.Collections.Generic;
using System.Linq;

namespace Roulette1
{
    
    public static class Number
    {
        public static readonly int N0 = 100;
        public static readonly int N00 = 10000;
        public static readonly int InFieldMin = 1;
        public static readonly int InFieldMax = 36;
        public static readonly int StreetCount = 12;
        public static int[] InFieldNumbers = Enumerable.Range(InFieldMin, InFieldMax).ToArray();

        public static readonly int[] RedNumbers = new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        public static readonly int[] BlackNumbers = new int[] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

        public static readonly Column[] AllColumns = new Column[] { Column.C1, Column.C2, Column.C3 };
        public static readonly Street[] AllStreets =
            Enum.GetValues(typeof(Street)).Cast<Street>()
            .Where(street => street != Street.None && street != Street.OutOfStreet)
            .OrderBy(street=>(int)street).ToArray();


        public static readonly Dictionary<Column, int[]> ColumnFactors = new Func<Dictionary<Column, int[]>>(() =>
        {
            Dictionary<Column, int[]> result = new Dictionary<Column, int[]>();
            foreach (var column in AllColumns)
                result.Add(column, InFieldNumbers.Where(num => GetColumn(num) == column).ToArray());
            return result;
        })();

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

        public static int[] GetFactor(EvenOdd evenOdd)
        {
            if (evenOdd == EvenOdd.None)
                return EmptyNumbers;

            return InFieldNumbers.Where(num => num.GetEvenOdd() == evenOdd).ToArray();
        }

        public static int[] GetFactor(NumberColor color)
        {
            switch (color)
            {
                case NumberColor.Red: return RedNumbers;
                case NumberColor.Black: return BlackNumbers;
                default: return EmptyNumbers;
            }
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

    public enum EvenOdd
    {
        None,
        Odd,
        Even,
    }
    public enum NumberColor
    {
        None,
        Red,
        Black,
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
