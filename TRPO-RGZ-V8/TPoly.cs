using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TRPO_RGZ_V8
{
    class TPoly
    {
        public SortedDictionary<int, TMember> polynom { get; }
        public TPoly(SortedDictionary<int, TMember> polynom)
        {
            this.polynom = polynom;
        }

        public int degree()
        {
            return this.polynom.Last().Key;
        }

        public int GetCoeff(int n)
        {
            return this.degree() > n ? 0 : this.polynom.ContainsKey(n) ? this.polynom[n].FCoeff : 0;
        }

        public void Clear()
        {
            this.polynom.Clear();
        }

        public static TPoly operator +(TPoly p1, TPoly p2)
        {
            SortedDictionary<int, TMember> result;
            result = p1.polynom;

            foreach (KeyValuePair<int, TMember> entry in p2.polynom)
            {
                if (result.ContainsKey(entry.Key))
                {
                    result[entry.Key].FCoeff += entry.Value.FCoeff; 
                }
                else
                {
                    result.Add(entry.Key, entry.Value);
                }
            }

            return new TPoly(result);
        }
        public static TPoly operator -(TPoly p1, TPoly p2)
        {
            SortedDictionary<int, TMember> result;
            result = p1.polynom;

            foreach (KeyValuePair<int, TMember> entry in p2.polynom)
            {
                if (result.ContainsKey(entry.Key))
                {
                    result[entry.Key].FCoeff -= entry.Value.FCoeff; 
                }
                else
                {
                    result.Add(entry.Key, new TMember(-entry.Value.FCoeff, entry.Key));
                }
            }

            return new TPoly(result);
        }

        public String PolynomToString()
        {
            String result = "";
            foreach (KeyValuePair<int, TMember> entry in this.polynom.Reverse())
            {
                if (entry.Value.FCoeff > 0) result += "+" + entry.Value.ToString();
                if (entry.Value.FCoeff < 0) result += entry.Value.ToString();
            }
            if (result.Length != 0) result = result[0] == '+' ? result.Substring(1, result.Length - 1) : result;
            return result.Length == 0 ? "0x^0" : result;
        }

        static public SortedDictionary<int, TMember> StringToMap(String str)
        {
            String[] coeffs = Regex.Split(str, "([+-]?(?:(?:\\d+x\\^\\d+)|(?:\\d+x)|(?:\\d+)|(?:x)))");
            SortedDictionary<int, TMember> result = new SortedDictionary<int, TMember>();
            String[] temp;
            if (str == "") result.Add(0, new TMember());
            else
            for (int i = 1; i < coeffs.Length - 1; i = i + 2)
                {
                    temp = Regex.Split(coeffs[i], "[xX^]");
                    TMember newMember = new TMember(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[2]));
                    result.Add(Convert.ToInt32(temp[2]), newMember);
                }
            return result;
        }
    }
}
