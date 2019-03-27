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
         * @param result An array with the same dimensions as array, whose values (time series in dimension 0)
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
         * @param result An array with the same dimensions as array, whose values (time series in dimension 0)
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
         * @param result An array whose values contains the aggregated correaltion for each time series.
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
         * @param slope Slope of the regression line.
         * @param intercept Intercept of the regression line.
         * @param rvalue Correlation coefficient.
         * @param pvalue Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero,
         * using Wald Test with t-distribution of the test statistic.
         * @param stderrest Standard error of the estimated gradient.
         */
        public static Tuple<array.Array, array.Array, array.Array, array.Array, array.Array> AggregatedLinearTrend(array.Array array, long chunkSize, int aggregationFunction)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.aggregated_linear_trend(ref reference,
                                                        ref chunkSize,
                                                        ref aggregationFunction,
                                                        out IntPtr slope, out IntPtr intercept, out IntPtr rvalue, out IntPtr pvalue, out IntPtr stderrest);
            return (new Tuple<array.Array, array.Array, array.Array, array.Array, array.Array>(new array.Array(slope),
                                                                                               new array.Array(intercept),
                                                                                               new array.Array(rvalue),
                                                                                               new array.Array(pvalue),
                                                                                               new array.Array(stderrest)));
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
         * @param result The vectorized approximate entropy for all the input time series in array.
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
         * @param result The cross-covariance value for the given time series.
         */
        public static array.Array CrossCovariance(array.Array xss, array.Array yss, bool unbiased)
        {
            IntPtr referenceXss = xss.Reference;
            IntPtr referenceYss = yss.Reference;
            interop.DLLFeatures.cross_covariance(ref referenceXss, ref referenceYss, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }
        /*
       public static array.Array auto_covariance(IntPtr array, bool unbiased, IntPtr result){}
        
       public static array.Array cross_correlation(IntPtr xss, IntPtr yss, bool unbiased, IntPtr result){}
        
       public static array.Array auto_correlation(IntPtr array, long max_lag, bool unbiased, IntPtr result){}
        
       public static array.Array binned_entropy(IntPtr array, int max_bins, IntPtr result){}
        
       public static array.Array c3(IntPtr array, long lag, IntPtr result){}
        
       public static array.Array cid_ce(IntPtr array, bool zNormalize, IntPtr result){}
        
       public static array.Array count_above_mean(IntPtr array, IntPtr result){}
        
       public static array.Array count_below_mean(IntPtr array, IntPtr result){}
        
       public static array.Array cwt_coefficients(IntPtr array, IntPtr width, int coeff, int w, IntPtr result){}
        
       public static array.Array energy_ratio_by_chunks(IntPtr array, long num_segments, long segment_focus, IntPtr result){}
        
       public static array.Array fft_aggregated(IntPtr array, IntPtr result){}
        
       public static array.Array fft_coefficient(IntPtr array, long coefficient, IntPtr real, IntPtr imag,
                              IntPtr absolute, IntPtr angle){}
        
       public static array.Array first_location_of_maximum(IntPtr array, IntPtr result){}
        
       public static array.Array first_location_of_minimum(IntPtr array, IntPtr result){}
        
       public static array.Array friedrich_coefficients(IntPtr array, int m, float r, IntPtr result){}
        
       public static array.Array has_duplicates(IntPtr array, IntPtr result){}
        
       public static array.Array has_duplicate_max(IntPtr array, IntPtr result){}
        
       public static array.Array has_duplicate_min(IntPtr array, IntPtr result){}
        
       public static array.Array index_mass_quantile(IntPtr array, float q, IntPtr result){}
        
       public static array.Array kurtosis(IntPtr array, IntPtr result){}
        
       public static array.Array large_standard_deviation(IntPtr array, float r, IntPtr result){}
        
       public static array.Array last_location_of_maximum(IntPtr array, IntPtr result){}
        
       public static array.Array last_location_of_minimum(IntPtr array, IntPtr result){}
        
       public static array.Array length(IntPtr array, IntPtr result){}
        
       public static array.Array linear_trend(IntPtr array, IntPtr pvalue, IntPtr rvalue, IntPtr intercept,
                           IntPtr slope, IntPtr stdrr){}
        
       public static array.Array local_maximals(IntPtr array, IntPtr result){}
        
       public static array.Array longest_strike_above_mean(IntPtr array, IntPtr result){}
        
       public static array.Array longest_strike_below_mean(IntPtr array, IntPtr result){}
        
       public static array.Array max_langevin_fixed_point(IntPtr array, int m, float r, IntPtr result){}
        
       public static array.Array maximum(IntPtr array, IntPtr result){}
        
       public static array.Array mean(IntPtr array, IntPtr result){}
        
       public static array.Array mean_absolute_change(IntPtr array, IntPtr result){}
        
       public static array.Array mean_change(IntPtr array, IntPtr result){}
        
       public static array.Array mean_second_derivative_central(IntPtr array, IntPtr result){}
        
       public static array.Array median(IntPtr array, IntPtr result){}
        
       public static array.Array minimum(IntPtr array, IntPtr result){}
        
       public static array.Array number_crossing_m(IntPtr array, int m, IntPtr result){}
        
       public static array.Array number_cwt_peaks(IntPtr array, int max_w, IntPtr result){}
        
       public static array.Array number_peaks(IntPtr array, int n, IntPtr result){}
        
       public static array.Array partial_autocorrelation(IntPtr array, IntPtr lags, IntPtr result){}
        
       public static array.Array percentage_of_reoccurring_datapoints_to_all_datapoints(IntPtr array, bool is_sorted,
                                                                     IntPtr result){}
        
       public static array.Array percentage_of_reoccurring_values_to_all_values(IntPtr array, bool is_sorted, IntPtr result){}
        
       public static array.Array quantile(IntPtr array, IntPtr q, float precision, IntPtr result){}
        
       public static array.Array range_count(IntPtr array, float min, float max, IntPtr result){}
        
       public static array.Array ratio_beyond_r_sigma(IntPtr array, float r, IntPtr result){}
        
       public static array.Array ratio_value_number_to_time_series_length(IntPtr array, IntPtr result){}
        
       public static array.Array sample_entropy(IntPtr array, IntPtr result){}
        
       public static array.Array skewness(IntPtr array, IntPtr result){}
        
       public static array.Array spkt_welch_density(IntPtr array, int coeff, IntPtr result){}
        
       public static array.Array standard_deviation(IntPtr array, IntPtr result){}
        
       public static array.Array sum_of_reoccurring_datapoints(IntPtr array, bool is_sorted, IntPtr result){}
        
       public static array.Array sum_of_reoccurring_values(IntPtr array, bool is_sorted, IntPtr result){}
        
       public static array.Array sum_values(IntPtr array, IntPtr result){}
        
       public static array.Array symmetry_looking(IntPtr array, float r, IntPtr result){}
        
       public static array.Array time_reversal_asymmetry_statistic(IntPtr array, int lag, IntPtr result){}
        
       public static array.Array value_count(IntPtr array, float v, IntPtr result){}
        
       public static array.Array variance(IntPtr array, IntPtr result){}
        
       public static array.Array variance_larger_than_standard_deviation(IntPtr array, IntPtr result){}
       */
    }
}
