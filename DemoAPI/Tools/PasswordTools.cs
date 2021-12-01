using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks; 
namespace DemoAPI.Tools
{
    public static class PasswordTools
    {
        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA512Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return  algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public static byte[] GenerateSalt(int length = 122)
        {
            // Create a buffer
            byte[] randBytes;

            if (length >= 1)
            {
                randBytes = new byte[length];
            }
            else
            {
                randBytes = new byte[1];
            }

            // Create a new RNGCryptoServiceProvider.
            using (RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider())
            {
                // Fill the buffer with random bytes.
                rand.GetBytes(randBytes);
            }

               
            // return the bytes.
            return randBytes;
        }
    
        public static bool CheckPassword(string password, byte[] Current, byte[] Salt)
        {
            byte[] bytePwd = Encoding.UTF8.GetBytes(password);

            return  Current.SequenceEqual(GenerateSaltedHash(bytePwd, Salt));
        }
    }
}
