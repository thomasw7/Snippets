using Snippets.Utilities.CsvSerializer;
using System;

namespace Snippets.Utilities.Tests.CsvSerializer.TestModels
{
    public class TestModelWithColumnNumbers : IEquatable<TestModelWithColumnNumbers>
    {
        [CsvSerializerHeader(ColumnNumber = 2)]
        public int IntValue { get; set; }

        [CsvSerializerHeader(ColumnNumber = 1)]
        public string StringValue { get; set; }
        

        [CsvSerializerHeader(ColumnNumber = 3, DateTimeFormat = "yyyy-MM-dd")]
        public DateTime? DateTimeValue { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TestModelWithColumnNumbers);
        }

        public bool Equals(TestModelWithColumnNumbers model)
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
