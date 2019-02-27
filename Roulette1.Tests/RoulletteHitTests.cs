﻿using NUnit.Framework;
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
        public void MultiHitTest()
        {
            List<HitChecker> allHitChecker = new List<HitChecker>();

            allHitChecker.AddRange(StraightHitChecker.Gen());
            allHitChecker.AddRange(SplitHitChecker.Gen());
            allHitChecker.AddRange(StreetHitChecker.Gen());
            allHitChecker.AddRange(SquareHitChecker.Gen());

            int pickedNumber = 5;
            int expectHitCount 
                = 1 //StraightHitChecker
                + 4 // SplitHitChecker
                + 1 //StreetHitChecker
                + 4 //SquareHitChecker
                ;

            var hits = allHitChecker.Where(hit => hit.IsHit(pickedNumber));
            Assert.AreEqual(expectHitCount, hits.Count());
        }

        [Test]
        public void StraightHitTest()
        {
            var list = StraightHitChecker.Gen();

            foreach (var hit in list.Cast<StraightHitChecker>())
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
            var list = SplitHitChecker.Gen();

            Assert.AreEqual(list.Count, 58);
            foreach (var hit in list.Cast<SplitHitChecker>())
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


        [Test]
        public void StreetHitTest()
        {
            var list = StreetHitChecker.Gen();

            Assert.AreEqual(NumberHelper.StreetCount, list.Count);

            foreach (var hit in list.Cast<StreetHitChecker>())
            {
                foreach (var num in allnum)
                {
                    if (hit.HitNumbers.Contains(num))
                        Assert.IsTrue(hit.IsHit(num));
                    else
                        Assert.IsFalse(hit.IsHit(num));
                }
            }
        }

        [Test]
        public void SquareHitTest()
        {
            var list = SquareHitChecker.Gen();

            Assert.AreEqual(2*11, list.Count); //

            foreach (var hit in list.Cast<SquareHitChecker>())
            {
                foreach (var num in allnum)
                {
                    if (hit.HitNumbers.Contains(num))
                        Assert.IsTrue(hit.IsHit(num));
                    else
                        Assert.IsFalse(hit.IsHit(num));
                }
            }
        }
    }
}
