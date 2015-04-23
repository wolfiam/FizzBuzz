using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System.Collections.Generic;

namespace FizzBuzzWebApplication.Tests
{
    [TestClass]
    public class FizzBuzzTest
    {
        bool divisor;

        public bool checkDivisor(int number, int divisor)
        {
            var boolValue = number % divisor == 0;
            return boolValue;
        }

        [TestMethod]
        public void returnTrueWhenNumberIsDivisble()
        {
            //Act
            divisor = checkDivisor(15, 5);

            //Assert
            Assert.AreEqual(divisor, true);
        }

        [TestMethod]
        public void returnFalseWhenNumberIsDivisble()
        {
            //Act
            divisor = checkDivisor(154, 5);

            //Assert
            Assert.AreEqual(divisor, false);
        }

        [TestMethod]
        public void printLines()
        {
            var list = new List<int>();
            //Act
            for (int i = 1; i < 100 + 1; i++)
            {
                list.Add(i);
            }

            var actual = list.Count;
            var expected = 100;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void printMessage()
        {
            var setUp = new SetUp
            {
                Active = 1,
                DateTimeEntered = Convert.ToDateTime("4/18/2015 12:00pm"),
                Id = 1,
                Message = "taco",
                Divisor = "10"
            };

            var FizzBuzz = new FizzBuzz
            {
                Active = 1,
                DateTimeEntered = Convert.ToDateTime("4/18/2015 12:00pm"),
                Id = 1,
                Message = "taco",
                Number = 30
            };

            var expected = setUp.Message;
            var actual = FizzBuzz.Message;

            var boolValue = FizzBuzz.Number % Convert.ToInt32(setUp.Divisor) == 0;

            if (boolValue)
            {
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.AreNotEqual(expected, actual);
            }

        }
    }


}
