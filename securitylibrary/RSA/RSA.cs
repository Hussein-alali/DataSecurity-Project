using SecurityLibrary.AES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public static long ModOfPower(int b, int p, int m)  // calc the ModOfPower
        {
            if (p == 1) return b % m;
            long theResult = ModOfPower(b, p / 2, m);
            if (p % 2 == 0)
            {
                return (theResult * theResult) % m;
            }
            else
            {
                return (theResult * theResult * (b % m)) % m;
            }

        }

        public int Encrypt(int p, int q, int M, int e)
        {
            //throw new NotImplementedException();
            int n = p * q;
            Console.WriteLine(n);
            return (int)ModOfPower(M, e, n);

        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int n = p * q;
            Console.WriteLine(n);
            int qn = (p - 1) * (q - 1);
            Console.WriteLine(qn);
            ExtendedEuclid extended = new ExtendedEuclid();
            int d = extended.GetMultiplicativeInverse(e, qn);
            Console.WriteLine(d);
            return (int)ModOfPower(C, d, n);

        }
    }
}