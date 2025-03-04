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
