using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace EasyCrypt
{
    class EasyCrypt
    {

        public static string EncryptString(string plainText)
        {
            byte[] IV, Key;

            using (Aes myAes = Aes.Create())
            {
                Key = myAes.Key;
                IV = myAes.IV;
            }

                // Check arguments.
                if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                string outEncrypted = System.Convert.ToBase64String(encrypted);
                string outIV = System.Convert.ToBase64String(IV);
                string outKey = System.Convert.ToBase64String(Key);

                /*
                Console.WriteLine(outKey);
                Console.WriteLine(outIV);
                Console.WriteLine(outEncrypted);
                */

                List<string> myArr = new List<string>();
                myArr.Add(outKey);
                myArr.Add(outIV);
                myArr.Add(outEncrypted);

                string returns = string.Join("", myArr);


                return returns;
            }
            
        }


        public static string DecryptString(string data)
        {
            string k = data.Substring(0, 44);
            string i = data.Substring(44, 24);
            string d = data.Substring(68, data.Length - 68);

            /*
            Console.WriteLine(k);
            Console.WriteLine(k.Length);
            Console.WriteLine(i);
            Console.WriteLine(i.Length);
            Console.WriteLine(d);
            Console.WriteLine(d.Length);
            */

            byte[] Key = System.Convert.FromBase64String(k);
            byte[] IV = System.Convert.FromBase64String(i);
            byte[] cipherText = System.Convert.FromBase64String(d);

            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            
            return plaintext;

        }

        public static void sendToFile(string input, string output)
        {
            byte[] fileInformation = File.ReadAllBytes(input);
            string result = EasyCrypt.EncryptString(Convert.ToBase64String(fileInformation));
            File.WriteAllText(output, result);
        }

        public static void getFromFile(string input, string output)
        {

            string fileInformation = File.ReadAllText(input);
            string b64result = EasyCrypt.DecryptString(fileInformation);
            if (b64result.Substring(b64result.Length -2, 2) != "==")
            {
                byte[] result = System.Text.UTF8Encoding.UTF8.GetBytes(b64result);
                File.WriteAllBytes(output, result);
            }
            else
            {
                byte[] result = System.Convert.FromBase64String(b64result);
                File.WriteAllBytes(output, result);
            }
            
        }
    }
}
