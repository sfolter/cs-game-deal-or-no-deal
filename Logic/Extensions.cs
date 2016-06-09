using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DealOrNo.Logic
{
    static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            T value;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);    
                //Fisher–Yates
                value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

       

    }
}
