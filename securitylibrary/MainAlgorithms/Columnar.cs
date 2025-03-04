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
            char[] cipherTextchars = cipherText.ToCharArray();
            int width = key.Max();
            int depth = cipherTextchars.Length / width;
            char[,] matrix = new char[depth + 1, width];
            char[,] matrix2 = new char[depth , width];
            char[] PLchars = new char[cipherTextchars.Length];
            int index = 0;
            for (int j = 0; j < width; j++)
            {
                for (int i = 0; i < depth; i++)
                {
                    matrix[i, j] = cipherText[index];
                    index++;
                }
            }
            for (int j = 0; j < width; j++)
            {
                matrix[depth, j] = (char)(j+1);
            }
            int count = 1;
            for (int j = 0; count <= width; j++)
            {
                if (matrix[depth, j% width] == (char)key.ElementAt(j % width))
                {
                    for (int i = 0; i < depth; i++)
                    {
                        matrix2[i, j % width] = matrix[i, j % width];
                        count++;
                    }
                }
            }
            int indx = 0;
            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    PLchars[indx] = matrix2[i, j];
                    indx++;
                }
            }
                    return PLchars.ToString();
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
            for (int j = 0; search <= width; j++)
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
