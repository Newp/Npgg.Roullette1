using System.Collections.Generic;

namespace Npgg
{
    public class Range
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public Range() { }

        public Range(int size) : this(size, size) { }


        public Range(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }

        public bool Contains(int value)
        {
            return this.Min <= value && value <= Max;
        }

        public override string ToString()
        {
            return $"{this.Min}~{this.Max}";
        }

        public IEnumerable<int> GetNumbers()
        {
            if (Min == Max)
            {
                yield return this.Min;
                yield break;
            }

            for(int i =Min; i<= Max;i++)
            {
                yield return i;
                
            }
            yield break;
        }
    }
}
