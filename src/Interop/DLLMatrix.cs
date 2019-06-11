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
        /// <param name="discordDistances">The distance of the best N discords.</param>
        /// <param name="discordIndices">The indices of the best N discords.</param>
        /// <param name="subsequenceIndices">The indices of the query sequences that produced
        /// the "N" bigger discords.</param>
        /// <param name="selfJoin">Indicates whether the input profile comes from a self join operation or not. It determines
        /// whether the mirror similar region is included in the output or not.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void find_best_n_discords([In] ref IntPtr profile, [In] ref IntPtr index, ref long m,
            ref long n,
            [Out] out IntPtr discordDistances, [Out] out IntPtr discordIndices,
            [Out] out IntPtr subsequenceIndices, ref bool selfJoin);

        /// <summary> Primitive of the findBestNMotifs function.
        ///</summary>
        /// <param name="profile">The matrix profile containing the minimum distance of each
        /// subsequence.</param>
        /// <param name="index">The matrix profile index containing where each minimum occurs.</param>
        /// <param name="m">Subsequence length value used to calculate the input matrix profile.</param>
        /// <param name="n">Number of motifs to extract.</param>
        /// <param name="motifDistances">The distance of the best N motifs.</param>
        /// <param name="motifIndices">The indices of the best N motifs.</param>
        /// <param name="subsequenceIndices">The indices of the query sequences that produced
        /// the minimum reported in the motifs.</param>
        /// <param name="selfJoin">Indicates whether the input profile comes from a self join operation or not. It determines
        /// whether the mirror similar region is included in the output or not.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void find_best_n_motifs([In] ref IntPtr profile, [In] ref IntPtr index, ref long m,
            ref long n,
            [Out] out IntPtr motifDistances, [Out] out IntPtr motifIndices,
            [Out] out IntPtr subsequenceIndices, ref bool selfJoin);

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
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void stomp([In] ref IntPtr tssa, [In] ref IntPtr tssb, ref long m, [Out] out IntPtr p,
            [Out] out IntPtr i);

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
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void stomp_self_join([In] ref IntPtr tss, ref long m, [Out] out IntPtr p,
            [Out] out IntPtr i);

        /// <summary>
        /// Mueen's Algorithm for Similarity Search.
        ///
        /// The result has the following structure:
        ///  - 1st dimension corresponds to the index of the subsequence in the time series.
        ///  - 2nd dimension corresponds to the number of queries.
        ///  - 3rd dimension corresponds to the number of time series.
        ///
        /// For example, the distance in the position (1, 2, 3) correspond to the distance of the third query to the fourth time
        /// series for the second subsequence in the time series.
        /// 
        /// [1] Yan Zhu, Zachary Zimmerman, Nader Shakibay Senobari, Chin-Chia Michael Yeh, Gareth Funning, Abdullah Mueen,
        /// Philip Brisk and Eamonn Keogh (2016). Matrix Profile II: Exploiting a Novel Algorithm and GPUs to break the one
        /// Hundred Million Barrier for Time Series Motifs and Joins. IEEE ICDM 2016.
        /// </summary>
        /// <param name="query">Array whose first dimension is the length of the query time series and the second dimension 
        /// is the number of queries.</param>
        /// <param name="tss">Array whose first dimension is the length of the time series and the second dimension is the
        /// number of time series.</param>
        /// <param name="distances">Resulting distances.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mass([In] ref IntPtr query, [In] ref IntPtr tss, [Out] out IntPtr distances);

        /// <summary>
        /// Calculates the N best matches of several queries in several time series.
        /// The result has the following structure:
        ///  - 1st dimension corresponds to the nth best match.
        ///  - 2nd dimension corresponds to the number of queries.
        ///  - 3rd dimension corresponds to the number of time series.
        ///
        /// For example, the distance in the position (1, 2, 3) corresponds to the second best distance of the third query in the
        /// fourth time series. The index in the position (1, 2, 3) is the is the index of the subsequence which leads to the
        /// second best distance of the third query in the fourth time series.
        /// </summary>
        /// <param name="query">Array whose first dimension is the length of the query time series and the second dimension 
        /// is the number of queries.</param>
        /// <param name="tss">Array whose first dimension is the length of the time series and the second dimension is the
        /// number of time series.</param>
        /// <param name="n">Number of matches to return.</param>
        /// <param name="distances">Resulting distances.</param>
        /// <param name="indexes">Resulting indexes.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void find_best_n_occurrences([In] ref IntPtr query, [In] ref IntPtr tss, ref long n,
            [Out] out IntPtr distances, [Out] out IntPtr indexes);
    }
}