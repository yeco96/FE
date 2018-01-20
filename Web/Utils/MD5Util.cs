using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Class.Utilidades
{
    public class MD5Util
    {
        public static String getMd5Hash(String input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input String to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a String.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal String. 
            for (Int32 i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal String. 
            return sBuilder.ToString();
        }

        // Verify a hash against a String. 
        public static bool verifyMd5Hash(String input, String hash)
        {
            MD5 md5Hash = MD5.Create();
            // Hash the input. 
            String hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
