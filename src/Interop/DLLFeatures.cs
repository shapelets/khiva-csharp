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
    /// Khiva Features class containing a number of features that can be extracted from time series. All the methods
    /// operate with instances of the ARRAY class as input and output.
    /// </summary>
    public static class DLLFeatures
    {
        /// <summary> Calculates the sum over the square values of the time series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series (all the same) and dimension one indicates the number of
        /// time series.</param>
        /// <param name="result">An array with the same dimensions as array, whose values (time series in dimension 0)
        /// contains the sum of the squares values in the time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void abs_energy([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the sum over the absolute value of consecutive changes in the time series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series (all the same) and dimension one indicates the number of
        /// time series.</param>
        /// <param name="result">An array with the same dimensions as array, whose values (time series in dimension 0)
        /// contains absolute value of consecutive changes in the time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void absolute_sum_of_changes([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the value of an aggregation function f_agg (e.g. var or mean) of the autocorrelation
        /// (Compare to http://en.wikipedia.org/wiki/Autocorrelation#Estimation), taken over different all possible
        /// lags (1 to length of x).
        /// </summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series (all the same) and dimension one indicates the number of time series.</param>
        /// <param name="aggregationFunction">Function to be used in the aggregation. It receives an integer which indicates the
        /// function to be applied:
        ///          {
        ///              0 : mean,
        ///              1 : median
        ///              2 : min,
        ///              3 : max,
        ///              4 : stdev,
        ///              5 : var,
        ///              default : mean
        ///          }</param>
        /// <param name="result">An array whose values contains the aggregated correlation for each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void aggregated_autocorrelation([In] ref IntPtr array, [In] ref int aggregationFunction,
            [Out] out IntPtr result);

        /// <summary> Calculates a linear least-squares regression for values of the time series that were aggregated
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
        ///          }</param>
        /// <param name="slope">Slope of the regression line.</param>
        /// <param name="intercept">Intercept of the regression line.</param>
        /// <param name="rvalue">Correlation coefficient.</param>
        /// <param name="pvalue">Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero,
        /// using Wald Test with t-distribution of the test statistic.</param>
        /// <param name="stderrest">Standard error of the estimated gradient.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void aggregated_linear_trend([In] ref IntPtr array, [In] ref long chunkSize,
            [In] ref int aggregationFunction,
            [Out] out IntPtr slope, [Out] out IntPtr intercept, [Out] out IntPtr rvalue,
            [Out] out IntPtr pvalue, [Out] out IntPtr stderrest);

        /// <summary> Calculates a vectorized Approximate entropy algorithm.
        /// https://en.wikipedia.org/wiki/Approximate_entropy
        /// For short time-series this method is highly dependent on the parameters, but should be stable for N > 2000,
        /// see: Yentes et al. (2012) - The Appropriate Use of Approximate Entropy and Sample Entropy with Short Data Sets
        /// Other shortcomings and alternatives discussed in:
        /// Richman &amp; Moorman (2000) - Physiological time-series analysis using approximate entropy and sample entropy.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series (all the same) and dimension one indicates the number of time series.</param>
        /// <param name="m">Length of compared run of data.</param>
        /// <param name="r">Filtering level, must be positive.</param>
        /// <param name="result">The vectorized approximate entropy for all the input time series in array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void approximate_entropy([In] ref IntPtr array, [In] ref int m, [In] ref float r,
            [Out] out IntPtr result);


        /// <summary> Calculates the cross-covariance of the given time series.
        ///</summary>
        /// <param name="xss">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="yss">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="unbiased">Determines whether it divides by n - lag (if true) or
        /// n (if false).</param>
        /// <param name="result">The cross-covariance value for the given time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cross_covariance([In] ref IntPtr xss, [In] ref IntPtr yss, [In] ref bool unbiased,
            [Out] out IntPtr result);


        /// <summary> Calculates the auto-covariance the given time series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="unbiased">Determines whether it divides by n - lag (if true) or
        /// n (if false).</param>
        /// <param name="result">The auto-covariance value for the given time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void auto_covariance([In] ref IntPtr array, [In] ref bool unbiased,
            [Out] out IntPtr result);


        /// <summary> Calculates the cross-correlation of the given time series.
        ///</summary>
        /// <param name="xss">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="yss">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="unbiased">Determines whether it divides by n - lag (if true) or
        /// n (if false).</param>
        /// <param name="result">The cross-correlation value for the given time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cross_correlation([In] ref IntPtr xss, [In] ref IntPtr yss, [In] ref bool unbiased,
            [Out] out IntPtr result);


        /// <summary> Calculates the autocorrelation of the specified lag for the given time.
        /// series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="maxLag">The maximum lag to compute.</param>
        /// <param name="unbiased">Determines whether it divides by n - lag (if true) or n ( if false)</param>
        /// <param name="result">The autocorrelation value for the given time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void auto_correlation([In] ref IntPtr array, [In] ref long maxLag, [In] ref bool unbiased,
            [Out] out IntPtr result);


        /// <summary> Calculates the binned entropy for the given time series and number of bins.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="maxBins">The number of bins.</param>
        /// <param name="result">The binned entropy value for the given time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void binned_entropy([In] ref IntPtr array, [In] ref int maxBins, [Out] out IntPtr result);


        /// <summary> Calculates the Schreiber, T. and Schmitz, A. (1997) measure of non-linearity
        /// for the given time series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="lag">The lag</param>
        /// <param name="result">The non-linearity value for the given time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void c3([In] ref IntPtr array, [In] ref long lag, [Out] out IntPtr result);


        /// <summary> Calculates an estimate for the time series complexity defined by
        /// Batista, Gustavo EAPA, et al (2014). (A more complex time series has more peaks,
        /// valleys, etc.).
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="zNormalize">Controls whether the time series should be z-normalized or not.</param>
        /// <param name="result">The complexity value for the given time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cid_ce([In] ref IntPtr array, [In] ref bool zNormalize, [Out] out IntPtr result);


        /// <summary> Calculates the number of values in the time series that are higher than
        /// the mean.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The number of values in the time series that are higher
        /// than the mean.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void count_above_mean([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates the number of values in the time series that are lower than
        /// the mean.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The number of values in the time series that are lower
        /// than the mean.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void count_below_mean([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates a Continuous wavelet transform for the Ricker wavelet, also known as
        /// the "Mexican hat wavelet" which is defined by:
        ///
        ///  .. math::
        ///      \frac{2}{\sqrt{3a} \pi^{
        ///  \frac{1} { 4 }}} (1 - \frac{x^2}{a^2}) exp(-\frac{x^2}{2a^2})
        ///
        ///  where :math:`a` is the width parameter of the wavelet function.
        ///
        /// This feature calculator takes three different parameter: widths, coeff and w. The feature calculator takes all
        /// the different widths arrays and then calculates the cwt one time for each different width array. Then the values
        /// for the different coefficient for coeff and width w are returned. (For each dic in param one feature is
        /// returned).
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="width">KhivaArray that contains all different widths.</param>
        /// <param name="coeff">Coefficient of interest.</param>
        /// <param name="w">Width of interest.</param>
        /// <param name="result">Result of calculated coefficients.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void cwt_coefficients([In] ref IntPtr array, [In] ref IntPtr width, [In] ref int coeff,
            [In] ref int w, [Out] out IntPtr result);


        /// <summary> Calculates the sum of squares of chunk i out of N chunks expressed as a ratio  with the sum of squares over the whole series.
        /// segment_focus should be lower than the number of segments.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="numSegments">The number of segments to divide the series into.</param>
        /// <param name="segmentFocus">The segment number (starting at zero) to return a feature on.</param>
        /// <param name="result">The energy ratio by chunk of the time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void energy_ratio_by_chunks([In] ref IntPtr array, [In] ref long numSegments,
            [In] ref long segmentFocus, [Out] out IntPtr result);


        /// <summary> Calculates the spectral centroid(mean), variance, skew, and kurtosis of the absolute fourier transform
        /// spectrum.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The spectral centroid (mean), variance, skew, and kurtosis of the absolute fourier transform
        /// spectrum.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void fft_aggregated([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates the fourier coefficients of the one-dimensional discrete
        /// Fourier Transform for real input by fast fourier transformation algorithm.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="coefficient">The coefficient to extract from the FFT.</param>
        /// <param name="real">The real part of the coefficient.</param>
        /// <param name="imag">The imaginary part of the coefficient.</param>
        /// <param name="absolute">The absolute value of the coefficient.</param>
        /// <param name="angle">The angle of the coefficient.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void fft_coefficient([In] ref IntPtr array, [In] ref long coefficient,
            [Out] out IntPtr real, [Out] out IntPtr imag,
            [Out] out IntPtr absolute, [Out] out IntPtr angle);


        /// <summary> Calculates the first relative location of the maximal value for each time series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The first relative location of the maximum value to the length of the time series,
        ///  for each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void first_location_of_maximum([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates the first location of the minimal value of each time series. The position
        /// is calculated relatively to the length of the series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The first relative location of the minimal value of each series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void first_location_of_minimum([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Coefficients of polynomial \f$h(x)\f$, which has been fitted to the deterministic
        /// dynamics of Langevin model:
        /// \f[
        ///    \dot(x)(t) = h(x(t)) + R \mathcal(N)(0,1)
        /// \f]
        /// as described by [1]. For short time series this method is highly dependent on the parameters.
        ///
        /// [1] Friedrich et al. (2000): Physics Letters A 271, p. 217-222
        /// Extracting model equations from experimental data.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="m">Order of polynom to fit for estimating fixed points of dynamics.</param>
        /// <param name="r">Number of quantiles to use for averaging.</param>
        /// <param name="result">The coefficients for each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void friedrich_coefficients([In] ref IntPtr array, [In] ref int m, [In] ref float r,
            [Out] out IntPtr result);


        /// <summary> Calculates if the input time series contain duplicated elements.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">KhivaArray containing True if the time series contains duplicated elements
        /// and false otherwise.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void has_duplicates([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates if the maximum within input time series is duplicated.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">KhivaArray containing True if the maximum value of the time series is duplicated
        /// and false otherwise.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void has_duplicate_max([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates if the minimum of the input time series is duplicated.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">KhivaArray containing True if the minimum of the time series is duplicated
        /// and false otherwise.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void has_duplicate_min([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates the index of the max quantile.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="q">The quantile.</param>
        /// <param name="result">The index of the max quantile q.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void index_mass_quantile([In] ref IntPtr array, [In] ref float q, [Out] out IntPtr result);


        /// <summary> Returns the kurtosis of array (calculated with the adjusted Fisher-Pearson
        /// standardized moment coefficient G2).
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The kurtosis of each array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void kurtosis([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Checks if the time series within array have a large standard deviation.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="r">The threshold.</param>
        /// <param name="result"> KhivaArray containing True for those time series in array that have a large standard deviation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void large_standard_deviation([In] ref IntPtr array, [In] ref float r,
            [Out] out IntPtr result);


        /// <summary> Calculates the last location of the maximum value of each time series. The position
        /// is calculated relatively to the length of the series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The last relative location of the maximum value of each series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void last_location_of_maximum([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates the last location of the minimum value of each time series. The position
        /// is calculated relatively to the length of the series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The last relative location of the minimum value of each series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void last_location_of_minimum([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Returns the length of the input time series.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The length of the time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void length([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculate a linear least-squares regression for the values of the time series versus the sequence from 0 to
        /// length of the time series minus one.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="pvalue">The pvalues for all time series.</param>
        /// <param name="rvalue">The rvalues for all time series.</param>
        /// <param name="intercept">The intercept values for all time series.</param>
        /// <param name="slope">The slope for all time series.</param>
        /// <param name="stdrr">The stderr values for all time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linear_trend([In] ref IntPtr array, [Out] out IntPtr pvalue, [Out] out IntPtr rvalue,
            [Out] out IntPtr intercept,
            [Out] out IntPtr slope, [Out] out IntPtr stdrr);

        /// <summary> Calculates all Local Maximals fot the time series in array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="result">The calculated local maximals for each time series in array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void local_maximals([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the length of the longest consecutive subsequence in array that is bigger than the mean of array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The length of the longest consecutive subsequence in the input time series that is bigger than the
        /// mean.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void longest_strike_above_mean([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the length of the longest consecutive subsequence in array that is below the mean of array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The length of the longest consecutive subsequence in the input time series that is below the mean.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void longest_strike_below_mean([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Largest fixed point of dynamics \f$\max_x {h(x)=0}\f$ estimated from polynomial
        /// \f$h(x)\f$, which has been fitted to the deterministic dynamics of Langevin model
        /// \f[
        ///    \dot(x)(t) = h(x(t)) + R \mathcal(N)(0,1)
        /// \f]
        /// as described by
        /// Friedrich et al. (2000): Physics Letters A 271, p. 217-222 ///Extracting model equations from experimental data.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="m">Order of polynom to fit for estimating fixed points of dynamics.</param>
        /// <param name="r">Number of quantiles to use for averaging.</param>
        /// <param name="result">Largest fixed point of deterministic dynamics.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void max_langevin_fixed_point([In] ref IntPtr array, [In] ref int m, [In] ref float r,
            [Out] out IntPtr result);

        /// <summary> Calculates the maximum value for each time series within array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The maximum value of each time series within array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void maximum([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the mean value for each time series within array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The mean value of each time series within array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mean([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the mean over the absolute differences between subsequent time series values in array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The maximum value of each time series within array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mean_absolute_change([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the mean over the differences between subsequent time series values in array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The mean over the differences between subsequent time series values.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mean_change([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates mean value of a central approximation of the second derivative for each time series in array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The mean value of a central approximation of the second derivative for each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mean_second_derivative_central([In] ref IntPtr array, [Out] out IntPtr result);


        /// <summary> Calculates the median value for each time series within array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The median value of each time series within array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void median([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the minimum value for each time series within array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The minimum value of each time series within array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void minimum([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the number of m-crossings. A m-crossing is defined as two sequential values where the first
        /// value is lower than m and the next is greater, or viceversa. If you set m to zero, you will get the number of
        /// zero crossings.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="m">The m value.</param>
        /// <param name="result">The number of m-crossings of each time series within array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void number_crossing_m([In] ref IntPtr array, [In] ref int m, [Out] out IntPtr result);

        /// <summary> This feature calculator searches for different peaks. To do so, the time series is smoothed by a ricker
        /// wavelet and for widths ranging from 1 to max_w. This feature calculator returns the number of peaks that occur at
        /// enough width scales and with sufficiently high Signal-to-Noise-Ratio (SNR).
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="maxW">The maximum width to consider.</param>
        /// <param name="result">The number of peaks for each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void number_cwt_peaks([In] ref IntPtr array, [In] ref int maxW, [Out] out IntPtr result);

        /// <summary> Calculates the number of peaks of at least support \f$n\f$ in the time series \f$array\f$. A peak of support
        /// \f$n\f$ is defined as a subsequence of \f$array\f$ where a value occurs, which is bigger than its \f$n\f$ neighbours
        /// to the left and to the right.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="n">The support of the peak.</param>
        /// <param name="result">The number of peaks of at least support \f$n\f$.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void number_peaks([In] ref IntPtr array, [In] ref int n, [Out] out IntPtr result);

        /// <summary> Calculates the value of the partial autocorrelation function at the given lag. The lag \f$k\f$ partial
        /// autocorrelation of a time series \f$\lbrace x_t, t = 1 \ldots T \rbrace\f$ equals the partial correlation of
        /// \f$x_t\f$ and \f$x_{t-k}\f$, adjusted for the intermediate variables \f$\lbrace x_{t-1}, \ldots, x_{t-k+1}
        /// \rbrace\f$ ([1]). Following [2], it can be defined as:
        ///
        /// \f[
        ///      \alpha_k = \frac{ Cov(x_t, x_{t-k} | x_{t-1}, \ldots, x_{t-k+1})}
        ///      {\sqrt{ Var(x_t | x_{t-1}, \ldots, x_{t-k+1}) Var(x_{t-k} | x_{t-1}, \ldots, x_{t-k+1} )}}
        /// \f]
        /// with (a) \f$x_t = f(x_{t-1}, \ldots, x_{t-k+1})\f$ and (b) \f$ x_{t-k} = f(x_{t-1}, \ldots, x_{t-k+1})\f$
        /// being AR(k-1) models that can be fitted by OLS. Be aware that in (a), the regression is done on past values to
        /// predict \f$ x_t \f$ whereas in (b), future values are used to calculate the past value \f$x_{t-k}\f$.
        /// It is said in [1] that "for an AR(p), the partial autocorrelations \f$ \alpha_k \f$ will be nonzero for \f$ k \le p \f$
        /// and zero for \f$ k \gt p \f$."
        /// With this property, it is used to determine the lag of an AR-Process.
        ///
        /// [1] Box, G. E., Jenkins, G. M., Reinsel, G. C., &amp; Ljung, G. M. (2015).
        /// Time series analysis: forecasting and control. John Wiley &amp; Sons.
        /// [2] https://onlinecourses.science.psu.edu/stat510/node/62
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="lags">Indicates the lags to be calculated.</param>
        /// <param name="result">Returns partial autocorrelation for each time series for the given lag.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void partial_autocorrelation([In] ref IntPtr array, [In] ref IntPtr lags,
            [Out] out IntPtr result);

        /// <summary> Calculates the percentage of unique values, that are present in the time series more than once.
        /// \f[
        ///      len(different values occurring more than once) / len(different values)
        /// \f]
        /// This means the percentage is normalized to the number of unique values, in contrast to the
        /// percentageOfReoccurringValuesToAllValues.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="isSorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <param name="result">Returns the percentage of unique values, that are present in the time series more than once.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void percentage_of_reoccurring_datapoints_to_all_datapoints([In] ref IntPtr array,
            [In] ref bool isSorted,
            [Out] out IntPtr result);

        /// <summary> Calculates the percentage of unique values, that are present in the time series more than once.
        /// \f[
        ///      \frac{\textit{number of data points occurring more than once}}{\textit{number of all data points})}
        /// \f]
        /// This means the percentage is normalized to the number of unique values, in contrast to the
        /// percentageOfReoccurringDatapointsToAllDatapoints.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="isSorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <param name="result">Returns the percentage of unique values, that are present in the time series more than once.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void percentage_of_reoccurring_values_to_all_values([In] ref IntPtr array,
            [In] ref bool isSorted, [Out] out IntPtr result);

        /// <summary> Returns values at the given quantile.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="q">Percentile(s) at which to extract score(s). One or many.</param>
        /// <param name="precision">Number of decimals expected.</param>
        /// <param name="result">Values at the given quantile.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void quantile([In] ref IntPtr array, [In] ref IntPtr q, [In] ref float precision,
            [Out] out IntPtr result);

        /// <summary> Counts observed values within the interval [min, max).
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time
        /// series (all the same) and dimension one indicates the number of
        /// time series.</param>
        /// <param name="min">Value that sets the lower limit.</param>
        /// <param name="max">Value that sets the upper limit.</param>
        /// <param name="result">Values at the given range.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void range_count([In] ref IntPtr array, [In] ref float min, [In] ref float max,
            [Out] out IntPtr result);

        /// <summary> Calculates the ratio of values that are more than \f$r///std(x)\f$ (so \f$r\f$ sigma) away from the mean of
        /// \f$x\f$.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="r">Number of times that the values should be away from.</param>
        /// <param name="result">The ratio of values that are more than \f$r///std(x)\f$ (so \f$r\f$ sigma) away from the mean of
        /// \f$x\f$.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void
            ratio_beyond_r_sigma([In] ref IntPtr array, [In] ref float r, [Out] out IntPtr result);

        /// <summary> Calculates a factor which is 1 if all values in the time series occur only once, and below one if this is
        /// not the case. In principle, it just returns:
        ///
        /// \f[
        ///      \frac{\textit{number\_unique\_values}}{\textit{number\_values}}
        /// \f]
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="result">The ratio of unique values with respect to the total number of values.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ratio_value_number_to_time_series_length([In] ref IntPtr array,
            [Out] out IntPtr result);

        /// <summary> Calculates a vectorized sample entropy algorithm.
        /// https://en.wikipedia.org/wiki/Sample_entropy
        /// https://www.ncbi.nlm.nih.gov/pubmed/10843903?dopt=Abstract
        /// For short time-series this method is highly dependent on the parameters, but should be stable for N > 2000,
        /// see: Yentes et al. (2012) - The Appropriate Use of Approximate Entropy and Sample Entropy with Short Data Sets
        /// Other shortcomings and alternatives discussed in:
        /// Richman &amp; Moorman (2000) - Physiological time-series analysis using approximate entropy and sample entropy.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">An array with the same dimensions as array, whose values (time series in dimension 0)
        /// contains the vectorized sample entropy for all the input time series in array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void sample_entropy([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the sample skewness of array (calculated with the adjusted Fisher-Pearson standardized
        /// moment coefficient G1).
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">KhivaArray containing the skewness of each time series in array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void skewness([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Estimates the cross power spectral density of the time series array at different frequencies. To do so, the
        /// time series is first shifted from the time domain to the frequency domain.
        ///
        /// Welch's method computes an estimate of the power spectral density by dividing the data into overlapping
        /// segments, computing a modified periodogram for each segment and averaging the periodograms.
        /// [1] P. Welch, "The use of the fast Fourier transform for the estimation of power spectra: A method based on time
        ///  averaging over short, modified periodograms", IEEE Trans. Audio Electroacoust. vol. 15, pp. 70-73, 1967.
        /// [2] M.S. Bartlett, "Periodogram Analysis and Continuous Spectra", Biometrika, vol. 37, pp. 1-16, 1950.
        /// [3] Rabiner, Lawrence R., and B. Gold. "Theory and Application of Digital Signal Processing" Prentice-Hall, pp.
        /// 414-419, 1975.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="coeff">The coefficient to be returned.</param>
        /// <param name="result">KhivaArray containing the power spectrum of the different frequencies for each time series in
        /// array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void
            spkt_welch_density([In] ref IntPtr array, [In] ref int coeff, [Out] out IntPtr result);

        /// <summary> Calculates the standard deviation of each time series within array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="result">The standard deviation of each time series within array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void standard_deviation([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates the sum of all data points, that are present in the time series more than once.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="isSorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <param name="result">Returns the sum of all data points, that are present in the time series more than once.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void sum_of_reoccurring_datapoints([In] ref IntPtr array, [In] ref bool isSorted,
            [Out] out IntPtr result);

        /// <summary> Calculates the sum of all values, that are present in the time series more than once.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same)
        /// and dimension one indicates the number of time series.</param>
        /// <param name="isSorted">Indicates if the input time series is sorted or not. Defaults to false.</param>
        /// <param name="result">Returns the sum of all values, that are present in the time series more than once.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void sum_of_reoccurring_values([In] ref IntPtr array, [In] ref bool isSorted,
            [Out] out IntPtr result);

        /// <summary> Calculates the sum over the time series array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="result">An array containing the sum of values in each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void sum_values([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates if the distribution of array ///looks symmetric///. This is the case if
        /// \f[
        ///      | mean(array)-median(array)| \lt r * (max(array)-min(array))
        /// \f]
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="r">The percentage of the range to compare with.</param>
        /// <param name="result">An array denoting if the input time series look symmetric.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void symmetry_looking([In] ref IntPtr array, [In] ref float r, [Out] out IntPtr result);

        /// <summary> This function calculates the value of:
        /// \f[
        ///      \frac{1}{n-2lag} \sum_{i=0}^{n-2lag} x_{i + 2 \cdot lag}^2 \cdot x_{i + lag} - x_{i + lag} \cdot  x_{i}^2
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
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="lag">The lag to be computed.</param>
        /// <param name="result">An array containing the time reversal asymmetry statistic value in each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void time_reversal_asymmetry_statistic([In] ref IntPtr array, [In] ref int lag,
            [Out] out IntPtr result);

        /// <summary> Counts occurrences of value in the time series array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the
        /// time series (all the same) and dimension one indicates the number of time
        /// series.</param>
        /// <param name="v">The value to be counted.</param>
        /// <param name="result">An array containing the count of the given value in each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void value_count([In] ref IntPtr array, [In] ref float v, [Out] out IntPtr result);

        /// <summary> Computes the variance for the time series array.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="result">An array containing the variance in each time series.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void variance([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Calculates if the variance of array is greater than the standard deviation. In other words, if the variance of
        /// array is larger than 1.
        ///</summary>
        /// <param name="array">Expects an input array whose dimension zero is the length of the time series (all the same) and
        /// dimension one indicates the number of time series.</param>
        /// <param name="result">An array denoting if the variance of array is greater than the standard deviation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void variance_larger_than_standard_deviation([In] ref IntPtr array,
            [Out] out IntPtr result);
    }
}