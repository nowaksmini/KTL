using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


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
            game.CreateFields(5);
            game.K = 4;
            game.CreateColors(4);
            game.CreateProgressions();
            game.HumanLevel = 3;
            game.SelectField(0, game.Colors[0]);
            game.ComputerLevel = 1;
            game.ComputerStep();
            var hint = game.GetHint(false);
            Assert.AreEqual(true, game.Progressions.Any(p => p.Contains(hint.Item1-1)));
        }

        [TestMethod]
        public void HintLevel3()
        {
            var game = new Game();
            game.CreateFields(5);
            game.K = 4;
            game.CreateColors(4);
            game.CreateProgressions();
            game.HumanLevel = 3;
            game.SelectField(0, game.Colors[0]);
            game.ComputerLevel = 5;
            var step = game.ComputerStep();
            var hint = game.GetHint(false);
            Assert.IsTrue(hint.Item2 == 0 || hint.Item2 == step.Item2);
        }
    }
}
