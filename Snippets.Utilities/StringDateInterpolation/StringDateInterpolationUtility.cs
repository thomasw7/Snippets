using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Snippets.Utilities.StringDateInterpolation
{
    public class StringDateInterpolationUtility
    {
        public string Interpolate(string interpolationKey, DateTime datetime, string rawString)
        {
            if (interpolationKey == null || !Regex.IsMatch(interpolationKey, @"^[a-zA-Z]+$"))
            {
                throw new ArgumentException($"Invalid interpolation key '{interpolationKey}'.");
            }

            var regex = new Regex($@"{{{interpolationKey}:[^{{|^}}]+}}");

            var result = rawString;
            
            foreach (var match in regex.Matches(rawString))
            {
                var m = match.ToString();
                var r = m.Substring(interpolationKey.Length + 2, m.Length - interpolationKey.Length - 3);
                result = result.Replace(m, datetime.ToString(r));
            }

            return result;
        }

        public string Interpolate(IDictionary<string, DateTime> keyDates, string rawString)
        {
            var result = rawString;

            foreach (var kd in keyDates)
            {
                result = Interpolate(kd.Key, kd.Value, result);
            }

            return result;
        }
    }
}
