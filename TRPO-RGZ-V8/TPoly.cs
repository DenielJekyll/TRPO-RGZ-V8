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
        Dictionary<int, TMember> polynom { get; }
        public TPoly(Dictionary<int, TMember> polynom)
        {
            this.polynom = polynom;
        }

        static public Dictionary<int, TMember> StringToMap(String str)
        {
            String[] coeffs = Regex.Split(str, "[+-]");
            Dictionary<int, TMember> result = new Dictionary<int, TMember>();
            String[] temp;
            if (str == "") result.Add(0, new TMember());
            else
            foreach (String coeff in coeffs)
            {
                temp = Regex.Split(coeff, "[xX^]");
                TMember newMember = new TMember(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[2]));
                result.Add(Convert.ToInt32(temp[2]), newMember);
            }
            return result;
        }
    }
}
