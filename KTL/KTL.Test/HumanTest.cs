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
            game.CreateFields(20);
            game.K = 4;
            
            game.CreateProgressions();
            game.HumanLevel = 1;
            var hint = game.GetHint(false);
            Assert.AreEqual(null, hint);
        }
    }
}
