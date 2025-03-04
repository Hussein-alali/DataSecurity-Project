using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            throw new NotImplementedException();
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {

            int Mat = (int)Math.Sqrt(key.count);

            List<int> cryptText = new List<int>(new int[Mat]);

            for (int i = 0; i < Mat; i++)
            {
                int sumofmult = 0;
                for (int j = 0; j < Mat; j++)
                {
                    sumofmult += key[i * size + j] * plainText[j]; 
                }
                cryptText[i] = sumofmult % 26; 
            }
            return cryptText;
        }



        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
      
            
            throw new NotImplementedException();
        }

    }
}
