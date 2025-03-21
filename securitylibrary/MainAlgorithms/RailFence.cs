using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();

            for (int k = 1; k <= 25; k++)
            {
                string encryptedText = Encrypt(plainText, k).ToLower();
                if (encryptedText == cipherText)
                {
                    return k;
                }
            }
            return -1;
        }

      /*  public string Decrypt(string cipherText, int key)
        {
            if (key == 1)
                return cipherText;

            char[,] ourword = new char[key, cipherText.Length];

            int r = 0, c = 0;
            bool last = false;

            for (int i = 0; i < cipherText.Length; i++)
            {
                ourword[r, c++] = '@';

                if (r == key - 1)
                    last = true;
                else if (r == 0)
                    last = false;

                if (last)
                    r--;
                else
                    r++;
            }

            int index = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cipherText.Length; j++)
                {
                    if (ourword[i, j] == '@' && index < cipherText.Length)
                        ourword[i, j] = cipherText[index++];
                }
            }

            string res = "";
            r = 0; c = 0; last = false;

            for (int i = 0; i < cipherText.Length; i++)
            {
                res += ourword[r, c++];

                if (r == key - 1)
                    last = true;
                else if (r == 0)
                    last = false;

                if (last)
                    r--;
                else
                    r++;
            }

            return res;
        }*/


        public string Encrypt(string plaintext, int key)
        {
            if (key == 1)
                return plaintext;

            char[,] word = new char[key, plaintext.Length];
            bool down = true;

            for (int r = 0; r < key; r++)
            {
                for (int c = 0; c < plaintext.Length; c++)
                {
                    if (c % key == r)
                    {
                        word[r, c / key] = plaintext[c];
                    }
                }
            }


            string final = "";
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < plaintext.Length; j++)
                {
                    if (word[i, j] != '\0')
                        final += word[i, j];
                }
            }
            return final.ToUpper();
        }


        public string Decrypt(string cipherText, int key)
        {

            cipherText = cipherText.Replace(" ", "");
            int countl = 0;
            int c = 0;

            int length = cipherText.Length;
            int MATRIX_COLUMNS = length / key;
            int MATRIX_ROWS = key;
            char last_char = cipherText[cipherText.Length - 1];

            if (length % key == 0)
            {
                MATRIX_COLUMNS = (length / key);
            }
            else
            {
                MATRIX_COLUMNS = (length / key) + 1;
            }
            int v = (MATRIX_ROWS * MATRIX_COLUMNS) - length;
            char[] cipher = new char[MATRIX_ROWS * MATRIX_COLUMNS];

            char[,] matrix = new char[MATRIX_ROWS, MATRIX_COLUMNS];



            for (int i = 0; i < v; i++)
            {
                cipherText += " ";

            }


            for (int i = 0; i < MATRIX_ROWS; i++)
            {

                for (int j = 0; j < MATRIX_COLUMNS; j++)
                {

                    matrix[i, j] = cipherText[c];
                    c++;

                    Console.WriteLine(matrix[i, j]);

                }
                Console.WriteLine("\n\n");

            }
            for (int i = 0; i < MATRIX_COLUMNS; i++)
            {

                for (int j = 0; j < MATRIX_ROWS; j++)
                {

                    cipher[countl] = matrix[j, i];
                    countl++;
                    //Console.WriteLine(cipher[j]);

                }


            }
            String message = string.Join("", cipher);
            message = message.Replace(" ", "");
            message = message.ToLower();
            Console.WriteLine(message);

            return message;

        }




        string str;


    }
}