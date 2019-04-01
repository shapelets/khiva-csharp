// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva.regression.tests
{
    [TestFixture]
    public class RegressionTests
    {
        double DELTA = 1e-6;

        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestLinear()
        {
            double[] tss = { 0.24580423, 0.59642861, 0.35879163, 0.37891011, 0.02445137,
                              0.23830957, 0.38793433, 0.68054104, 0.83934083, 0.76073689 };
            double[] tss2 = { 0.2217416, 0.06344161, 0.77944375, 0.72174137, 0.19413884,
                              0.51146167, 0.06880307, 0.39414268, 0.98172767, 0.30490851 };
            using(array.Array arr = new array.Array(tss), arr2 = new array.Array(tss2))
            {
                var (slopeArr, interceptArr, rvalueArr, pvalueArr, stderrestArr) = Regression.Linear(arr, arr2);
                using (slopeArr)
                using (interceptArr)
                using (rvalueArr)
                using (pvalueArr)
                using (stderrestArr)
                {
                    double[] slope = slopeArr.GetData1D<double>();
                    double[] intercept = interceptArr.GetData1D<double>();
                    double[] rvalue = rvalueArr.GetData1D<double>();
                    double[] pvalue = pvalueArr.GetData1D<double>();
                    double[] stderrest = stderrestArr.GetData1D<double>();

                    for (int i = 0; i < slope.Length; i++)
                    {
                        Assert.AreEqual(0.344864266, slope[i], DELTA);
                        Assert.AreEqual(0.268578232, intercept[i], DELTA);
                        Assert.AreEqual(0.283552942, rvalue[i], DELTA);
                        Assert.AreEqual(0.427239418, pvalue[i], DELTA);
                        Assert.AreEqual(0.412351891, stderrest[i], DELTA);
                    }
                }
            }
        }

        [Test]
        public void TestLinearMultipleTimeSeries()
        {
            double[,] tss = {{ 0.24580423, 0.59642861, 0.35879163, 0.37891011, 0.02445137,
                              0.23830957, 0.38793433, 0.68054104, 0.83934083, 0.76073689 },
                            { 0.24580423, 0.59642861, 0.35879163, 0.37891011, 0.02445137,
                              0.23830957, 0.38793433, 0.68054104, 0.83934083, 0.76073689 }};
            double[,] tss2 = {{ 0.2217416, 0.06344161, 0.77944375, 0.72174137, 0.19413884,
                              0.51146167, 0.06880307, 0.39414268, 0.98172767, 0.30490851 },
                            { 0.2217416, 0.06344161, 0.77944375, 0.72174137, 0.19413884,
                              0.51146167, 0.06880307, 0.39414268, 0.98172767, 0.30490851 }};
            using (array.Array arr = new array.Array(tss), arr2 = new array.Array(tss2))
            {
                var (slopeArr, interceptArr, rvalueArr, pvalueArr, stderrestArr) = Regression.Linear(arr, arr2);
                using (slopeArr)
                using (interceptArr)
                using (rvalueArr)
                using (pvalueArr)
                using (stderrestArr)
                {
                    double[,] slope = slopeArr.GetData2D<double>();
                    double[,] intercept = interceptArr.GetData2D<double>();
                    double[,] rvalue = rvalueArr.GetData2D<double>();
                    double[,] pvalue = pvalueArr.GetData2D<double>();
                    double[,] stderrest = stderrestArr.GetData2D<double>();

                    for (int i = 0; i < slope.GetLength(0); i++)
                    {
                        for (int j = 0; j < slope.GetLength(1); j++)
                        {
                            Assert.AreEqual(0.344864266, slope[i,j], DELTA);
                            Assert.AreEqual(0.268578232, intercept[i,j], DELTA);
                            Assert.AreEqual(0.283552942, rvalue[i,j], DELTA);
                            Assert.AreEqual(0.427239418, pvalue[i,j], DELTA);
                            Assert.AreEqual(0.412351891, stderrest[i,j], DELTA);
                        }
                    }
                }
            }
        }

    }
}
