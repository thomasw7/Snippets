using NUnit.Framework;
using Snippets.Utilities.CsvSerializer;
using Snippets.Utilities.Tests.CsvSerializer.TestModels;
using System;
using System.IO;

namespace Snippets.Utilities.Tests.CsvSerializer
{
    public class CsvSerializerUtilityTest
    {
        [SetUp]
        public void SetUp()
        {
            var dir = Path.Combine(TestContext.CurrentContext.TestDirectory, this.GetType().Name.Replace("UtilityTest", ""));
            Environment.CurrentDirectory = dir;
            Directory.SetCurrentDirectory(dir);
        }


        #region SERIALIZATION
        [Test]
        public void TestSerialize()
        {
            //
            var serializer = new CsvSerializerUtility();
            var expected = "TestFiles/expected/CsvTestModels.csv";
            var models = new[]
            {
                new TestModel { StringValue = "WO\"OT", IntValue = 100,  DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModel { StringValue = "WAS",    IntValue = 9999, DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModel { StringValue = "HERE",   IntValue = -1,   DateTimeValue = null }
            };

            //
            var actual = "TestFiles/actual/test-serialize.csv";
            serializer.Serialize(actual, models);

            //
            Assert.AreEqual(
                File.ReadAllText(expected).Replace("\n", "").Replace("\r", ""),
                File.ReadAllText(actual).Replace("\n", "").Replace("\r", ""));

        }

        [Test]
        public void TestSerializeWithColumnNumbers()
        {
            //
            var serializer = new CsvSerializerUtility();
            var expected = "TestFiles/expected/CsvTestModels.csv";
            var models = new[]
            {
                new TestModelWithColumnNumbers { StringValue = "WO\"OT", IntValue = 100,  DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithColumnNumbers { StringValue = "WAS",    IntValue = 9999, DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithColumnNumbers { StringValue = "HERE",   IntValue = -1,   DateTimeValue = null }
            };

            //
            var actual = "TestFiles/actual/test-serialize.csv";
            serializer.Serialize(actual, models);

            //
            Assert.AreEqual(
                File.ReadAllText(expected).Replace("\n", "").Replace("\r", ""),
                File.ReadAllText(actual).Replace("\n", "").Replace("\r", ""));
        }

        [Test]
        public void TestSerializeWithHeaderNames()
        {
            //
            var serializer = new CsvSerializerUtility();
            var expected = "TestFiles/expected/CsvTestModelsWithHeaderNames.csv";
            var models = new[]
            {
                new TestModelWithHeaderNames { StringValue = "WO\"OT", IntValue = 100,  DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithHeaderNames { StringValue = "WAS",    IntValue = 9999, DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithHeaderNames { StringValue = "HERE",   IntValue = -1,   DateTimeValue = null }
            };

            //
            var actual = "TestFiles/actual/test-serialize.csv";
            serializer.Serialize(actual, models);

            //
            Assert.AreEqual(
                File.ReadAllText(expected).Replace("\n", "").Replace("\r", ""),
                File.ReadAllText(actual).Replace("\n", "").Replace("\r", ""));
        }
        #endregion

        #region DESERIALIZATION
        [Test]
        public void TestDeserialize()
        {
            //
            var serializer = new CsvSerializerUtility();
            var expected = new[]
            {
                new TestModel { StringValue = "WO\"OT", IntValue = 100,  DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModel { StringValue = "WAS",    IntValue = 9999, DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModel { StringValue = "HERE",   IntValue = -1,   DateTimeValue = null }
            };

            //
            var actual = serializer.Deserialize<TestModel>("TestFiles/CsvTestModels.csv");

            //
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDeserializeWithColumNumbers()
        {
            //
            var serializer = new CsvSerializerUtility();
            var expected = new[]
            {
                new TestModelWithColumnNumbers { StringValue = "WO\"OT", IntValue = 100,  DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithColumnNumbers { StringValue = "WAS",    IntValue = 9999, DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithColumnNumbers { StringValue = "HERE",   IntValue = -1,   DateTimeValue = null }
            };

            //
            var actual = serializer.Deserialize<TestModelWithColumnNumbers>("TestFiles/CsvTestModels.csv");

            //
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDeserializeWithNoHeaders()
        {
            //
            var serializer = new CsvSerializerUtility();
            var expected = new[]
            {
                new TestModelWithColumnNumbers { StringValue = "WO\"OT", IntValue = 100,  DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithColumnNumbers { StringValue = "WAS",    IntValue = 9999, DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithColumnNumbers { StringValue = "HERE",   IntValue = -1,   DateTimeValue = null }
            };

            //
            var actual = serializer.Deserialize<TestModelWithColumnNumbers>("TestFiles/CsvTestModelsWithNoHeaders.csv", false);

            //
            CollectionAssert.AreEqual(expected, actual);
        }


        [Test]
        public void TestDeserializeWithHeaderNames()
        {
            //
            var serializer = new CsvSerializerUtility();
            var expected = new[]
            {
                new TestModelWithHeaderNames { StringValue = "WO\"OT", IntValue = 100,  DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithHeaderNames { StringValue = "WAS",    IntValue = 9999, DateTimeValue = new DateTime(2020, 5, 21) },
                new TestModelWithHeaderNames { StringValue = "HERE",   IntValue = -1,   DateTimeValue = null }
            };

            //
            var actual = serializer.Deserialize<TestModelWithHeaderNames>("TestFiles/CsvTestModelsWithHeaderNames.csv");

            //
            CollectionAssert.AreEqual(expected, actual);
        }
        #endregion
    }
}
