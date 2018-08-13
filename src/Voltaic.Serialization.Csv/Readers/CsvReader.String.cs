using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Csv
{
    public static partial class CsvReader
    {
        public static bool TryReadString(ref ReadOnlySpan<byte> remaining, out string result)
        {
            return Utf8Reader.TryReadString(ref remaining, out result);
        }        
    }
}
