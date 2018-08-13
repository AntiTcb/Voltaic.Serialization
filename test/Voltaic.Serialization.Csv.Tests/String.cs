using System;
using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Csv.Tests
{
    public class StringTests : BaseTest<string>
    {
        public static IEnumerable<object[]> GetData()
        {
            //yield return Read("hello", "hello");
            yield return Read("hello,hello", "hello");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void String(TextTestData<string> data) => RunQuoteTest(data);

    }
}
