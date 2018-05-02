using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO_RGZ_V8
{
    class TMember
    {
        public int FCoeff { get; set; }
        public int FDegree { get; set; }

        public TMember(int coeff, int degree)
        {
            this.FCoeff = coeff;
            this.FDegree = degree;
        }

        public TMember(int coeff)
        {
            this.FCoeff = coeff;
            this.FDegree = 0;
        }

        public TMember()
        {
            this.FCoeff = 0;
            this.FDegree = 0;
        }

        public static bool operator ==(TMember p1, TMember p2)
        {
            return p1.FDegree == p2.FDegree && p1.FCoeff == p2.FCoeff;
        }

        public static bool operator !=(TMember p1, TMember p2)
        {
            return p1.FDegree != p2.FDegree && p1.FCoeff != p2.FCoeff;
        }

        public TMember Derivative()
        {
            return new TMember(this.FCoeff * (this.FDegree - 1), this.FDegree - 1);
        }

        public int Calculate(int x)
        {
            return Convert.ToInt32(Math.Pow(x, this.FDegree)) * this.FCoeff;
        }

        override public String ToString()
        {
            return this.FCoeff + "x^" + this.FDegree;
        }
    }
}
