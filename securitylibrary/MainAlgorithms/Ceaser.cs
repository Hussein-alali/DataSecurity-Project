using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {

        // throw new NotImplementedException();
        public string Encrypt(string ourinput, int shift)
        {
            string encrypted = "";
            foreach (char character in ourinput)
            {
                if (char.IsLetter(character))
                {
                    char baseChar = char.IsUpper(character) ? 'A' : 'a';
                    encrypted += (char)(((character - baseChar + shift) % 26) + baseChar);
                }
                else
                {
                    encrypted += character;
                }
            }
            return encrypted;
        }


        public string Decrypt(string cipherText, int key)
        {
          //  throw new NotImplementedException();
            return Encrypt(cipherText, 26 - (key % 26));
        }

        public int Analyse(string plainText, string cipheredText)
        {
          //throw new NotImplementedException();
            int key = (cipheredText[0] - plainText[0] + 26) % 26;
            return key;
        }
    }
}