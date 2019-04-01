using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace polynomial
    {
        public static class Polynomial
        {
            /**
             * @brief Least squares polynomial fit. Fit a polynomial \f$p(x) = p[0] * x^{deg} + ... + p[deg]\f$ of degree \f$deg\f$
             * to points \f$(x, y)\f$. Returns a vector of coefficients \f$p\f$ that minimises the squared error.
             *
             * @param x x-coordinates of the M sample points \f$(x[i], y[i])\f$.
             * @param y y-coordinates of the sample points.
             * @param deg Degree of the fitting polynomial.
             * @return result Polynomial coefficients, highest power first.
             */
            public static array.Array Polyfit(array.Array x, array.Array y, int deg)
            {
                IntPtr xReference = x.Reference;
                IntPtr yReference = y.Reference;
                interop.DLLPolynomial.polyfit(ref xReference, ref yReference, ref deg, out IntPtr result);
                x.Reference = xReference;
                y.Reference = yReference;
                return (new array.Array(result));
            }


            /**
             * @brief Calculates the roots of a polynomial with coefficients given in \f$p\f$. The values in the rank-1 array
             * \f$p\f$ are coefficients of a polynomial. If the length of \f$p\f$ is \f$n+1\f$ then the polynomial is described by:
             * \f[
             *      p[0] * x^n + p[1] * x^{n-1} + ... + p[n-1] * x + p[n]
             * \f]
             *
             * @param pp Array of polynomial coefficients.
             * @return result Array containing the roots of the polynomial.
             */
            public static array.Array Poots(array.Array p)
            {
                IntPtr reference = p.Reference;
                interop.DLLPolynomial.roots(ref reference, out IntPtr result);
                return (new array.Array(result));
            }
        }
    }
}
