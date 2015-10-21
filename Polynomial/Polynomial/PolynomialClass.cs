using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    public class PolynomialClass
    {
        private readonly PolynomItem[] _polynomial;

        /// <summary>
        /// Returns coefficient of item of specified degree. 
        /// </summary>
        /// <param name="degree">Degree of item</param>
        /// <returns>Coefficient of item of specified degree</returns>
        public double CoefficientNearDegree(int degree)
        {
            foreach(PolynomItem item in _polynomial)
            {
                if (item.degree == degree)
                {
                    return item.coefficient;
                }
            }
            return 0;
        }

        /// <summary>
        /// Create polynomial with single item.
        /// </summary>
        /// <param name="degree">Degree of single item</param>
        /// <param name="coefficient">Coefficient near single item</param>
        /// <param name="eps">Coefficients which absolute value less than eps will be ignored.</param>
        public PolynomialClass(double coefficient, int degree, double eps = 1E-6)
        {
            if (degree < 0)
            {
                throw new ArgumentException();
            }
            _polynomial = new PolynomItem[1];
            if (Math.Abs(coefficient)<eps)
            {
                _polynomial[0] = new PolynomItem();
            }
            else
            {
                _polynomial[0] = new PolynomItem(degree, coefficient);
            }
        }

        /// <summary>
        /// Create polynomial with coefficients in parameters. 
        /// Number of parameter means degree which will have coefficient equals this parameter.
        /// </summary>
        /// <param name="eps">Coefficients which absolute value less than eps will be ignored.</param>
        /// <param name="coefficients">Coefficients near item with degree, equals number of parameter, starting from 0</param>
        public PolynomialClass(double eps = 1E-6, params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException();
            }
            if (coefficients.Count() == 0)
            {
                throw new ArgumentNullException();
            }
            _polynomial = new PolynomItem[coefficients.Count()];
            int arrayIndex = 0;
            for (int i = 0; i<coefficients.Count(); i++)
            {
                if (Math.Abs(coefficients[i]) > eps)
                {
                    _polynomial[arrayIndex] = new PolynomItem(i, coefficients[i]);
                    arrayIndex++;
                }
            }
            Array.Resize(ref _polynomial, arrayIndex);
        }

        /// <summary>
        /// Create polynomial with single item.
        /// </summary>
        /// <param name="item">PolynomItem variable with degree and coefficient</param>
        /// <param name="eps">Coefficients which absolute value less than eps will be ignored.</param>
        public PolynomialClass(PolynomItem item, double eps = 1E-6)
        {
            if (item.degree < 0)
            {
                throw new ArgumentException();
            }
            _polynomial = new PolynomItem[1];
            if (Math.Abs(item.coefficient) < eps)
            {
                _polynomial[0] = new PolynomItem();
            }
            else
            {
                _polynomial[0] = item;
            }
        }

        private PolynomialClass(double eps = 1E-6, params PolynomItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }
            if (items.Count() == 0)
            {
                throw new ArgumentNullException();
            }
            _polynomial = new PolynomItem[items.Count()];
            int index = 0;
            for (int i = 0; i<items.Count(); i++)
            {
                if (Math.Abs(items[i].coefficient) > eps)
                {
                    _polynomial[index] = items[i];
                    index++;
                }
            }
            Array.Resize(ref _polynomial, index);
        }

        /// <summary>
        /// Calculate polynomial with defined parameter
        /// </summary>
        /// <param name="variable">Indeterminate or variable of polynomial</param>
        /// <returns>Result of calculation</returns>
        public double Calculate(double variable)
        {
            double result = 0;
            foreach (PolynomItem item in _polynomial)
            {
                result += Math.Pow(variable, item.degree)*item.coefficient;
            }
            return result;
        }

        public static PolynomialClass operator +(PolynomialClass first, PolynomialClass second)
        {
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }
            PolynomItem[] resultArray = new PolynomItem[first._polynomial.Count() + second._polynomial.Count()];
            int firstIndex, secondIndex, maxDegree;
            firstIndex = 0;
            secondIndex = 0;
            maxDegree = Math.Max(first._polynomial.Last().degree, 
                second._polynomial.Last().degree);
            int resultIndex = 0;
            int currentFirstDegree = 0, currentSecondDegree = 0;
            for (int i = 0; i <= maxDegree; i++)
            {
                if (firstIndex < first._polynomial.Count())
                    currentFirstDegree = first._polynomial[firstIndex].degree;
                else
                    //first polynomial is calculated
                    currentFirstDegree = -1;
                if (secondIndex < second._polynomial.Count())
                    currentSecondDegree = second._polynomial[secondIndex].degree;
                else
                    //second polynomial is calculated
                    currentSecondDegree = -2;
                if (currentFirstDegree == currentSecondDegree)
                {
                    resultArray[resultIndex] = new PolynomItem(currentFirstDegree,
                        first._polynomial[firstIndex].coefficient +
                        second._polynomial[secondIndex].coefficient);
                    resultIndex++;
                    firstIndex++;
                    secondIndex++;
                    continue;
                }
                if (currentFirstDegree == i)
                {
                    resultArray[resultIndex] = first._polynomial[firstIndex];
                    resultIndex++;
                    firstIndex++;
                    continue;
                }
                if (currentSecondDegree == i)
                {
                    resultArray[resultIndex] = second._polynomial[secondIndex];
                    resultIndex++;
                    secondIndex++;
                }
            }
            return new PolynomialClass(1E-6, resultArray);
        }

        public static PolynomialClass operator -(PolynomialClass first, PolynomialClass second)
        {
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }
            PolynomItem[] resultArray = new PolynomItem[first._polynomial.Count() + second._polynomial.Count()];
            int firstIndex, secondIndex, maxDegree;
            firstIndex = 0;
            secondIndex = 0;
            maxDegree = Math.Max(first._polynomial.Last().degree,
                second._polynomial.Last().degree);
            int resultIndex = 0;
            int currentFirstDegree = 0, currentSecondDegree = 0;
            for (int i = 0; i <= maxDegree; i++)
            {
                if (firstIndex < first._polynomial.Count())
                    currentFirstDegree = first._polynomial[firstIndex].degree;
                else
                    //first polynomial is calculated
                    currentFirstDegree = -1;
                if (secondIndex < second._polynomial.Count())
                    currentSecondDegree = second._polynomial[secondIndex].degree;
                else
                    //second polynomial is calculated
                    currentSecondDegree = -2;
                if (currentFirstDegree == currentSecondDegree)
                {
                    resultArray[resultIndex] = new PolynomItem(currentFirstDegree,
                        first._polynomial[firstIndex].coefficient -
                        second._polynomial[secondIndex].coefficient);
                    resultIndex++;
                    firstIndex++;
                    secondIndex++;
                    continue;
                }
                if (currentFirstDegree == i)
                {
                    resultArray[resultIndex] = first._polynomial[firstIndex];
                    resultIndex++;
                    firstIndex++;
                    continue;
                }
                if (currentSecondDegree == i)
                {
                    resultArray[resultIndex] = new PolynomItem(i,
                        - second._polynomial[secondIndex].coefficient);
                    resultIndex++;
                    secondIndex++;
                }
            }
            return new PolynomialClass(1E-6, resultArray);
        }



        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is PolynomialClass))
            {
                return false;
            }
            PolynomialClass comparPolynom = (PolynomialClass) obj;
            foreach(PolynomItem item in _polynomial)
            {
                if (Math.Abs(comparPolynom.CoefficientNearDegree(item.degree) - item.coefficient) > 1E-6)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Equals(PolynomialClass polynomial)
        {
            if (polynomial == null)
            {
                return false;
            }
            foreach (PolynomItem item in _polynomial)
            {
                if (Math.Abs(polynomial.CoefficientNearDegree(item.degree) - item.coefficient) > 1E-6)
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            foreach (PolynomItem item in _polynomial)
            {
                result += item.GetHashCode()*(result ^ _polynomial.Count());
            }
            return result;
        }
    }

    public struct PolynomItem
    {
        public readonly double coefficient;
        public readonly int degree;

        public PolynomItem(int degree, double coefficient)
        {
            this.coefficient = coefficient;
            this.degree = degree;
        }

        public override bool Equals(object obj)
        {
            PolynomItem comparPolynomItem;
            if (obj == null)
                return false;
            if (!(obj is PolynomItem))
            {
                return false;
            }
            comparPolynomItem = (PolynomItem)obj;
            if (comparPolynomItem.degree == degree &&
                (Math.Abs(comparPolynomItem.coefficient - coefficient) < 1E-6))
            {
                return true;
            }
            return false;
        }

        public bool Equals(PolynomItem obj)
        {
            if (obj.degree == degree &&
                Math.Abs(obj.coefficient - coefficient) < 1E-6)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int truncatedCoefficient = (int)Math.Floor(coefficient);
            double fractionalPart = coefficient - truncatedCoefficient;
            int result = truncatedCoefficient * degree + 11;
            result = result + (int)(fractionalPart*(truncatedCoefficient + degree + result));
            return result;
        }
    }
}
