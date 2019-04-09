// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    /// <summary>
    /// Khiva Distances class containing distances methods.
    /// </summary>
    public static class DLLDistances
    {
        
        /// <summary> Calculates the Dynamic Time Warping Distance.
        ///</summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="result">An upper triangular matrix where each position corresponds to the distance between
        /// two time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the
        /// distance between time series 0 and time series 1.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void dtw([In] ref IntPtr tss, [Out] out IntPtr result);
        
        
         /// <summary> Calculates euclidean distances between time series.
         ///</summary>
         /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and
         /// dimension one indicates the number of time series.
         ///</param>
         /// <param name="result">An upper triangular matrix where each position corresponds to the distance between two
         /// time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
         /// between time series 0 and time series 1.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void euclidean([In] ref IntPtr tss, [Out] out IntPtr result);

        
         /// <summary> Calculates Hamming distances between time series.
         ///</summary>
         /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and
         /// dimension one indicates the number of time series.</param>
         /// <param name="result">An upper triangular matrix where each position corresponds to the distance between two
         /// time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
         /// between time series 0 and time series 1.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void hamming([In] ref IntPtr tss, [Out] out IntPtr result);

        
         /// <summary> Calculates Manhattan distances between time series.
         ///</summary>
         /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and
         /// dimension one indicates the number of time series.
         ///</param>
         /// <param name="result">An upper triangular matrix where each position corresponds to the distance between two
         /// time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
         /// between time series 0 and time series 1.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void manhattan([In] ref IntPtr tss, [Out] out IntPtr result);

        
        /// <summary> Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
        /// minus the value that maximizes the correlation value between each pair of time series.
        ///</summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.
        ///</param>
        /// <param name="result">An upper triangular matrix where each position corresponds to the distance between two time series.
        /// Diagonal elements will be zero. For example: Position row 0 column 1 records the distance between time series 0
        /// and time series 1.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void sbd([In] ref IntPtr tss, [Out] out IntPtr result);

        
        /// <summary> Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
        /// minus the value that maximizes the correlation value between each pair of time series.
        ///</summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        ///
        /// <param name="result">An upper triangular matrix where each position corresponds to the distance between two time series.
        /// Diagonal elements will be zero. For example: Position row 0 column 1 records the distance between time series 0
        /// and time series 1.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void squared_euclidean([In] ref IntPtr tss, [Out] out IntPtr result);
    }
}
