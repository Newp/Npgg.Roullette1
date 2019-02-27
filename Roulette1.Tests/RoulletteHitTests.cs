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
