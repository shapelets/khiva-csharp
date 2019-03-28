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

namespace khiva.features.tests
{
    [TestFixture]
    public class FeaturesTests
    {
        readonly double DELTA = 1e-6;

        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestAbsEnergy()
        {
            int[] tss = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            using (array.Array arr = new array.Array(tss), absEnergyResult = Features.AbsEnergy(arr))
            {
                int[] result = absEnergyResult.GetData1D<int>();
                Assert.AreEqual(385, result[0], DELTA);
            }
        }

        [Test]
        public void TestAbsoluteSumOfChanges()
        {
            int[,] tss = new int[,] { { 0, 1, 2, 3 }, { 4, 6, 8, 10 }, { 11, 14, 17, 20 } };
            using (array.Array arr = new array.Array(tss), absoluteSumOfChangesResult = Features.AbsoluteSumOfChanges(arr))
            {
                float[,] result = absoluteSumOfChangesResult.GetData2D<float>();
                Assert.AreEqual(3, result[0, 0]);
                Assert.AreEqual(6, result[1, 0]);
                Assert.AreEqual(9, result[2, 0]);
            }
        }

        [Test]
        public void TestAggregatedAutocorrelation()
        {
            float[,] tss = new float[,] { { 1, 2, 3, 4, 5, 6 }, { 7, 8, 9, 10, 11, 12 } };
            using (array.Array arr = new array.Array(tss), aggregatedAutocorrelationResult = Features.AggregatedAutocorrelation(arr, 0))
            {
                float[,] result = aggregatedAutocorrelationResult.GetData2D<float>();
                Assert.AreEqual(-0.6571428571428571F, result[0, 0], DELTA);
                Assert.AreEqual(-0.6571428571428571F, result[1, 0], DELTA);
            }
        }

        [Test]
        public void TestAggregatedLinearTrendMean()
        {
            float[] tss = new float[] { 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 };
            using (array.Array arr = new array.Array(tss)) {
                var (slopeArr, interceptArr, rvalueArr, pValueArr, stderrestArr) = Features.AggregatedLinearTrend(arr, 3, 0);
                using (slopeArr)
                using (interceptArr)
                using (rvalueArr)
                using (pValueArr)
                using (stderrestArr) {
                    float[] slope = slopeArr.GetData1D<float>();
                    float[] intercept = interceptArr.GetData1D<float>();
                    float[] rvalue = rvalueArr.GetData1D<float>();
                    float[] pvalue = pValueArr.GetData1D<float>();
                    float[] stderrest = stderrestArr.GetData1D<float>();
                    Assert.AreEqual(1, slope[0], DELTA);
                    Assert.AreEqual(2, intercept[0], DELTA);
                    Assert.AreEqual(1, rvalue[0], DELTA);
                    Assert.AreEqual(0, pvalue[0], DELTA);
                    Assert.AreEqual(0, stderrest[0], DELTA);
                }
            }
        }

        [Test]
        public void TestApproximateEntropy()
        {
            float[,] tss = new float[,] { { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 } };
            using (array.Array arr = new array.Array(tss), approximateEntropyResult = Features.ApproximateEntropy(arr, 4, 0.5F))
            {
                float[] result = approximateEntropyResult.GetData1D<float>();
                Assert.AreEqual(0.13484281753639338, result[0], DELTA);
                Assert.AreEqual(0.13484281753639338, result[1], DELTA);
            }
        }

        [Test]
        public void TestCrossCovariance()
        {
            float[,] tss = new float[,] { { 0, 1, 2, 3 }, { 10, 11, 12, 13 } };
            float[,] tss2 = new float[,] { { 4, 6, 8, 10, 12 }, { 14, 16, 18, 20, 22 } };
            using (array.Array xss = new array.Array(tss), yss = new array.Array(tss2),
                approximateEntropyResult = Features.CrossCovariance(xss, yss, false))
            {
                float[,,] result = approximateEntropyResult.GetData3D<float>();
                float[] flattenResult = new float[result.Length];
                Flatten3D<float>(ref flattenResult, result);
                for (int i = 0; i < 4; i++)
                {
                    Assert.AreEqual(2.5, flattenResult[(i * 5)], DELTA);
                    Assert.AreEqual(2.5, flattenResult[(i * 5) + 1], DELTA);
                    Assert.AreEqual(0.25, flattenResult[(i * 5) + 2], DELTA);
                    Assert.AreEqual(-1.25, flattenResult[(i * 5) + 3], DELTA);
                    Assert.AreEqual(-1.5, flattenResult[(i * 5) + 4], DELTA);
                }
            }
        }

        private void Flatten3D<T>(ref T[] flattenArr, T[,,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    for (int k = 0; k < arr.GetLength(2); k++)
                    {
                        flattenArr[i * (arr.GetLength(1) * arr.GetLength(2)) + j * arr.GetLength(2) + k] = arr[i, j, k];
                    }
                }
            }
        }

        [Test]
        public void TestAutoCovariance()
        {
            float[,] tss = { { 0, 1, 2, 3 }, { 10, 11, 12, 13 } };
            using(array.Array arr = new array.Array(tss), autoCovarianceResult = Features.AutoCovariance(arr))
            {
                float[,] result = autoCovarianceResult.GetData2D<float>();
                Assert.AreEqual(1.25, result[0, 0], DELTA);
                Assert.AreEqual(0.3125, result[0, 1], DELTA);
                Assert.AreEqual(-0.375, result[0, 2], DELTA);
                Assert.AreEqual(-0.5625, result[0, 3], DELTA);
                Assert.AreEqual(1.25, result[1, 0], DELTA);
                Assert.AreEqual(0.3125, result[1, 1], DELTA);
                Assert.AreEqual(-0.375, result[1, 2], DELTA);
                Assert.AreEqual(-0.5625, result[1, 3], DELTA);
            }
        }

        [Test]
        public void TestCrossCorrelation()
        {
            double[] tss = { 1, 2, 3, 4 };
            double[] tss2 = { 4, 6, 8, 10, 12 };
            using (array.Array xss = new array.Array(tss), yss = new array.Array(tss2),
                crossCorrelationResult = Features.CrossCorrelation(xss, yss, false))
            {
                double[] result = crossCorrelationResult.GetData1D<double>();
                Assert.AreEqual(0.790569415, result[0], DELTA);
                Assert.AreEqual(0.790569415, result[1], DELTA);
                Assert.AreEqual(0.0790569415, result[2], DELTA);
                Assert.AreEqual(-0.395284707, result[3], DELTA);
                Assert.AreEqual(-0.474341649, result[4], DELTA);
            }
        }

        [Test]
        public void TestAutoCorrelation()
        {
            float[,] tss = { { 0, 1, 2, 3 }, { 10, 11, 12, 13 } };
            using (array.Array arr = new array.Array(tss), autoCorrelationResult = Features.AutoCorrelation(arr, 4, false))
            {
                float[,] result = autoCorrelationResult.GetData2D<float>();
                Assert.AreEqual(1, result[0, 0], DELTA);
                Assert.AreEqual(0.25, result[0, 1], DELTA);
                Assert.AreEqual(-0.3, result[0, 2], DELTA);
                Assert.AreEqual(-0.45, result[0, 3], DELTA);
                Assert.AreEqual(1, result[1, 0], DELTA);
                Assert.AreEqual(0.25, result[1, 1], DELTA);
                Assert.AreEqual(-0.3, result[1, 2], DELTA);
                Assert.AreEqual(-0.45, result[1, 3], DELTA);
            }
        }

        [Test]
        public void TestBinnedEntropy()
        {
            double[,] tss = { { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
                                14, 15, 16, 17, 18, 19, 20 },
                              { 1, 1, 3, 10, 5, 6, 1, 8, 9, 10, 11, 1, 13, 14, 10, 16,
                                17, 10, 19, 20 }
                            };
            using(array.Array arr = new array.Array(tss), binnedEntropyResult = Features.BinnedEntropy(arr, 5))
            {
                double[,] result = binnedEntropyResult.GetData2D<double>();
                Assert.AreEqual(1.6094379124341005, result[0, 0], DELTA);
                Assert.AreEqual(1.5614694247763998, result[1, 0], DELTA);
            }
        }

        [Test]
        public void TestC3()
        {
            float[,] tss = { { 0, 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10, 11 } };
            using(array.Array arr = new array.Array(tss), c3Result = Features.C3(arr, 2))
            {
                float[,] result = c3Result.GetData2D<float>();
                Assert.AreEqual(7.5, result[0, 0]);
                Assert.AreEqual(586.5, result[1, 0]);
            }

        }

        [Test]
        public void TestCidCe()
        {
            double[,] tss = { { 0, 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10, 11 } };
            using(array.Array arr = new array.Array(tss), cidCeFalse = Features.CidCe(arr, false), cidCeTrue = Features.CidCe(arr, true))
            {
                double[,] resultFalse = cidCeFalse.GetData2D<double>();
                double[,] resultTrue = cidCeTrue.GetData2D<double>();
                Assert.AreEqual(2.23606797749979, resultFalse[0, 0], DELTA);
                Assert.AreEqual(2.23606797749979, resultFalse[1, 0], DELTA);
                Assert.AreEqual(1.30930734141595, resultTrue[0, 0], DELTA);
                Assert.AreEqual(1.30930734141595, resultTrue[1, 0], DELTA);
            }
        }

        [Test]
        public void TestCountAboveMean()
        {
            float[,] tss = { { 0, 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10, 11 } };
            using (array.Array arr = new array.Array(tss), countAboveMeanResult = Features.CountAboveMean(arr))
            {
                uint[,] result = countAboveMeanResult.GetData2D<uint>();
                Assert.AreEqual(3, result[0, 0], DELTA);
                Assert.AreEqual(3, result[1, 0], DELTA);
            }
        }

        [Test]
        public void TestCountBellowMean()
        {
            float[,] tss = { { 0, 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10, 11 } };
            using (array.Array arr = new array.Array(tss), countBellowMeanResult = Features.CountBelowMean(arr))
            {
                uint[,] result = countBellowMeanResult.GetData2D<uint>();
                Assert.AreEqual(3, result[0, 0], DELTA);
                Assert.AreEqual(3, result[1, 0], DELTA);
            }
        }


    }
}
