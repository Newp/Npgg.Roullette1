using NUnit.Framework;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Roulette1.Tests
{
    [TestFixture]
    class MultiHitCheckerTests
    {
        List<HitChecker> hitCheckers = new List<HitChecker>();

        Type[] casebycase = new Type[]
                {
                    typeof(FiveNumberHitChecker),
                    typeof(SixNumberHitChecker),
                    typeof(CourtesyLineHitChecker),
                    typeof(SplitHitChecker),
                    typeof(SquareHitChecker),
                };

        List<Type> defaultcase = new List<Type>();

        [OneTimeSetUp]
        public void SetUp()
        {
            hitCheckers.Clear();
            var checkerType = typeof(HitChecker);
            Type[] allCheckers = checkerType.Assembly.GetTypes().Where(type => type.IsAbstract == false && checkerType.IsAssignableFrom(type)).ToArray();

            foreach (var checker in allCheckers)
            {
                var m = checker.GetMethods().Where(method => method.IsStatic && method.Name == "Gen").First();
                var checkerList = (List<HitChecker>)m.Invoke(null, null);
                hitCheckers.AddRange(checkerList);

                if(casebycase.Contains(checker) == false)
                {
                    defaultcase.Add(checker);
                }
            }

        }

        //[Test]
        //public void MultiHitTest()
        //{
        //    int pickedNumber = 5;
        //    int expectHitCount
        //        = 1 //StraightHitChecker
        //        + 4 // SplitHitChecker
        //        + 1 //StreetHitChecker
        //        + 4 //SquareHitChecker
        //        + 0 // FiveNumberHitChecker , 5는 FiveNumber 에 Hit하지 않음. (0,00,1,2,3)
        //        + 2 //SixNumberHitChecker
        //        ;

        //    var hits = hitCheckers.Where(hit => hit.IsHit(pickedNumber));
        //    Assert.AreEqual(expectHitCount, hits.Count());
        //}

        readonly int defaultHit = 1 // straight
            + 1 // color
            + 1 // evenodd
            + 1 // highlow
            + 1 // street
            + 1 // column
            + 1 // dozen
            ;



        List<HitChecker> GetHits(int num) => hitCheckers.Where(hit => hit.IsHit(num)).ToList();
        void ExpectHit(int num, int fiveNumber, int sixNumber, int courtesyLine, int split, int square)
        {
            int expect = defaultHit + fiveNumber + sixNumber + courtesyLine + split + square;
            var hitList = hitCheckers.Where(hit => hit.IsHit(num)).ToList();

            Dictionary<Type, int> hitMap = new Dictionary<Type, int>();
            var grouping = hitList.GroupBy(hc => hc.GetType());

            foreach (var group in grouping)
                hitMap.Add(group.Key, group.Count());

            foreach(var checkerType in defaultcase)
            {
                Assert.AreEqual(1, hitMap[checkerType]);
            }

            Func<Type, int> GetHitCount = new Func<Type, int>((type)=>
            {
                hitMap.TryGetValue(type, out int result);
                return result;
            });

            Assert.AreEqual(fiveNumber, GetHitCount(typeof(FiveNumberHitChecker)));
            Assert.AreEqual(sixNumber, GetHitCount(typeof(SixNumberHitChecker)));
            Assert.AreEqual(courtesyLine, GetHitCount(typeof(CourtesyLineHitChecker)));
            Assert.AreEqual(split, GetHitCount(typeof(SplitHitChecker)));
            Assert.AreEqual(square, GetHitCount(typeof(SquareHitChecker)));

            Assert.AreEqual(expect, hitList.Count);
        }
        

        [Test]
        public void InFieldMinimumHitTests()
        {
            foreach (int num in Number.InFieldNumbers)
            {
                var result = GetHits(num);
                Assert.GreaterOrEqual(result.Count, defaultHit);
            }
        }

        [Test]
        public void HitTests123()
        {
            int fiveNum = 1;
            int sixNum = 1;
            int courtesyLine = 0;
            //split + square

            ExpectHit(1, fiveNum, sixNum, courtesyLine, 2, 1);
            ExpectHit(2, fiveNum, sixNum, courtesyLine, 3, 2);
            ExpectHit(3, fiveNum, sixNum, courtesyLine, 2, 1);
        }


        [Test]
        public void HitTests4to12()
        {
            int fiveNum = 0;
            int sixNum = 2;
            int courtesyLine = 0;

            var targetStreets = new Street[] { Street.S4, Street.S7, Street.S10 };

            foreach (var street in targetStreets)
            {
                var factors = street.GetFactor();

                ExpectHit(factors[0], fiveNum, sixNum, courtesyLine, 3, 2);
                ExpectHit(factors[1], fiveNum, sixNum, courtesyLine, 4, 4);
                ExpectHit(factors[2], fiveNum, sixNum, courtesyLine, 3, 2);
            }

        }

        [Test]
        public void HitTests13to33()
        {
            int fiveNum = 0;
            int sixNum = 2;
            int courtesyLine = 1;

            var targetStreets = Number.AllStreets.Where(s => Street.S13 <= s && s <= Street.S31);

            foreach (var street in targetStreets)
            {
                var factors = street.GetFactor();

                ExpectHit(factors[0], fiveNum, sixNum, courtesyLine, 3, 2);// column1
                ExpectHit(factors[1], fiveNum, sixNum, courtesyLine, 4, 4);// column2
                ExpectHit(factors[2], fiveNum, sixNum, courtesyLine, 3, 2);// column3
            }
        }

        [Test]
        public void HitTests34to36()
        {
            int fiveNum = 0;
            int sixNum = 1;
            int courtesyLine = 1;
            
            ExpectHit(34, fiveNum, sixNum, courtesyLine, 2, 1);
            ExpectHit(35, fiveNum, sixNum, courtesyLine, 3, 2);
            ExpectHit(36, fiveNum, sixNum, courtesyLine, 2, 1);
        }
    }
}
