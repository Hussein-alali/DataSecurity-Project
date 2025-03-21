using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        //new update decr
        public string Decrypt(string cipherText, string key)
        {
            key = key.ToLower();
            string ciphertext = cipherText.ToLower();

            char[,] Matrix = CreateMatrix(key);

            //DECRYPTION
            int firstchar_row = 0, firstchar_col = 0, secondchar_row = 0, secondchar_col = 0;
            int Dfirstchar_row = 0, Dfirstchar_col = 0, Dsecondchar_row = 0, Dsecondchar_col = 0;
            string plaintext = "";

            for (int i = 0; i < ciphertext.Length; i += 2)
            {

                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (ciphertext[i] == Matrix[row, col])
                        {
                            firstchar_row = row;
                            firstchar_col = col;
                        }
                        else if (ciphertext[i + 1] == Matrix[row, col])
                        {
                            secondchar_row = row;
                            secondchar_col = col;
                        }
                    }
                }

                if (firstchar_col == secondchar_col) //the 2 chars in the same column
                {
                    Dfirstchar_col = firstchar_col;
                    Dsecondchar_col = secondchar_col;

                    Dfirstchar_row = (firstchar_row - 1 + 5) % 5;
                    Dsecondchar_row = (secondchar_row - 1 + 5) % 5;

                }
                else if (firstchar_row == secondchar_row) //the 2 chars in the same row
                {
                    Dfirstchar_row = firstchar_row;
                    Dsecondchar_row = secondchar_row;

                    Dfirstchar_col = (firstchar_col - 1 + 5) % 5;
                    Dsecondchar_col = (secondchar_col - 1 + 5) % 5;

                }
                else
                {
                    Dfirstchar_row = firstchar_row;
                    Dfirstchar_col = secondchar_col;

                    Dsecondchar_row = secondchar_row;
                    Dsecondchar_col = firstchar_col;
                }



                plaintext = plaintext + Matrix[Dfirstchar_row, Dfirstchar_col];
                plaintext = plaintext + Matrix[Dsecondchar_row, Dsecondchar_col];


            }
            if (plaintext[plaintext.Length - 1] == 'x')
            {
                plaintext = plaintext.Substring(0, plaintext.Length - 1);
            }
            for (int i = 0; i < plaintext.Length - 1; i += 2)
            {
                if (plaintext[i + 1] == 'x' && plaintext[i] == plaintext[i + 2])
                {
                    plaintext = plaintext.Substring(0, i + 1) + plaintext.Substring(i + 2);
                    i--;
                }

            }

            return plaintext;
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();

            string Encrypt_Key;
            string Encrypt_PT;

            Encrypt_Key = key.ToLower();
            Encrypt_PT = plainText.ToLower();

            //CREATING MATRIX

            char[,] Matrix = CreateMatrix(key);


            //ENCRYPTION

            int firstchar_row = 0, firstchar_col = 0, secondchar_row = 0, secondchar_col = 0;
            int Efirstchar_row = 0, Efirstchar_col = 0, Esecondchar_row = 0, Esecondchar_col = 0;
            string encrypted = "";

            for (int i = 0; i < Encrypt_PT.Length; i += 2)
            {

                // !(Encrypt_PT.Length % 2 == 0)

                if (i == Encrypt_PT.Length - 1)
                {
                    Encrypt_PT = Encrypt_PT + 'x';
                }
                else if (Encrypt_PT[i] == Encrypt_PT[i + 1])
                {
                    Encrypt_PT = Encrypt_PT.Substring(0, i + 1) + 'x' + Encrypt_PT.Substring(i + 1);
                }
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (Encrypt_PT[i] == Matrix[row, col])
                        {
                            firstchar_row = row;
                            firstchar_col = col;
                        }
                        else if (Encrypt_PT[i + 1] == Matrix[row, col])
                        {
                            secondchar_row = row;
                            secondchar_col = col;
                        }
                    }
                }

                //the 2 chars in the same column

                if (firstchar_col == secondchar_col)
                {
                    Efirstchar_col = firstchar_col;
                    Esecondchar_col = secondchar_col;

                    Efirstchar_row = (firstchar_row + 1) % 5;
                    Esecondchar_row = (secondchar_row + 1) % 5;

                }

                //the 2 chars in the same row

                else if (firstchar_row == secondchar_row)
                {
                    Efirstchar_row = firstchar_row;
                    Esecondchar_row = secondchar_row;

                    Efirstchar_col = (firstchar_col + 1) % 5;
                    Esecondchar_col = (secondchar_col + 1) % 5;

                }
                else
                {
                    Efirstchar_row = firstchar_row;
                    Efirstchar_col = secondchar_col;

                    Esecondchar_row = secondchar_row;
                    Esecondchar_col = firstchar_col;
                }
                encrypted = encrypted + Matrix[Efirstchar_row, Efirstchar_col];
                encrypted = encrypted + Matrix[Esecondchar_row, Esecondchar_col];
            }
            return encrypted.ToUpper();
        }
        public char[,] CreateMatrix(string k)
        {
            char[,] M = new char[5, 5];

            string alpha = "abcdefghijklmnopqrstuvwxyz";

            //char[] alpha = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


            string letters = k + alpha;

            ArrayList UniqueLetters = new ArrayList();

            for (int i = 0; i < letters.Length; i++)
            {
                if (!UniqueLetters.Contains(letters[i]))
                {
                    if ((letters[i] == 'i' && !UniqueLetters.Contains('j')) || (letters[i] == 'j' && !UniqueLetters.Contains('i')) ||
                        (letters[i] != 'i' && letters[i] != 'j'))
                    {

                        UniqueLetters.Add(letters[i]);

                    }
                }

            }

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    M[row, col] = (char)UniqueLetters[row * 5 + col];

                }

            }
            return M;

        }

        public string Analyse(string largeCipher)
        {


            throw new NotImplementedException();
        }





        /* public string Decrypt(string cipherText, string key)
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
         }*/




        /* public string edigraph(char[,] arr, char a, char b)
         {
             (int ra, int ca) = findpso(arr, a);
             (int rb, int cb) = findpso(arr, b);

             if (ra == rb)
                 return $"{arr[ra, (ca + 1 )% 5]}{ arr[rb,(cb + 1 )% 5]}";
             else if (ca == cb)
                 return $"{arr[(ra + 1) % 5, ca ]}{arr[(rb + 1) % 5, cb]}";
             else
                 return $"{arr[ra,cb]}{arr[rb,ca]}";

         }*/

        /* public string ddigraph(char[,] arr, char a, char b)
         {
             (int ra, int ca) = findpso(arr, a);
             (int rb, int cb) = findpso(arr, b);

             if (ra == rb)
                 return $"{arr[ra, (ca - 1+5) % 5]}{arr[rb, (cb - 1+5) % 5]}";
             else if (ca == cb)
                 return $"{arr[(ra - 1+5) % 5, ca]}{arr[(rb - 1+5) % 5, cb]}";
             else
                 return $"{arr[ra, cb]}{arr[rb, ca]}";

         }*/
        /* public string Encrypt(string plainText, string key)
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
         }*/







    }

    
}
   
