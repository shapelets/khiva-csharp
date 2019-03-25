﻿// Copyright (c) 2019 Shapelets.io
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
    public class DLLClustering
    {
        /**
         * @brief Calculates the k-means algorithm.
         *
         * [1] S. Lloyd. 1982. Least squares quantization in PCM. IEEE Transactions on Information Theory, 28, 2,
         * Pages 129-137.
         *
         * @param tss            Expects an input array whose dimension zero is the length of the time series (all the same) and
         *                       dimension one indicates the number of time series.
         * @param k              The number of means to be computed.
         * @param centroids      The resulting means or centroids.
         * @param labels         The resulting labels of each time series which is the closest centroid.
         * @param tolerance      The error tolerance to stop the computation of the centroids.
         * @param max_iterations The maximum number of iterations allowed.
         */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void k_means(ref IntPtr tss, ref int k, ref IntPtr centroids, ref IntPtr labels, ref float tolerance, ref int max_iterations);

        /**
         * @brief Calculates the K-Shape algorithm.
         *
         * [1] John Paparrizos and Luis Gravano. 2016. k-Shape: Efficient and Accurate Clustering of Time Series.
         * SIGMOD Rec. 45, 1 (June 2016), 69-76.
         *
         * @param tss            Expects an input array whose dimension zero is the length of the time series (all the same) and
         *                       dimension one indicates the number of time series.
         * @param k              The number of means to be computed.
         * @param centroids      The resulting means or centroids.
         * @param labels         The resulting labels of each time series which is the closest centroid.
         * @param tolerance      The error tolerance to stop the computation of the centroids.
         * @param max_iterations The maximum number of iterations allowed.
         */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void k_shape(ref IntPtr tss, ref int k, ref IntPtr centroids, ref IntPtr labels, ref float tolerance, ref int max_iterations);
    }
}