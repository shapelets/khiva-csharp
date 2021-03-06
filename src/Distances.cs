﻿// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using Khiva.Interop;

namespace Khiva
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
        /// two time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the
        /// distance between time series 0 and time series 1.</returns>
        public static KhivaArray Dtw(KhivaArray arr)
        {
            var reference = arr.Reference;
            DLLDistances.dtw(ref reference, out var result);
            arr.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Calculates euclidean distances between time series.
        /// </summary>
        /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>An upper triangular matrix where each position corresponds to the distance between two
        /// time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
        /// between time series 0 and time series 1.</returns>
        public static KhivaArray Euclidean(KhivaArray arr)
        {
            var reference = arr.Reference;
            DLLDistances.euclidean(ref reference, out var result);
            arr.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Calculates Hamming distances between time series.
        /// </summary>
        /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns> An upper triangular matrix where each position corresponds to the distance between two
        /// time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
        /// between time series 0 and time series 1.</returns>
        public static KhivaArray Hamming(KhivaArray arr)
        {
            var reference = arr.Reference;
            DLLDistances.hamming(ref reference, out var result);
            arr.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Calculates Manhattan distances between time series.
        /// </summary>
        /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>An upper triangular matrix where each position corresponds to the distance between two
        /// time series. Diagonal elements will be zero. For example: Position row 0 column 1 records the distance
        /// between time series 0 and time series 1.</returns>
        public static KhivaArray Manhattan(KhivaArray arr)
        {
            var reference = arr.Reference;
            DLLDistances.manhattan(ref reference, out var result);
            arr.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
        /// minus the value that maximizes the correlation value between each pair of time series.
        /// </summary>
        /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>An upper triangular matrix where each position corresponds to the distance between two time series.
        /// Diagonal elements will be zero. For example: Position row 0 column 1 records the distance between time series 0
        /// and time series 1.</returns>
        public static KhivaArray Sbd(KhivaArray arr)
        {
            var reference = arr.Reference;
            DLLDistances.sbd(ref reference, out var result);
            arr.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Calculates the Shape-Based distance (SBD). It computes the normalized cross-correlation and it returns 1.0
        /// minus the value that maximizes the correlation value between each pair of time series.
        /// </summary>
        /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>An upper triangular matrix where each position corresponds to the distance between two time series.
        /// Diagonal elements will be zero. For example: Position row 0 column 1 records the distance between time series 0
        /// and time series 1.</returns>
        public static KhivaArray SquaredEuclidean(KhivaArray arr)
        {
            var reference = arr.Reference;
            DLLDistances.squared_euclidean(ref reference, out var result);
            arr.Reference = reference;
            return KhivaArray.Create(result);
        }
    }
}