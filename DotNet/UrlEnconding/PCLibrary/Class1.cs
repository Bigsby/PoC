using System;
using System.Linq;

namespace PCLibrary
{
    public static class UrlEncoder
    {
        public static string Encoder(byte[] bytes)
        {
            var base64 = Convert.ToBase64String(bytes);
            var paddingCount = base64.Cast<char>().Count(c => c == '=');

            return base64.TrimEnd('=') + paddingCount;
        }
    }
}
