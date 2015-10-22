using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polynomial;


namespace PolynomialConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PolynomialClass p1 = new PolynomialClass(coefficients: new double[ ] { 10, 0, 0, -2.5, 9});
            PolynomialClass p2 = new PolynomialClass(-11.11111111, 3, eps: 1E-4);
            PolynomialClass p3 = p1 + p2;
            PolynomialClass p4 = p1 + p1;
            PolynomialClass p5 = p1 - p2;
            PolynomialClass p6 = p1 - p1;
            PolynomialClass p7 = p1*p2;
            p6 = p6 + p6;
            p6 = p6 - p6;
            for (int i = -2; i<3; i++)
            {
                Console.WriteLine(p1.Calculate(i));//10-2.5x^3+9x^4
            }
            Console.WriteLine();
            for (int i = -2; i < 3; i++)
            {
                Console.WriteLine(p2.Calculate(i));//-11.11111x^3
            }
            Console.WriteLine();
            for (int i = -2; i < 3; i++)
            {
                Console.WriteLine(p3.Calculate(i));//10-13.6111x^3+9x^4
            }
            Console.WriteLine();
            for (int i = -2; i < 3; i++)
            {
                Console.WriteLine(p4.Calculate(i));//20-5x^3+18x^4
            }
            for (int i = -2; i < 3; i++)
            {
                Console.WriteLine(p5.Calculate(i));//10+8.6111x^3+9x^4
            }
            for (int i = -2; i < 3; i++)
            {
                Console.WriteLine(p6.Calculate(i));//0
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
