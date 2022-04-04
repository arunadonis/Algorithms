using System;

namespace WordLadder
{
    public static class Utility
    {
        public static void ThrowIfNull<T>(this T obj, string name = null)
        {
            if (obj != null) return;
            throw new ArgumentNullException(name);
        }
        
        public static void ThrowIfNullOrWhiteSpace(this string obj, string name = null)
        {
            if (obj != null && !string.IsNullOrWhiteSpace(obj)) return;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(name);
            throw new ArgumentNullException(name);
        }
    }
}