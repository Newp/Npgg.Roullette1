using System;
using System.Collections.Generic;
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

        //public static bool IsInFieldNumber(this Row row)
        //{
        //    if (row == Row.None || row == Row.InvalidRow)
        //        return false;
        //    return true;
        //}
    }
}
