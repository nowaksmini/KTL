using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KTL.Test
{
    /// <summary>
    /// Summary description for VictoryTest
    /// </summary>
    [TestClass]
    public class VictoryTest
    {
        private Game game;

        public VictoryTest()
        {
            game = new Game();
            game.CreateFields(6);
            game.CreateProgressions();
            game.CreateColors(4);
            game.K = 3;
        }
        [TestMethod]
        public void VinTest()
        {
            game.Fields[0].Select(game.Colors[0]);
            game.Fields[1].Select(game.Colors[1]);
            game.Fields[2].Select(game.Colors[2]);
            var result = game.VerifyVictory();
            Assert.AreEqual(true, result);
        }
    }
}
