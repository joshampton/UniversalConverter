using System;

namespace UniversalConverter
{
    public sealed class ConverterResult
    {
        public object Result { get; }
        public Exception Exception { get; }
        public bool Success { get; }

        public ConverterResult(object result = null, Exception exception = null, bool success = false)
        {
            Result = result;
            Exception = exception;
            Success = success;
        }
    }
}
