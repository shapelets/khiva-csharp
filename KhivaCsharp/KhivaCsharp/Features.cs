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

namespace khiva.features
{
    /// <summary>
    /// Khiva Features class containing a number of features that can be extracted from time series. All the methods
    /// operate with instances of the ARRAY class as input and output.
    /// </summary>
    public static class Features
    {

        /// <summary>
        /// Calculates the sum over the square values of the timeseries
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>An array with the same dimensions as array, whose values (time series in dimension 0) 
        /// contains the sum of the squares values in the time series.</returns>
        public static array.Array AbsEnergy(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.abs_energy(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the sum over the absolute value of consecutive changes in the time series.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>An array with the same dimensions as array, whose values (time series in dimension 0)
        /// contains absolute value of consecutive changes in the time series.</returns>
        public static array.Array AbsoluteSumOfChanges(array.Array array)
        {

            IntPtr reference = array.Reference;
            interop.DLLFeatures.absolute_sum_of_changes( ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the value of an aggregation function f_agg (e.g. var or mean) of the autocorrelation
        /// (Compare to http://en.wikipedia.org/wiki/Autocorrelation#Estimation), taken over different all possible
        /// lags(1 to length of x).</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="aggregationFunction">Function to be used in the aggregation.It receives an integer which indicates the
        /// function to be applied:
        ///          {
        ///             0 : mean,
        ///             1 : median
        ///             2 : min,
        ///             3 : max,
        ///             4 : stdev,
        ///             5 : var,
        ///             default : mean
        ///          }</param>
        /// <returns>An array whose values contains the aggregated correaltion for each time series.</returns>
        public static array.Array AggregatedAutocorrelation(array.Array array, int aggregationFunction)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.aggregated_autocorrelation(ref reference, ref aggregationFunction, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        ///  Calculates a linear least-squares regression for values of the time series that were aggregated
        /// over chunks versus the sequence from 0 up to the number of chunks minus one.
        /// </summary>
        /// <param name="array">The time series to calculate the features of</param>
        /// <param name="chunkSize">The chunk size used to aggregate the data.</param>
        /// <param name="aggregationFunction">Function to be used in the aggregation. It receives an integer which indicates the
        /// function to be applied:
        ///          {
        ///              0 : mean,
        ///              1 : median
        ///              2 : min,
        ///              3 : max,
        ///              4 : stdev,
        ///              default : mean
        ///         }
        ///</param>
        /// <returns>Tuple with:</returns>
        /// <returns>Slope of the regression line.</returns>
        /// <returns>Intercept of the regression line.</returns>
        /// <returns>Correlation coefficient.</returns>
        /// <returns>Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero, using Wald Test with t-distribution of the test statistic.</returns>
        /// <returns>Standard error of the estimated gradient.</returns>
        public static (array.Array, array.Array, array.Array, array.Array, array.Array) AggregatedLinearTrend(array.Array array, long chunkSize, int aggregationFunction)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.aggregated_linear_trend(ref reference,
                                                        ref chunkSize,
                                                        ref aggregationFunction,
                                                        out IntPtr slope, out IntPtr intercept, out IntPtr rvalue, out IntPtr pvalue, out IntPtr stderrest);
            array.Reference = reference;
            var tuple = (slopeArr: new array.Array(slope),
                    interceptArr: new array.Array(intercept),
                    rvalueArr: new array.Array(rvalue),
                    pvalueArr: new array.Array(pvalue),
                    stderrestArr: new array.Array(stderrest));
            return tuple;
        }

        /// <summary>
        /// Calculates a vectorized Approximate entropy algorithm.
        /// https://en.wikipedia.org/wiki/Approximate_entropy
        /// For short time-series this method is highly dependent on the parameters, but should be stable for N > 2000,
        /// see: Yentes et al. (2012) - The Appropriate Use of Approximate Entropy and Sample Entropy with Short Data Sets
        /// Other shortcomings and alternatives discussed in:
        /// Richman & Moorman (2000) - Physiological time-series analysis using approximate entropy and sample entropy.
        /// </summary>
        /// <param name="array"> Expects an input array whose dimension zero is the length of the time
        /// series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="m">Length of compared run of data.</param>
        /// <param name="r">Filtering level, must be positive.</param>
        /// <returns>The vectorized approximate entropy for all the input time series in array.</returns>
        public static array.Array ApproximateEntropy(array.Array array, int m, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.approximate_entropy(ref reference, ref m, ref r,  out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the cross-covariance of the given time series.
        /// </summary>
        /// <param name="xss">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="yss">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="unbiased">Determines whether it divides by n - lag (if true) or n(if false).</param>
        /// <returns>The cross-covariance value for the given time series.</returns>
        public static array.Array CrossCovariance(array.Array xss, array.Array yss, bool unbiased)
        {
            IntPtr referenceXss = xss.Reference;
            IntPtr referenceYss = yss.Reference;
            interop.DLLFeatures.cross_covariance(ref referenceXss, ref referenceYss, ref unbiased, out IntPtr result);
            xss.Reference = referenceXss;
            yss.Reference = referenceYss;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the auto-covariance the given time series.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="unbiased">Determines whether it divides by n - lag (if true) or n(if false).</param>
        /// <returns>The auto-covariance value for the given time series.</returns>
        public static array.Array AutoCovariance(array.Array array, bool unbiased = false)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.auto_covariance(ref reference, ref unbiased, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the cross-correlation of the given time series.
        /// </summary>
        /// <param name="xss">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="yss">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="unbiased"></param>
        /// <returns>The cross-correlation value for the given time series.</returns>
        public static array.Array CrossCorrelation(array.Array xss, array.Array yss, bool unbiased)
        {
            IntPtr referenceXss = xss.Reference;
            IntPtr referenceYss = yss.Reference;
            interop.DLLFeatures.cross_correlation(ref referenceXss, ref referenceYss, ref unbiased, out IntPtr result);
            xss.Reference = referenceXss;
            yss.Reference = referenceYss;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the autocorrelation of the specified lag for the given time series.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="max_lag">The maximum lag to compute.</param>
        /// <param name="unbiased">Determines whether it divides by n - lag (if true) or n ( if false)</param>
        /// <returns>The autocorrelation value for the given time series.</returns>
        public static array.Array AutoCorrelation(array.Array array, long max_lag, bool unbiased)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.auto_correlation(ref reference, ref max_lag, ref unbiased, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the binned entropy for the given time series and number of bins.
        /// </summary>
        /// <param name="array"> Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="max_bins">The number of bins.</param>
        /// <returns>The binned entropy value for the given time series.</returns>
        public static array.Array BinnedEntropy(array.Array array, int max_bins)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.binned_entropy(ref reference, ref max_bins, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the Schreiber, T. and Schmitz, A. (1997) measure of non-linearity for the given time series.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="lag">The lag</param>
        /// <returns>The non-linearity value for the given time series.</returns>
        public static array.Array C3(array.Array array, long lag)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.c3(ref reference, ref lag, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));

        }

        /// <summary>
        /// Calculates an estimate for the time series complexity defined by
        /// Batista, Gustavo EAPA, et al(2014). (A more complex time series has more peaks, valleys, etc.).
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="zNormalize">Controls whether the time series should be z-normalized or not.</param>
        /// <returns>The complexity value for the given time series.</returns>
        public static array.Array CidCe(array.Array array, bool zNormalize)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.cid_ce(ref reference, ref zNormalize, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the number of values in the time series that are higher than the mean.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The number of values in the time series that are higher than the mean.</returns>
        public static array.Array CountAboveMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.count_above_mean(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the number of values in the time series that are lower than the mean.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The number of values in the time series that are lower than the mean.</returns>
        public static array.Array CountBelowMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.count_below_mean(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates a Continuous wavelet transform for the Ricker wavelet, also known as
        /// the "Mexican hat wavelet" which is defined by:
        ///
        ///  .. math::
        ///      \frac{2}{\sqrt{3a} \pi^{
        ///  \frac{1} { 4 }}} (1 - \frac{x^2}{a^2}) exp(-\frac{ x ^ 2}{2a^2})
        ///
        /// where :math:`a` is the width parameter of the wavelet function.
        ///
        /// This feature calculator takes three different parameter: widths, coeff and w.The feature calculator takes all
        /// the different widths arrays and then calculates the cwt one time for each different width array.Then the values
        /// for the different coefficient for coeff and width w are returned. (For each dic in param one feature is
        /// returned).
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="width">Array that contains all different widths.</param>
        /// <param name="coeff">Coefficient of interest.</param>
        /// <param name="w">Width of interest.</param>
        /// <returns>Result of calculated coefficients.</returns>
        public static array.Array CwtCoefficients(array.Array array, array.Array width, int coeff, int w)
        {
            IntPtr reference = array.Reference;
            IntPtr widthReference = width.Reference;
            interop.DLLFeatures.cwt_coefficients(ref reference, ref widthReference, ref coeff, ref w, out IntPtr result);
            array.Reference = reference;
            width.Reference = widthReference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the sum of squares of chunk i out of N chunks expressed as a ratio.
        /// with the sum of squares over the whole series.segmentFocus should be lower than the number of segments
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="num_segments">The number of segments to divide the series into.</param>
        /// <param name="segment_focus">The segment number (starting at zero) to return a feature on.</param>
        /// <returns>The energy ratio by chunk of the time series.</returns>
        public static array.Array EnergyRatioByChunks(array.Array array, long num_segments, long segment_focus)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.energy_ratio_by_chunks(ref reference, ref num_segments, ref segment_focus, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the spectral centroid(mean), variance, skew, and kurtosis of the absolute fourier transform spectrum.
        /// </summary>
        /// <param name="array"> Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The spectral centroid (mean), variance, skew, and kurtosis of the absolute fourier transform spectrum.</returns>
        public static array.Array FftAggregated(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.fft_aggregated(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the fourier coefficients of the one-dimensional discrete Fourier Transform for real input by fast fourier transformation algorithm.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="coefficient">The coefficient to extract from the FFT.</param>
        /// <returns>tuple with:
        /// The real part of the coefficient.
        /// The imaginary part of the cofficient.
        /// The absolute value of the coefficient.
        /// The angle of the coefficient.</returns>
        public static (array.Array, array.Array, array.Array, array.Array) FftCoefficient(array.Array array, long coefficient)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.fft_coefficient(ref reference, ref coefficient,
                                                out IntPtr real, out IntPtr imag, out IntPtr absolute, out IntPtr angle);
            array.Reference = reference;
            var tuple = (realArr: new array.Array(real),
                        imagArr: new array.Array(imag),
                        absoluteArr: new array.Array(absolute),
                        angleArr: new array.Array(angle));
            return tuple;
        }

        /// <summary>
        /// Calculates the first relative location of the maximal value for each time series.
        /// </summary>
        /// <param name="array">array Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The first relative location of the maximum value to the length of the time series, for each time series.</returns>
        public static array.Array FirstLocationOfMaximum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.first_location_of_maximum(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the first location of the minimal value of each time series. The position is calculated relatively to the length of the series.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The first relative location of the minimal value of each series.</returns>
        public static array.Array FirstLocationOfMinimum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.first_location_of_minimum(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Coefficients of polynomial \f$h(x)\f$, which has been fitted to the deterministic dynamics of Langevin model:
        /// \f[
        ///    \dot(x)(t) = h(x(t)) + R \mathcal(N)(0,1)
        /// \f]
        /// as described by[1]. For short time series this method is highly dependent on the parameters.
        ///
        ///[1] Friedrich et al. (2000): Physics Letters A 271, p. 217-222
        /// Extracting model equations from experimental data.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="m">Order of polynom to fit for estimating fixed points of dynamics.</param>
        /// <param name="r">Number of quantils to use for averaging.</param>
        /// <returns>The coefficients for each time series.</returns>
        public static array.Array FriedrichCoefficients(array.Array array, int m, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.friedrich_coefficients(ref reference, ref m, ref r, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates if the input time series contain duplicated elements.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>Array containing True if the time series contains duplicated elements and false otherwise.</returns>
        public static array.Array HasDuplicates(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.has_duplicates(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates if the maximum within input time series is duplicated.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>Array containing True if the maximum value of the time series is duplicated and false otherwise.</returns>
        public static array.Array HasDuplicateMax(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.has_duplicate_max(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates if the minimum of the input time series is duplicated.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>Array containing True if the minimum of the time series is duplicated and false otherwise.</returns>
        public static array.Array HasDuplicateMin(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.has_duplicate_min(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the index of the max quantile.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="q">The quantile.</param>
        /// <returns>The index of the max quantile q.</returns>
        public static array.Array IndexMassQuantile(array.Array array, float q)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.index_mass_quantile(ref reference, ref q, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        ///  Returns the kurtosis of array (calculated with the adjusted Fisher-Pearson standardized moment coefficient G2).
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The kurtosis of each array.</returns>
        public static array.Array Kurtosis(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.kurtosis(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Checks if the time series within array have a large standard deviation.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="r">The threshold.</param>
        /// <returns>Array containing True for those time series in array that have a large standard deviation.</returns>
        public static array.Array LargeStandardDeviation(array.Array array, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.large_standard_deviation(ref reference, ref r, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the last location of the maximum value of each time series. The position is calculated relatively to the length of the series.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The last relative location of the maximum value of each series.</returns>
        public static array.Array LastLocationOfMaximum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.last_location_of_maximum(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the last location of the minimum value of each time series. The position is calculated relatively to the length of the series.
        /// </summary>
        /// <param name="array"> Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The last relative location of the minimum value of each series.</returns>
        public static array.Array LastLocationOfMinimum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.last_location_of_minimum(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Returns the length of the input time series.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The length of the time series.</returns>
        public static array.Array Length(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.length(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculate a linear least-squares regression for the values of the time series versus the sequence from 0 to length of the time series minus one.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>Tuple with:
        /// The pvalues for all time series.
        /// The rvalues for all time series.
        /// The intercept values for all time series.
        /// The slope for all time series.
        /// The stderr values for all time series.</returns>
        public static (array.Array, array.Array, array.Array, array.Array, array.Array) LinearTrend(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.linear_trend(ref reference,
                                        out IntPtr pvalue, out IntPtr rvalue, out IntPtr intercept, out IntPtr slope, out IntPtr stdrr);
            array.Reference = reference;
            var tuple = (pvalueArr: new array.Array(pvalue),
                        rvalueArr: new array.Array(rvalue),
                        interceptArr: new array.Array(intercept),
                        slopeArr: new array.Array(slope),
                        stdrrArr: new array.Array(stdrr));
            return tuple;
        }

        /// <summary>
        /// Calculates all Local Maximals fot the time series in array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <returns>The calculated local maximals for each time series in array.</returns>
        public static array.Array LocalMaximals(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.local_maximals(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the length of the longest consecutive subsequence in array that is bigger than the mean of array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The length of the longest consecutive subsequence in the input time series that is bigger than the mean.</returns>
        public static array.Array LongestStrikeAboveMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.longest_strike_above_mean(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the length of the longest consecutive subsequence in array that is below the mean of array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The length of the longest consecutive subsequence in the input time series that is below the mean.</returns>
        public static array.Array LongestStrikeBelowMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.longest_strike_below_mean(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }
       
        /// <summary>
        /// Largest fixed point of dynamics \f$\max_x {h(x)=0}\f$ estimated from polynomial
        /// \f$h(x)\f$, which has been fitted to the deterministic dynamics of Langevin model
        /// \f[
        ///    \dot(x)(t) = h(x(t)) + R \mathcal(N)(0, 1)
        /// \f]
        /// as described by
        /// Friedrich et al. (2000): Physics Letters A 271, p. 217-222 *Extracting model equations from experimental data.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="m">Order of polynom to fit for estimating fixed points of dynamics.</param>
        /// <param name="r">Number of quantiles to use for averaging.</param>
        /// <returns>Largest fixed point of deterministic dynamics.</returns>
        public static array.Array MaxLangevinFixedPoint(array.Array array, int m, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.max_langevin_fixed_point(ref reference, ref m, ref r, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the maximum value for each time series within array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The maximum value of each time series within array.</returns>
        public static array.Array Maximum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.maximum(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the mean value for each time series within array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The mean value of each time series within array.</returns>
        public static array.Array Mean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the mean over the absolute differences between subsequent time series values in array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The maximum value of each time series within array.</returns>
        public static array.Array MeanAbsoluteChange(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean_absolute_change(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the mean over the differences between subsequent time series values in array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The mean over the differences between subsequent time series values.</returns>
        public static array.Array MeanChange(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean_change(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates mean value of a central approximation of the second derivative for each time series in array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The mean value of a central approximation of the second derivative for each time series.</returns>
        public static array.Array MeanSecondDerivativeCentral(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean_second_derivative_central(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the median value for each time series within array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The median value of each time series within array.</returns>
        public static array.Array Median(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.median(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the minimum value for each time series within array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The minimum value of each time series within array.</returns>
        public static array.Array Minimum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.minimum(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the number of m-crossings. A m-crossing is defined as two sequential values where the first
        /// value is lower than m and the next is greater, or viceversa.If you set m to zero, you will get the number of
        /// zero crossings.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="m">The m value.</param>
        /// <returns>The number of m-crossings of each time series within array.</returns>
        public static array.Array NumberCrossingM(array.Array array, int m)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.number_crossing_m(ref reference, ref m, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// This feature calculator searches for different peaks. To do so, the time series is smoothed by a ricker
        /// wavelet and for widths ranging from 1 to max_w.This feature calculator returns the number of peaks that occur at
        /// enough width scales and with sufficiently high Signal-to-Noise-Ratio (SNR).
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="max_w">The maximum width to consider.</param>
        /// <returns>The number of peaks for each time series.</returns>
        public static array.Array NumberCwtPeaks(array.Array array, int max_w)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.number_cwt_peaks(ref reference, ref max_w, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the number of peaks of at least support \f$n\f$ in the time series \f$array\f$. A peak of support
        /// \f$n\f$ is defined as a subsequence of \f$array\f$ where a value occurs, which is bigger than its \f$n\f$ neighbours
        /// to the left and to the right.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="n">The support of the peak.</param>
        /// <returns>The number of peaks of at least support \f$n\f$.</returns>
        public static array.Array NumberPeaks(array.Array array, int n)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.number_peaks(ref reference, ref n, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        ///  Calculates the value of the partial autocorrelation function at the given lag. The lag \f$k\f$ partial
        /// autocorrelation of a time series \f$\lbrace x_t, t = 1 \ldots T \rbrace\f$ equals the partial correlation of
        /// \f$x_t\f$ and \f$x_{t-k}\f$, adjusted for the intermediate variables \f$\lbrace x_ { t-1}, \ldots, x_{t-k+1}
        /// \rbrace\f$ ([1]). Following[2], it can be defined as:
        ///
        /// \f[
        ///      \alpha_k = \frac{ Cov(x_t, x_{ t - k} | x_{t-1}, \ldots, x_{t-k+1})}
        ///      {\sqrt{ Var(x_t | x_{ t - 1}, \ldots, x_{t-k+1}) Var(x_{ t - k} | x_{t-1}, \ldots, x_{t-k+1} )}}
        /// \f]
        /// with(a) \f$x_t = f(x_{ t - 1}, \ldots, x_{t-k+1})\f$ and(b) \f$ x_{t-k} = f(x_{ t - 1}, \ldots, x_{t-k+1})\f$
        /// being AR(k-1) models that can be fitted by OLS.Be aware that in (a), the regression is done on past values to
        /// predict \f$ x_t \f$ whereas in (b), future values are used to calculate the past value \f$x_{t-k}\f$.
        /// It is said in [1] that "for an AR(p), the partial autocorrelations \f$ \alpha_k \f$ will be nonzero for \f$ k \le p \f$
        /// and zero for \f$ k \gt p \f$."
        /// With this property, it is used to determine the lag of an AR-Process.
        ///
        ///[1] Box, G.E., Jenkins, G.M., Reinsel, G.C., & Ljung, G.M. (2015).
        /// Time series analysis: forecasting and control. John Wiley & Sons.
        /// [2] https://onlinecourses.science.psu.edu/stat510/node/62
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="lags">Indicates the lags to be calculated.</param>
        /// <returns>Returns partial autocorrelation for each time series for the given lag.</returns>
        public static array.Array PartialAutocorrelation(array.Array array, array.Array lags)
        {
            IntPtr reference = array.Reference;
            IntPtr lagsReference = lags.Reference;
            interop.DLLFeatures.partial_autocorrelation(ref reference, ref lagsReference, out IntPtr result);
            array.Reference = reference;
            lags.Reference = lagsReference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the percentage of unique values, that are present in the time series more than once.
        /// \f[
        ///len(different values occurring more than once) / len(different values)
        /// \f]
        /// This means the percentage is normalized to the number of unique values, in contrast to the
        /// percentageOfReoccurringValuesToAllValues.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="is_sorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <returns>Returns the percentage of unique values, that are present in the time series more than once.</returns>
        public static array.Array PercentageOfReoccurringDatapointsToAllDatapoints(array.Array array, bool is_sorted=false)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.percentage_of_reoccurring_datapoints_to_all_datapoints(ref reference, ref is_sorted, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        ///  Calculates the percentage of unique values, that are present in the time series more than once.
        /// \f[
        ///      \frac{\textit{number of data points occurring more than once}}{\textit{number of all data points})}
        /// \f]
        /// This means the percentage is normalized to the number of unique values, in contrast to the
        /// percentageOfReoccurringDatapointsToAllDatapoints.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="is_sorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <returns>Returns the percentage of unique values, that are present in the time series more than once.</returns>
        public static array.Array PercentageOfReoccurringValuesToAllValues(array.Array array, bool is_sorted=false)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.percentage_of_reoccurring_values_to_all_values(ref reference, ref is_sorted, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Returns values at the given quantile.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="q">Percentile(s) at which to extract score(s). One or many.</param>
        /// <param name="precision">Number of decimals expected. Defaults to 1e8F</param>
        /// <returns>Values at the given quantile.</returns>
        public static array.Array Quantile(array.Array array, array.Array q, float precision = 1e8F)
        {
            IntPtr reference = array.Reference;
            IntPtr qReference = q.Reference;
            interop.DLLFeatures.quantile(ref reference, ref qReference, ref precision, out IntPtr result);
            array.Reference = reference;
            q.Reference = qReference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Counts observed values within the interval [min, max).
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="min">Value that sets the lower limit.</param>
        /// <param name="max">Value that sets the upper limit.</param>
        /// <returns>Values at the given range.</returns>
        public static array.Array RangeCount(array.Array array, float min, float max)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.range_count(ref reference, ref min, ref max, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }
       
        /// <summary>
        /// Calculates the ratio of values that are more than \f$r*std(x)\f$ (so \f$r\f$ sigma) away from the mean of
        /// \f$x\f$.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="r"> Number of times that the values should be away from.</param>
        /// <returns>The ratio of values that are more than \f$r*std(x)\f$ (so \f$r\f$ sigma) away from the mean of
        /// \f$x\f$.</returns>
        public static array.Array RatioBeyondRSigma(array.Array array, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.ratio_beyond_r_sigma(ref reference, ref r, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates a factor which is 1 if all values in the time series occur only once, and below one if this is
        /// not the case. In principle, it just returns:
        ///
        /// \f[
        ///      \frac{\textit{number\_unique\_values}}{\textit{number\_values}}
        /// \f]
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>The ratio of unique values with respect to the total number of values.</returns>
        public static array.Array RatioValueNumberToTimeSeriesLength(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.ratio_value_number_to_time_series_length(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates a vectorized sample entropy algorithm.
        /// https://en.wikipedia.org/wiki/Sample_entropy
        /// https://www.ncbi.nlm.nih.gov/pubmed/10843903?dopt=Abstract
        /// For short time-series this method is highly dependent on the parameters, but should be stable for N > 2000,
        /// see: Yentes et al. (2012) - The Appropriate Use of Approximate Entropy and Sample Entropy with Short Data Sets
        /// Other shortcomings and alternatives discussed in:
        /// Richman & Moorman (2000) - Physiological time-series analysis using approximate entropy and sample entropy.
        /// </summary>
        /// <param name="array"> Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <returns>An array with the same dimensions as array, whose values (time series in dimension 0)
        /// contains the vectorized sample entropy for all the input time series in array.</returns>
    public static array.Array SampleEntropy(array.Array array)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.sample_entropy(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the sample skewness of array (calculated with the adjusted Fisher-Pearson standardized
        /// moment coefficient G1).
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <returns>Array containing the skewness of each time series in array.</returns>
        public static array.Array Skewness(array.Array array)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.skewness(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Estimates the cross power spectral density of the time series array at different frequencies. To do so, the
        /// time series is first shifted from the time domain to the frequency domain.
        ///
        /// Welch's method computes an estimate of the power spectral density by dividing the data into overlapping
        /// segments, computing a modified periodogram for each segment and averaging the periodograms.
        /// [1] P.Welch, "The use of the fast Fourier transform for the estimation of power spectra: A method based on time
        ///  averaging over short, modified periodograms", IEEE Trans. Audio Electroacoust. vol. 15, pp. 70-73, 1967.
        /// [2] M.S.Bartlett, "Periodogram Analysis and Continuous Spectra", Biometrika, vol. 37, pp. 1-16, 1950.
        /// [3] Rabiner, Lawrence R., and B. Gold. "Theory and Application of Digital Signal Processing" Prentice-Hall, pp.
        /// 414-419, 1975.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="coeff">The coefficient to be returned.</param>
        /// <returns>Array containing the power spectrum of the different frequencies for each time series in array.</returns>
        public static array.Array SpktWelchDensity(array.Array array, int coeff)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.spkt_welch_density(ref reference, ref coeff, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the standard deviation of each time series within array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <returns>The standard deviation of each time series within array.</returns>
        public static array.Array StandardDeviation(array.Array array)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.standard_deviation(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the sum of all data points, that are present in the time series more than once.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="is_sorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <returns>Returns the sum of all data points, that are present in the time series more than once.</returns>
        public static array.Array SumOfReoccurringDatapoints(array.Array array, bool is_sorted = false)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.sum_of_reoccurring_datapoints(ref reference, ref is_sorted, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the sum of all values, that are present in the time series more than once.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="is_sorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <returns>Returns the sum of all values, that are present in the time series more than once.</returns>
        public static array.Array SumOfReoccurringValues(array.Array array, bool is_sorted = false)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.sum_of_reoccurring_values(ref reference, ref is_sorted, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates the sum over the time series array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>An array containing the sum of values in each time series.</returns>
        public static array.Array SumValues(array.Array array)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.sum_values(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates if the distribution of array *looks symmetric*. This is the case if
        /// \f[
        ///      | mean(array) - median(array) | \lt r * (max(array) - min(array))
        /// \f]
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="r">The percentage of the range to compare with.</param>
        /// <returns>An array denoting if the input time series look symmetric.</returns>
        public static array.Array SymmetryLooking(array.Array array, float r)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.symmetry_looking(ref reference, ref r, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// This function calculates the value of:
        /// \f[
        ///      \frac{1}{n-2lag} \sum_{i=0}^{n-2lag} x_{i + 2 \cdot lag}^2 \cdot x_ { i + lag } - x_{i + lag} \cdot x_ { i }^2
        /// \f]
        /// which is
        /// \f[
        ///       \mathbb{E}[L^2(X)^2 \cdot L(X) - L(X) \cdot X^2]
        /// \f]
        /// where \f$ \mathbb{E} \f$ is the mean and \f$ L \f$ is the lag operator. It was proposed in [1] as a promising
        /// feature to extract from time series.
        ///
        /// [1] Fulcher, B.D., Jones, N.S. (2014). Highly comparative feature-based time-series classification.
        /// Knowledge and Data Engineering, IEEE Transactions on 26, 3026–3037.
        /// </summary>
        /// <param name="array">xpects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="lag">The lag to be computed.</param>
        /// <returns>An array containing the time reversal asymetry statistic value in each time series.</returns>
        public static array.Array TimeReversalAsymmetryStatistic(array.Array array, int lag)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.time_reversal_asymmetry_statistic(ref reference, ref lag, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Counts occurrences of value in the time series array.
        /// </summary>
        /// <param name="array"> Expects an input array whose dimension zero is the length of the
        /// time series(all the same) and dimension one indicates the number of time series.</param>
        /// <param name="v">The value to be counted.</param>
        /// <returns>An array containing the count of the given value in each time series.</returns>
        public static array.Array ValueCount(array.Array array, float v)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.value_count(ref reference, ref v, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Computes the variance for the time series array.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>An array containing the variance in each time series.</returns>
        public static array.Array Variance(array.Array array)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.variance(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));
        }

        /// <summary>
        /// Calculates if the variance of array is greater than the standard deviation. In other words, if the variance of
        /// array is larger than 1.
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <returns>An array denoting if the variance of array is greater than the standard deviation.</returns>
        public static array.Array VarianceLargerThanStandardDeviation(array.Array array)
       {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.variance_larger_than_standard_deviation(ref reference, out IntPtr result);
            array.Reference = reference;
            return (new array.Array(result));}
       
    }
}
