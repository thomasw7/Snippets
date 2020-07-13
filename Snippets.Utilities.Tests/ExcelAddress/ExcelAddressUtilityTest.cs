using NUnit.Framework;
using Snippets.Utilities.ExcelAddress;
using System;
using System.IO;

namespace Snippets.Tests.Utilities.ExcelAddress
{
    public class ExcelAddressUtilityTest
    {
        [Test]
        [TestCase("M", ExpectedResult = 13)]
        [TestCase("AN", ExpectedResult = 40)]
        [TestCase("DV", ExpectedResult = 126)]
        [TestCase("XFD", ExpectedResult = 16384)]
        public int TestGetColumnNumber(string rowLetters)
        {
            //
            var utility = new ExcelAddressUtility();

            //
            var result = utility.GetColumnNumber(rowLetters);

            //
            return result;
        }

        [Test]
        [TestCase("293")]
        [TestCase("A388")]
        [TestCase(":")]
        [TestCase("")]
        public void TestGetColumnNumberForFailure(string badRowLetters)
        {
            //
            var utility = new ExcelAddressUtility();

            //
            TestDelegate testDelegate = () => utility.GetColumnNumber(badRowLetters);

            //
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        [TestCase(13, ExpectedResult = "M")]
        [TestCase(40, ExpectedResult = "AN")]
        [TestCase(126, ExpectedResult = "DV")]
        [TestCase(16384, ExpectedResult = "XFD")]
        public string TestGetColumnLetters(int columnNumber)
        {
            //
            var utility = new ExcelAddressUtility();

            //
            var result = utility.GetColumnLetters(columnNumber);

            //
            return result;
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void TestGetColumnLettersForFailure(int badColumnNumber)
        {
            //
            var utility = new ExcelAddressUtility();

            //
            TestDelegate testDelegate = () => utility.GetColumnLetters(badColumnNumber);

            //
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        [TestCase("B2", ExpectedResult = "2")]
        [TestCase("B2:Z5", ExpectedResult = "2,3,4,5")]
        [TestCase("B2:Z5,A11,Z10:Z15,B:D,3:5", ExpectedResult = "2,3,4,5,11,10,11,12,13,14,15,3,4,5")]
        [TestCase("B2:B2,Z2:B5,WOOT", ExpectedResult = "2,2,3,4,5")]
        public string TestGetRowsFromAddress(string address)
        {
            //
            var utility = new ExcelAddressUtility();

            //
            var result = utility.GetRowsFromAddress(address);

            //
            return String.Join(",", result);
        }

        [Test]
        [TestCase("B2", ExpectedResult = "2")]
        [TestCase("B2:D5", ExpectedResult = "2,3,4")]
        [TestCase("B2:D5,A11,AD10:AF15,ZZ1:AAB1,B:D,3:5", ExpectedResult = "2,3,4,1,30,31,32,702,703,704,2,3,4")]
        [TestCase("B2:B2,D2:B5,WOOT", ExpectedResult = "2,2,3,4")]
        public string TestGetColumnsFromAddress(string address)
        {
            //
            var utility = new ExcelAddressUtility();

            //
            var result = utility.GetColumnsFromAddress(address);

            //
            return String.Join(",", result);
        }
    }
}
