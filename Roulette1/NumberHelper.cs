using System;
using System.Collections.Generic;

namespace Roulette1
{
    public static class NumberHelper
    {
        public static IEnumerable<int> GetAllNumbers()
        {
            yield return 100;
            yield return 10000;
            for(int i =1;i<37;i++)
            {
                yield return i;
            }
            yield break;
        }

        public static bool IsAtomicNumber(int value)
        {
            if (value == 100 || value == 10000)
                return true;

            if (0 < value && value < 37)
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

        public static Row GetRow(int value)
        {
            if (IsAtomicNumber(value) == false)
                return Row.InvalidRow;

            if (IsOutFieldNumber(value))
                return Row.OutOfRow;

            int remain = (int)Math.Floor((value-1d) / 3);
            return (Row)remain + 1;
        }

        public static bool IsOutFieldNumber(int value)
        {
            return Is0(value) || Is00(value);
        }

        public static bool Is0(int value) => value == 100;
        public static bool Is00(int value) => value == 10000;
        //public static bool IsInsideNumber(int value)
        //{
        //    if (0 < value && value < 37)
        //        return true;
        //    return false;
        //}
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

    public enum Row
    {
        None = 0,
        R1,
        R4,
        R7,
        R10,
        R13,
        R16,
        R19,
        R22,
        R25,
        R28,
        R31,
        R34,
        OutOfRow,
        InvalidRow = 4444
    }
}
