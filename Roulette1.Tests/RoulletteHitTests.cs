using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette1.Tests
{
    [TestFixture]
    class RoulletteHitTests
    {
        int[] allnum = NumberHelper.GetAllNumbers().ToArray();

        [Test]
        public void StraightHitTest()
        {
            var list = StraightHit.Gen();//.ToDictionary(key=>key.Key.Numbers.First());

            foreach (var hit in list.Cast<StraightHit>())
            {
                foreach (var num in allnum)
                {
                    if (num == hit.HitNumber)
                        Assert.IsTrue(hit.IsHit(num));
                    else
                        Assert.IsFalse(hit.IsHit(num));
                }
            }
        }

        [Test]
        public void GetRowTest()
        {
            var numbers = NumberHelper.GetAllNumbers().ToList();

            Dictionary<int, Row> rows = new Dictionary<int, Row>();

            foreach (var num in allnum)
            {
                rows.Add(num, NumberHelper.GetRow(num));
            }

            foreach (var rowGroup in rows.GroupBy(kvp => kvp.Value))
            {
                switch (rowGroup.Key)
                {
                    case Row.None:
                    case Row.InvalidRow:
                        throw new Exception("invalid row type=>" + rowGroup.Key.ToString());
                    case Row.OutOfRow:
                        Assert.AreEqual(2, rowGroup.Count());
                        break;
                    default:
                        Assert.AreEqual(3, rowGroup.Count());
                        break;
                }
            }
        }


        [Test]
        public void GetColumnTest()
        {
            var numbers = NumberHelper.GetAllNumbers().ToList();

            Dictionary<int, Column> cols = new Dictionary<int, Column>();

            foreach (var num in allnum)
            {
                cols.Add(num, NumberHelper.GetColumn(num));
            }

            foreach (var colGroup in cols.GroupBy(kvp => kvp.Value))
            {
                switch (colGroup.Key)
                {
                    case Column.None:
                    case Column.InvalidColumn:
                        throw new Exception("invalid column type=>" + colGroup.Key.ToString());
                    case Column.OutOfColumn:
                        Assert.AreEqual(2, colGroup.Count());
                        break;
                    default:
                        Assert.AreEqual(12, colGroup.Count());
                        break;
                }
            }
        }

        [Test]
        public void SplitHitTest()
        {
            var list = SplitHit.Gen();//.ToDictionary(key=>key.Key.Numbers.First());

            Assert.AreEqual(list.Count, 58);
            foreach (var hit in list.Cast<SplitHit>())
            {
                foreach(var num in allnum)
                {
                    if (num == hit.HitNumber1 || num == hit.HitNumber2)
                        Assert.IsTrue(hit.IsHit(num));
                    else
                        Assert.IsFalse(hit.IsHit(num));
                }
            }
        }
    }
}
