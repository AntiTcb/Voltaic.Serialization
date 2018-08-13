using System;

namespace Voltaic.Serialization.Csv
{
    public static partial class CsvReader
    {
        public static CsvTokenType GetTokenType(ref ReadOnlySpan<byte> remaining)
        {
            for (int i = 0; i < remaining.Length; i++)
            {
                byte c = remaining[i];
                switch (c)
                {
                    case (byte)'\n':
                    case (byte)'\r':
                        return CsvTokenType.EndLine;
                    case (byte)',':
                    case (byte)'\t':
                        return CsvTokenType.Delimiter;
                    default:
                        return CsvTokenType.None;
                }
            }
            return CsvTokenType.None;
        }

        public static bool Skip(ref ReadOnlySpan<byte> remaining, out ReadOnlySpan<byte> skipped)
        {
            skipped = default;
            return false;

            //var stack = new ResizableMemory<byte>(32);
            //var currentToken = CsvTokenType.None;

            //int i = 0;
            //for (; i <= remaining.Length || currentToken != CsvTokenType.None;)
            //{
            //    byte c = remaining[i];
            //    switch (c)
            //    {
            //        case (byte)' ': // Whitespace
            //        case (byte)'\n':
            //        case (byte)'\r':
            //        case (byte)'\t':
            //            i++;
            //            continue;
            //        case (byte)',':
            //            remaining = remaining.Slice(i);
            //            return CsvTokenType.Delimiter;

            //        default:
            //            i++;
            //            bool incomplete = true;
            //            while (i < remaining.Length)
            //            {
            //                switch (remaining[i])
            //                {
            //                    case (byte)'\':
            //                        i += 1;
            //                        continue;
            //                    case (byte)'"'
            //                }
            //            }
            //    }
            //}
        }
    }
}
