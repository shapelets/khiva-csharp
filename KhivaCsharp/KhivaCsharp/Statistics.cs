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
    namespace statistics
    {
        /// <summary>
        /// Khiva Statistics class containing statistics methods.
        /// </summary>
        public static class Statistics
        {

            /// <summary>
            /// Returns the covariance matrix of the time series contained in tss.
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <param name="unbiased">Determines whether it divides by n - 1 (if false) or n (if true).</param>
            /// <returns>The covariance matrix of the time series.</returns>
            public static KhivaArray CovarianceStatistics(KhivaArray tss, bool unbiased)
            {
                IntPtr reference = tss.Reference;
			    IntPtr result;
                interop.DLLStatistics.covariance_statistics(ref reference, ref unbiased, out result);
                tss.Reference = reference;
                return (KhivaArray.Create(result));
            }

            /// <summary>
            /// Returns the kurtosis of tss (calculated with the adjusted Fisher-Pearson standardized moment coefficient G2).
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <returns>The kurtosis of tss.</returns>
            public static KhivaArray KurtosisStatistics(KhivaArray tss)
            {
                IntPtr reference = tss.Reference;
			    IntPtr result;
                interop.DLLStatistics.kurtosis_statistics(ref reference, out result);
                tss.Reference = reference;
                return (KhivaArray.Create(result));
            }

            /// <summary>
            /// The Ljung–Box test checks that data whithin the time series are independently distributed (i.e. the
            /// correlations in the population from which the sample is taken are 0, so that any observed correlations in the data
            /// result from randomness of the sampling process). Data are no independently distributed, if they exhibit serial
            /// correlation.
            ///
            /// The test statistic is:
            ///
            /// \f[
            /// Q = n\left(n + 2\right)\sum_{ k = 1}^h\frac{\hat{\rho}^2_k}{n-k}
            /// \f]
            ///
            /// where ''n'' is the sample size, \f$\hat{\rho}k \f$ is the sample autocorrelation at lag ''k'', and ''h'' is the
            /// number of lags being tested.Under \f$ H_0 \f$ the statistic Q follows a \f$\chi^2{ (h)} \f$. For significance level
            /// \f$\alpha\f$, the \f$critical region\f$ for rejection of the hypothesis of randomness is:
            ///
            /// \f[
            /// Q > \chi_{1-\alpha,h}^2
            /// \f]
            ///
            /// where \f$ \chi_{1-\alpha,h}^2 \f$ is the \f$\alpha\f$-quantile of the chi-squared distribution with ''h'' degrees of
            /// freedom.
            ///
            /// [1] G.M.Ljung G. E.P.Box (1978). On a measure of lack of fit in time series models.
            /// Biometrika, Volume 65, Issue 2, 1 August 1978, Pages 297–303.
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <param name="lags">Number of lags being tested.</param>
            /// <returns>The Ljung-Box statistic test.</returns>
            public static KhivaArray LjungBox(KhivaArray tss, long lags)
            {
                IntPtr reference = tss.Reference;
			    IntPtr result;
                interop.DLLStatistics.ljung_box(ref reference, ref lags, out result);
                tss.Reference = reference;
                return (KhivaArray.Create(result));
            }

            /// <summary>
            /// Returns the kth moment of the given time series.
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <param name="k">The specific moment to be calculated.</param>
            /// <returns>The kth moment of the given time series.</returns>
            public static KhivaArray MomentStatistics(KhivaArray tss, int k)
            {
                IntPtr reference = tss.Reference;
			    IntPtr result;
                interop.DLLStatistics.moment_statistics(ref reference, ref k, out result);
                tss.Reference = reference;
                return (KhivaArray.Create(result));
            }

            /// <summary>
            /// Returns values at the given quantile.
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.NOTE: the time series should be sorted.</param>
            /// <param name="q">Percentile(s) at which to extract score(s). One or many.</param>
            /// <param name="precision">Number of decimals expected.</param>
            /// <returns>Values at the given quantile.</returns>
            public static KhivaArray QuantileStatistics(KhivaArray tss, KhivaArray q, float precision = 1e-8F)
            {
                IntPtr reference = tss.Reference;
                IntPtr qReference = q.Reference;
			    IntPtr result;
                interop.DLLStatistics.quantile_statistics(ref reference, ref qReference, ref precision, out result);
                tss.Reference = reference;
                q.Reference = qReference;
                return (KhivaArray.Create(result));
            }

            /// <summary>
            /// Discretizes the time series into equal-sized buckets based on sample quantiles.
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.NOTE: the time series should be sorted.</param>
            /// <param name="quantiles">Number of quantiles to extract. From 0 to 1, step 1/quantiles.</param>
            /// <param name="precision">Number of decimals expected.</param>
            /// <returns>Matrix with the categories, one category per row, the start of the category in the first column and
            /// the end in the second category.</returns>
            public static KhivaArray QuantilesCutStatistics(KhivaArray tss, float quantiles, float precision = 1e-8F)
            {
                IntPtr reference = tss.Reference;
			    IntPtr result;
                interop.DLLStatistics.quantiles_cut_statistics(ref reference, ref quantiles, ref precision, out result);
                tss.Reference = reference;
                return (KhivaArray.Create(result));
            }
            
            /// <summary>
            /// Estimates standard deviation based on a sample. The standard deviation is calculated using the "n-1" method.
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <returns>The sample standard deviation.</returns>
            public static KhivaArray SampleStdevStatistics(KhivaArray tss)
            {
                IntPtr reference = tss.Reference;
			    IntPtr result;
                interop.DLLStatistics.sample_stdev_statistics(ref reference, out result);
                tss.Reference = reference;
                return (KhivaArray.Create(result));
            }

            /// <summary>
            /// Calculates the sample skewness of tss (calculated with the adjusted Fisher-Pearson standardized moment coefficient G1).
            /// </summary>
            /// <param name="tss">Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            /// one indicates the number of time series.</param>
            /// <returns>KhivaArray containing the skewness of each time series in tss.</returns>
            public static KhivaArray SkewnessStatistics(KhivaArray tss)
            {
                IntPtr reference = tss.Reference;
			    IntPtr result;
                interop.DLLStatistics.skewness_statistics(ref reference, out result);
                tss.Reference = reference;
                return (KhivaArray.Create(result));
            }
        }
    }
}
