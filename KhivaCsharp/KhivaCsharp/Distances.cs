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
    namespace distances
    {
        public class Distances
        {
            /**
             * @brief Calculates the Dynamic Time Warping Distance.
             *
             * @param arr Expects an input array whose dimension zero is the length of the time series (all the same) and
             * dimension one indicates the number of time series.
             * @param result An upper triangular matrix where each position corresponds to the distance between
             * two time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the
             * distance between time series 0 and time series 1.
             */
            public static array.Array DTW(array.Array arr)
            {
                IntPtr result;
                IntPtr reference = arr.Reference;
                interop.DLLDistances.dtw(reference, out result);
                return (new array.Array(result));
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
            public static array.Array Euclidean(array.Array arr)
            {
                IntPtr result;
                IntPtr reference = arr.Reference;
                interop.DLLDistances.euclidean(reference, out result);
                return (new array.Array(result));
            }

            /**
             * @brief Calculates Hamming distances between time series.
             *
             * @param arr Expects an input array whose dimension zero is the length of the time series (all the same) and
             * dimension one indicates the number of time series.
             * @param result An upper triangular matrix where each position corresponds to the distance between two
             * time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
             * between time series 0 and time series 1.
             */
            public static array.Array Hamming(array.Array arr)
            {
                IntPtr result;
                IntPtr reference = arr.Reference;
                interop.DLLDistances.hamming(reference, out result);
                return (new array.Array(result));
            }

            /**
             * @brief Calculates Manhattan distances between time series.
             *
             * @param arr Expects an input array whose dimension zero is the length of the time series (all the same) and
             * dimension one indicates the number of time series.
             *
             * @param result An upper triangular matrix where each position corresponds to the distance between two
             * time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
             * between time series 0 and time series 1.
             */
            public static array.Array Manhattan(array.Array arr)
            {
                IntPtr result;
                IntPtr reference = arr.Reference;
                interop.DLLDistances.manhattan(reference, out result);
                return (new array.Array(result));
            }

            /**
             * @brief Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
             * minus the value that maximizes the correlation value between each pair of time series.
             *
             * @param arr Expects an input array whose dimension zero is the length of the time series (all the same) and
             * dimension one indicates the number of time series.
             *
             * @return array An upper triangular matrix where each position corresponds to the distance between two time series.
             * Diagonal elements will be zero. For example: Position row 0 column 1 records the distance between time series 0
             * and time series 1.
             */
            public static array.Array SBD(array.Array arr)
            {
                IntPtr result;
                IntPtr reference = arr.Reference;
                interop.DLLDistances.sbd(reference, out result);
                return (new array.Array(result));
            }

                        /**
             * @brief Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
             * minus the value that maximizes the correlation value between each pair of time series.
             *
             * @param arr Expects an input array whose dimension zero is the length of the time series (all the same) and
             * dimension one indicates the number of time series.
             *
             * @return array An upper triangular matrix where each position corresponds to the distance between two time series.
             * Diagonal elements will be zero. For example: Position row 0 column 1 records the distance between time series 0
             * and time series 1.
             */
            public static array.Array SquaredEuclidean(array.Array arr)
            {
                IntPtr result;
                IntPtr reference = arr.Reference;
                interop.DLLDistances.squared_euclidean(reference, out result);
                return (new array.Array(result));
            }

        }
    }
   
}
