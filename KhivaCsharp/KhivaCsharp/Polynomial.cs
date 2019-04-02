// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace polynomial
    {
        /// <summary>
        /// Khiva Polynomial class containing a number of polynomial methods.
        /// </summary>
        public static class Polynomial
        {

            /// <summary>
            /// Least squares polynomial fit. Fit a polynomial \f$p(x) = p[0] * x^{deg} + ... + p[deg]\f$ of degree \f$deg\f$
            /// to points \f$(x, y)\f$. Returns a vector of coefficients \f$p\f$ that minimises the squared error.
            /// </summary>
            /// <param name="x">x-coordinates of the M sample points \f$(x[i], y[i])\f$.</param>
            /// <param name="y">y-coordinates of the sample points.</param>
            /// <param name="deg">Degree of the fitting polynomial.</param>
            /// <returns>Polynomial coefficients, highest power first.</returns>
            public static array.Array Polyfit(array.Array x, array.Array y, int deg)
            {
                IntPtr xReference = x.Reference;
                IntPtr yReference = y.Reference;
                interop.DLLPolynomial.polyfit(ref xReference, ref yReference, ref deg, out IntPtr result);
                x.Reference = xReference;
                y.Reference = yReference;
                return (new array.Array(result));
            }

            /// <summary>
            /// Calculates the roots of a polynomial with coefficients given in \f$p\f$. The values in the rank-1 array
            /// \f$p\f$ are coefficients of a polynomial.If the length of \f$p\f$ is \f$n+1\f$ then the polynomial is described by:
            /// \f[
            ///p[0] * x ^ n + p[1] * x ^{n-1} + ... + p[n - 1] * x + p[n]
            /// \f]
            /// </summary>
            /// <param name="p">Array of polynomial coefficients.</param>
            /// <returns>Array containing the roots of the polynomial.</returns>
            public static array.Array Roots(array.Array p)
            {
                IntPtr reference = p.Reference;
                interop.DLLPolynomial.roots(ref reference, out IntPtr result);
                p.Reference = reference;
                return (new array.Array(result));
            }
        }
    }
}
