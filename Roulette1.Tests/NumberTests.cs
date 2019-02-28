using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette1.Tests
{
    [TestFixture]
    class NumberTests
    {
        int[] allnum = Number.GetAllNumbers().ToArray();
        int[] infieldnum = Number.InFieldNumbers;

        [Test]
        public void GetStreetFactorTest()
        {
            foreach (var num in infieldnum)
            {
                var street = Number.GetStreet(num);
                var list = Number.GetFactor(street).ToList();

                Assert.IsTrue(list.Contains(num));
            }
        }

        [Test]
        public void GetStreetTest()
        {
            Dictionary<int, Street> streets = new Dictionary<int, Street>();

            foreach (var num in allnum)
            {
                streets.Add(num, Number.GetStreet(num));
            }

            foreach (var rowGroup in streets.GroupBy(kvp => kvp.Value))
            {
                switch (rowGroup.Key)
                {
                    case Street.None:
                        throw new Exception("invalid row type=>" + rowGroup.Key.ToString());
                    case Street.OutOfStreet:
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

            Dictionary<int, Column> cols = new Dictionary<int, Column>();

            foreach (var num in allnum)
            {
                cols.Add(num, Number.GetColumn(num));
            }

            foreach (var colGroup in cols.GroupBy(kvp => kvp.Value))
            {
                switch (colGroup.Key)
                {
                    case Column.None:
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
        public void GetColumnFactorTest()
        {
            foreach (var num in infieldnum)
            {
                var column = Number.GetColumn(num);
                var list = Number.GetFactor(column).ToList();

                Assert.IsTrue(list.Contains(num));
            }
        }

    }
}
