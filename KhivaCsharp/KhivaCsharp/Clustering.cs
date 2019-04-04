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
        /// <summary>
        /// Khiva Clustering class containing several clustering methods.
        /// </summary>
        public static class Clustering
        {

            /// <summary>
            /// Calculates the k-means algorithm.
            ///
            /// [1] S.Lloyd. 1982. Least squares quantization in PCM.IEEE Transactions on Information Theory, 28, 2,
            /// Pages 129-137.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            ///                       dimension one indicates the number of time series.</param>
            /// <param name="k">The number of means to be computed.</param>
            /// <param name="tolerance">The error tolerance to stop the computation of the centroids.</param>
            /// <param name="max_iterations">The maximum number of iterations allowed.</param>
            /// <returns> Tuple with:
            /// <list type="bullet">
            /// <term>Centroids</term>
            /// <description>The resulting means or centroids.</description>
            /// <term>Labels</term>
            /// <description>The resulting labels of each time series which is the closest centroid.</description>
            /// </list>
            /// </returns>
            public static (array.Array, array.Array) KMeans(array.Array arr, int k, float tolerance = 1e-10F, int max_iterations = 100)
            {
                IntPtr reference = arr.Reference;
                interop.DLLClustering.k_means(ref reference, ref k, out IntPtr centroids, out IntPtr labels, ref tolerance, ref max_iterations);
                arr.Reference = reference;
                var tuple = (centroidsArr: new array.Array(centroids), labelsArr: new array.Array(labels));
                return tuple;
            }

            /// <summary>
            /// Calculates the K-Shape algorithm.
            ///
            /// [1] John Paparrizos and Luis Gravano. 2016. k-Shape: Efficient and Accurate Clustering of Time Series.
            /// SIGMOD Rec. 45, 1 (June 2016), 69-76.
            /// </summary>
            /// <param name="arr">Expects an input array whose dimension zero is the length of the time series (all the same) and
            ///                       dimension one indicates the number of time series.</param>
            /// <param name="k">The number of means to be computed.</param>
            /// <param name="tolerance">The error tolerance to stop the computation of the centroids.</param>
            /// <param name="max_iterations">The maximum number of iterations allowed.</param>
            /// <returns> Tuple with:
            /// <list type="bullet">
            /// <term>Centroids</term>
            /// <description>The resulting means or centroids.</description>
            /// <term>Labels</term>
            /// <description>The resulting labels of each time series which is the closest centroid.</description>
            /// </list>
            /// </returns>
            public static (array.Array, array.Array) KShape(array.Array arr, int k, float tolerance = 1e-10F, int max_iterations = 100)
            {
                IntPtr reference = arr.Reference;
                interop.DLLClustering.k_shape(ref reference, ref k, out IntPtr centroids, out IntPtr labels, ref tolerance, ref max_iterations);
                arr.Reference = reference;
                var tuple = (centroidsArr: new array.Array(centroids), labelsArr: new array.Array(labels));
                return tuple;
            }
        }
    }
}
