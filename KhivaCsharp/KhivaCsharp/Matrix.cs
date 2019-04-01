using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace matrix
    {
        public static class Matrix
        {
            /**
            * @brief Primitive of the findBestNDiscords function.
            *
            * @param profile The matrix profile containing the minimum distance of each
            * subsequence
            * @param The matrix profile index containing the index of the most similar
            * subsequence
            * @param length_profile Length of the matrix profile
            * @param m Subsequence length value used to calculate the input matrix profile.
            * @param n Number of discords to extract
            * @return discord_distances The distance of the best N discords
            * @return discord_indices The indices of the best N discords
            * @return subsequence_indices The indices of the query sequences that produced
            * the "N" bigger discords.
            * @param self_join Indicates whether the input profile comes from a self join operation or not. It determines
            * whether the mirror similar region is included in the output or not.
            */
            public static (array.Array, array.Array, array.Array) FindbestNDiscords(array.Array profile, array.Array index, long m, long n, bool self_join = false)
            {
                IntPtr profileReference = profile.Reference;
                IntPtr indexReference = index.Reference;
                interop.DLLMatrix.find_best_n_discords(ref profileReference, ref indexReference, ref m, ref n,
                                                        out IntPtr discords_distances, out IntPtr discords_indices, out IntPtr subsequence_indices,
                                                        ref self_join);
                profile.Reference = profileReference;
                index.Reference = indexReference;
                var tuple = (distances: new array.Array(discords_distances),
                             indices: new array.Array(discords_indices),
                             subsequence: new array.Array(subsequence_indices));
                return tuple;
            }

            /**
             * @brief Primitive of the findBestNMotifs function.
             *
             * @param profile The matrix profile containing the minimum distance of each
             * subsequence.
             * @param index The matrix profile index containing where each minimum occurs.
             * @param length_profile Length of the matrix profile.
             * @param m Subsequence length value used to calculate the input matrix profile.
             * @param n Number of motifs to extract.
             * @return motif_distances The distance of the best N motifs.
             * @return motif_indices The indices of the best N motifs.
             * @return subsequence_indices The indices of the query sequences that produced
             * the minimum reported in the motifs.
             * @param self_join Indicates whether the input profile comes from a self join operation or not. It determines
             * whether the mirror similar region is included in the output or not.
             */
            public static (array.Array, array.Array, array.Array) FindbestNMotifs(array.Array profile, array.Array index, long m, long n, bool self_join=false)
            {
                IntPtr profileReference = profile.Reference;
                IntPtr indexReference = index.Reference;
                interop.DLLMatrix.find_best_n_motifs(ref profileReference, ref indexReference, ref m, ref n,
                                                        out IntPtr motif_distances, out IntPtr motif_indices, out IntPtr subsequence_indices,
                                                        ref self_join);
                profile.Reference = profileReference;
                index.Reference = indexReference;
                var tuple = (distances: new array.Array(motif_distances),
                             indices: new array.Array(motif_indices),
                             subsequence: new array.Array(subsequence_indices));
                return tuple;
            }

            /**
             * @brief  Primitive of the STOMP algorithm.
             *
             * [1] Yan Zhu, Zachary Zimmerman, Nader Shakibay Senobari, Chin-Chia Michael Yeh, Gareth Funning, Abdullah Mueen,
             * Philip Brisk and Eamonn Keogh (2016). Matrix Profile II: Exploiting a Novel Algorithm and GPUs to break the one
             * Hundred Million Barrier for Time Series Motifs and Joins. IEEE ICDM 2016.
             *
             * @param tssa Query time series
             * @param tssb Reference time series
             * @param m Pointer to a long with the length of the subsequence.
             * @return p The matrix profile, whichlects the distance to the closer element of the subsequence
             * from 'tssa' in 'tssb'.
             * @return i The matrix profile index, which points to where the aforementioned minimum is located.
             */
            public static (array.Array, array.Array) Stomp(array.Array tssa, array.Array tssb, long m)
            {
                IntPtr aReference = tssa.Reference;
                IntPtr bReference = tssb.Reference;
                interop.DLLMatrix.stomp(ref aReference, ref bReference, ref m,
                                        out IntPtr p, out IntPtr i);
                tssa.Reference = aReference;
                tssb.Reference = bReference;
                var tuple = (p: new array.Array(p),
                             i: new array.Array(i));
                return tuple;
            }

            /**
             * @brief Primitive of the STOMP self join algorithm.
             *
             * [1] Yan Zhu, Zachary Zimmerman, Nader Shakibay Senobari, Chin-Chia Michael Yeh, Gareth Funning, Abdullah Mueen,
             * Philip Brisk and Eamonn Keogh (2016). Matrix Profile II: Exploiting a Novel Algorithm and GPUs to break the one
             * Hundred Million Barrier for Time Series Motifs and Joins. IEEE ICDM 2016.
             *
             * @param tss Query anderence time series
             * @param m Pointer to a long with the length of the subsequence.
             * @return p The matrix profile, whichlects the distance to the closer element of the subsequence
             * from 'tss' in a different location of itself
             * @return i The matrix profile index, which points to where the aforementioned minimum is located
             */
            public static (array.Array, array.Array) StompSelfJoin(array.Array tss, long m)
            {
                IntPtr reference = tss.Reference;
                interop.DLLMatrix.stomp_self_join(ref reference, ref m,
                                        out IntPtr p, out IntPtr i);
                tss.Reference = reference;
                var tuple = (p: new array.Array(p),
                             i: new array.Array(i));
                return tuple;
            }
        }
    } 
}
