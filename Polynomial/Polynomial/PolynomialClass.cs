using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    public class PolynomialClass
    {
        private PolynomItem[] _polynomial;

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
                _polynomial[0].coefficient = 0;
                _polynomial[0].degree = 0;
            }
            else
            {
                _polynomial[0].coefficient = coefficient;
                _polynomial[0].degree = degree;
            }
        }

        /// <summary>
        /// Create polynomial with two items
        /// </summary>
        /// <param name="firstCoefficient">Coefficient near first degree</param>
        /// <param name="firstDegree">Degree of first item</param>
        /// <param name="secondCoefficient">Coefficient near second degree</param>
        /// <param name="secondDegree">Degree of second item</param>
        /// <param name="eps">Coefficients which absolute value less than eps will be ignored.</param>
        public PolynomialClass(double firstCoefficient, int firstDegree,
            double secondCoefficient, int secondDegree, double eps = 1E-6)
        {
            if (firstDegree < 0 || secondDegree < 0)
            {
                throw new ArgumentException();
            }
            _polynomial = new PolynomItem[2];
            if (Math.Abs(firstCoefficient) < eps)
            {
                _polynomial[0].coefficient = 0;
                _polynomial[0].degree = 0;
            }
            else
            {
                _polynomial[0].coefficient = firstCoefficient;
                _polynomial[0].degree = firstDegree;
            }
            if (Math.Abs(secondCoefficient) < eps)
            {
                _polynomial[1].coefficient = 0;
                _polynomial[1].degree = 0;
            }
            else
            {
                _polynomial[1].coefficient = secondCoefficient;
                _polynomial[1].degree = secondDegree;
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

        //!
        /// <summary>
        /// Calculate polynomial with defined parameter
        /// </summary>
        /// <param name="variable"></param>
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

        //!
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
            return true;
        }
    }

    public struct PolynomItem
    {
        public double coefficient;
        public int degree;

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
    }
}
