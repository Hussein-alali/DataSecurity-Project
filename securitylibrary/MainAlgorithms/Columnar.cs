using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, List<int> key)
        {
            char[] PTchars = plainText.ToCharArray();
            int width = key.Max();
            int arrindex = PTchars.Length;
            while (PTchars.Length % width != 0)
            {
                PTchars[arrindex] = 'x';
                arrindex++;
            }
            char[] CTchars = new char[arrindex];
            int depth = PTchars.Length/ width;
            char[,] matrix = new char[depth+1, width];
            int index = 0;
            for (int i=0;i< depth;i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i,j]= PTchars[index];
                    index++;
                }
            }
            for (int j = 0; j < width; j++)
            {
                matrix[depth, j] =(char)key.ElementAt(j);
            }
            int search = 1;
            int CTindex = 0;
            for (int j = 0; search < width; j++)
            {
                    if (matrix[depth, j % width] == (char)search)
                    {
                        for (int i = 0; i < depth; i++)
                        {
                            CTchars[CTindex] = matrix[i, j % width];
                            CTindex++;
                        }
                        search++;
                    }
            }

                return CTchars.ToString();
        }
    }
}
