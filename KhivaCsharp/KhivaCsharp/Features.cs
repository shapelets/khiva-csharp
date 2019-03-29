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
    public class Features
    {

        /**
         * @brief Calculates the sum over the square values of the timeseries
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of
         * time series.
         * @return result An array with the same dimensions as array, whose values (time series in dimension 0)
         * contains the sum of the squares values in the time series.
         */
        public static array.Array AbsEnergy(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.abs_energy(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the sum over the absolute value of consecutive changes in the time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of
         * time series.
         * @return result An array with the same dimensions as array, whose values (time series in dimension 0)
         * contains absolute value of consecutive changes in the time series.
         */
        public static array.Array AbsoluteSumOfChanges(array.Array array)
        {

            IntPtr reference = array.Reference;
            interop.DLLFeatures.absolute_sum_of_changes( ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the value of an aggregation function f_agg (e.g. var or mean) of the autocorrelation
         * (Compare to http://en.wikipedia.org/wiki/Autocorrelation#Estimation), taken over different all possible
         * lags (1 to length of x).
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of time series.
         * @param aggregation_function Function to be used in the aggregation. It receives an integer which indicates the
         * function to be applied:
         *          {
         *              0 : mean,
         *              1 : median
         *              2 : min,
         *              3 : max,
         *              4 : stdev,
         *              5 : var,
         *              default : mean
         *          }
         * @return result An array whose values contains the aggregated correaltion for each time series.
         */
        public static array.Array AggregatedAutocorrelation(array.Array array, int aggregationFunction)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.aggregated_autocorrelation(ref reference, ref aggregationFunction, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates a linear least-squares regression for values of the time series that were aggregated
         * over chunks versus the sequence from 0 up to the number of chunks minus one.
         *
         * @param array The time series to calculate the features of
         * @param chunkSize The chunk size used to aggregate the data.
         * @param aggregation_function Function to be used in the aggregation. It receives an integer which indicates the
         * function to be applied:
         *          {
         *              0 : mean,
         *              1 : median
         *              2 : min,
         *              3 : max,
         *              4 : stdev,
         *              default : mean
         *          }
         * @return slope Slope of the regression line.
         * @return intercept Intercept of the regression line.
         * @return rvalue Correlation coefficient.
         * @return pvalue Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero,
         * using Wald Test with t-distribution of the test statistic.
         * @return stderrest Standard error of the estimated gradient.
         */
        public static (array.Array, array.Array, array.Array, array.Array, array.Array) AggregatedLinearTrend(array.Array array, long chunkSize, int aggregationFunction)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.aggregated_linear_trend(ref reference,
                                                        ref chunkSize,
                                                        ref aggregationFunction,
                                                        out IntPtr slope, out IntPtr intercept, out IntPtr rvalue, out IntPtr pvalue, out IntPtr stderrest);
            var tuple = (slopeArr: new array.Array(slope),
                    interceptArr: new array.Array(intercept),
                    rvalueArr: new array.Array(rvalue),
                    pvalueArr: new array.Array(pvalue),
                    stderrestArr: new array.Array(stderrest));
            return tuple;
        }

        /**
         * @brief Calculates a vectorized Approximate entropy algorithm.
         * https://en.wikipedia.org/wiki/Approximate_entropy
         * For short time-series this method is highly dependent on the parameters, but should be stable for N > 2000,
         * see: Yentes et al. (2012) - The Appropriate Use of Approximate Entropy and Sample Entropy with Short Data Sets
         * Other shortcomings and alternatives discussed in:
         * Richman & Moorman (2000) - Physiological time-series analysis using approximate entropy and sample entropy.
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of
         * time series.
         * @param m Length of compared run of data.
         * @param r Filtering level, must be positive.
         * @return result The vectorized approximate entropy for all the input time series in array.
         */
        public static array.Array ApproximateEntropy(array.Array array, int m, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.approximate_entropy(ref reference, ref m, ref r,  out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the cross-covariance of the given time series.
         *
         * @param xss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param yss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param unbiased Determines whether it divides by n - lag (if true) or
         * n (if false).
         * @return result The cross-covariance value for the given time series.
         */
        public static array.Array CrossCovariance(array.Array xss, array.Array yss, bool unbiased)
        {
            IntPtr referenceXss = xss.Reference;
            IntPtr referenceYss = yss.Reference;
            interop.DLLFeatures.cross_covariance(ref referenceXss, ref referenceYss, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the auto-covariance the given time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param unbiased Determines whether it divides by n - lag (if true) or
         * n (if false).
         * @return result The auto-covariance value for the given time series.
         */
        public static array.Array AutoCovariance(array.Array array, bool unbiased = false)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.auto_covariance(ref reference, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the cross-correlation of the given time series.
         *
         * @param xss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param yss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param unbiased Determines whether it divides by n - lag (if true) or
         * n (if false).
         * @return result The cross-correlation value for the given time series.
         */
        public static array.Array CrossCorrelation(array.Array xss, array.Array yss, bool unbiased)
        {
            IntPtr referenceXss = xss.Reference;
            IntPtr referenceYss = yss.Reference;
            interop.DLLFeatures.cross_correlation(ref referenceXss, ref referenceYss, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the autocorrelation of the specified lag for the given time.
         * series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param max_lag The maximum lag to compute.
         * @param unbiased Determines whether it divides by n - lag (if true) or n ( if false)
         * @return result The autocorrelation value for the given time series.
         */
        public static array.Array AutoCorrelation(array.Array array, long max_lag, bool unbiased)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.auto_correlation(ref reference, ref max_lag, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the binned entropy for the given time series and number of bins.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param max_bins The number of bins.
         * @return result The binned entropy value for the given time series.
         */
        public static array.Array BinnedEntropy(array.Array array, int max_bins)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.binned_entropy(ref reference, ref max_bins, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the Schreiber, T. and Schmitz, A. (1997) measure of non-linearity
         * for the given time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param lag The lag
         * @return result The non-linearity value for the given time series.
         */
        public static array.Array C3(array.Array array, long lag)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.c3(ref reference, ref lag, out IntPtr result);
            return (new array.Array(result));

        }

        /**
         * @brief Calculates an estimate for the time series complexity defined by
         * Batista, Gustavo EAPA, et al (2014). (A more complex time series has more peaks,
         * valleys, etc.).
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param zNormalize Controls whether the time series should be z-normalized or not.
         * @return result The complexity value for the given time series.
         */
        public static array.Array CidCe(array.Array array, bool zNormalize)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.cid_ce(ref reference, ref zNormalize, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the number of values in the time series that are higher than
         * the mean.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The number of values in the time series that are higher
         * than the mean.
         */
        public static array.Array CountAboveMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.count_above_mean(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
        * @brief Calculates the number of values in the time series that are lower than
        * the mean.
        *
        * @param array Expects an input array whose dimension zero is the length of the
        * time series (all the same) and dimension one indicates the number of time
        * series.
        * @return result The number of values in the time series that are lower
        * than the mean.
        */
        public static array.Array CountBelowMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.count_below_mean(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates a Continuous wavelet transform for the Ricker wavelet, also known as
         * the "Mexican hat wavelet" which is defined by:
         *
         *  .. math::
         *      \frac{2}{\sqrt{3a} \pi^{
         *  \frac{1} { 4 }}} (1 - \frac{x^2}{a^2}) exp(-\frac{x^2}{2a^2})
         *
         *  where :math:`a` is the width parameter of the wavelet function.
         *
         * This feature calculator takes three different parameter: widths, coeff and w. The feature calculator takes all
         * the different widths arrays and then calculates the cwt one time for each different width array. Then the values
         * for the different coefficient for coeff and width w are returned. (For each dic in param one feature is
         * returned).
         *
         * @param array Expects an input array whose dimension zero is the length of the time series (all the same)
         * and dimension one indicates the number of time series.
         * @param width Array that contains all different widths.
         * @param coeff Coefficient of interest.
         * @param w Width of interest.
         * @return result Result of calculated coefficients.
         */
        public static array.Array CwtCoefficients(array.Array array, array.Array width, int coeff, int w)
        {
            IntPtr reference = array.Reference;
            IntPtr widthReference = width.Reference;
            interop.DLLFeatures.cwt_coefficients(ref reference, ref widthReference, ref coeff, ref w, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the sum of squares of chunk i out of N chunks expressed as a ratio.
         * with the sum of squares over the whole series. segmentFocus should be lower
         * than the number of segments
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param num_segments The number of segments to divide the series into.
         * @param segment_focus The segment number (starting at zero) to return a feature on.
         * @return result The energy ratio by chunk of the time series.
         */
        public static array.Array EnergyRatioByChunks(array.Array array, long num_segments, long segment_focus)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.energy_ratio_by_chunks(ref reference, ref num_segments, ref segment_focus, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the spectral centroid(mean), variance, skew, and kurtosis of the absolute fourier transform
         * spectrum.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The spectral centroid (mean), variance, skew, and kurtosis of the absolute fourier transform
         * spectrum.
         */
        public static array.Array FftAggregated(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.fft_aggregated(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the fourier coefficients of the one-dimensional discrete
         * Fourier Transform for real input by fast fourier transformation algorithm.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param coefficient The coefficient to extract from the FFT.
         * @return real The real part of the coefficient.
         * @return imag The imaginary part of the cofficient.
         * @return absolute The absolute value of the coefficient.
         * @return angle The angle of the coefficient.
         */
        public static (array.Array, array.Array, array.Array, array.Array) FftCoefficient(array.Array array, long coefficient)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.fft_coefficient(ref reference, ref coefficient,
                                                out IntPtr real, out IntPtr imag, out IntPtr absolute, out IntPtr angle);
            var tuple = (realArr: new array.Array(real),
                        imagArr: new array.Array(imag),
                        absoluteArr: new array.Array(absolute),
                        angleArr: new array.Array(angle));
            return tuple;
        }

        /**
         * @brief Calculates the first relative location of the maximal value for each time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The first relative location of the maximum value to the length of the time series,
         *  for each time series.
         */
        public static array.Array FirstLocationOfMaximum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.first_location_of_maximum(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the first location of the minimal value of each time series. The position
         * is calculated relatively to the length of the series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The first relative location of the minimal value of each series.
         */
        public static array.Array FirstLocationOfMinimum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.first_location_of_minimum(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Coefficients of polynomial \f$h(x)\f$, which has been fitted to the deterministic
         * dynamics of Langevin model:
         * \f[
         *    \dot(x)(t) = h(x(t)) + R \mathcal(N)(0,1)
         * \f]
         * as described by [1]. For short time series this method is highly dependent on the parameters.
         *
         * [1] Friedrich et al. (2000): Physics Letters A 271, p. 217-222
         * Extracting model equations from experimental data.
         *
         * @param array Expects an input array whose dimension zero is the length of the time series (all the same)
         * and dimension one indicates the number of time series.
         * @param m Order of polynom to fit for estimating fixed points of dynamics.
         * @param r Number of quantils to use for averaging.
         * @return result The coefficients for each time series.
         */
        public static array.Array FriedrichCoefficients(array.Array array, int m, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.friedrich_coefficients(ref reference, ref m, ref r, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates if the input time series contain duplicated elements.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result Array containing True if the time series contains duplicated elements
         * and false otherwise.
         */
        public static array.Array HasDuplicates(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.has_duplicates(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates if the maximum within input time series is duplicated.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result Array containing True if the maximum value of the time series is duplicated
         * and false otherwise.
         */
        public static array.Array HasDuplicateMax(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.has_duplicate_max(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates if the minimum of the input time series is duplicated.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result Array containing True if the minimum of the time series is duplicated
         * and false otherwise.
         */
        public static array.Array HasDuplicateMin(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.has_duplicate_min(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the index of the max quantile.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param q The quantile.
         * @return result The index of the max quantile q.
         */
        public static array.Array IndexMassQuantile(array.Array array, float q)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.index_mass_quantile(ref reference, ref q, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Returns the kurtosis of array (calculated with the adjusted Fisher-Pearson
         * standardized moment coefficient G2).
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The kurtosis of each array.
         */
        public static array.Array Kurtosis(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.kurtosis(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Checks if the time series within array have a large standard deviation.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param r The threshold.
         * @return result  Array containing True for those time series in array that have a large standard deviation.
         */
        public static array.Array LargeStandardDeviation(array.Array array, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.large_standard_deviation(ref reference, ref r, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the last location of the maximum value of each time series. The position
         * is calculated relatively to the length of the series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The last relative location of the maximum value of each series.
         */
        public static array.Array LastLocationOfMaximum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.last_location_of_maximum(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the last location of the minimum value of each time series. The position
         * is calculated relatively to the length of the series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The last relative location of the minimum value of each series.
         */
        public static array.Array LastLocationOfMinimum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.last_location_of_minimum(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Returns the length of the input time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The length of the time series.
         */
        public static array.Array Length(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.length(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculate a linear least-squares regression for the values of the time series versus the sequence from 0 to
         * length of the time series minus one.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return pvalue The pvalues for all time series.
         * @return rvalue The rvalues for all time series.
         * @return intercept The intercept values for all time series.
         * @return slope The slope for all time series.
         * @return stdrr The stderr values for all time series.
         */
        public static (array.Array, array.Array, array.Array, array.Array, array.Array) LinearTrend(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.linear_trend(ref reference,
                                        out IntPtr pvalue, out IntPtr rvalue, out IntPtr intercept, out IntPtr slope, out IntPtr stdrr);
            var tuple = (pvalueArr: new array.Array(pvalue),
                        rvalueArr: new array.Array(rvalue),
                        interceptArr: new array.Array(intercept),
                        slopeArr: new array.Array(slope),
                        stdrrArr: new array.Array(stdrr));
            return tuple;
        }

        /**
         * @brief Calculates all Local Maximals fot the time series in array.
         *
         * @param array Expects an input array whose dimension zero is the length of the time series (all the same)
         * and dimension one indicates the number of time series.
         * @return result The calculated local maximals for each time series in array.
         */
        public static array.Array LocalMaximals(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.local_maximals(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the length of the longest consecutive subsequence in array that is bigger than the mean of array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The length of the longest consecutive subsequence in the input time series that is bigger than the
         * mean.
         */
        public static array.Array LongestStrikeAboveMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.longest_strike_above_mean(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the length of the longest consecutive subsequence in array that is below the mean of array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The length of the longest consecutive subsequence in the input time series that is below the mean.
         */
        public static array.Array LongestStrikeBelowMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.longest_strike_below_mean(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Largest fixed point of dynamics \f$\max_x {h(x)=0}\f$ estimated from polynomial
         * \f$h(x)\f$, which has been fitted to the deterministic dynamics of Langevin model
         * \f[
         *    \dot(x)(t) = h(x(t)) + R \mathcal(N)(0,1)
         * \f]
         * as described by
         * Friedrich et al. (2000): Physics Letters A 271, p. 217-222 *Extracting model equations from experimental data.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param m Order of polynom to fit for estimating fixed points of dynamics.
         * @param r Number of quantiles to use for averaging.
         * @return result Largest fixed point of deterministic dynamics.
         */
        public static array.Array MaxLangevinFixedPoint(array.Array array, int m, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.max_langevin_fixed_point(ref reference, ref m, ref r, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the maximum value for each time series within array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The maximum value of each time series within array.
         */
        public static array.Array Maximum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.maximum(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the mean value for each time series within array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The mean value of each time series within array.
         */
        public static array.Array Mean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the mean over the absolute differences between subsequent time series values in array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The maximum value of each time series within array.
         */
        public static array.Array MeanAbsoluteChange(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean_absolute_change(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the mean over the differences between subsequent time series values in array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The mean over the differences between subsequent time series values.
         */
        public static array.Array MeanChange(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean_change(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates mean value of a central approximation of the second derivative for each time series in array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The mean value of a central approximation of the second derivative for each time series.
         */
        public static array.Array MeanSecondDerivativeCentral(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.mean_second_derivative_central(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the median value for each time series within array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The median value of each time series within array.
         */
        public static array.Array Median(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.median(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the minimum value for each time series within array.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The minimum value of each time series within array.
         */
        public static array.Array Minimum(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.minimum(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the number of m-crossings. A m-crossing is defined as two sequential values where the first
         * value is lower than m and the next is greater, or viceversa. If you set m to zero, you will get the number of
         * zero crossings.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param m The m value.
         * @return result The number of m-crossings of each time series within array.
         */
        public static array.Array NumberCrossingM(array.Array array, int m)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.number_crossing_m(ref reference, ref m, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief This feature calculator searches for different peaks. To do so, the time series is smoothed by a ricker
         * wavelet and for widths ranging from 1 to max_w. This feature calculator returns the number of peaks that occur at
         * enough width scales and with sufficiently high Signal-to-Noise-Ratio (SNR).
         *
         * @param array Expects an input array whose dimension zero is the length of the time series (all the same)
         * and dimension one indicates the number of time series.
         * @param max_w The maximum width to consider.
         * @return result The number of peaks for each time series.
         */
        public static array.Array NumberCwtPeaks(array.Array array, int max_w)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.number_cwt_peaks(ref reference, ref max_w, out IntPtr result);
            return (new array.Array(result));
        }

        /**
        * @brief Calculates the number of peaks of at least support \f$n\f$ in the time series \f$array\f$. A peak of support
        * \f$n\f$ is defined as a subsequence of \f$array\f$ where a value occurs, which is bigger than its \f$n\f$ neighbours
        * to the left and to the right.
        *
        * @param array Expects an input array whose dimension zero is the length of the
        * time series (all the same) and dimension one indicates the number of time
        * series.
        * @param n The support of the peak.
        * @return result The number of peaks of at least support \f$n\f$.
        */
        public static array.Array NumberPeaks(array.Array array, int n)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.number_peaks(ref reference, ref n, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the value of the partial autocorrelation function at the given lag. The lag \f$k\f$ partial
         * autocorrelation of a time series \f$\lbrace x_t, t = 1 \ldots T \rbrace\f$ equals the partial correlation of
         * \f$x_t\f$ and \f$x_{t-k}\f$, adjusted for the intermediate variables \f$\lbrace x_{t-1}, \ldots, x_{t-k+1}
         * \rbrace\f$ ([1]). Following [2], it can be defined as:
         *
         * \f[
         *      \alpha_k = \frac{ Cov(x_t, x_{t-k} | x_{t-1}, \ldots, x_{t-k+1})}
         *      {\sqrt{ Var(x_t | x_{t-1}, \ldots, x_{t-k+1}) Var(x_{t-k} | x_{t-1}, \ldots, x_{t-k+1} )}}
         * \f]
         * with (a) \f$x_t = f(x_{t-1}, \ldots, x_{t-k+1})\f$ and (b) \f$ x_{t-k} = f(x_{t-1}, \ldots, x_{t-k+1})\f$
         * being AR(k-1) models that can be fitted by OLS. Be aware that in (a), the regression is done on past values to
         * predict \f$ x_t \f$ whereas in (b), future values are used to calculate the past value \f$x_{t-k}\f$.
         * It is said in [1] that "for an AR(p), the partial autocorrelations \f$ \alpha_k \f$ will be nonzero for \f$ k<=p \f$
         * and zero for \f$ k>p \f$."
         * With this property, it is used to determine the lag of an AR-Process.
         *
         * [1] Box, G. E., Jenkins, G. M., Reinsel, G. C., & Ljung, G. M. (2015).
         * Time series analysis: forecasting and control. John Wiley & Sons.
         * [2] https://onlinecourses.science.psu.edu/stat510/node/62
         *
         * @param array Expects an input array whose dimension zero is the length of the time series (all the same) and
         * dimension one indicates the number of time series.
         * @param lags Indicates the lags to be calculated.
         * @return result Returns partial autocorrelation for each time series for the given lag.
         */
        public static array.Array PartialAutocorrelation(array.Array array, array.Array lags)
        {
            IntPtr reference = array.Reference;
            IntPtr lagsReference = lags.Reference;
            interop.DLLFeatures.partial_autocorrelation(ref reference, ref lagsReference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the percentage of unique values, that are present in the time series more than once.
         * \f[
         *      len(different values occurring more than once) / len(different values)
         * \f]
         * This means the percentage is normalized to the number of unique values, in contrast to the
         * percentageOfReoccurringValuesToAllValues.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param is_sorted Indicates if the input time series is sorted or not. Defaults to false.
         * @return result Returns the percentage of unique values, that are present in the time series more than once.
         */
        public static array.Array PercentageOfReoccurringDatapointsToAllDatapoints(array.Array array, bool is_sorted)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.percentage_of_reoccurring_datapoints_to_all_datapoints(ref reference, ref is_sorted, out IntPtr result);
            return (new array.Array(result));
        }

        /**
        * @brief Calculates the percentage of unique values, that are present in the time series more than once.
        * \f[
        *      \frac{\textit{number of data points occurring more than once}}{\textit{number of all data points})}
        * \f]
        * This means the percentage is normalized to the number of unique values, in contrast to the
        * percentageOfReoccurringDatapointsToAllDatapoints.
        *
        * @param array Expects an input array whose dimension zero is the length of the time series (all the same)
        * and dimension one indicates the number of time series.
        * @param is_sorted Indicates if the input time series is sorted or not. Defaults to false.
        * @return result Returns the percentage of unique values, that are present in the time series more than once.
        */
        public static array.Array PercentageOfReoccurringValuesToAllValues(array.Array array, bool is_sorted)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.percentage_of_reoccurring_values_to_all_values(ref reference, ref is_sorted, out IntPtr result);
            return (new array.Array(result));
        }
        /*
       public static array.Array quantile(array.Array array, IntPtr q, float precision){}
        
       public static array.Array range_count(array.Array array, float min, float max){}
        
       public static array.Array ratio_beyond_r_sigma(array.Array array, float r){}
        
       public static array.Array ratio_value_number_to_time_series_length(array.Array array){}
        
       public static array.Array sample_entropy(array.Array array){}
        
       public static array.Array skewness(array.Array array){}
        
       public static array.Array spkt_welch_density(array.Array array, int coeff){}
        
       public static array.Array standard_deviation(array.Array array){}
        
       public static array.Array sum_of_reoccurring_datapoints(array.Array array, bool is_sorted){}
        
       public static array.Array sum_of_reoccurring_values(array.Array array, bool is_sorted){}
        
       public static array.Array sum_values(array.Array array){}
        
       public static array.Array symmetry_looking(array.Array array, float r){}
        
       public static array.Array time_reversal_asymmetry_statistic(array.Array array, int lag){}
        
       public static array.Array value_count(array.Array array, float v){}
        
       public static array.Array variance(array.Array array){}
        
       public static array.Array variance_larger_than_standard_deviation(array.Array array){}
       */
    }
}
