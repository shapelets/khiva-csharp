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
    namespace regression
    {
        /// <summary>
        /// Khiva Regression class containing regression methods.
        /// </summary>
        public static class Regression
        {

            /// <summary>
            /// Calculates a linear least-squares regression for two sets of measurements. Both arrays should have the same
            /// length.
            /// </summary>
            /// <param name="xss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <param name="yss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <returns>tuple with:
            /// Slope of the regression line.
            /// Correlation coefficient.
            /// Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero, using Wald
            /// Test with t-distribution of the test statistic.
            /// Standard error of the estimated gradient.</returns>
            public static (array.Array, array.Array, array.Array, array.Array, array.Array) Linear(array.Array xss, array.Array yss)
            {
                IntPtr xReference = xss.Reference;
                IntPtr yReference = yss.Reference;
                interop.DLLRegression.linear(ref xReference, ref yReference,
                                            out IntPtr slope, out IntPtr intercept, out IntPtr rvalue, out IntPtr pvalue, out IntPtr stderrest);
                xss.Reference = xReference;
                yss.Reference = yReference;
                var tuple = (slope: new array.Array(slope),
                            intercept: new array.Array(intercept),
                            rvalue: new array.Array(rvalue),
                            pvalue: new array.Array(pvalue),
                            stderrest: new array.Array(stderrest));
                return tuple;
            }
        }
    }
}
