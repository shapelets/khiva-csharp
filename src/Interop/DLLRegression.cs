// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Runtime.InteropServices;

namespace Khiva.Interop
{
    /// <summary>
    /// Khiva Regression class containing regression methods.
    /// </summary>
    public static class DLLRegression
    {
        /// <summary> Calculates a linear least-squares regression for two sets of measurements. Both arrays should have the same
        /// length.
        ///</summary>
        /// <param name="xss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        /// <param name="yss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        /// <param name="slope">Slope of the regression line.</param>
        /// <param name="intercept">Intercept of the regression line.</param>
        /// <param name="rvalue">Correlation coefficient.</param>
        /// <param name="pvalue">Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero, using Wald
        /// Test with t-distribution of the test statistic.</param>
        /// <param name="stderrest">Standard error of the estimated gradient.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linear([In] ref IntPtr xss, [In] ref IntPtr yss, [Out] out IntPtr slope,
            [Out] out IntPtr intercept,
            [Out] out IntPtr rvalue, [Out] out IntPtr pvalue, [Out] out IntPtr stderrest);
    }
}