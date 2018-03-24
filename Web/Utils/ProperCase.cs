using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Class.Utilidades
{
    public class ProperCase
    {
        public static string ToProperCase(string s)
        {
            if (s == null) return s;

            string[] words = s.Split(' ');
            for (Int32 i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                string rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return string.Join(" ", words);
        }

        public static string ToTitleCase(string str)
        {
            if (!ReferenceEquals(null, str))
            {
                var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
