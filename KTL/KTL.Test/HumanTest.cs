using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KTL.Test
{
    [TestClass]
    public class HumanTest
    {
        [TestMethod]
        public void HintLevel1()
        {
            var game = new Game();
            game.HumanLevel = 1;
            var hint = game.GetHint();
        }
    }
}
