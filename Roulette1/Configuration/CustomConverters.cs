using Npgg.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Npgg
{
    public class RangeConverter : CustomConverter<Range>
    {
        char _spliter = '~';
        public override Range Convert(string value)
        {
            int spliterIndex = value.IndexOf(_spliter);

            Range result = new Range();

            if (spliterIndex > 0)
            {
                string minString = value.Substring(0, spliterIndex);
                string maxString = value.Substring(spliterIndex + 1);

                result.Min = int.Parse(minString);
                result.Max = int.Parse(maxString);
            }
            else
            {
                int minmax = int.Parse(value);

                result.Min = minmax;
                result.Max = minmax;
            }

            return result;
        }
    }

    public class FloatConverter : CustomConverter<float>
    {
        public override float Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0f;

            return float.Parse(value);
        }
    }
    
    
    
}
