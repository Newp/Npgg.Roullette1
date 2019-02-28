using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette1.Tests
{
    [TestFixture]
    class BettingTests
    {
        RouletteBoard board = null;
        [OneTimeSetUp]
        public void SetUp()
        {
            board = new RouletteBoard();
        }
    }
}
