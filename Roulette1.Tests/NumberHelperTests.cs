using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette1.Tests
{
    [TestFixture]
    class NumberHelperTests
    {
        int[] allnum = NumberHelper.GetAllNumbers().ToArray();
        int[] infieldnum = NumberHelper.GetInFieldNumbers().ToArray();

        [Test]
        public void GetStreetFactorTest()
        {
            foreach (var num in infieldnum)
            {
                var street = NumberHelper.GetStreet(num);
                var list = NumberHelper.GetStreetFactor(street).ToList();

                Assert.IsTrue(list.Contains(num));
            }
        }

        [Test]
        public void GetStreetTest()
        {

            Dictionary<int, Street> streets = new Dictionary<int, Street>();

            foreach (var num in infieldnum)
            {
                streets.Add(num, NumberHelper.GetStreet(num));
            }

            foreach (var rowGroup in streets.GroupBy(kvp => kvp.Value))
            {
                switch (rowGroup.Key)
                {
                    case Street.None:
                    case Street.InvalidStreet:
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

            foreach (var num in infieldnum)
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
        public void GetColumnFactorTest()
        {
            foreach (var num in infieldnum)
            {
                var column = NumberHelper.GetColumn(num);
                var list = NumberHelper.GetColumnFactor(column).ToList();

                Assert.IsTrue(list.Contains(num));
            }
        }

    }
}
