using System;
using System.Diagnostics;

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
                        remaining = remaining.Slice(i);
                        return CsvTokenType.EndLine;
                    case (byte)',':
                    case (byte)'\t':
                        remaining = remaining.Slice(i);
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
            
            var stack = new ResizableMemory<byte>(1);
            var currentToken = CsvTokenType.None;

            int i = 0;
            for (; i <= remaining.Length || currentToken != CsvTokenType.None;)
            {
                byte c = remaining[i];
                Debug.Write((char)c);
                switch (c)
                {
                    case (byte)' ': // Whitespace
                    case (byte)'\n':
                    case (byte)'\r':
                    case (byte)'\t':
                        i++;
                        continue;
                    case (byte)',':
                        i++;
                        stack.Pop();
                        return false;

                    default:
                        if (stack.Length < 1)
                            stack.Push(c);
                        return false;
                }
            }

            skipped = remaining.Slice(0, i);
            remaining = remaining.Slice(i);
            return true;
        }
    }
}
