using System;

namespace Core.Utils
{
    public static class Guard
    {
        public static void ThrowIfNull(this object source, string parameterName)
        {
            if (source == null)
                throw new ArgumentNullException(parameterName);
        }
    }
}
