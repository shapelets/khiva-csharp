// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Normalization")]
    public class NormalizationTests
    {
        private const double Delta = 1e-6;

        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestDecimalScalingNorm()
        {
            float[,] tss = {{0, 1, -2, 3}, {40, 50, 60, -70}};
            using (KhivaArray arr = KhivaArray.Create(tss), decimalScalingNorm = Normalization.DecimalScalingNorm(arr))
            {
                float[,] expected = {{0.0F, 0.1F, -0.2F, 0.3F}, {0.4F, 0.5F, 0.6F, -0.7F}};
                var result = decimalScalingNorm.GetData2D<float>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j]);
                    }
                }
            }
        }

        [Test]
        public void TestDecimalScalingNormInPlace()
        {
            float[,] tss = {{0, 1, -2, 3}, {40, 50, 60, -70}};
            var arr = KhivaArray.Create(tss);
            Normalization.DecimalScalingNorm(ref arr);
            float[,] expected = {{0.0F, 0.1F, -0.2F, 0.3F}, {0.4F, 0.5F, 0.6F, -0.7F}};
            var result = arr.GetData2D<float>();
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }

            arr.Dispose();
        }

        [Test]
        public void TestMaxMinNorm()
        {
            double[,] tss = {{0, 1, 2, 3}, {4, 5, 6, 7}};
            using (KhivaArray arr = KhivaArray.Create(tss), maxMinNorm = Normalization.MaxMinNorm(arr, 2.0, 1.0))
            {
                double[,] expected = {{1.0, 1.3333333333333, 1.66666667, 2.0}, {1.0, 1.3333333333333, 1.66666667, 2.0}};
                var result = maxMinNorm.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], Delta);
                    }
                }
            }
        }

        [Test]
        public void TestMaxMinNormInPlace()
        {
            double[,] tss = {{0, 1, 2, 3}, {4, 5, 6, 7}};
            var arr = KhivaArray.Create(tss);
            Normalization.MaxMinNorm(ref arr, 2.0, 1.0);
            double[,] expected = {{1.0, 1.3333333333333, 1.66666667, 2.0}, {1.0, 1.3333333333333, 1.66666667, 2.0}};
            var result = arr.GetData2D<double>();
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], Delta);
                }
            }

            arr.Dispose();
        }

        [Test]
        public void TestMeanNorm()
        {
            double[,] tss = {{0, 1, 2, 3}, {4, 5, 6, 7}};
            using (KhivaArray arr = KhivaArray.Create(tss), meanNorm = Normalization.MeanNorm(arr))
            {
                double[,] expected = {{-0.5, -0.166666667, 0.166666667, 0.5}, {-0.5, -0.166666667, 0.166666667, 0.5}};
                var result = meanNorm.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], Delta);
                    }
                }
            }
        }

        [Test]
        public void TestMeanNormInPlace()
        {
            double[,] tss = {{0, 1, 2, 3}, {4, 5, 6, 7}};
            var arr = KhivaArray.Create(tss);
            Normalization.MeanNorm(ref arr);
            double[,] expected = {{-0.5, -0.166666667, 0.166666667, 0.5}, {-0.5, -0.166666667, 0.166666667, 0.5}};
            var result = arr.GetData2D<double>();
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], Delta);
                }
            }

            arr.Dispose();
        }

        [Test]
        public void TestZNorm()
        {
            double[,] tss = {{0, 1, 2, 3}, {4, 5, 6, 7}};
            using (KhivaArray arr = KhivaArray.Create(tss), zNorm = Normalization.ZNorm(arr, 0.00000001))
            {
                double[] expected = {-1.341640786499870, -0.447213595499958, 0.447213595499958, 1.341640786499870};
                var result = zNorm.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[j], result[i, j], Delta);
                    }
                }
            }
        }

        [Test]
        public void TestZNormInPlace()
        {
            double[,] tss = {{0, 1, 2, 3}, {4, 5, 6, 7}};
            var arr = KhivaArray.Create(tss);
            Normalization.ZNorm(ref arr, 0.00000001);
            double[] expected = {-1.341640786499870, -0.447213595499958, 0.447213595499958, 1.341640786499870};
            var result = arr.GetData2D<double>();
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[j], result[i, j], Delta);
                }
            }

            arr.Dispose();
        }
    }
}