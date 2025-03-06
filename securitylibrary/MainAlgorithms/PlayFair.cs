using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            key = key.ToUpper();
            cipherText = cipherText.ToUpper();

            //generate key//////////////////////////////////////////////////////////////////

            char[,] keyarr = new char[5, 5];
            HashSet<char> used = new HashSet<char>();
            string letters = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            int col = 0, row = 0;

            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i];

                if (!used.Contains(c))
                {
                    keyarr[row, col] = c;
                    used.Add(c);
                    col++;

                    if (col == 5)
                    {
                        col = 0;
                        row++;
                    }
                }
            }
            for (int i = 0; i < letters.Length; i++)
            {
                char c = letters[i];

                if (!used.Contains(c))
                {
                    keyarr[row, col] = c;
                    used.Add(c);
                    col++;

                    if (col == 5)
                    {
                        col = 0;
                        row++;
                    }
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////// decryption stage

            StringBuilder cipher = new StringBuilder();

            for (int i = 0; i < cipherText.Length; i += 2)
            {
                char a = cipherText[i];
                char b = cipherText[i + 1];
                cipher.Append(ddigraph(keyarr, a, b));
            }


            cipher = cipher.Replace("X", "");
            string plaintext = cipher.ToString();

            return plaintext;
        }

        public (int, int) findpso(char[,]arr, char letter)
        {
            for(int i=0 ; i<5; i++)
            {
                for(int j=0; j<5; j++)
                {
                    if (arr[i, j] == letter)
                        return (i,j);
                }
            }

            throw new Exception();
        }
        public string edigraph(char[,] arr, char a, char b)
        {
            (int ra, int ca) = findpso(arr, a);
            (int rb, int cb) = findpso(arr, b);

            if (ra == rb)
                return $"{arr[ra, (ca + 1 )% 5]}{ arr[rb,(cb + 1 )% 5]}";
            else if (ca == cb)
                return $"{arr[(ra + 1) % 5, ca ]}{arr[(rb + 1) % 5, cb]}";
            else
                return $"{arr[ra,cb]}{arr[rb,ca]}";

        }

        public string ddigraph(char[,] arr, char a, char b)
        {
            (int ra, int ca) = findpso(arr, a);
            (int rb, int cb) = findpso(arr, b);

            if (ra == rb)
                return $"{arr[ra, (ca - 1+5) % 5]}{arr[rb, (cb - 1+5) % 5]}";
            else if (ca == cb)
                return $"{arr[(ra - 1+5) % 5, ca]}{arr[(rb - 1+5) % 5, cb]}";
            else
                return $"{arr[ra, cb]}{arr[rb, ca]}";

        }
        public string Encrypt(string plainText, string key)
        {
            key= key.ToUpper();
            plainText = plainText.ToUpper();

            //generate key//////////////////////////////////////////////////////////////////

            char[,] keyarr = new char[5, 5];
            HashSet<char> used = new HashSet<char>();
            string letters= "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            int col = 0, row = 0;

            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i]; 

                if (!used.Contains(c)) 
                {
                    keyarr[row, col] = c; 
                    used.Add(c); 
                    col++; 

                    if (col == 5) 
                    {
                        col = 0; 
                        row++; 
                    }
                }
            }
            for (int i = 0; i < letters.Length; i++)
            {
                char c = letters[i]; 

                if (!used.Contains(c)) 
                {
                    keyarr[row, col] = c; 
                    used.Add(c); 
                    col++; 

                    if (col == 5) 
                    {
                        col = 0; 
                        row++; 
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////


            //////////// text preparation
            ///

            string text= plainText.Replace("J","I").Replace(" ","");
            StringBuilder preptxt = new StringBuilder();

            for (int i = 1; i < text.Length; i += 2)
            {
                if (text[i] == text[i - 1])
                {
                    preptxt.Append(text[i - 1]);
                    preptxt.Append("X");
                    preptxt.Append(text[i]);

                }
                else
                {
                    preptxt.Append(text[i - 1]);
                    preptxt.Append(text[i]);
                }
            }
            if (text.Length % 2 != 0)
            {
                preptxt.Append(text.Last());

            }
            if (preptxt.Length % 2 != 0)
            {
                preptxt.Append("X");
            }

            string txt = preptxt.ToString();
            ///////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////// encryption stage

            StringBuilder cipher= new StringBuilder();

            for (int i= 0;i < txt.Length; i+= 2)
            {
                char a= txt[i];
                char b = txt[i+1];
                cipher.Append(edigraph(keyarr,a,b));
            }

            string lastcipher= cipher.ToString();

            return lastcipher;
        }
    }
}