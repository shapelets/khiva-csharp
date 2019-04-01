using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace statistics
    {
        public static class Statistics
        {
            /**
            * @brief Returns the covariance matrix of the time series contained in tss.
            *
            * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
            * one indicates the number of time series.
            * @param unbiased Determines whether it divides by n - 1 (if false) or n (if true).
            * @return result The covariance matrix of the time series.
            */
            public static array.Array CovarianceStatistics(array.Array tss, bool unbiased)
            {
                IntPtr reference = tss.Reference;
                interop.DLLStatistics.covariance_statistics(ref reference, ref unbiased, out IntPtr result);
                tss.Reference = reference;
                return (new array.Array(result));
            }

            /**
             * @brief Returns the kurtosis of tss (calculated with the adjusted Fisher-Pearson standardized moment coefficient G2).
             *
             * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series.
             * @return result The kurtosis of tss.
             */
            public static array.Array KurtosisStatistics(array.Array tss)
            {
                IntPtr reference = tss.Reference;
                interop.DLLStatistics.kurtosis_statistics(ref reference, out IntPtr result);
                tss.Reference = reference;
                return (new array.Array(result));
            }

            /**
             * @brief The Ljung–Box test checks that data whithin the time series are independently distributed (i.e. the
             * correlations in the population from which the sample is taken are 0, so that any observed correlations in the data
             * result from randomness of the sampling process). Data are no independently distributed, if they exhibit serial
             * correlation.
             *
             * The test statistic is:
             *
             * \f[
             * Q = n\left(n+2\right)\sum_{k=1}^h\frac{\hat{\rho}^2_k}{n-k}
             * \f]
             *
             * where ''n'' is the sample size, \f$\hat{\rho}k \f$ is the sample autocorrelation at lag ''k'', and ''h'' is the
             * number of lags being tested. Under \f$ H_0 \f$ the statistic Q follows a \f$\chi^2{(h)} \f$. For significance level
             * \f$\alpha\f$, the \f$critical region\f$ for rejection of the hypothesis of randomness is:
             *
             * \f[
             * Q > \chi_{1-\alpha,h}^2
             * \f]
             *
             * where \f$ \chi_{1-\alpha,h}^2 \f$ is the \f$\alpha\f$-quantile of the chi-squared distribution with ''h'' degrees of
             * freedom.
             *
             * [1] G. M. Ljung  G. E. P. Box (1978). On a measure of lack of fit in time series models.
             * Biometrika, Volume 65, Issue 2, 1 August 1978, Pages 297–303.
             *
             * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series.
             * @param lags Number of lags being tested.
             * @return The Ljung-Box statistic test.
             */
            public static array.Array LjungBox(array.Array tss, long lags)
            {
                IntPtr reference = tss.Reference;
                interop.DLLStatistics.ljung_box(ref reference, ref lags, out IntPtr result);
                tss.Reference = reference;
                return (new array.Array(result));
            }

            /**
             * @brief Returns the kth moment of the given time series.
             *
             * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series.
             * @param k The specific moment to be calculated.
             * @return result The kth moment of the given time series.
             */
            public static array.Array MomentStatistics(array.Array tss, int k)
            {
                IntPtr reference = tss.Reference;
                interop.DLLStatistics.moment_statistics(ref reference, ref k, out IntPtr result);
                tss.Reference = reference;
                return (new array.Array(result));
            }

            /**
             * @brief Returns values at the given quantile.
             *
             * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series. NOTE: the time series should be sorted.
             * @param q Percentile(s) at which to extract score(s). One or many.
             * @param precision Number of decimals expected.
             * @return result Values at the given quantile.
             */
            public static array.Array QuantileStatistics(array.Array tss, array.Array q, float precision)
            {
                IntPtr reference = tss.Reference;
                IntPtr qReference = q.Reference;
                interop.DLLStatistics.quantile_statistics(ref reference, ref qReference, ref precision, out IntPtr result);
                tss.Reference = reference;
                q.Reference = qReference;
                return (new array.Array(result));
            }

            /**
             * @brief Discretizes the time series into equal-sized buckets based on sample quantiles.
             *
             * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series. NOTE: the time series should be sorted.
             * @param quantiles Number of quantiles to extract. From 0 to 1, step 1/quantiles.
             * @param precision Number of decimals expected.
             * @return result Matrix with the categories, one category per row, the start of the category in the first column and
             * the end in the second category.
             */
            public static array.Array QuantilesCutStatistics(array.Array tss, float quantiles, float precision)
            {
                IntPtr reference = tss.Reference;
                interop.DLLStatistics.quantiles_cut_statistics(ref reference, ref quantiles, ref precision, out IntPtr result);
                tss.Reference = reference;
                return (new array.Array(result));
            }

            /**
             * @brief Estimates standard deviation based on a sample. The standard deviation is calculated using the "n-1" method.
             *
             * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series.
             * @return result The sample standard deviation.
             */
            public static array.Array SampleStdevStatistics(array.Array tss)
            {
                IntPtr reference = tss.Reference;
                interop.DLLStatistics.sample_stdev_statistics(ref reference, out IntPtr result);
                tss.Reference = reference;
                return (new array.Array(result));
            }

            /**
             * @brief Calculates the sample skewness of tss (calculated with the adjusted Fisher-Pearson standardized moment
             * coefficient G1).
             *
             * @param tss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series.
             * @return result Array containing the skewness of each time series in tss.
             */
            public static array.Array SkewnessStatistics(array.Array tss)
            {
                IntPtr reference = tss.Reference;
                interop.DLLStatistics.skewness_statistics(ref reference, out IntPtr result);
                tss.Reference = reference;
                return (new array.Array(result));
            }
        }
    }
}
