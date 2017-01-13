﻿using System;
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
        [TestMethod]
        public void HintLevel2()
        {
            var game = new Game();
            game.CreateFields(20);
            game.K = 4;
            game.CreateColors(6);
            game.CreateProgressions();
            game.HumanLevel = 2;
            game.SelectField(0, game.Colors[0]);
            game.ComputerStep();
            var hint = game.GetHint(false);
            Assert.AreEqual(null, hint);
        }
    }
}
