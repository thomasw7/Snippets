using NUnit.Framework;
using Snippets.Utilities.Solver;
using System;

namespace Snippets.Utilities.Tests.Solver
{
    public class SolverUtilityTest
    {
        [Test]
        public void TestSolve()
        {
            //
            var expected = 8;
            var utility = new SolverUtility();

            //
            var actual = Math.Round(utility.Solve(x => Math.Pow(x, 2), 64, 6),6);

            //
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(ExpectedResult = "Unable to converge. Goal: 2, Tolerance: 1E-05.")]
        public string TestForException()
        {
            //
            var utility = new SolverUtility();

            //
            TestDelegate testDelegate = () => utility.Solve(x => 1, 2, 1);

            //
            var ex = Assert.Throws<Exception>(testDelegate);
            return ex.Message;
        }
    }
}
