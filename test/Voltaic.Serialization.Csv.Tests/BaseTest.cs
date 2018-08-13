using System;
using System.Collections.Generic;
using System.Text;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Csv.Tests
{
    public abstract class BaseTest<T>
    {
        private readonly CsvSerializer _serializer;
        private readonly IEqualityComparer<T> _comparer;

        public BaseTest(IEqualityComparer<T> comparer = null)
        {
            _serializer = new CsvSerializer();
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public static object[] Read(string str, T value)
          => new object[] { new TextTestData<T>(TestType.Read, str, value) };

        protected void RunTest(TextTestData<T> test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.FailRead:
                    //Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>(test.String, converter));
                    //Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>(' ' + test.String + ' ', converter));
                    break;
                case TestType.FailWrite:
                    //Assert.Throws<SerializationException>(() => _serializer.WriteUtf16String(test.Value, converter));
                    break;
                case TestType.Read:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String, converter), _comparer);
                    //Assert.True(TestSkip(test.String));
                    //Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String, converter), _comparer);
                    //Assert.True(TestSkip(' ' + test.String));
                    //Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String + ' ', converter), _comparer);
                    //Assert.True(TestSkip(test.String + ' '));
                    //Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String + ' ', converter), _comparer);
                    //Assert.True(TestSkip(' ' + test.String + ' '));
                    break;
                case TestType.Write:
                    //Assert.Equal(test.String, _serializer.WriteUtf16String(test.Value, converter));
                    //Assert.True(TestSkip(test.String));
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String, converter), _comparer);
                    Assert.True(TestSkip(test.String));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String, converter), _comparer);
                    Assert.True(TestSkip(' ' + test.String));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String + ' ', converter), _comparer);
                    Assert.True(TestSkip(test.String + ' '));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String + ' ', converter), _comparer);
                    Assert.True(TestSkip(' ' + test.String + ' '));
                    //Assert.Equal(test.String, _serializer.WriteUtf16String(test.Value, converter));
                    break;
            }
        }

        private static bool TestSkip(string str)
        {
            throw new NotImplementedException();
        }
    }
}
