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
        int[] allnum = Number.GetAllNumbers().ToArray();


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
        public void FiveNumberHitTest()
        {
            var list = FiveNumberHitChecker.Gen();

            List<int> allowed = new List<int>() { Number.N0, Number.N00, 1, 2, 3 };
            foreach (var hit in list.Cast<FiveNumberHitChecker>())
            {
                foreach (var num in allnum)
                {
                    if (allowed.Contains(num))
                        Assert.IsTrue(hit.IsHit(num));
                    else
                        Assert.IsFalse(hit.IsHit(num));
                }
            }
        }

        [Test]
        public void CourtesyLineHitTest()
        {
            var list = CourtesyLineHitChecker.Gen();
            Assert.AreEqual(1, list.Count);
            var hit = list.First() as CourtesyLineHitChecker;
            Assert.IsNotNull(hit);
            
            List<int> allowed = new List<int>() { Number.N0, Number.N00 };
            Assert.AreEqual(allowed.Count, hit.HitNumbers.Count);
            
            foreach (var num in allnum)
            {
                if (allowed.Contains(num))
                    Assert.IsTrue(hit.IsHit(num));
                else
                    Assert.IsFalse(hit.IsHit(num));
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

            Assert.AreEqual(Number.StreetCount, list.Count);

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

            Assert.AreEqual(2 * (Number.StreetCount - 1), list.Count); //

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

        [Test]
        public void SixNumberHitTest()
            => NumberListedHitCheckerHitTest<SixNumberHitChecker>(SixNumberHitChecker.Gen(), SixNumberHitChecker.Allowed.Length);

        [Test]
        public void SixNumberInvalidNumberTest()
            => NumberListedHitCheckerHitTest<SixNumberHitChecker>(SixNumberHitChecker.Gen(), SixNumberHitChecker.Allowed.Length);




        [Test]
        public void DozenInvalidNumberTest() => InvalidCreateTest(DozenHitChecker.Allowed, (num) => new DozenHitChecker(num));

        [Test]
        public void DozenHitTest() =>
            NumberListedHitCheckerHitTest<DozenHitChecker>(DozenHitChecker.Gen(), DozenHitChecker.Allowed.Length);



        [Test]
        public void HighLowHitTest()
          => NumberListedHitCheckerHitTest<HighLowHitChecker>(HighLowHitChecker.Gen(), HighLowHitChecker.Allowed.Length);

        [Test]
        public void HighLowInvalidNumberTest()
            => NumberListedHitCheckerHitTest<HighLowHitChecker>(HighLowHitChecker.Gen(), HighLowHitChecker.Allowed.Length);



        [Test]
        public void EvenOddInvalidNumberTest()
         => NumberListedHitCheckerHitTest<EvenOddHitChecker>(EvenOddHitChecker.Gen(), EvenOddHitChecker.Allowed.Length);


        [Test]
        public void EvenOddHitTest()
        {
            var list = EvenOddHitChecker.Gen();

            foreach (int num in Number.GetFactor(EvenOdd.Even))
                Assert.IsTrue(list[0].IsHit(num));

            foreach (int num in Number.GetFactor(EvenOdd.Odd))
                Assert.IsTrue(list[1].IsHit(num));
        }

     

        [Test]
        public void ColorHitTest()
        {
            var list = ColorHitChecker.Gen();
            Assert.AreEqual(2, list.Count);

            foreach (int num in Number.GetFactor(NumberColor.Red))
                Assert.IsTrue(list[0].IsHit(num));

            foreach (int num in Number.GetFactor(NumberColor.Black))
                Assert.IsTrue(list[1].IsHit(num));

            NumberListedHitCheckerHitTest<ColorHitChecker>(ColorHitChecker.Gen(), ColorHitChecker.Allowed.Length);
        }

        [Test]
        public void ColorInvalidNumberTest()
          => NumberListedHitCheckerHitTest<ColorHitChecker>(ColorHitChecker.Gen(), ColorHitChecker.Allowed.Length);


        [Test]
        public void ColumnInvalidNumberTest()
         => NumberListedHitCheckerHitTest<ColumnHitChecker>(ColumnHitChecker.Gen(), ColumnHitChecker.Allowed.Length);


        [Test]
        public void ColumnHitTest()
        {
            var list = ColumnHitChecker.Gen();
            var columns = new Column[] { Column.C1, Column.C2, Column.C3 };
            Assert.AreEqual(columns.Length, list.Count);
            
            for (int i = 0; i < columns.Length; i++)
            {
                var hitchecker = list[i];
                for(int y= 0;y < columns.Length; y++)
                {
                    foreach (int num in columns[y].GetFactor())
                    {
                        if(i==y)
                            Assert.IsTrue(hitchecker.IsHit(num));
                        else
                            Assert.IsFalse(hitchecker.IsHit(num));
                    }
                }
            }
        }


        void InvalidCreateTest(int[] allowed, Action<int> creator)
        {
            foreach (var num in allnum)
            {
                if (allowed.Contains(num))
                    continue;
                try
                {
                    creator(num);
                    throw new Exception("허용되지 않은 HitChecker가 생성되었습니다.");
                }
                catch (InvalidHitInfoException) { } // 이거정상
            }
        }

        void NumberListedHitCheckerHitTest<T>(List<HitChecker> list, int expectCount)
            where T : NumberListedHitChecker
        {
            Assert.AreEqual(expectCount, list.Count); 

            foreach (var hit in list.Cast<T>())
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
