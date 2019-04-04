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
using khiva.array;

namespace khiva
{
    namespace distances
    {
        /// <summary>
        /// Khiva Distances class containing distances methods.
        /// </summary>
        public static class Distances
        {
            /// <summary>
            /// Calculates the Dynamic Time Warping Distance.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            /// dimension one indicates the number of time series.</param>
            /// <returns>An upper triangular matrix where each position corresponds to the distance between
            /// two time series.Diagonal elements will be zero.For example: Position row 0 column 1 records the
            /// distance between time series 0 and time series 1.</returns>
            public static KhivaArray DTW(KhivaArray arr)
            {
                IntPtr reference = arr.Reference;
                interop.DLLDistances.dtw(ref reference, out IntPtr result);
                arr.Reference = reference;
                return (new KhivaArray(result));
            }

            /**
             * @brief Calculates euclidean distances between time series.
             *
             * @param arr Expects an input array whose dimension zero is the length of the time series (all the same) and
             * dimension one indicates the number of time series.
             *
             * @param result An upper triangular matrix where each position corresponds to the distance between two
             * time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
             * between time series 0 and time series 1.
             */
            /// <summary>
            /// Calculates euclidean distances between time series.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            /// dimension one indicates the number of time series.</param>
            /// <returns>An upper triangular matrix where each position corresponds to the distance between two
            /// time series.Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
            /// between time series 0 and time series 1.</returns>
            public static KhivaArray Euclidean(KhivaArray arr)
            {
                IntPtr reference = arr.Reference;
                interop.DLLDistances.euclidean(ref reference, out IntPtr result);
                arr.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Calculates Hamming distances between time series.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            /// dimension one indicates the number of time series.</param>
            /// <returns> An upper triangular matrix where each position corresponds to the distance between two
            /// time series. Diagonal elements will be zero.For example: Position row 0 column 1 records the distance
            /// between time series 0 and time series 1.</returns>
            public static KhivaArray Hamming(KhivaArray arr)
            {
                IntPtr reference = arr.Reference;
                interop.DLLDistances.hamming(ref reference, out IntPtr result);
                arr.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Calculates Manhattan distances between time series.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            /// dimension one indicates the number of time series.</param>
            /// <returns>An upper triangular matrix where each position corresponds to the distance between two
            /// time series.Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
            /// between time series 0 and time series 1.</returns>
            public static KhivaArray Manhattan(KhivaArray arr)
            {
                IntPtr reference = arr.Reference;
                interop.DLLDistances.manhattan(ref reference, out IntPtr result);
                arr.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
            /// minus the value that maximizes the correlation value between each pair of time series.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            /// dimension one indicates the number of time series.</param>
            /// <returns>An upper triangular matrix where each position corresponds to the distance between two time series.
            /// Diagonal elements will be zero.For example: Position row 0 column 1 records the distance between time series 0
            /// and time series 1.</returns>
            public static KhivaArray SBD(KhivaArray arr)
            {
                IntPtr reference = arr.Reference;
                interop.DLLDistances.sbd(ref reference, out IntPtr result);
                arr.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
            /// minus the value that maximizes the correlation value between each pair of time series.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            /// dimension one indicates the number of time series.</param>
            /// <returns>An upper triangular matrix where each position corresponds to the distance between two time series.
            /// Diagonal elements will be zero.For example: Position row 0 column 1 records the distance between time series 0
            /// and time series 1.</returns>
            public static KhivaArray SquaredEuclidean(KhivaArray arr)
            {
                IntPtr reference = arr.Reference;
                interop.DLLDistances.squared_euclidean(ref reference, out IntPtr result);
                arr.Reference = reference;
                return (new KhivaArray(result));
            }

        }
    }
   
}
