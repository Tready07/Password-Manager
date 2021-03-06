﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Shared
{
    public class CryptManager
    {
        /**
         * @brief
         *      The encrypt function takes a string (plaintext) input and a key
         *      then it encrypts using TripleDes Electronic Cookbook (ecb).
         *      It returns a (encrypted) string.
         */
        public static string encrypt(string input, byte [] key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = key;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform encryptor = tripleDES.CreateEncryptor();           
            byte[] resultArray = encryptor.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /**
         * @brief
         *      The decrypt function takes a string (encrypted) input and a key.
         *      It is to be of note if they key used to encrypt isn't the same
         *      as the key used to decrypt you will not get the original plaintext.
         *      it then returns a (plaintext) string.
         */

        public static string decrypt(string input, byte [] key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = key;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform decryptor = tripleDES.CreateDecryptor();
            byte[] resultArray = decryptor.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /**
         * @brief
         *      Generates a secret key used for symmetric encryption and decryption
         */
        public static byte[] generateKey()
        {
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.GenerateKey();
            return tripleDES.Key;
        }

        /**
         * @brief
         *      Generates a random string
         */
        public static String generateSalt(int byteCount)
        {
            var rngCsp = new RNGCryptoServiceProvider();
            Byte[] random = new Byte[byteCount];
            rngCsp.GetBytes(random);
            string value = BitConverter.ToString(random);
            return value;
        }      

        /**
         * @brief
         *      Generates the hash of the input and
         *      returns the string
         */      
        public static String hash(String msgToHash)
        {
            SHA256 Hasher = SHA256Managed.Create();
            byte[] hashValue;            
            byte[] msg = Encoding.UTF8.GetBytes(msgToHash);
            hashValue = Hasher.ComputeHash(msg);
            string value = BitConverter.ToString(hashValue);
            return value;
        }

#if false
            --- This is example code of how the cryptManager class works it is used to encrypt and decrypt text
            CryptManager cryptManager = new CryptManager();
            String encryptedText = cryptManager.encrypt("saltySpitoon", "sblw-3hn8-sqoy19");
            Console.WriteLine("The encrypted text is {0}", encryptedText);
            var plaintext = cryptManager.decrypt(encryptedText, "sblw-3hn8-sqoy19");
            Console.WriteLine("the plain text is {0}", plaintext);
            Console.ReadKey();
#endif

    }
}
