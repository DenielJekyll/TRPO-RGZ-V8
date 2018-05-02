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
            // TestSum
            log(ConsoleColor.Cyan, Consts.startTestSum);
            TestSum("1x^0", "1x^0", "2x^0");
            TestSum("1x^0", "-1x^0", "0x^0");
            TestSum("-1x^0", "-1x^0", "-2x^0");
            TestSum("2x^2-1x^0", "4x^2-1x^0", "6x^2-2x^0");
            TestSum("7x^8+2x^2-1x^0", "-9x^4+4x^2-1x^0", "7x^8-9x^4+6x^2-2x^0");
            log(ConsoleColor.Cyan, Consts.finishTestSum);

            //TestSub
            log(ConsoleColor.Cyan, Consts.startTestSub);
            TestSub("1x^0", "1x^0", "0x^0");
            TestSub("1x^0", "-1x^0", "2x^0");
            TestSub("-1x^0", "-1x^0", "0x^0");
            TestSub("2x^2-1x^0", "4x^2-1x^0", "-2x^2");
            TestSub("7x^8+2x^2-1x^0", "-9x^4+4x^2-1x^0", "7x^8+9x^4-2x^2");
            log(ConsoleColor.Cyan, Consts.finishTestSub);

            Console.ReadKey();
        }

        static public void TestSum(String poly1, String poly2, String answer)
        {
            string testDescr = poly1 + " + " + poly2;
            Console.Write("\n___________________________________________________________________________________\nTestSum: " + testDescr);
            log(ConsoleColor.Magenta, " ? ");

            //initialize polynoms
            TPoly p1 = new TPoly(TPoly.StringToMap(poly1));
            TPoly p2 = new TPoly(TPoly.StringToMap(poly2));

            // calculation
            var test = p1 + p2;

            // write result
            Console.Write(test.PolynomToString());

            // checking
            if (test.PolynomToString() == answer) log(ConsoleColor.Green, "\t>>> ok \n");
            else log(ConsoleColor.Red, "\t>>> fail \n");
        }

        static public void TestSub(String poly1, String poly2, String answer)
        {
            string testDescr = poly1 + " - " + poly2;
            Console.Write("\n___________________________________________________________________________________\nTestSub: " + testDescr);
            log(ConsoleColor.Magenta, " ? ");

            //initialize polynoms
            TPoly p1 = new TPoly(TPoly.StringToMap(poly1));
            TPoly p2 = new TPoly(TPoly.StringToMap(poly2));

            // calculation
            var test = p1 - p2;

            // write result
            Console.Write(test.PolynomToString());

            // checking
            if (test.PolynomToString() == answer) log(ConsoleColor.Green, "\t>>> ok \n");
            else log(ConsoleColor.Red, "\t>>> fail \n");
        }

        static public void log(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
