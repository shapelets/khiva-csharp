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
    namespace matrix
    {
        /// <summary>
        /// Khiva Matrix Profile class containing matrix profile methods.
        /// </summary>
        public static class Matrix
        {
            /// <summary>
            /// Primitive of the findBestNDiscords function.
            /// </summary>
            /// <param name="profile">The matrix profile containing the minimum distance of each subsequence</param>
            /// <param name="index">The matrix profile index containing the index of the most similar subsequence</param>
            /// <param name="m">Length of the matrix profile</param>
            /// <param name="n">Number of discords to extract</param>
            /// <param name="self_join">Indicates whether the input profile comes from a self join operation or not. It determines
            /// whether the mirror similar region is included in the output or not.</param>
            /// <returns>Tuple with the distance of the best N discords, the indices of the best N discords and 
            /// the indices of the query sequences that produced the "N" bigger discords.</returns>
            public static Tuple<KhivaArray, KhivaArray, KhivaArray> FindbestNDiscords(KhivaArray profile, KhivaArray index, long m, long n, bool self_join = false)
            {
                IntPtr profileReference = profile.Reference;
                IntPtr indexReference = index.Reference;
                IntPtr discords_distances; IntPtr discords_indices; IntPtr subsequence_indices;
                interop.DLLMatrix.find_best_n_discords(ref profileReference, ref indexReference, ref m, ref n,
                                                        out discords_distances, out discords_indices, out subsequence_indices,
                                                        ref self_join);
                profile.Reference = profileReference;
                index.Reference = indexReference;
                var tuple = Tuple.Create(KhivaArray.Create(discords_distances),
                             KhivaArray.Create(discords_indices),
                             KhivaArray.Create(subsequence_indices));
                return tuple;
            }
           
            /// <summary>
            /// Primitive of the findBestNMotifs function.
            /// </summary>
            /// <param name="profile">The matrix profile containing the minimum distance of each subsequence.</param>
            /// <param name="index">The matrix profile index containing where each minimum occurs.</param>
            /// <param name="m">Subsequence length value used to calculate the input matrix profile.</param>
            /// <param name="n">Number of motifs to extract.</param>
            /// <param name="self_join">Indicates whether the input profile comes from a self join operation or not. It determines
            /// whether the mirror similar region is included in the output or not.</param>
            /// <returns>Tuple with the distance of the best N motifs, the indices of the best N motifs,
            /// the indices of the query sequences that produced the minimum reported in the motifs.</returns>
            public static Tuple<KhivaArray, KhivaArray, KhivaArray> FindbestNMotifs(KhivaArray profile, KhivaArray index, long m, long n, bool self_join=false)
            {
                IntPtr profileReference = profile.Reference;
                IntPtr indexReference = index.Reference;
                IntPtr motif_distances; IntPtr motif_indices; IntPtr subsequence_indices;
                interop.DLLMatrix.find_best_n_motifs(ref profileReference, ref indexReference, ref m, ref n,
                                                        out motif_distances, out motif_indices, out subsequence_indices,
                                                        ref self_join);
                profile.Reference = profileReference;
                index.Reference = indexReference;
                var tuple = Tuple.Create(KhivaArray.Create(motif_distances),
                                         KhivaArray.Create(motif_indices),
                                         KhivaArray.Create(subsequence_indices));
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
            /// <returns>Tuple with 
            /// the matrix profile, whichlects the distance to the closer element of the subsequence from 'tssa' in 'tssb' and 
            /// the matrix profile index, which points to where the aforementioned minimum is located.</returns>
            public static Tuple<KhivaArray, KhivaArray> Stomp(KhivaArray tssa, KhivaArray tssb, long m)
            {
                IntPtr aReference = tssa.Reference;
                IntPtr bReference = tssb.Reference;
                IntPtr p; IntPtr i;
                interop.DLLMatrix.stomp(ref aReference, ref bReference, ref m,
                                        out p, out i);
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
            /// <returns>Tuple with 
            /// the matrix profile, whichlects the distance to the closer element of the subsequence from 'tss' in a different location of itself and
            /// the matrix profile index, which points to where the aforementioned minimum is located.</returns>
            public static Tuple<KhivaArray, KhivaArray> StompSelfJoin(KhivaArray tss, long m)
            {
                IntPtr reference = tss.Reference;
                IntPtr p; IntPtr i;
                interop.DLLMatrix.stomp_self_join(ref reference, ref m,
                                        out p, out i);
                tss.Reference = reference;
                var tuple = Tuple.Create(KhivaArray.Create(p),
                                         KhivaArray.Create(i));
                return tuple;
            }
        }
    } 
}
