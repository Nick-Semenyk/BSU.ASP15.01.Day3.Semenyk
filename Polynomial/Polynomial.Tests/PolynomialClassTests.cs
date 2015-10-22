﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Polynomial;

namespace Polynomial.Tests
{
    [TestFixture]
    class PolynomialClassTests
    {
        [TestCase(new double[] { 0, 2, 4, -1}, 0, 0, 1E-6)]
        [TestCase(new double[] { 0, 2, 4, -1 }, 1, 5, 1E-6)]
        [TestCase(new double[] { 0, 2, 4, -1 }, 2, 12, 1E-6)]
        [TestCase(new double[] { 0, 2, 4, -1 }, 33.33, -32515.711, 0.5)]
        public void CalculateTests(double[] coefficients, double variable, double result, double eps)
        {
            Assert.AreEqual(new PolynomialClass(coefficients, eps).Calculate(variable), result, eps);
            // return new PolynomialClass(eps, coefficients).Calculate(variable);
        }

        //[Test]
        //public void OperatorAddTests()
        //{
        //    PolynomialClass a = new PolynomialClass(1E-6, new double[] { 0, 2, 4, -1 });
        //    PolynomialClass b = new PolynomialClass(1E-6, new double[] { -14, 8, 0, 3, 9 });
        //    Assert.AreEqual(a + b, new PolynomialClass(1E-6, new double[] {-14, 10, 4, 2, 9 }));
        //}

        //new PolynomClass sum will have coefficients equals sum of arrays of coefficients
        [TestCase(new double[] { 3, 2, -78, 0, 5 }, new double[] { 0, 0, 9, -21 }, 1E-6)]
        [TestCase(new double[] { 0, -1 }, new double[] { 10, 1, 999 }, 1E-6)]
        [TestCase(new double[] { 0.5, -78.991, 0, -0.41 }, new double[] { -43.8, 21.21, 99.1 }, 1E-6)]
        [TestCase(new double[] { 0.5, -78.991, 0, -0.41 }, new double[] { -0.5, 78, -99.1 }, 1)]
        public void OperatorAddTests(double[] firstCoefficients, double[] secondCoefficients, double eps)
        {
            PolynomialClass firstSummand = new PolynomialClass(firstCoefficients, eps);
            PolynomialClass secondSummand = new PolynomialClass(secondCoefficients, eps);
            double[] sumArray = new double[Math.Max(firstCoefficients.Count(), secondCoefficients.Count())];
            for (int i = 0; i < sumArray.Count(); i++)
            {
                if (i < firstCoefficients.Count() && i < secondCoefficients.Count())
                {
                    sumArray[i] = firstCoefficients[i] + secondCoefficients[i];
                }
                if (i > firstCoefficients.Count())
                {
                    sumArray[i] = secondCoefficients[i];
                }
                if (i > secondCoefficients.Count())
                {
                    sumArray[i] = firstCoefficients[i];
                }
            }
            PolynomialClass sum = new PolynomialClass(sumArray, eps);
            Assert.AreEqual(sum, firstSummand + secondSummand);
        }

        [TestCase(new double[] { 3, 2, -78, 0, 5 }, new double[] { 0, 0, 9, -21 }, 1E-6)]
        [TestCase(new double[] { 0, -1 }, new double[] { 10, 1, 999 }, 1E-6)]
        [TestCase(new double[] { 0.5, -78.991, 0, -0.41 }, new double[] { -43.8, 21.21, 99.1 }, 1E-6)]
        [TestCase(new double[] { 0.5, -78.991, 0, -0.41 }, new double[] { -0.5, 78, -99.1 }, 1)]
        public void OperatorSubtractTests(double[] firstCoefficients, double[] secondCoefficients, double eps)
        {
            PolynomialClass firstSummand = new PolynomialClass(firstCoefficients, eps);
            PolynomialClass secondSummand = new PolynomialClass(secondCoefficients, eps);
            double[] sumArray = new double[Math.Max(firstCoefficients.Count(), secondCoefficients.Count())];
            for (int i = 0; i < sumArray.Count(); i++)
            {
                if (i < firstCoefficients.Count() && i < secondCoefficients.Count())
                {
                    sumArray[i] = firstCoefficients[i] - secondCoefficients[i];
                }
                if (i > firstCoefficients.Count())
                {
                    sumArray[i] = secondCoefficients[i];
                }
                if (i > secondCoefficients.Count())
                {
                    sumArray[i] = firstCoefficients[i];
                }
            }
            PolynomialClass sum = new PolynomialClass(sumArray, eps);
            Assert.AreEqual(sum, firstSummand - secondSummand);
        }
        
    //    [TestCase(new double[] { 3, 2, -78, 0, 5 }, new double[] { 0, 0, 9, -21 }, 1E-6)]
    //    [TestCase(new double[] { 0, -1 }, new double[] { 10, 1, 999 }, 1E-6)]
    //    [TestCase(new double[] { 0.5, -78.991, 0, -0.41 }, new double[] { -43.8, 21.21, 99.1 }, 1E-6)]
    //    [TestCase(new double[] { 0.5, -78.991, 0, -0.41 }, new double[] { -0.5, 78, -99.1 }, 1)]
    //    public void OperatorMultyplyTests(double[] firstCoefficients, double[] secondCoefficients, double eps)
    //    {
    //        PolynomialClass firstSummand = new PolynomialClass(firstCoefficients, eps);
    //        PolynomialClass secondSummand = new PolynomialClass(secondCoefficients, eps);
    //        double[] sumArray = new double[Math.Max(firstCoefficients.Count(), secondCoefficients.Count())];

    //        for (int i = 0; i < sumArray.Count(); i++)
    //        {
    //            if (i < firstCoefficients.Count() && i < secondCoefficients.Count())
    //            {
    //                sumArray[i] = firstCoefficients[i] - secondCoefficients[i];
    //            }
    //            if (i > firstCoefficients.Count())
    //            {
    //                sumArray[i] = secondCoefficients[i];
    //            }
    //            if (i > secondCoefficients.Count())
    //            {
    //                sumArray[i] = firstCoefficients[i];
    //            }
    //        }
    //        PolynomialClass sum = new PolynomialClass(sumArray, eps);
    //        Assert.AreEqual(sum, firstSummand - secondSummand);
    //    }
    //}
}
