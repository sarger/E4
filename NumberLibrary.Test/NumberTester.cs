using NUnit.Framework;
using StringCalculator;
using System;

namespace NumberLibrary.Test
{
    [TestFixture]
    public class NumberTester
    {
        [Test]
        public void WhenInputIsEmpty() 
        {
            //Arrange
            string input = string.Empty;
            int answer = 0;

            //Act
            int outvalue = Program.Add(input);

            //Assert
            Assert.AreEqual(answer, outvalue);
            
        }

        [Test]
        public void WhenInputIsOneNumber()
        {
            //Arrange
            string input = "1";
            int answer = 1;

            //Act
            int outvalue = Program.Add(input);

            //Assert
            Assert.AreEqual(answer, outvalue);

        }

        [Test]
        public void WhenInputIsOneNumberAndComma()
        {
            //Arrange
            string input = "1,2";
            int answer = 3;

            //Act
            int outvalue = Program.Add(input);

            //Assert
            Assert.AreEqual(answer, outvalue);

        }
        [Test]
        public void WhenInputIsOneNumberLineBreakAndComma()
        {
            //Arrange
            string input = "1\n2,3";
            int answer = 6;

            //Act
            int outvalue = Program.Add(input);

            //Assert
            Assert.AreEqual(answer, outvalue);

        }
        [Test]
        public void WhenInputIsCustomParameter()
        {
            //Arrange
            string input = "//;\n1;2";
            int answer = 3;

            //Act
            int outvalue = Program.Add(input);

            //Assert
            Assert.AreEqual(answer, outvalue);

        }

        [Test]
        public void WhenInputIsNegative()
        {
            //Arrange
            string input = "//;\n1;-2";
            string answer = "-2";

            //Act
            //int outvalue = exception;

            //Assert
     
            var ex = Assert.Throws<Exception>(() => Program.Add(input));
            Assert.That(ex.Message, Is.EqualTo(answer));

        }
    }
     
}

/**
 * Test cases not complete
 * Couldnt add when string ends with '/n'
 * **/
