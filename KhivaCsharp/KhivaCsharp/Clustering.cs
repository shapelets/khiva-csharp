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
    namespace clustering
    {
        public class Clustering
        {
            public Clustering()
            {

            }

            /**
             * @brief Calculates the k-means algorithm.
             *
             * [1] S. Lloyd. 1982. Least squares quantization in PCM. IEEE Transactions on Information Theory, 28, 2,
             * Pages 129-137.
             *
             * @param tss            Expects an input array whose dimension zero is the length of the time series (all the same) and
             *                       dimension one indicates the number of time series.
             * @param k              The number of means to be computed.
             * @return centroids      The resulting means or centroids.
             * @return labels         The resulting labels of each time series which is the closest centroid.
             * @param tolerance      The error tolerance to stop the computation of the centroids.
             * @param max_iterations The maximum number of iterations allowed.
             */
            public static Tuple<array.Array, array.Array> KMeans(array.Array arr, int k, float tolerance = 1e-10F, int max_iterations = 100)
            {
                IntPtr reference = arr.Reference;
                interop.DLLClustering.k_means(ref reference, ref k, out IntPtr centroids, out IntPtr labels, ref tolerance, ref max_iterations);
                return (new Tuple<array.Array, array.Array>(new array.Array(centroids), new array.Array(labels)));
            }

            /**
             * @brief Calculates the K-Shape algorithm.
             *
             * [1] John Paparrizos and Luis Gravano. 2016. k-Shape: Efficient and Accurate Clustering of Time Series.
             * SIGMOD Rec. 45, 1 (June 2016), 69-76.
             *
             * @param tss            Expects an input array whose dimension zero is the length of the time series (all the same) and
             *                       dimension one indicates the number of time series.
             * @param k              The number of means to be computed.
             * @return centroids      The resulting means or centroids.
             * @return labels         The resulting labels of each time series which is the closest centroid.
             * @param tolerance      The error tolerance to stop the computation of the centroids.
             * @param max_iterations The maximum number of iterations allowed.
             */
            public static Tuple<array.Array, array.Array> KShape(array.Array arr, int k, float tolerance = 1e-10F, int max_iterations = 100)
            {
                IntPtr reference = arr.Reference;
                interop.DLLClustering.k_shape(ref reference, ref k, out IntPtr centroids, out IntPtr labels, ref tolerance, ref max_iterations);
                return (new Tuple<array.Array, array.Array>(new array.Array(centroids), new array.Array(labels)));
            }
        }
    }
}
