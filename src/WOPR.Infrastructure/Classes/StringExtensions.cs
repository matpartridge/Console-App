using System;

namespace WOPR.Infrastructure.Classes
{
    public static class StringExtensions
    {
        public static int NumberOfCharactersInString(this string target, ConsoleKeyInfo keyPressed, bool ignoreCase)
        {
            var i = 0;
            var ctr = 0;
            var key = keyPressed.KeyChar.ToString();
            if (ignoreCase)
            {
                target = target.ToLower();
                key = key.ToLower();
            }
            var array = target.ToCharArray();
            while(++i < target.Length)
            {
                if (array[i].ToString() == key)
                    ctr++;
            }
            return ctr;
        }
    }
}
