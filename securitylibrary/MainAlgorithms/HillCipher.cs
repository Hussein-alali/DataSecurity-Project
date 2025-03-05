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
        public int detkeyF(List<int> key)
        {
            int detkey =( key[0] * key[3] - key[1] * key[2])%26;
            if (detkey < 0)
                detkey += 26;
            return detkey;
            //throw new NotImplementedException();
        }
        public int adjF(List<int> key,int i ,int j)
        {
            List<int> listfornow = new List<int>(4);

            for (int k = 0; k < 3; k++)
            {
                for (int i1 = 0; i1 < 3; i1++)
                {
                    if (k != i && i1 != j)
                    {
                        listfornow.Add(key[k*3 + i1]);
                    }
                }
            }

            int finalnum = detkeyF(listfornow);
            if ((i + j) % 2!=0)
            {
                finalnum *= -1;
            }
            finalnum %= 26;
            if (finalnum < 0)
                finalnum += 26;
            return finalnum;
            //throw new NotImplementedException();
        }
        public int detInverse(int det )
        {
            det %= 26;
            if (det < 0) det += 26;
            for (int x = 1; x < 26; x++)
            {
                if ((det * x) % 26 == 1)
                    return x;
            }
            throw new Exception("No det inverse exists.");
        }
        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {

            int Mat = (int)Math.Sqrt(key.Count);
            int detkey;
            List<int> InverseKey = new List<int>(new int[key.Count]);

            if (Mat ==2)
            {
                detkey = detkeyF(key);
                detkey=detInverse(detkey);
                InverseKey[0] = ((key[3] * detkey)%26);
                  InverseKey[1] =(((key[1]*-1) * detkey) % 26);
                  InverseKey[2] =(((key[2] * -1) * detkey) % 26);
                  InverseKey[3] =((key[0] * detkey) % 26);
            }
            else if(Mat == 3)
            {
                detkey = (key[0] * key[4] * key[8] + key[1] * key[5] * key[6] + key[2] * key[3] * key[7]) - (key[1] * key[3] * key[8] + key[0] * key[5] * key[7] + key[2] * key[4] * key[6]);
                detkey %= 26;
                if (detkey < 0)
                    detkey += 26;
                detkey= detInverse(detkey);

                for (int i = 0; i < Mat; i++)
                {
                    int value;
                    for (int j = 0; j < Mat; j++)
                    {
                        value = (adjF(key, i, j) * detkey) % 26;
                        if (value < 0)
                            value += 26;
                        InverseKey[j *Mat + i]=value;
                    }
                }
            }

            return Encrypt(cipherText, InverseKey);

            //throw new NotImplementedException();
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
