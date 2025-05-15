using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        /// 

        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            List<long> tmp1 = new List<long>();

            int ya = power_mod(alpha, y, q);
            int key = power_mod(y, k, q);
            int c1 = power_mod(alpha, k, q);
            int c2 = (key * m) % q;
            tmp1.Add(c1);
            tmp1.Add(c2);


            return tmp1;

            //throw new NotImplementedException();

        } // encrypt

        public int Decrypt(int c1, int c2, int x, int q)
        {
            int key = power_mod(c1, x, q);
            int inv = ModInverse(key, q);
            int plain = (inv * c2) % q;
            return plain;
            //throw new NotImplementedException();

        }//decrypt

        private static int power_mod(int n1, int n2, int n3)
        {
            int tmp1 = 1;
            for (int i = 0; i < n2; i++)
            {
                tmp1 = (tmp1 * n1) % n3;
            }
            return tmp1;
        } // power with mod


        public static int ModInverse(int a, int m)
        {
            int m0 = m, t, qz;
            int x0 = 0, x1 = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                qz = a / m;
                t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - qz * x0;
                x1 = t;
            }
            if (x1 < 0)
                x1 += m0;
            return x1;
        } // mod inverse 
    }
}
