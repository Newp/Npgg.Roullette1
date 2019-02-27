using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgg
{
    public class VaryDictionary<KEY> : Dictionary<KEY, int>
    {
        public int Get(KEY key)
        {
            return this.ContainsKey(key) ? this[key] : 0;
        }

        public int Increase(KEY key, int value) => this.calc(key, (int)value);
        public int Decrease(KEY key, int value) => this.calc(key, (int)value);


        int calc(KEY key, int value)
        {
            int before = 0;
            if(this.ContainsKey(key) == false)
            {
                this.Add(key, value);
            }
            else
            {
                before = this[key];
                this[key] += value;
            }
            return this[key];
        }
    }
}
