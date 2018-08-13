using System;
using System.Buffers;

namespace Voltaic.Serialization.Csv
{
    //public class BooleanCsvConverter : ValueConverter<bool>
    //{
    //    private readonly StandardFormat _format;
    //    public BooleanCsvConverter(StandardFormat format = default)
    //    {
    //        _format = format;
    //    }
    //    public override bool CanWrite(bool value, PropertyMap propMap = null)
    //        => propMap == null || !propMap.ExcludeDefault || value != default;
    //}

    public class StringCsvConverter : ValueConverter<string>
    {
        public override bool CanWrite(string value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeDefault && !propMap.ExcludeNull) || !(value is null);

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out string result, PropertyMap propMap = null)
        {
            switch (CsvReader.GetTokenType(ref remaining))
            {
                default:
                    return CsvReader.TryReadString(ref remaining, out result);
            }
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, string value, PropertyMap propMap = null)
        {
            throw new NotImplementedException();
        }
    }
}
