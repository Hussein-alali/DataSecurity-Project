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
            throw new NotImplementedException();
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

            throw new NotImplementedException();
        }
        public string Encrypt(string plainText, string key)
        {
            key= key.ToUpper();
            plainText= plainText.ToUpper();

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

            string text= plainText.Replace("J","I");
            StringBuilder preptxt = new StringBuilder();

            for (int i=0;i < text.Length; i++)
            {
                if (text[i] == preptxt[i - 1])
                {
                    preptxt.Append("X");
                }
                preptxt.Append(text[i]);     
            }
            if (preptxt.Length % 2 != 0)
            {
                preptxt.Append("X");
            }

            string txt = preptxt.ToString();
            ///////////////////////////////////////////////////////////////////////////////////////////
            ///

            ///////////////////////////////////////// encryption stage

            StringBuilder ciphertext= new StringBuilder();

            for (int i= 0;i < txt.Length;)
            {
                ciphertext.Append(txt[i]);
            }


            return plainText;
            //throw new NotImplementedException();
        }
    }
}
