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
    /// Khiva Matrix Profile class containing matrix profile methods.
    /// </summary>
    public static class DLLMatrix
    {
        /// <summary> Primitive of the findBestNDiscords function.
        ///</summary>
        /// <param name="profile">The matrix profile containing the minimum distance of each
        /// subsequence.</param>
        /// <param name="index">The matrix profile index containing the index of the most similar
        /// subsequence.</param>
        /// <param name="m">Subsequence length value used to calculate the input matrix profile.</param>
        /// <param name="n">Number of discords to extract.</param>
        /// <param name="discord_distances">The distance of the best N discords.</param>
        /// <param name="discord_indices">The indices of the best N discords.</param>
        /// <param name="subsequence_indices">The indices of the query sequences that produced
        /// the "N" bigger discords.</param>
        /// <param name="self_join">Indicates whether the input profile comes from a self join operation or not. It determines
        /// whether the mirror similar region is included in the output or not.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void find_best_n_discords([In] ref IntPtr profile, [In] ref IntPtr index, ref long m, ref long n,
                                           [Out] out IntPtr discord_distances, [Out] out IntPtr discord_indices,
                                           [Out] out IntPtr subsequence_indices, ref bool self_join);

        /// <summary> Primitive of the findBestNMotifs function.
        ///</summary>
        /// <param name="profile">The matrix profile containing the minimum distance of each
        /// subsequence.</param>
        /// <param name="index">The matrix profile index containing where each minimum occurs.</param>
        /// <param name="m">Subsequence length value used to calculate the input matrix profile.</param>
        /// <param name="n">Number of motifs to extract.</param>
        /// <param name="motif_distances">The distance of the best N motifs.</param>
        /// <param name="motif_indices">The indices of the best N motifs.</param>
        /// <param name="subsequence_indices">The indices of the query sequences that produced
        /// the minimum reported in the motifs.</param>
        /// <param name="self_join">Indicates whether the input profile comes from a self join operation or not. It determines
        /// whether the mirror similar region is included in the output or not.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void find_best_n_motifs([In] ref IntPtr profile, [In] ref IntPtr index, ref long m, ref long n,
                                           [Out] out IntPtr motif_distances, [Out] out IntPtr motif_indices,
                                           [Out] out IntPtr subsequence_indices, ref bool self_join);

        /// <summary>  Primitive of the STOMP algorithm.
        ///
        /// [1] Yan Zhu, Zachary Zimmerman, Nader Shakibay Senobari, Chin-Chia Michael Yeh, Gareth Funning, Abdullah Mueen,
        /// Philip Brisk and Eamonn Keogh (2016). Matrix Profile II: Exploiting a Novel Algorithm and GPUs to break the one
        /// Hundred Million Barrier for Time Series Motifs and Joins. IEEE ICDM 2016.
        ///</summary>
        /// <param name="tssa">Query time series.</param>
        /// <param name="tssb">Reference time series.</param>
        /// <param name="m">Pointer to a long with the length of the subsequence.</param>
        /// <param name="p">The matrix profile, which reflects the distance to the closer element of the subsequence
        /// from 'tssa' in 'tssb'.</param>
        /// <param name="i">The matrix profile index, which points to where the aforementioned minimum is located.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void stomp([In] ref IntPtr tssa, [In] ref IntPtr tssb, ref long m, [Out] out IntPtr p, [Out] out IntPtr i);

        /// <summary> Primitive of the STOMP self join algorithm.
        ///
        /// [1] Yan Zhu, Zachary Zimmerman, Nader Shakibay Senobari, Chin-Chia Michael Yeh, Gareth Funning, Abdullah Mueen,
        /// Philip Brisk and Eamonn Keogh (2016). Matrix Profile II: Exploiting a Novel Algorithm and GPUs to break the one
        /// Hundred Million Barrier for Time Series Motifs and Joins. IEEE ICDM 2016.
        ///</summary>
        /// <param name="tss">Query and reference time series.</param>
        /// <param name="m">Pointer to a long with the length of the subsequence.</param>
        /// <param name="p">The matrix profile, which reflects the distance to the closer element of the subsequence
        /// from 'tss' in a different location of itself.</param>
        /// <param name="i">The matrix profile index, which points to where the aforementioned minimum is located.</param>
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void stomp_self_join([In] ref IntPtr tss, ref long m, [Out] out IntPtr p, [Out] out IntPtr i);
    }
}
