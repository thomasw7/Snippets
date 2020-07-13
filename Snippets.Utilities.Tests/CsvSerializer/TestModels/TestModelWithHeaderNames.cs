using Snippets.Utilities.CsvSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Snippets.Utilities.Tests.CsvSerializer.TestModels
{
    public class TestModelWithHeaderNames : IEquatable<TestModelWithHeaderNames>
    {
        [CsvSerializerHeader(HeaderName = "SV")]
        public string StringValue { get; set; }


        [CsvSerializerHeader(HeaderName = "IV")]
        public int IntValue { get; set; }
        

        [CsvSerializerHeader(HeaderName = "DTV", DateTimeFormat = "yyyy-MM-dd")]
        public DateTime? DateTimeValue { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TestModelWithHeaderNames);
        }

        public bool Equals(TestModelWithHeaderNames model)
        {
            return model != null &&
                   StringValue == model.StringValue &&
                   IntValue == model.IntValue &&
                   DateTimeValue == model.DateTimeValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StringValue, IntValue, DateTimeValue);
        }
    }
}
