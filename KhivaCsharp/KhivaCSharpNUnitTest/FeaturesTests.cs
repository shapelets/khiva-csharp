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
            array.Array arr = new array.Array(tss);
            array.Array absEnergyResult = Features.AbsEnergy(arr);
            int[] result = absEnergyResult.GetData1D<int>();
            Assert.AreEqual(385, result[0], DELTA);
        }

        [Test]
        public void TestAbsoluteSumOfChanges()
        {
            int[,] tss = new int[,] { { 0, 1, 2, 3 }, { 4, 6, 8, 10 }, { 11, 14, 17, 20 } };
            array.Array arr = new array.Array(tss);
            array.Array absoluteSumOfChangesResult = Features.AbsoluteSumOfChanges(arr);
            float[,] result = absoluteSumOfChangesResult.GetData2D<float>();
            Assert.AreEqual(3, result[0,0]);
            Assert.AreEqual(6, result[1,0]);
            Assert.AreEqual(9, result[2,0]);
        }

        [Test]
        public void TestAggregatedAutocorrelation()
        {
            float[,] tss = new float[,] { { 1, 2, 3, 4, 5, 6 }, { 7, 8, 9, 10, 11, 12 } };
            array.Array arr = new array.Array(tss);
            array.Array aggregatedAutocorrelationResult = Features.AggregatedAutocorrelation(arr, 0);
            float[,] result = aggregatedAutocorrelationResult.GetData2D<float>();
            Assert.AreEqual(-0.6571428571428571F, result[0, 0], DELTA);
            Assert.AreEqual(-0.6571428571428571F, result[1, 0], DELTA);
        }

        [Test]
        public void TestAggregatedLinearTrendMean()
        {
            float[] tss = new float[] { 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 };
            array.Array arr = new array.Array(tss);
            Tuple<array.Array, array.Array, array.Array, array.Array, array.Array> aggregatedLinearTrendResult = 
                Features.AggregatedLinearTrend(arr, 3, 0);
            float[] slope = aggregatedLinearTrendResult.Item1.GetData1D<float>();
            float[] intercept = aggregatedLinearTrendResult.Item2.GetData1D<float>();
            float[] rvalue = aggregatedLinearTrendResult.Item3.GetData1D<float>();
            float[] pvalue = aggregatedLinearTrendResult.Item4.GetData1D<float>();
            float[] stderrest = aggregatedLinearTrendResult.Item5.GetData1D<float>();
            Assert.AreEqual(1, slope[0], DELTA);
            Assert.AreEqual(2, intercept[0], DELTA);
            Assert.AreEqual(1, rvalue[0], DELTA);
            Assert.AreEqual(0, pvalue[0], DELTA);
            Assert.AreEqual(0, stderrest[0], DELTA);
        }

        [Test]
        public void TestApproximateEntropy()
        {
            float[,] tss = new float[,] { { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 } };
            array.Array arr = new array.Array(tss);
            array.Array approximateEntropyResult = Features.ApproximateEntropy(arr, 4, 0.5F);
            float[] result = approximateEntropyResult.GetData1D<float>();
            Assert.AreEqual(0.13484281753639338, result[0], DELTA);
            Assert.AreEqual(0.13484281753639338, result[1], DELTA);
        }

        [Test]
        public void TestCrossCovariance()
        {
            float[,] tss = new float[,] { { 0, 1, 2, 3 }, { 10, 11, 12, 13 } };
            float[,] tss2 = new float[,] { { 4, 6, 8, 10, 12 }, { 14, 16, 18, 20, 22 } };
            array.Array xss = new array.Array(tss);
            array.Array yss = new array.Array(tss2);
            array.Array approximateEntropyResult = Features.CrossCovariance(xss, yss, false);
            float[,,] result = approximateEntropyResult.GetData3D<float>();
            float[] flattenResult = new float[result.Length];
            Flatten3D<float>(ref flattenResult, result);
            for (int i = 0; i<4; i++)
            {
                Assert.AreEqual(2.5, flattenResult[(i * 5)], DELTA);
                Assert.AreEqual(2.5, flattenResult[(i * 5) + 1], DELTA);
                Assert.AreEqual(0.25, flattenResult[(i * 5) + 2], DELTA);
                Assert.AreEqual(-1.25, flattenResult[(i * 5) + 3], DELTA);
                Assert.AreEqual(-1.5, flattenResult[(i * 5) + 4], DELTA);
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


    }
}
