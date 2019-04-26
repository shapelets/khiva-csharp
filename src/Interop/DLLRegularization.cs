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
    /// Khiva Regularization class containing different regularization methods.
    /// </summary>
    public static class DLLRegularization
    {
        /// <summary> Group by operation in the input array using n_columns_key columns as group keys and n_columns_value columns as
        /// values. The data is expected to be sorted. The aggregation function determines the operation to aggregate the values.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series (all the same) and dimension one indicates the number of time series.</param>
        /// <param name="aggregationFunction">Function to be used in the aggregation. It receives an integer which indicates the
        /// function to be applied:
        ///          {
        ///              0 : mean,
        ///              1 : median
        ///              2 : min,
        ///              3 : max,
        ///              4 : stdev,
        ///              5 : var,
        ///              default : mean
        ///          }</param>
        /// <param name="nColumnsKey">Number of columns conforming the key.</param>
        /// <param name="nColumnsValue">Number of columns conforming the value (they are expected to be consecutive to the column
        /// keys).
        ///</param>
        /// <param name="result">An array with the values of the group keys aggregated using the aggregation_function.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void group_by([In] ref IntPtr array, [In] ref int aggregationFunction,
            [In] ref int nColumnsKey, [In] ref int nColumnsValue,
            [Out] out IntPtr result);
    }
}