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
            HashSet<char> usedCipherLetters = new HashSet<char>();

            
            for (int i = 0; i < plainText.Length; i++)
            {
                char p = char.ToLower(plainText[i]);
                char c = char.ToLower(cipherText[i]);

                if (!keyMapping.ContainsKey(p))
                {
                    keyMapping[p] = c;
                    usedCipherLetters.Add(c);
                }
            }

            
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] key = new char[26];
            bool[] used = new bool[26];

            
            foreach (var pair in keyMapping)
            {
                int index = pair.Key - 'a';
                key[index] = pair.Value;
                used[pair.Value - 'a'] = true;
            }

            
            int missingIndex = 0;
            for (int i = 0; i < 26; i++)
            {
                if (key[i] == '\0')
                { 
                    while (missingIndex < 26 && used[missingIndex])
                        missingIndex++;

                    key[i] = (char)('a' + missingIndex);
                    used[missingIndex] = true;
                }
            }

            return new string(key);
        }


        public string Decrypt(string cipherText, string key)
        {
            Dictionary<char, char> dk = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                dk[key[i]] = (char)('a' + i);
            }
            StringBuilder plainText = new StringBuilder();
            foreach (char c in cipherText.ToLower())
            {
                plainText.Append(dk.ContainsKey(c) ? dk[c] : c);
            }

            return plainText.ToString();
        }


        public string Encrypt(string plainText, string key)
        {
            Dictionary<char, char> ek = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                ek[(char)('a' + i)] = key[i];
            }
            StringBuilder cipherText = new StringBuilder();
            foreach (char c in plainText.ToLower())
            {
                cipherText.Append(ek.ContainsKey(c) ? ek[c] : c);
            }
            return cipherText.ToString();
        }

        public string AnalyseUsingCharFrequency(string cipher)
        {
            string alphabetFreq = "ETAOINSRHLDCUMFPGWYBVKXJQZ".ToLower();
            SortedDictionary<char, char> keyTable = new SortedDictionary<char, char>();
            cipher = cipher.ToLower();
            int CTLength = cipher.Length;
            string key = "";
            Dictionary<char, int> d1 = new Dictionary<char, int>();
            for (int i = 97; i < 123; i++)
            {
                int c = 0;

                for (int j = 0; j < cipher.Length; j++)
                {
                    if (cipher[j] == (char)i)
                    {
                        c++;
                    }
                }
                d1.Add((char)i, c);
            }

            d1 = d1.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);
            int counter = 0;
            foreach (var item in d1)
            {
                keyTable.Add(item.Key, alphabetFreq[counter]);
                counter++;
            }
            for (int i = 0; i < CTLength; i++)
            {
                key += keyTable[cipher[i]];
            }

            return key;
        }


        /*  public string AnalyseUsingCharFrequency(string cipher)
          {
              string Order = "ETAOINSHRDLCUMWFGYPBVKJXQZ".ToLower();
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
                  mapping[sortedFrequencies[i]] = Order[i];
              }
              StringBuilder plainText = new StringBuilder();
              foreach (char c in cipher)
              {
                  plainText.Append(mapping.ContainsKey(c) ? mapping[c] : c);
              }
              return plainText.ToString();
          }*/
    }
}
