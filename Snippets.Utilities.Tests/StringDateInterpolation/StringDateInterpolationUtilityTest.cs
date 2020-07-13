using NUnit.Framework;
using Snippets.Utilities.StringDateInterpolation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Snippets.Tests.Utilities.StringDateInterpolation
{
    public class StringDateInterpolationUtilityTest
    {
        [Test]
        [TestCase("date", "2020-01-02", "d:/test/{date:yyyyMMdd}/{date:yyyy}", ExpectedResult = "d:/test/20200102/2020")]
        [TestCase("datetime", "2020-01-02 22:02:01", "d:/test/{datetime:yyyyMMdd_HHmmss}/", ExpectedResult = "d:/test/20200102_220201/")]
        [TestCase("date", "2020-01-02 22:02:01", "d:/test/{date:yyyyMMdd}/", ExpectedResult = "d:/test/20200102/")]
        [TestCase("key", "2020-01-02", "d:/test/{date:yyyyMMdd}/", ExpectedResult = "d:/test/{date:yyyyMMdd}/")]
        public string TestInterpolate(string interpolationKey, DateTime datetime, string rawString)
        {
            //
            var utility = new StringDateInterpolationUtility();

            //
            var result = utility.Interpolate(interpolationKey, datetime, rawString);

            //
            return result;
        }

        [Test]
        [TestCase("")]
        [TestCase("abc@")]
        [TestCase(null)]
        public void TestInterpolationForBadKey(string interpolationKey)
        {
            //
            var utility = new StringDateInterpolationUtility();

            //
            TestDelegate testDelegate = () => utility.Interpolate(interpolationKey, DateTime.MinValue, null);

            //
            Assert.Throws<ArgumentException>(testDelegate);
        }
    }
}
