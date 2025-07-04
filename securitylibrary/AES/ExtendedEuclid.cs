﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {

            int A1, A2, A3, B1, B2, B3, Temp1, Temp2, Temp3, Q;
            A1 = 1;
            A2 = 0;
            A3 = baseN;
            B1 = 0;
            B2 = 1;
            B3 = number;
            while (true)
            {
                if (B3 == 0)
                {
                    return -1;
                }
                else if (B3 == 1)
                    return ((B2 % baseN) + baseN) % baseN;

                Q = A3 / B3;
                Temp1 = A1 - (Q * B1);
                Temp2 = A2 - (Q * B2);
                Temp3 = A3 - (Q * B3);
                A1 = B1;
                A2 = B2;
                A3 = B3;
                B1 = Temp1;
                B2 = Temp2;
                B3 = Temp3;
            }
           // throw new NotImplementedException();
        }
    }
}
