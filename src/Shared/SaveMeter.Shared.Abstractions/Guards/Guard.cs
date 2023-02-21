using System;

namespace SaveMeter.Shared.Abstractions.Guards
{
    public class Guard
    {
        public static void Against<T>(bool unacceptable) where T: System.Exception, new()
        {
            if (unacceptable)
            {
                throw new T();
            }
        }
        public static void Against<T> (bool unacceptable, params object?[] args) where T: System.Exception
        {
            if (unacceptable)
            {
                throw (T)Activator.CreateInstance(typeof(T), args);
            }
        }
        
        public static void Against<T>(bool unacceptable, T exception) where T: System.Exception
        {
            if (unacceptable)
            {
                throw exception;
            }
        }
    }
}
