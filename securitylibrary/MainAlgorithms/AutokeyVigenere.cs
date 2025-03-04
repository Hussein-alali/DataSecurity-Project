using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {

        public string Encrypt(string plainText, string key)
        {
            StringBuilder cipherText = new StringBuilder();
            StringBuilder extendedKey = new StringBuilder(key);

            extendedKey.Append(plainText);

            for (int i = 0; i < plainText.Length; i++)
            {
                char encrypted_Char = (char)((((char.ToUpper(plainText[i]) - 'A') + (char.ToUpper(extendedKey[i]) - 'A')) % 26) + 'A');
                cipherText.Append(encrypted_Char);
            }

            Console.WriteLine($"Encrypting: {plainText} with key: {extendedKey} => {cipherText}");
            return cipherText.ToString();
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            StringBuilder plainText = new StringBuilder();
            StringBuilder extendedKey = new StringBuilder(key);

            for (int i = 0; i < cipherText.Length; i++)
            {
                char decrypted_Char = (char)((((char.ToUpper(cipherText[i]) - 'A') - (char.ToUpper(extendedKey[i]) - 'A') + 26) % 26) + 'A');
                plainText.Append(decrypted_Char);
                extendedKey.Append(decrypted_Char);
            }

            Console.WriteLine($"Decrypting: {cipherText} with key: {extendedKey} => {plainText}");
            return plainText.ToString();
            throw new NotImplementedException();
        }
        //new

        public string Analyse(string plainText, string cipherText)
        {
            StringBuilder key = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                char keyChar = (char)((((char.ToUpper(cipherText[i]) - 'A') - (char.ToUpper(plainText[i]) - 'A') + 26) % 26) + 'A');
                key.Append(keyChar);
            }

            string derivedKey = key.ToString();
            Console.WriteLine($"AutoKeyVigenere Analyse -> Full Derived Key: {derivedKey}");

            string extractedKey = ExtractAutoKey(plainText, derivedKey);
            Console.WriteLine($"AutoKeyVigenere Analyse -> Extracted Key: {extractedKey}");

            return extractedKey;
            throw new NotImplementedException();
        }
        private string ExtractAutoKey(string plainText, string derivedKey)
        {
            for (int keyLength = 1; keyLength <= derivedKey.Length; keyLength++)
            {
                string possibleKey = derivedKey.Substring(0, keyLength);
                string reconstructed = possibleKey + plainText.Substring(0, plainText.Length - keyLength);

                if (reconstructed.Equals(derivedKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    return possibleKey;
                }
            }

            return derivedKey;
        }

        /* private string ExtendKey(string plainText, string key)
         {
             StringBuilder extendedKey = new StringBuilder(key);

             for (int i = 0; extendedKey.Length < plainText.Length; i++)
             {
                 extendedKey.Append(plainText[i]);  
             }

             return extendedKey.ToString();
         }*/

        /* private string ExtractOriginalKey(string fullKey, string plainText)
         {
             if (fullKey.EndsWith(plainText))
                 return fullKey.Substring(0, fullKey.Length - plainText.Length);

             return fullKey;
         }*/

    }
}
