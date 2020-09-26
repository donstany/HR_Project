using IOWebFramework.Shared.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace ConsoleSandBox.Tests
{
    [TestClass()]
    public class ExperienceCalculatorTests
    {
        [DataTestMethod()]
        [DataRow(800, 2, 2, 20)]
        [DataRow(360, 1, 0, 0)]
        [DataRow(365, 1, 0, 5)]
        [DataRow(385, 1, 0, 25)]
        [DataRow(720, 2, 0, 0)]
        [DataRow(726, 2, 0, 6)]
        [DataRow(756, 2, 1, 6)]
        [DataRow(1050, 2, 11, 0)]
        [DataRow(1058, 2, 11, 8)]

        [DataRow(1079, 2, 11, 29)]
        [DataRow(1080, 3, 0, 0)]
        [DataRow(1081, 3, 0, 1)]
        public void SplitDateTest(int number, int expectedYear, int expectedMonth, int expectedDay)
        {
            //Arrange
            var expCalculator = new ExperienceCalculator();
            //Act
            var result = expCalculator.SplitDate(number);
            //Assert
            Assert.AreEqual(expectedYear, result.Year);
            Assert.AreEqual(expectedMonth, result.Month);
            Assert.AreEqual(expectedDay, result.Day);
        }

        [DataTestMethod()]
        [DataRow(2, 2, 20, 800)]
        [DataRow(1, 0, 0, 360)]
        [DataRow(1, 0, 5, 365)]
        [DataRow(1, 0, 25, 385)]
        [DataRow(2, 0, 0, 720)]
        [DataRow(2, 0, 6, 726)]
        [DataRow(2, 1, 6, 756)]
        [DataRow(2, 11, 0, 1050)]
        [DataRow(2, 11, 8, 1058)]

        [DataRow(3, 0, 1, 1081)]
        [DataRow(2, 11, 29, 1079)]
        [DataRow(3, 0, 0, 1080)]

        [DataRow(null, 0, 0, 0)]
        [DataRow(null, null, 0, 0)]
        [DataRow(null, null, null, 0)]
        [DataRow(0, 0, null, 0)]

        public void AggregateDateTokensTest(int? year, int? month, int? day, int expectedNumber)
        {
            //Arrange
            var expCalculator = new ExperienceCalculator();
            //Act
            var result = expCalculator.AggregateDateTokens(year, month, day);

            //Assert
            Assert.AreEqual(expectedNumber, result);
        }
    }
}