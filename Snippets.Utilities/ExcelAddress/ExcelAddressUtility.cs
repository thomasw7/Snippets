using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Snippets.Utilities.ExcelAddress
{
    public class ExcelAddressUtility
    {
        private IEnumerable<string> Matches(string address)
        {
            var regex = new Regex(@"(^|,)(([a-zA-Z]+[0-9]+\:[a-zA-Z]+[0-9]+|[a-zA-Z]+[0-9]+)|[a-zA-Z]+:[a-zA-Z]|[0-9]+:[0-9]+)");
            var matches = regex.Matches(address);

            return matches.Cast<Match>().Select(x => x.Value.Trim(','));
        }

        public IEnumerable<int> GetRowsFromAddress(string address)
        {
            var rows = new List<int>();
            var matches = Matches(address);

            var regex = new Regex(@"[0-9]+");

            foreach (var m in matches)
            {
                if (m.Contains(":"))
                {
                    var r = regex.Matches(m)
                                .Cast<Match>()
                                .Select(x => Convert.ToInt32(x.Value))
                                .ToList();

                    if (!r.Any())
                    {
                        continue;
                    }

                    var l = Math.Min(r[0], r[1]);
                    var h = Math.Max(r[0], r[1]);
                    rows = rows.Concat(Enumerable.Range(l, 1 + h - l)).ToList();
                }
                else
                {
                    var r = regex.Match(m);
                    rows.Add(Convert.ToInt32(r.Value));
                }
            }

            return rows;
        }

        public IEnumerable<int> GetColumnsFromAddress(string address)
        {
            var columns = new List<int>();
            var matches = Matches(address);

            var regex = new Regex(@"[a-zA-Z]+");

            foreach (var m in matches)
            {
                if (m.Contains(":"))
                {
                    var c = regex.Matches(m)
                                .Cast<Match>()
                                .Select(x => GetColumnNumber(x.Value))
                                .ToList();

                    if (!c.Any())
                    {
                        continue;
                    }

                    var l = Math.Min(c[0], c[1]);
                    var h = Math.Max(c[0], c[1]);

                    columns = columns.Concat(Enumerable.Range(l, 1 + h - l)).ToList();
                }
                else
                {
                    var c = regex.Match(m);
                    columns.Add(GetColumnNumber(c.Value));
                }
            }

            return columns;
        }


        public string GetColumnLetters(int columnNumber)
        {
            if (columnNumber <= 0)
            {
                throw new ArgumentException($"Invalid excel column number '{columnNumber}'.");
            }

            var letters = "";

            var power = columnNumber;

            while (power > 0)
            {
                var cn = power % 26;
                power = (int)(power / 26);
                letters = (char)(cn + 64) + letters;
            }

            return letters;
        }

        public int GetColumnNumber(string columnLetters)
        {
            var regex = new Regex(@"[a-zA-Z]+$");
            if (!regex.IsMatch(columnLetters))
            {
                throw new ArgumentException($"'{columnLetters}' is not a valid excel column letter.");
            }

            columnLetters = columnLetters.ToUpper();

            double columnNumber = 0;

            for (var i = 1; i <= columnLetters.Length; i++)
            {
                var cn = (int)columnLetters[i - 1] - 64;

                if (i == columnLetters.Length)
                {
                    columnNumber += cn;
                    continue;
                }

                columnNumber += cn * Math.Pow(26, columnLetters.Length - i);
            }

            return (int)columnNumber;
        }
    }
}
