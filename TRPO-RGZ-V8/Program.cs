using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO_RGZ_V8
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSum("1x^0", "1x^0", "2x^0");
            TestSum("1x^0", "-1x^0", "0x^0");
            TestSum("-1x^0", "-1x^0", "-2x^0");
            TestSum("2x^2-1x^0", "4x^2-1x^0", "6x^2-2x^0");
            TestSum("7x^8+2x^2-1x^0", "-9x^4+4x^2-1x^0", "7x^8-9x^4+6x^2-2x^0");
            Console.ReadKey();
        }

        static public void TestSum(String poly1, String poly2, String answer)
        {
            string testDescr = poly1 + " + " + poly2;
            Console.Write("\n___________________________________________________________________________________\nTestSum: " + testDescr);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" ? ");
            Console.ForegroundColor = ConsoleColor.White;

            //initialize polynoms
            TPoly p1 = new TPoly(TPoly.StringToMap(poly1));
            TPoly p2 = new TPoly(TPoly.StringToMap(poly2));

            // calculation
            var test = p1 + p2;

            // write result
            Console.Write(test.PolynomToString());

            // checking
            if (test.PolynomToString() == answer)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\t>>> ok \n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t>>> fail \n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
