// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using Khiva.Interop;

namespace Khiva
{
    /// <summary>
    /// Khiva Normalization class containing several normalization methods.
    /// </summary>
    public static class Normalization
    {
        /// <summary>
        /// Normalizes the given time series according to its maximum value and adjusts each value within the range (-1, 1).
        /// </summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        /// <returns>An array with the same dimensions as tss, whose values (time series in dimension 0) have been
        /// normalized by dividing each number by 10^j, where j is the number of integer digits of the max number in the time
        /// series.</returns>
        public static KhivaArray DecimalScalingNorm(KhivaArray tss)
        {
            var reference = tss.Reference;
            DLLNormalization.decimal_scaling_norm(ref reference, out var result);
            tss.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Same as decimal_scaling_norm, but it performs the operation inplace, without allocating further memory.
        /// </summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        public static void DecimalScalingNorm(ref KhivaArray tss)
        {
            var reference = tss.Reference;
            DLLNormalization.decimal_scaling_norm_in_place(ref reference);
            tss.Reference = reference;
        }

        /// <summary>
        /// Normalizes the given time series according to its minimum and maximum value and adjusts each value within the range[low, high].
        /// </summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        /// <param name="high">Maximum final value (Defaults to 1.0).</param>
        /// <param name="low">Minimum final value (Defaults to 0.0).</param>
        /// <param name="epsilon">Safeguard for constant (or near constant) time series as the operation implies a unit scale operation
        /// between min and max values in the tss.</param>
        /// <returns>KhivaArray with the same dimensions as tss, whose values (time series in dimension 0) have been
        /// normalized by maximum and minimum values, and scaled as per high and low parameters.</returns>
        public static KhivaArray MaxMinNorm(KhivaArray tss, double high, double low, double epsilon = 0.00000001)
        {
            var reference = tss.Reference;
            DLLNormalization.max_min_norm(ref reference, ref high, ref low, ref epsilon, out var result);
            tss.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Same as max_min_norm, but it performs the operation inplace, without allocating further memory.
        /// </summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        /// <param name="high">Maximum final value (Defaults to 1.0).</param>
        /// <param name="low">Minimum final value (Defaults to 0.0).</param>
        /// <param name="epsilon">Safeguard for constant (or near constant) time series as the operation implies a unit scale operation
        /// between min and max values in the tss.</param>
        public static void MaxMinNorm(ref KhivaArray tss, double high, double low, double epsilon = 0.00000001)
        {
            var reference = tss.Reference;
            DLLNormalization.max_min_norm_in_place(ref reference, ref high, ref low, ref epsilon);
            tss.Reference = reference;
        }

        /// <summary>
        /// Normalizes the given time series according to its maximum-minimum value and its mean. It follows the following
        /// formulae:
        /// \f[
        /// \acute{x} = \frac{x - mean(x)}{max(x) - min(x)}.
        /// \f]
        /// </summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        /// <returns>An array with the same dimensions as tss, whose values (time series in dimension 0) have been
        /// normalized by subtracting the mean from each number and dividing each number by \f$ max(x) - min(x)\f$, in the
        /// time series.</returns>
        public static KhivaArray MeanNorm(KhivaArray tss)
        {
            var reference = tss.Reference;
            DLLNormalization.mean_norm(ref reference, out var result);
            tss.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Normalizes the given time series according to its maximum-minimum value and its mean. It follows the following
        /// formulae:
        /// \f[
        /// \acute{x} = \frac{x - mean(x)}{max(x) - min(x)}.
        /// \f]
        /// </summary>
        /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
        /// one indicates the number of time series.</param>
        public static void MeanNorm(ref KhivaArray tss)
        {
            var reference = tss.Reference;
            DLLNormalization.mean_norm_in_place(ref reference);
            tss.Reference = reference;
        }

        /// <summary>
        /// Calculates a new set of times series with zero mean and standard deviation one.
        /// </summary>
        /// <param name="tss">Time series concatenated in a single row.</param>
        /// <param name="epsilon"> Minimum standard deviation to consider. It acts as a gatekeeper for
        /// those time series that may be constant or near constant.</param>
        /// <returns>KhivaArray with the same dimensions as tss where the time series have been 
        /// adjusted for zero mean and one as standard deviation.</returns>
        public static KhivaArray ZNorm(KhivaArray tss, double epsilon)
        {
            var reference = tss.Reference;
            DLLNormalization.znorm(ref reference, ref epsilon, out var result);
            tss.Reference = reference;
            return KhivaArray.Create(result);
        }

        /// <summary>
        /// Adjusts the time series in the given input and performs z-norm inplace(without allocating further memory).
        /// </summary>
        /// <param name="tss"> Expects an input array whose dimension zero is the length of the time
        /// series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="epsilon">Minimum standard deviation to consider. It acts as a gatekeeper for
        /// those time series that may be constant or near constant.</param>
        public static void ZNorm(ref KhivaArray tss, double epsilon)
        {
            var reference = tss.Reference;
            DLLNormalization.znorm_in_place(ref reference, ref epsilon);
            tss.Reference = reference;
        }
    }
}