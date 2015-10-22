using System;
using System.Collections.Generic;
using System.IO;
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
                if (item.Degree == degree)
                {
                    return item.Coefficient;
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
            if (arrayIndex > 0)
            {
                Array.Resize(ref _polynomial, arrayIndex);
            }
            else
            {
                Array.Resize(ref _polynomial, 1);
            }
        }

        /// <summary>
        /// Create polynomial with single item.
        /// </summary>
        /// <param name="item">PolynomItem variable with degree and coefficient</param>
        /// <param name="eps">Coefficients which absolute value less than eps will be ignored.</param>
        public PolynomialClass(PolynomItem item, double eps = 1E-6)
        {
            if (item.Degree < 0)
            {
                throw new ArgumentException();
            }
            _polynomial = new PolynomItem[1];
            if (Math.Abs(item.Coefficient) < eps)
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
            int arrayIndex = 0;
            for (int i = 0; i<items.Count(); i++)
            {
                if (Math.Abs(items[i].Coefficient) > eps)
                {
                    _polynomial[arrayIndex] = items[i];
                    arrayIndex++;
                }
            }
            if (arrayIndex > 0)
            {
                Array.Resize(ref _polynomial, arrayIndex);
            }
            else
            {
                Array.Resize(ref _polynomial, 1);
            }
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
                result += Math.Pow(variable, item.Degree)*item.Coefficient;
            }
            return result;
        }

        public static PolynomialClass operator +(PolynomialClass first, PolynomialClass second)
        {
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }
            if (first._polynomial.Count() == 0)
            {
                return new PolynomialClass(1E-6, second._polynomial);
            }
            if (second._polynomial.Count() == 0)
            {
                return first;
            }
            PolynomItem[] resultArray = new PolynomItem[first._polynomial.Count() + second._polynomial.Count()];
            int firstIndex, secondIndex, maxDegree;
            firstIndex = 0;
            secondIndex = 0;
            maxDegree = Math.Max(first._polynomial.Last().Degree, 
                second._polynomial.Last().Degree);
            int resultIndex = 0;
            int currentFirstDegree = 0, currentSecondDegree = 0;
            for (int i = 0; i <= maxDegree; i++)
            {
                if (firstIndex < first._polynomial.Count())
                    currentFirstDegree = first._polynomial[firstIndex].Degree;
                else
                    //first polynomial is calculated
                    currentFirstDegree = -1;
                if (secondIndex < second._polynomial.Count())
                    currentSecondDegree = second._polynomial[secondIndex].Degree;
                else
                    //second polynomial is calculated
                    currentSecondDegree = -2;
                if (currentFirstDegree == currentSecondDegree)
                {
                    resultArray[resultIndex] = new PolynomItem(currentFirstDegree,
                        first._polynomial[firstIndex].Coefficient +
                        second._polynomial[secondIndex].Coefficient);
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
            maxDegree = Math.Max(first._polynomial.Last().Degree,
                second._polynomial.Last().Degree);
            int resultIndex = 0;
            int currentFirstDegree = 0, currentSecondDegree = 0;
            for (int i = 0; i <= maxDegree; i++)
            {
                if (firstIndex < first._polynomial.Count())
                    currentFirstDegree = first._polynomial[firstIndex].Degree;
                else
                    //first polynomial is calculated
                    currentFirstDegree = -1;
                if (secondIndex < second._polynomial.Count())
                    currentSecondDegree = second._polynomial[secondIndex].Degree;
                else
                    //second polynomial is calculated
                    currentSecondDegree = -2;
                if (currentFirstDegree == currentSecondDegree)
                {
                    resultArray[resultIndex] = new PolynomItem(currentFirstDegree,
                        first._polynomial[firstIndex].Coefficient -
                        second._polynomial[secondIndex].Coefficient);
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
                        - second._polynomial[secondIndex].Coefficient);
                    resultIndex++;
                    secondIndex++;
                }
            }
            return new PolynomialClass(1E-6, resultArray);
        }

        public static PolynomialClass operator *(PolynomialClass first, PolynomialClass second)
        {
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }
            PolynomialClass result = new PolynomialClass(0,0);
            foreach (PolynomItem itemFirst in first._polynomial)
            {
                PolynomItem[] resultArray = new PolynomItem[second._polynomial.Count()];
                int resultIndex = 0;
                foreach(PolynomItem itemSecond in second._polynomial)
                {
                    resultArray[resultIndex] = new PolynomItem(
                        itemFirst.Degree + itemSecond.Degree,
                        itemFirst.Coefficient * itemSecond.Coefficient);
                    resultIndex++;
                }
                result = result + new PolynomialClass(1E-6, resultArray);
            }
            return result;
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
                if (Math.Abs(comparPolynom.CoefficientNearDegree(item.Degree) - item.Coefficient) > 1E-6)
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
                if (Math.Abs(polynomial.CoefficientNearDegree(item.Degree) - item.Coefficient) > 1E-6)
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
        public readonly double Coefficient;
        public readonly int Degree;

        public PolynomItem(int degree, double coefficient)
        {
            Coefficient = coefficient;
            Degree = degree;
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
            if (comparPolynomItem.Degree == Degree &&
                (Math.Abs(comparPolynomItem.Coefficient - Coefficient) < 1E-6))
            {
                return true;
            }
            return false;
        }

        public bool Equals(PolynomItem obj)
        {
            if (obj.Degree == Degree &&
                Math.Abs(obj.Coefficient - Coefficient) < 1E-6)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int truncatedCoefficient = (int)Math.Floor(Coefficient);
            double fractionalPart = Coefficient - truncatedCoefficient;
            int result = truncatedCoefficient * Degree + 11;
            result = result + (int)(fractionalPart*(truncatedCoefficient + Degree + result));
            return result;
        }
    }
}
