﻿// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using Khiva.Interop;

namespace Khiva
{
    /// <summary>
    /// Khiva Matrix Profile class containing matrix profile methods.
    /// </summary>
    public static class Matrix
    {
        /// <summary>
        /// Primitive of the findBestNDiscords function.
        /// </summary>
        /// <param name="profile">The matrix profile containing the minimum distance of each subsequence.</param>
        /// <param name="index">The matrix profile index containing the index of the most similar subsequence.</param>
        /// <param name="m">Length of the matrix profile.</param>
        /// <param name="n">Number of discords to extract.</param>
        /// <param name="selfJoin">Indicates whether the input profile comes from a self join operation or not. It determines
        /// whether the mirror similar region is included in the output or not.</param>
        /// <returns>Tuple with the distance of the best N discords, the indices of the best N discords and 
        /// the indices of the query sequences that produced the "N" bigger discords.</returns>
        public static Tuple<KhivaArray, KhivaArray, KhivaArray> FindBestNDiscords(KhivaArray profile, KhivaArray index,
            long m, long n, bool selfJoin = false)
        {
            var profileReference = profile.Reference;
            var indexReference = index.Reference;
            DLLMatrix.find_best_n_discords(ref profileReference, ref indexReference, ref m, ref n,
                out var discordsDistances, out var discordsIndices, out var subsequenceIndices,
                ref selfJoin);
            profile.Reference = profileReference;
            index.Reference = indexReference;
            var tuple = Tuple.Create(KhivaArray.Create(discordsDistances),
                KhivaArray.Create(discordsIndices),
                KhivaArray.Create(subsequenceIndices));
            return tuple;
        }

        /// <summary>
        /// Primitive of the findBestNMotifs function.
        /// </summary>
        /// <param name="profile">The matrix profile containing the minimum distance of each subsequence.</param>
        /// <param name="index">The matrix profile index containing where each minimum occurs.</param>
        /// <param name="m">Subsequence length value used to calculate the input matrix profile.</param>
        /// <param name="n">Number of motifs to extract.</param>
        /// <param name="selfJoin">Indicates whether the input profile comes from a self join operation or not. It determines
        /// whether the mirror similar region is included in the output or not.</param>
        /// <returns>Tuple with the distance of the best N motifs, the indices of the best N motifs,
        /// the indices of the query sequences that produced the minimum reported in the motifs.</returns>
        public static Tuple<KhivaArray, KhivaArray, KhivaArray> FindBestNMotifs(KhivaArray profile, KhivaArray index,
            long m, long n, bool selfJoin = false)
        {
            var profileReference = profile.Reference;
            var indexReference = index.Reference;
            DLLMatrix.find_best_n_motifs(ref profileReference, ref indexReference, ref m, ref n,
                out var motifDistances, out var motifIndices, out var subsequenceIndices,
                ref selfJoin);
            profile.Reference = profileReference;
            index.Reference = indexReference;
            var tuple = Tuple.Create(KhivaArray.Create(motifDistances),
                KhivaArray.Create(motifIndices),
                KhivaArray.Create(subsequenceIndices));
            return tuple;
        }

        /// <summary>
        /// Primitive of the STOMP algorithm.
        /// 
        /// [1] Yan Zhu, Zachary Zimmerman, Nader Shakibay Senobari, Chin-Chia Michael Yeh, Gareth Funning, Abdullah Mueen,
        /// Philip Brisk and Eamonn Keogh (2016). Matrix Profile II: Exploiting a Novel Algorithm and GPUs to break the one
        /// Hundred Million Barrier for Time Series Motifs and Joins. IEEE ICDM 2016.
        /// </summary>
        /// <param name="tssa">Query time series.</param>
        /// <param name="tssb">Reference time series.</param>
        /// <param name="m">Pointer to a long with the length of the subsequence.</param>
        /// <returns>Tuple with the matrix profile, which has the distance to the closer element of the subsequence from
        /// 'tssa' in 'tssb' and the matrix profile index, which points to where the aforementioned minimum is located.</returns>
        public static Tuple<KhivaArray, KhivaArray> Stomp(KhivaArray tssa, KhivaArray tssb, long m)
        {
            var aReference = tssa.Reference;
            var bReference = tssb.Reference;
            DLLMatrix.stomp(ref aReference, ref bReference, ref m,
                out var p, out var i);
            tssa.Reference = aReference;
            tssb.Reference = bReference;
            var tuple = Tuple.Create(KhivaArray.Create(p),
                KhivaArray.Create(i));
            return tuple;
        }

        /// <summary>
        /// Primitive of the STOMP self join algorithm.
        /// 
        /// [1] Yan Zhu, Zachary Zimmerman, Nader Shakibay Senobari, Chin-Chia Michael Yeh, Gareth Funning, Abdullah Mueen,
        /// Philip Brisk and Eamonn Keogh (2016). Matrix Profile II: Exploiting a Novel Algorithm and GPUs to break the one
        /// Hundred Million Barrier for Time Series Motifs and Joins. IEEE ICDM 2016.
        /// </summary>
        /// <param name="tss">Query and reference time series.</param>
        /// <param name="m">Pointer to a long with the length of the subsequence.</param>
        /// <returns>Tuple with the matrix profile, which has the distance to the closer element of the subsequence from
        /// 'tss' in a different location of itself and the matrix profile index, which points to where the aforementioned
        ///  minimum is located.</returns>
        public static Tuple<KhivaArray, KhivaArray> StompSelfJoin(KhivaArray tss, long m)
        {
            var reference = tss.Reference;
            DLLMatrix.stomp_self_join(ref reference, ref m,
                out var p, out var i);
            tss.Reference = reference;
            var tuple = Tuple.Create(KhivaArray.Create(p),
                KhivaArray.Create(i));
            return tuple;
        }

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
        /// <returns>Resulting distances.</returns>
        public static KhivaArray Mass(KhivaArray query, KhivaArray tss)
        {
            var q = query.Reference;
            var t = tss.Reference;
            DLLMatrix.mass(ref q, ref t, out var distances);
            query.Reference = q;
            tss.Reference = t;
            return KhivaArray.Create(distances);
        }

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
        /// <returns>Tuple with the resulting distances and indexes.</returns>
        public static Tuple<KhivaArray, KhivaArray> FindBestNOccurrences(KhivaArray query, KhivaArray tss, long n)
        {
            var q = query.Reference;
            var t = tss.Reference;
            DLLMatrix.find_best_n_occurrences(ref q, ref t, ref n, out var distances, out var indexes);
            query.Reference = q;
            tss.Reference = t;
            return Tuple.Create(KhivaArray.Create(distances),
                        KhivaArray.Create(indexes));
        }
    }
}