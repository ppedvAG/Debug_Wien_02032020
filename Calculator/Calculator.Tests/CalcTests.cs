using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Calc_Sum_3_and_4_results_7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Calc_Sum_0_and_0_results_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Calc_Sum_MAX_and_1_throws()
        {
            //Arrange
            var calc = new Calc();

            //Act
            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));

        }

        [TestMethod]
        public void Calc_IsWeekend()
        {
            var calc = new Calc();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 3, 02);//mo
                Assert.IsFalse(calc.IsWeekEnd());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 3, 03);//di
                Assert.IsFalse(calc.IsWeekEnd());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 3, 04);//mi
                Assert.IsFalse(calc.IsWeekEnd());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 3, 05);//do
                Assert.IsFalse(calc.IsWeekEnd());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 3, 06);//fr
                Assert.IsFalse(calc.IsWeekEnd());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 3, 07);//sa
                Assert.IsTrue(calc.IsWeekEnd());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2020, 3, 08);//so
                Assert.IsTrue(calc.IsWeekEnd());

            }

        }



    }
}
