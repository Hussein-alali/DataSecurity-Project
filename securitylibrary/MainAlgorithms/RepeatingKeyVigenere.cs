using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {

        public string Encrypt(string plainText, string key)
        {
            StringBuilder cipherText = new StringBuilder();
            key = ExtendKey(plainText, key);

            for (int i = 0; i < plainText.Length; i++)
            {
                char encryptedChar = (char)((((char.ToUpper(plainText[i]) - 'A') + (char.ToUpper(key[i]) - 'A')) % 26) + 'A');
                cipherText.Append(encryptedChar);
            }
            Console.WriteLine($"Encrypting: {plainText} with key: {key} => {cipherText}");
            return cipherText.ToString();
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            StringBuilder plainText = new StringBuilder();
            key = ExtendKey(cipherText, key);

            for (int i = 0; i < cipherText.Length; i++)
            {
                char decryptedChar = (char)((((char.ToUpper(cipherText[i]) - 'A') - (char.ToUpper(key[i]) - 'A') + 26) % 26) + 'A');
                plainText.Append(decryptedChar);
            }
            Console.WriteLine($"Decrypting: {cipherText} with key: {key} => {plainText}");
            return plainText.ToString();
            throw new NotImplementedException();
        }


        public string Analyse(string plainText, string cipherText)
        {
            StringBuilder key = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                char keyChar = (char)((((char.ToUpper(cipherText[i]) - 'A') - (char.ToUpper(plainText[i]) - 'A') + 26) % 26) + 'A');
                key.Append(keyChar);
            }

            string derivedKey = ExtractRepeatingK(key.ToString());
            Console.WriteLine($"RepeatingKeyVigenere Analyse -> Derived Key: {derivedKey}");
            return derivedKey;
            throw new NotImplementedException();
        }
        private string ExtractRepeatingK(string derivedKey)
        {
            for (int len = 1; len <= derivedKey.Length / 2; len++)
            {
                string candidate = derivedKey.Substring(0, len);
                bool valid = true;
                for (int i = len; i + len <= derivedKey.Length; i += len)
                {
                    if (derivedKey.Substring(i, len) != candidate)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                    return candidate;
            }
            return derivedKey;
        }




        private string ExtendKey(string text, string key)
        {
            StringBuilder extendedKey = new StringBuilder();
            int keyIndex = 0;

            for (int i = 0; i < text.Length; i++)
            {
                extendedKey.Append(key[keyIndex]);
                keyIndex = (keyIndex + 1) % key.Length;
            }

            return extendedKey.ToString();
        }

        /* private string ExtractRepeatingKey(string key)
         {
             for (int i = 1; i <= key.Length / 2; i++)
             {
                 string possibleKey = key.Substring(0, i);
                 if (key.StartsWith(possibleKey, StringComparison.Ordinal) && key.Length % i == 0)
                 {
                     return possibleKey;
                 }
             }
             return key; 
         }*/
    }
}


