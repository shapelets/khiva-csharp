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

namespace khiva.normalization.tests
{
    [TestFixture]
    public class NormalizationTests
    {
        double DELTA = 1e-6;

        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestDecimalScalingNorm()
        {
            float[,] tss = { { 0, 1, -2, 3 }, { 40, 50, 60, -70 } };
            using(array.Array arr = new array.Array(tss), decimalScalingNorm = Normalization.DecimalScalingNorm(arr))
            {
                float[,] expected = { { 0.0F, 0.1F, -0.2F, 0.3F }, { 0.4F, 0.5F, 0.6F, -0.7F } };
                float[,] result = decimalScalingNorm.GetData2D<float>();
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j]);
                    }
                }
            }
        }

        [Test]
        public void TestDecimalScalingNormInPlace()
        {
            float[,] tss = { { 0, 1, -2, 3 }, { 40, 50, 60, -70 } };
            array.Array arr = new array.Array(tss);
            Normalization.DecimalScalingNorm(ref arr);
            float[,] expected = { { 0.0F, 0.1F, -0.2F, 0.3F }, { 0.4F, 0.5F, 0.6F, -0.7F } };
            float[,] result = arr.GetData2D<float>();
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
            arr.Dispose();
        }

        [Test]
        public void TestMaxMinNorm()
        {
            double[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 } };
            using (array.Array arr = new array.Array(tss), maxMinNorm = Normalization.MaxMinNorm(arr, 2.0, 1.0))
            {
                double[,] expected = { { 1.0, 1.3333333333333, 1.66666667, 2.0 }, { 1.0, 1.3333333333333, 1.66666667, 2.0 } };
                double[,] result = maxMinNorm.GetData2D<double>();
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], DELTA);
                    }
                }
            }
        }

        [Test]
        public void TestMaxMinNormInPlace()
        {
            double[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 } };
            array.Array arr = new array.Array(tss);
            Normalization.MaxMinNorm(ref arr, 2.0, 1.0);
            double[,] expected = { { 1.0, 1.3333333333333, 1.66666667, 2.0 }, { 1.0, 1.3333333333333, 1.66666667, 2.0 } };
            double[,] result = arr.GetData2D<double>();
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], DELTA);
                }
            }
            arr.Dispose();
        }

        [Test]
        public void TestMeanNorm()
        {
            double[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 } };
            using (array.Array arr = new array.Array(tss), meanNorm = Normalization.MeanNorm(arr))
            {
                double[,] expected = { { -0.5, -0.166666667, 0.166666667, 0.5 }, { -0.5, -0.166666667, 0.166666667, 0.5 } };
                double[,] result = meanNorm.GetData2D<double>();
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], DELTA);
                    }
                }
            }
        }

        [Test]
        public void TestMeanNormInPlace()
        {
            double[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 } };
            array.Array arr = new array.Array(tss);
            Normalization.MeanNorm(ref arr);
            double[,] expected = { { -0.5, -0.166666667, 0.166666667, 0.5 }, { -0.5, -0.166666667, 0.166666667, 0.5 } };
            double[,] result = arr.GetData2D<double>();
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], DELTA);
                }
            }
            arr.Dispose();
        }

        [Test]
        public void TestZnorm()
        {
            double[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 } };
            using (array.Array arr = new array.Array(tss), znorm = Normalization.Znorm(arr, 0.00000001))
            {
                double[] expected = { -1.341640786499870, -0.447213595499958, 0.447213595499958, 1.341640786499870 };
                double[,] result = znorm.GetData2D<double>();
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[j], result[i, j], DELTA);
                    }
                }
            }
        }

        [Test]
        public void TestZnormInPlace()
        {
            double[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 } };
            array.Array arr = new array.Array(tss);
            Normalization.Znorm(ref arr, 0.00000001);
            double[] expected = { -1.341640786499870, -0.447213595499958, 0.447213595499958, 1.341640786499870 };
            double[,] result = arr.GetData2D<double>();
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[j], result[i, j], DELTA);
                }
            }
            arr.Dispose();
        }
    }
}
