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

            //TestMult
            log(ConsoleColor.Cyan, Consts.startTestMult);
            TestMult("1x^0", "1x^0", "1x^0");
            TestMult("1x^0", "-1x^0", "-1x^0");
            TestMult("-1x^0", "-1x^0", "1x^0");
            TestMult("2x^2-1x^0", "4x^2-1x^0", "8x^4-6x^2+1x^0");
            TestMult("7x^8+2x^2-1x^0", "-9x^4+4x^2-1x^0", "-63x^12+28x^10-7x^8-18x^6+17x^4-6x^2+1x^0");
            log(ConsoleColor.Cyan, Consts.finishTestMult);

            //TestEq
            log(ConsoleColor.Cyan, Consts.startTestEq);
            TestEq("1x^0", "1x^0", true);
            TestEq("1x^0", "-1x^0", false);
            TestEq("-1x^0", "-1x^0", true);
            TestEq("4x^2-1x^0", "4x^2-1x^0", true);
            TestEq("7x^8+2x^2-1x^0", "-9x^4+4x^2-1x^0", false);
            log(ConsoleColor.Cyan, Consts.finishTestEq);

            //TestDerr
            log(ConsoleColor.Cyan, Consts.startTestDerr);
            TestDerr("1x^0", "0x^0");
            TestDerr("-1x^0", "0x^0");
            TestDerr("7x^1", "7x^0");
            TestDerr("4x^2-1x^0", "8x^1");
            TestDerr("7x^8+2x^2-1x^0", "56x^7+4x^1");
            log(ConsoleColor.Cyan, Consts.finishTestDerr);

            //TestVal
            log(ConsoleColor.Cyan, Consts.startTestVal);
            TestVal("1x^0", 7, 1);
            TestVal("-1x^0", 7, -1);
            TestVal("7x^1", 7, 49);
            TestVal("4x^2-1x^0", 7, 195);
            TestVal("7x^8+2x^2-1x^0", 7, 40353704);
            log(ConsoleColor.Cyan, Consts.finishTestVal);

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

        static public void TestMult(String poly1, String poly2, String answer)
        {
            string testDescr = poly1 + " * " + poly2;
            Console.Write("\n___________________________________________________________________________________\nTestMult: " + testDescr);
            log(ConsoleColor.Magenta, " ? ");

            //initialize polynoms
            TPoly p1 = new TPoly(TPoly.StringToMap(poly1));
            TPoly p2 = new TPoly(TPoly.StringToMap(poly2));

            // calculation
            var test = p1 * p2;

            // write result
            Console.Write(test.PolynomToString());

            // checking
            if (test.PolynomToString() == answer) log(ConsoleColor.Green, "\t>>> ok \n");
            else log(ConsoleColor.Red, "\t>>> fail \n");
        }

        static public void TestEq(String poly1, String poly2, bool answer)
        {
            string testDescr = poly1 + " == " + poly2;
            Console.Write("\n___________________________________________________________________________________\nTestEquality: " + testDescr);
            log(ConsoleColor.Magenta, " ? ");

            //initialize polynoms
            TPoly p1 = new TPoly(TPoly.StringToMap(poly1));
            TPoly p2 = new TPoly(TPoly.StringToMap(poly2));

            // calculation
            var test = p1 == p2;

            // write result
            Console.Write(test);

            // checking
            if (test == answer) log(ConsoleColor.Green, "\t>>> ok \n");
            else log(ConsoleColor.Red, "\t>>> fail \n");
        }

        static public void TestDerr(String poly, String answer)
        {
            string testDescr = "(" + poly + ")'";
            Console.Write("\n___________________________________________________________________________________\nTestDerr: " + testDescr);
            log(ConsoleColor.Magenta, " ? ");

            //initialize polynom
            TPoly p = new TPoly(TPoly.StringToMap(poly));

            // calculation
            var test = p.Derivative();

            // write result
            Console.Write(test.PolynomToString());

            // checking
            if (test.PolynomToString() == answer) log(ConsoleColor.Green, "\t>>> ok \n");
            else log(ConsoleColor.Red, "\t>>> fail \n");
        }

        static public void TestVal(String poly, int x, int answer)
        {
            string testDescr = "f(x) = " + poly + ", x = " + x;
            Console.Write("\n___________________________________________________________________________________\nTestVal: " + testDescr);
            log(ConsoleColor.Magenta, " ? ");

            //initialize polynom
            TPoly p = new TPoly(TPoly.StringToMap(poly));

            // calculation
            var test = p.Value(x);

            // write result
            Console.Write(test);

            // checking
            if (test == answer) log(ConsoleColor.Green, "\t>>> ok \n");
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
