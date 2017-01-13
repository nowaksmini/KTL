using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace KTL.Test
{
    [TestClass]
    public class ComputerTest
    {
        [TestMethod]
        public void StepLevel1()
        {
            var game = new Game();
            game.CreateFields(4);
            game.K = 2;
            game.CreateColors(4);
            game.CreateProgressions();
            game.HumanLevel = 5;
            game.ComputerLevel = 1;
            game.SelectField(0, game.Colors[0]);
            var step = game.ComputerStep();
            Assert.IsNotNull(step);
            Assert.IsNotNull(step.Item1);
            Assert.IsNotNull(step.Item2);
            Assert.IsTrue(step.Item2 == 0 || step.Item2 == 1 || step.Item2 == 2
                || step.Item2 == 3);
            Assert.IsTrue(step.Item1 == 1 || step.Item1 == 2 || step.Item1 == 3);
        }

        [TestMethod]
        public void StepLevel2()
        {
            var game = new Game();
            game.CreateFields(4);
            game.K = 3;
            game.CreateColors(4);
            game.CreateProgressions();
            game.HumanLevel = 5;
            game.ComputerLevel = 2;
            game.SelectField(0, game.Colors[0]);
            var step = game.ComputerStep();
            Assert.IsNotNull(step);
            Assert.IsNotNull(step.Item1);
            Assert.IsNotNull(step.Item2);
            Assert.IsTrue(step.Item2 != 0);
            var computerStep1Color = step.Item2;
            Assert.IsTrue(step.Item1 == 1 || step.Item1 == 2 || step.Item1 == 3);
            var hint = game.GetHint(false);
            game.SelectField(hint.Item1-1, game.Colors[hint.Item2]);
            step = game.ComputerStep();
        }

        [TestMethod]
        public void StepLevel3()
        {
            var game = new Game();
            game.CreateFields(4);
            game.K = 3;
            game.CreateColors(4);
            game.CreateProgressions();
            game.HumanLevel = 5;
            game.ComputerLevel = 3;
            game.SelectField(0, game.Colors[0]);
            game.SelectField(1, game.Colors[1]);
            game.SelectField(3, game.Colors[1]);
            var step = game.ComputerStep();
            Assert.IsNotNull(step);
            Assert.IsNotNull(step.Item1);
            Assert.IsNotNull(step.Item2);
            Assert.IsTrue(step.Item1 == 2);
            Assert.IsTrue(step.Item2 == 2 || step.Item2 == 3);
        }

        [TestMethod]
        public void StepLevel4()
        {
            var game = new Game();
            game.CreateFields(4);
            game.K = 3;
            game.CreateColors(4);
            game.CreateProgressions();
            game.HumanLevel = 5;
            game.ComputerLevel = 4;
            game.SelectField(0, game.Colors[0]);
            var step = game.ComputerStep();
            Assert.IsNotNull(step);
            Assert.IsNotNull(step.Item1);
            Assert.IsNotNull(step.Item2);
            Assert.IsTrue(step.Item1 == 2 || step.Item1 == 1 || step.Item1 == 3);
        }

        [TestMethod]
        public void StepLevel5()
        {
            var game = new Game();
            game.CreateFields(4);
            game.K = 3;
            game.CreateColors(4);
            game.CreateProgressions();
            game.HumanLevel = 5;
            game.ComputerLevel = 4;
            game.SelectField(0, game.Colors[0]);
            var step = game.ComputerStep();
            Assert.IsNotNull(step);
            Assert.IsNotNull(step.Item1);
            Assert.IsNotNull(step.Item2);
            var prevColor = step.Item2;
            Assert.IsTrue(step.Item1 == 2 || step.Item1 == 1);
            game.SelectField(3, game.Colors[0]);
            step = game.ComputerStep();
            Assert.IsNotNull(step);
            Assert.IsNotNull(step.Item1);
            Assert.IsNotNull(step.Item2);
            Assert.IsTrue(step.Item2 != prevColor);
        }
    }
}
