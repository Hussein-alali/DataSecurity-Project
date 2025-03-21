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
            //    throw new NotImplementedException();


            //      CT = "CTIPSCOEEMRNUCE"
            //      PT = "Computer Science" = 15
            //      key = 1 3 4 2 5 ??


            string PT = plainText;
            string CT = cipherText;

            CT = CT.ToLower();

            double PT_Length = PT.Length;
            //double CT_Length = CT.Length;

            //string CT = cipherText.ToUpper();
            //double CT_Length = CT.Length;    //  CT_Length = 15

            //    string PT = plainText.ToUpper();
            //    double PT_Length = PT.Length;

            //    /////////////////////////////////////////////////////////////////////
            //    /



            //      CT = "CTIPSCOEEMRNUCE"
            //      PT = "Computer Science" = 15
            //      key = 1 3 4 2 5 ??

            int col = 0;

            for (int i = 1; i < 8; i++)
            {
                if (PT_Length % i == 0)
                {
                    col = i; // col = 5
                }
            }

            int row = (int)PT_Length / col;  // 15 / 5 = 3 // row = 3

            char[,] PT_Matrix = new char[row, col];
            char[,] CT_Matrix = new char[row, col];

            List<int> key = new List<int>(col);
            int counter = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (counter < PT_Length)
                    {
                        PT_Matrix[i, j] = PT[counter];
                        counter++;
                    }
                }
            }

            counter = 0;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (counter < PT_Length)
                    {
                        CT_Matrix[j, i] = CT[counter];
                        counter++;
                    }
                }
            }

            int check = 0;
            for (int i = 0; i < col; i++)
            {
                for (int s = 0; s < col; s++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        if (PT_Matrix[j, i] == CT_Matrix[j, s])
                        {
                            check++;
                        }
                        if (check == row)
                            key.Add(s + 1);
                    }
                    check = 0;
                }
            }

            if (key.Count() == 0)
            {
                for (int i = 0; i < col + 2; i++)
                {
                    key.Add(i);
                }
            }
            return key;
        


            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            int width = key.Count();
            int depth = cipherText.Length / width;
            char[,] matrix = new char[depth, width];

            Dictionary<int, int> keyMapping = new Dictionary<int, int>();
            for (int j = 0; j < width; j++)
            {
                keyMapping[key[j]] = j;
            }

            int index = 0;
            foreach (var column in key.OrderBy(k => k))
            {
                int j = keyMapping[column];
                for (int i = 0; i < depth; i++)
                {
                    matrix[i, j] = cipherText[index++];
                }
            }

            StringBuilder plainText = new StringBuilder();
            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    plainText.Append(matrix[i, j]);
                }
            }

            return plainText.ToString().TrimEnd('x');
        }

        public string Encrypt(string plainText, List<int> key)
        {
            while (plainText.Length % key.Count() != 0)
            {
                plainText += 'x';
            }

            char[] PTchars = plainText.ToCharArray();
            int width = key.Count();
            int depth = plainText.Length / width;
            char[,] matrix = new char[depth, width];

            int index = 0;
            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = PTchars[index++];
                }
            }

            Dictionary<int, int> keyMapping = new Dictionary<int, int>();
            for (int j = 0; j < width; j++)
            {
                keyMapping[key[j]] = j;
            }

            StringBuilder cipherText = new StringBuilder();
            foreach (var column in key.OrderBy(k => k))
            {
                int j = keyMapping[column];
                for (int i = 0; i < depth; i++)
                {
                    cipherText.Append(matrix[i, j]);
                }
            }

            return cipherText.ToString();
        }
    }
}
