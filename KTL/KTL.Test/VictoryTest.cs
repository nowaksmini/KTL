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
            game.CreateColors(4);
            game.K = 3;
            game.CreateProgressions();
        }
        [TestMethod]
        public void WinTest1()
        {
            ClearFields();
            game.Fields[0].Select(game.Colors[0]);
            game.Fields[1].Select(game.Colors[1]);
            game.Fields[2].Select(game.Colors[2]);
            var result = game.VerifyVictory(false);
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void WinTest2()
        {
            ClearFields();
            game.Fields[0].Select(game.Colors[0]);
            game.Fields[2].Select(game.Colors[1]);
            game.Fields[4].Select(game.Colors[2]);
            var result = game.VerifyVictory(false);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void LostTest()
        {
            ClearFields();
            game.Fields[0].Select(game.Colors[0]);
            game.Fields[1].Select(game.Colors[0]);
            game.Fields[2].Select(game.Colors[0]);
            game.Fields[3].Select(game.Colors[0]);
            game.Fields[4].Select(game.Colors[0]);
            var result = game.VerifyVictory(false);
            Assert.AreEqual(true, result);
        }

        private void ClearFields()
        {
            game.Fields.ForEach(f => f.Enable());
        }
    }
}
