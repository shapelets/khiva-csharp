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
    /// Khiva Clustering class containing several clustering methods.
    /// </summary>
    public class DLLClustering
    {
        
         /// <summary> Calculates the k-means algorithm.
         ///
         /// [1] S. Lloyd. 1982. Least squares quantization in PCM. IEEE Transactions on Information Theory, 28, 2,
         /// Pages 129-137.
         ///</summary>
         /// <param name="tss">           Expects an input array whose dimension zero is the length of the time series (all the same) and
         ///                              dimension one indicates the number of time series.</param>
         /// <param name="k">             The number of means to be computed.</param>
         /// <param name="centroids">     The resulting means or centroids.</param>
         /// <param name="labels">        The resulting labels of each time series which is the closest centroid.</param>
         /// <param name="tolerance">     The error tolerance to stop the computation of the centroids.</param>
         /// <param name="max_iterations">The maximum number of iterations allowed.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void k_means([In] ref IntPtr tss, [In] ref int k, [Out] out IntPtr centroids, [Out] out IntPtr labels, [In] ref float tolerance, [In] ref int max_iterations);

         /// <summary> Calculates the K-Shape algorithm.
         ///
         /// [1] John Paparrizos and Luis Gravano. 2016. k-Shape: Efficient and Accurate Clustering of Time Series.
         /// SIGMOD Rec. 45, 1 (June 2016), 69-76.
         ///</summary>
         /// <param name="tss">           Expects an input array whose dimension zero is the length of the time series (all the same) and
         ///                       dimension one indicates the number of time series.</param>
         /// <param name="k">             The number of means to be computed.</param>
         /// <param name="centroids">     The resulting means or centroids.</param>
         /// <param name="labels">        The resulting labels of each time series which is the closest centroid.</param>
         /// <param name="tolerance">     The error tolerance to stop the computation of the centroids.</param>
         /// <param name="max_iterations">The maximum number of iterations allowed.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void k_shape([In] ref IntPtr tss, [In] ref int k, [Out] out IntPtr centroids, [Out] out IntPtr labels, [In] ref float tolerance, [In] ref int max_iterations);
    }
}
