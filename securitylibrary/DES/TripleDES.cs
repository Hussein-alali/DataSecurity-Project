using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class TripleDES : ICryptographicTechnique<string, List<string>>
    {
        public string Decrypt(string cipherText, List<string> key)
        {
            DES des1 = new DES();
            // throw new NotImplementedException();
            string one = des1.Decrypt(cipherText, key[0]);
            string two = des1.Encrypt(one, key[1]);
            string final = des1.Decrypt(two, key[0]);

            return final;
        }

        public string Encrypt(string plainText, List<string> key)
        {
            DES des1 = new DES();
            // throw new NotImplementedException();
            string one = des1.Encrypt(plainText, key[0]);
            string two = des1.Decrypt(one, key[1]);
            string final = des1.Encrypt(two, key[0]);

            return final;
        }

        public List<string> Analyse(string plainText,string cipherText)
        {
            throw new NotSupportedException();
        }

    }
}
