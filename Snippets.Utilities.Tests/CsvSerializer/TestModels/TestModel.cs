using Snippets.Utilities.CsvSerializer;
using System;

namespace Snippets.Utilities.Tests.CsvSerializer.TestModels
{
    public class TestModel : IEquatable<TestModel>
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }

        [CsvSerializerHeader(DateTimeFormat = "yyyy-MM-dd")]
        public DateTime? DateTimeValue { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TestModel);
        }

        public bool Equals(TestModel model)
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
