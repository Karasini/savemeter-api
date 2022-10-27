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
    }
}
