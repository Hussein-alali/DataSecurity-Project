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

            int Mat = (int)Math.Sqrt(key.Count);

            if (Mat ==2)
            {
                




            }
            else if(Mat == 3)
            {
                






            }






            throw new NotImplementedException();
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {

            int Mat = (int)Math.Sqrt(key.Count);

            List<int> cryptText = new List<int>();

            for (int i = 0; i < plainText.Count; i+=Mat)
            {
                for (int z = 0; z < Mat; z++)
                {
                    int sumofmult = 0;
                    for (int j = 0; j < Mat; j++)
                    {
                        sumofmult += key[z * Mat + j] * plainText[i+j];
                    }
                    cryptText.Add(sumofmult % 26);
                }
            }
            return cryptText;
        }



        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
      
            
            throw new NotImplementedException();
        }

    }
}
