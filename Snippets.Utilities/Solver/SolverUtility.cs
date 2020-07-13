using System;
using System.Collections.Generic;
using System.Text;

namespace Snippets.Utilities.Solver
{
    public class SolverUtility
    {
        public double Solve(Func<double, double> function, double goal, double trial, double epsilon = 0.000001, double tolerance = 0.00001, int iterations = 100)
        {
            if (iterations < 1)
            {
                throw new Exception($"Unable to converge. Goal: {goal}, Tolerance: {tolerance}.");
            }

            var result = function(trial);

            if (Math.Abs(goal - result) < tolerance)
            {
                return trial;
            }

            var delta = function(trial + epsilon) - result;

            var approx = trial + epsilon * ((goal - result) / delta);

            return Solve(function, goal, approx, epsilon, tolerance, iterations - 1);
        }
    }
}
