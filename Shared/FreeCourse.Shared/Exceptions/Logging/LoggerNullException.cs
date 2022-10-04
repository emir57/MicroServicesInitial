using System;

namespace FreeCourse.Shared.Exceptions.Logging
{
    public class LoggerNullException : Exception
    {
        public LoggerNullException() : base("Logger is not null") { }

        public LoggerNullException(string? message) : base(message) { }

        public LoggerNullException(string? message, Exception? innerException) : base(message, innerException) { }

        public static void ThrowIfNull(object obj)
        {
            if (obj is null)
                throw new LoggerNullException();
        }
    }
}
