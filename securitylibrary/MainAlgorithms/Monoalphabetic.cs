using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            Dictionary<char, char> keyMapping = new Dictionary<char, char>();
            for (int i = 0; i < plainText.Length; i++)
            {
                if (!keyMapping.ContainsKey(plainText[i]))
                    keyMapping[plainText[i]] = cipherText[i];
            }

            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] key = new char[26];
            for (int i = 0; i < alphabet.Length; i++)
            {
                key[i] = keyMapping.ContainsKey(alphabet[i]) ? keyMapping[alphabet[i]] : '?';
            }

            return new string(key);
        }

        public string Decrypt(string cipherText, string key)
        {
            Dictionary<char, char> decryptionKey = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                decryptionKey[key[i]] = (char)('a' + i);
            }

            StringBuilder plainText = new StringBuilder();
            foreach (char c in cipherText)
            {
                plainText.Append(decryptionKey.ContainsKey(c) ? decryptionKey[c] : c);
            }

            return plainText.ToString();
        }

        public string Encrypt(string plainText, string key)
        {
            Dictionary<char, char> encryptionKey = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                encryptionKey[(char)('a' + i)] = key[i];
            }

            StringBuilder cipherText = new StringBuilder();
            foreach (char c in plainText)
            {
                cipherText.Append(encryptionKey.ContainsKey(c) ? encryptionKey[c] : c);
            }

            return cipherText.ToString();
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            string frequencyOrder = "ETAOINSHRDLCUMWFGYPBVKJXQZ".ToLower();
            Dictionary<char, int> letterFrequency = new Dictionary<char, int>();

            foreach (char c in cipher)
            {
                if (char.IsLetter(c))
                {
                    if (!letterFrequency.ContainsKey(c))
                        letterFrequency[c] = 0;
                    letterFrequency[c]++;
                }
            }

            var sortedFrequencies = letterFrequency.OrderByDescending(x => x.Value).Select(x => x.Key).ToArray();
            Dictionary<char, char> mapping = new Dictionary<char, char>();

            for (int i = 0; i < sortedFrequencies.Length; i++)
            {
                mapping[sortedFrequencies[i]] = frequencyOrder[i];
            }

            StringBuilder plainText = new StringBuilder();
            foreach (char c in cipher)
            {
                plainText.Append(mapping.ContainsKey(c) ? mapping[c] : c);
            }

            return plainText.ToString();
        }
    }
}
