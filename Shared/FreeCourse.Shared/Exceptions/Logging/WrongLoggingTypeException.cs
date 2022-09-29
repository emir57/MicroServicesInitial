using System;

namespace FreeCourse.Shared.Exceptions.Logging
{
    public class WrongLoggingTypeException : Exception
    {
        public WrongLoggingTypeException() : base("Wrong logging type") { }

        public WrongLoggingTypeException(string? message) : base(message) { }

        public WrongLoggingTypeException(string? message, Exception? innerException) : base(message, innerException) { }

        public static void ThrowIfWrongLoggingType(Type type, object obj)
        {
            if (type.IsAssignableFrom(obj.GetType()) == false)
                throw new WrongLoggingTypeException();
        }
        public static void ThrowIfWrongLoggingType(Type arg1, Type arg2)
        {
            if (arg1.IsAssignableFrom(arg2) == false)
                throw new WrongLoggingTypeException();
        }
    }
}
