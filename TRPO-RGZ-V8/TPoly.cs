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
        public static TPoly operator *(TPoly p1, TPoly p2)
        {
            SortedDictionary<int, TMember> tempPolynom;
            TPoly result = new TPoly(TPoly.StringToMap("0x^0"));

            foreach (KeyValuePair<int, TMember> entry1 in p2.polynom)
            {
                tempPolynom = new SortedDictionary<int, TMember>();
                foreach (KeyValuePair<int, TMember> entry2 in p1.polynom)
                {
                    int coeff = entry1.Value.FCoeff * entry2.Value.FCoeff;
                    int degree = entry1.Key + entry2.Key;
                    tempPolynom.Add(degree, new TMember(coeff, degree));
                }
                result = result + (new TPoly(tempPolynom));
            }

            return result;
        }

        public static bool operator ==(TPoly p1, TPoly p2)
        {
            foreach (KeyValuePair<int, TMember> entry in p2.polynom)
            {
                if (p1.polynom.ContainsKey(entry.Key))
                {
                    if (p1.polynom[entry.Key] != entry.Value) return false;
                }
                else return false;
            }
            return true;
        }

        public static bool operator !=(TPoly p1, TPoly p2)
        {
            foreach (KeyValuePair<int, TMember> entry in p2.polynom)
            {
                if (p1.polynom.ContainsKey(entry.Key))
                {
                    if (p1.polynom[entry.Key] != entry.Value) return true;
                }
                else return true;
            }
            return false;
        }

        public TPoly Derivative()
        {
            SortedDictionary<int, TMember> result = new SortedDictionary<int, TMember>();
            foreach (KeyValuePair<int, TMember> entry in this.polynom)
                result.Add(entry.Key, entry.Value.Derivative());
            return new TPoly(result);
        }

        public int Value(int x)
        {
            int result = 0;
            foreach (KeyValuePair<int, TMember> entry in this.polynom)
                result += entry.Value.Calculate(x);
            return result;
        }

        public TMember GetMember(int i)
        {
            TMember result;
            int j = 0;
            foreach (KeyValuePair<int, TMember> entry in this.polynom.Reverse())
            {
                if (j == i) return entry.Value;
                j++;
            }
            throw new Exception("Polinom index out of range!");
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
