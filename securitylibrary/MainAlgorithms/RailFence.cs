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

        public string Decrypt(string cipherText, int key)
        {
           // cipherText = cipherText.TrimEnd('x');

            string plainText = "";

            if (key == 1)
                return cipherText;

            char[,] word = new char[key, cipherText.Length];
            int r = 0, c = 0;
            bool lastrow = false;

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (r == 0)
                    lastrow = false;
                if (r == key - 1)
                    lastrow = true;

                word[r, c] = 'm';
                c++;

                if (lastrow)
                {
                    r--;
                }
                else
                {
                    r++;
                }
            }

            int index = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cipherText.Length; j++)
                {
                    if ( index < cipherText.Length && word[i, j] == 'm')
                    {
                        word[i, j] = cipherText[index];
                        index++;
                    }
                }
            }

            r = 0;
            c = 0;
            lastrow = false;

            for (int j = 0; j < cipherText.Length; j++)
            {
                if (r == 0)
                {
                    lastrow = false;
                }
                if (r == key - 1)
                {
                    lastrow = true;
                }

                plainText += word[r, c];
                c++;

                r += lastrow ? -1 : 1;
            }
            return plainText;
        }

        public string Encrypt(string plaintext, int key)
        {
            //while (plaintext.Length % key != 0)
            //{
            //    plaintext += 'x';
            //}
           plaintext = plaintext.TrimEnd('x');

            if (key == 1)
                return plaintext;
            char[,] word = new char[key, plaintext.Length];
            int r = 0;
            int c = 0;
            bool bottom = false;
            for (int i = 0; i < plaintext.Length; i++)
            {
                if (r == 0)
                    bottom = false;
                else if (r == key - 1)
                    bottom = true;

                word[r, c] = plaintext[i];
                c++;
                if (bottom == false)
                {
                    r += 1;
                }
                else
                {
                    r -= 1;
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
            return final;
        }
    }
}



