using System;
namespace lab_2
{
    public class ClassBaseSys
    {
        public ClassBaseSys()
        {
        }
        public string ToEx(int i)
        {
            int k = 0;
            int litEng = 26;
            int[] Arr = new int[100];
            while (i > litEng-1)
            {
                Arr[k] = i / litEng - 1;
                k++;
                i = i % litEng;
            }
            Arr[k] = i;
            string res = "";
            for (int j = 0; j <= k; j++)
            {
                res = res + ((char)('A' + Arr[j])).ToString();
            }
            return res;
        }
        public int FromEx(string s)
        {
            int litEng = 26;
            char[] chArray = s.ToCharArray();
            int l = chArray.Length;
            int res = 0;
            for (int i = l - 2; i >= 0; i--)
            {
                res = res + (((int)(chArray[i] - (int)'A') + 1) * Convert.ToInt32(Math.Pow(litEng, l - i - 1)));
            }
            res = res + ((int)chArray[l - 1] - (int)'A');
            return res;
        }
    }
}
