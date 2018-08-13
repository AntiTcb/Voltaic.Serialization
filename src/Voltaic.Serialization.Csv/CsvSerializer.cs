using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Voltaic.Serialization.Csv
{
    public class CsvSerializer : Serializer
    {
        public CsvSerializer(ConverterCollection converts = null, ArrayPool<byte> bytePool = null)
            : base(converts, bytePool)
        {
            _converters.SetDefault<string, StringCsvConverter>();
        }

        public T ReadUtf16<T>(ResizableMemory<char> data, ValueConverter<T> converter = null)
           => ReadUtf16(MemoryMarshal.AsBytes(data.AsReadOnlySpan()), converter);
        public T ReadUtf16<T>(ReadOnlyMemory<char> data, ValueConverter<T> converter = null)
            => ReadUtf16(MemoryMarshal.AsBytes(data.Span), converter);
        public T ReadUtf16<T>(ReadOnlySpan<char> data, ValueConverter<T> converter = null)
            => ReadUtf16(MemoryMarshal.AsBytes(data), converter);
        public T ReadUtf16<T>(ResizableMemory<byte> data, ValueConverter<T> converter = null)
            => ReadUtf16(data.AsReadOnlySpan(), converter);
        public T ReadUtf16<T>(ReadOnlyMemory<byte> data, ValueConverter<T> converter = null)
            => ReadUtf16(data.Span, converter);
        public T ReadUtf16<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
        {
            if (Encodings.Utf16.ToUtf8Length(data, out int bytes) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var utf8 = _pool.Rent(bytes);
            try
            {
                if (Encodings.Utf16.ToUtf8(data, MemoryMarshal.AsBytes(utf8.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF8");
                return Read(utf8.AsSpan(0, bytes), converter);
            }
            finally
            {
                _pool.Return(utf8);
            }
        }
        public T ReadUtf16<T>(string data, ValueConverter<T> converter = null)
            => ReadUtf16(MemoryMarshal.AsBytes(data.AsSpan()), converter);

        public object ReadUtf16(Type type, ResizableMemory<char> data, ValueConverter converter = null)
            => ReadUtf16(type, MemoryMarshal.AsBytes(data.AsReadOnlySpan()), converter);
        public object ReadUtf16(Type type, ReadOnlyMemory<char> data, ValueConverter converter = null)
            => ReadUtf16(type, MemoryMarshal.AsBytes(data.Span), converter);
        public object ReadUtf16(Type type, ReadOnlySpan<char> data, ValueConverter converter = null)
            => ReadUtf16(type, MemoryMarshal.AsBytes(data), converter);
        public object ReadUtf16(Type type, ResizableMemory<byte> data, ValueConverter converter = null)
            => ReadUtf16(type, data.AsReadOnlySpan(), converter);
        public object ReadUtf16(Type type, ReadOnlyMemory<byte> data, ValueConverter converter = null)
            => ReadUtf16(type, data.Span, converter);
        public object ReadUtf16(Type type, ReadOnlySpan<byte> data, ValueConverter converter = null)
        {
            if (Encodings.Utf16.ToUtf8Length(data, out int bytes) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var utf8 = _pool.Rent(bytes);
            try
            {
                if (Encodings.Utf16.ToUtf8(data, MemoryMarshal.AsBytes(utf8.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF8");
                return Read(type, utf8.AsSpan(0, bytes), converter);
            }
            finally
            {
                _pool.Return(utf8);
            }
        }
        public object ReadUtf16(Type type, string data, ValueConverter converter = null)
            => ReadUtf16(type, MemoryMarshal.AsBytes(data.AsSpan()), converter);
    }
}
