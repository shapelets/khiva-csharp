// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using Khiva.Interop;

namespace Khiva
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
        /// <returns>Tuple with the slope of the regression line, the correlation coefficient, 
        /// the two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero, using Wald
        /// Test with t-distribution of the test statistic and the standard error of the estimated gradient.</returns>
        public static Tuple<KhivaArray, KhivaArray, KhivaArray, KhivaArray, KhivaArray> Linear(KhivaArray xss,
            KhivaArray yss)
        {
            var xReference = xss.Reference;
            var yReference = yss.Reference;
            DLLRegression.linear(ref xReference, ref yReference,
                out var slope, out var intercept, out var rvalue, out var pvalue, out var stderrest);
            xss.Reference = xReference;
            yss.Reference = yReference;
            var tuple = Tuple.Create(KhivaArray.Create(slope),
                KhivaArray.Create(intercept),
                KhivaArray.Create(rvalue),
                KhivaArray.Create(pvalue),
                KhivaArray.Create(stderrest));
            return tuple;
        }
    }
}