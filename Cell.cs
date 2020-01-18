using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class Cell
    {
        public string value_1;
        public string exp;
        public List<string> dependents = new List<string>();
        public Cell()
        {
            value_1 = "0";
            exp = " ";
        }
        public string getName(int colum, int row)
        {
            string temp = null;
            int c = 65;
            int r = 48;
            temp += (char)(colum + c);
            temp += (char)(row + r);
            return temp;
        }
    }
}
