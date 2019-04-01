using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace regression
    {
        public static class Regression
        {
            /**
             * @brief Calculates a linear least-squares regression for two sets of measurements. Both arrays should have the same
             * length.
             *
             * @param xss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series.
             * @param yss Expects an input array whose dimension zero is the length of the time series (all the same) and dimension
             * one indicates the number of time series.
             * @return slope Slope of the regression line.
             * @return intercept Intercept of the regression line.
             * @return rvalue Correlation coefficient.
             * @return pvalue Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero, using Wald
             * Test with t-distribution of the test statistic.
             * @return stderrest Standard error of the estimated gradient.
             */
            public static (array.Array, array.Array, array.Array, array.Array, array.Array) Linear(array.Array xss, array.Array yss)
            {
                IntPtr xReference = xss.Reference;
                IntPtr yReference = yss.Reference;
                interop.DLLRegression.linear(ref xReference, ref yReference,
                                            out IntPtr slope, out IntPtr intercept, out IntPtr rvalue, out IntPtr pvalue, out IntPtr stderrest);
                xss.Reference = xReference;
                yss.Reference = yReference;
                var tuple = (slope: new array.Array(slope),
                            intercept: new array.Array(intercept),
                            rvalue: new array.Array(rvalue),
                            pvalue: new array.Array(pvalue),
                            stderrest: new array.Array(stderrest));
                return tuple;
            }
        }
    }
}
