// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Regression")]
    public class RegressionTests
    {
        private const double Delta = 1e-6;

        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestLinear()
        {
            double[] tss =
            {
                0.24580423, 0.59642861, 0.35879163, 0.37891011, 0.02445137,
                0.23830957, 0.38793433, 0.68054104, 0.83934083, 0.76073689
            };
            double[] tss2 =
            {
                0.2217416, 0.06344161, 0.77944375, 0.72174137, 0.19413884,
                0.51146167, 0.06880307, 0.39414268, 0.98172767, 0.30490851
            };
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2))
            {
                var (slopeArr, interceptArr, rvalueArr, pvalueArr, stderrestArr) = Regression.Linear(arr, arr2);
                using (slopeArr)
                using (interceptArr)
                using (rvalueArr)
                using (pvalueArr)
                using (stderrestArr)
                {
                    var slope = slopeArr.GetData1D<double>();
                    var intercept = interceptArr.GetData1D<double>();
                    var rvalue = rvalueArr.GetData1D<double>();
                    var pvalue = pvalueArr.GetData1D<double>();
                    var stderrest = stderrestArr.GetData1D<double>();

                    for (var i = 0; i < slope.Length; i++)
                    {
                        Assert.AreEqual(0.344864266, slope[i], Delta);
                        Assert.AreEqual(0.268578232, intercept[i], Delta);
                        Assert.AreEqual(0.283552942, rvalue[i], Delta);
                        Assert.AreEqual(0.427239418, pvalue[i], Delta);
                        Assert.AreEqual(0.412351891, stderrest[i], Delta);
                    }
                }
            }
        }

        [Test]
        public void TestLinearMultipleTimeSeries()
        {
            double[,] tss =
            {
                {
                    0.24580423, 0.59642861, 0.35879163, 0.37891011, 0.02445137,
                    0.23830957, 0.38793433, 0.68054104, 0.83934083, 0.76073689
                },
                {
                    0.24580423, 0.59642861, 0.35879163, 0.37891011, 0.02445137,
                    0.23830957, 0.38793433, 0.68054104, 0.83934083, 0.76073689
                }
            };
            double[,] tss2 =
            {
                {
                    0.2217416, 0.06344161, 0.77944375, 0.72174137, 0.19413884,
                    0.51146167, 0.06880307, 0.39414268, 0.98172767, 0.30490851
                },
                {
                    0.2217416, 0.06344161, 0.77944375, 0.72174137, 0.19413884,
                    0.51146167, 0.06880307, 0.39414268, 0.98172767, 0.30490851
                }
            };
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2))
            {
                var (slopeArr, interceptArr, rvalueArr, pvalueArr, stderrestArr) = Regression.Linear(arr, arr2);
                using (slopeArr)
                using (interceptArr)
                using (rvalueArr)
                using (pvalueArr)
                using (stderrestArr)
                {
                    var slope = slopeArr.GetData2D<double>();
                    var intercept = interceptArr.GetData2D<double>();
                    var rvalue = rvalueArr.GetData2D<double>();
                    var pvalue = pvalueArr.GetData2D<double>();
                    var stderrest = stderrestArr.GetData2D<double>();

                    for (var i = 0; i < slope.GetLength(0); i++)
                    {
                        for (var j = 0; j < slope.GetLength(1); j++)
                        {
                            Assert.AreEqual(0.344864266, slope[i, j], Delta);
                            Assert.AreEqual(0.268578232, intercept[i, j], Delta);
                            Assert.AreEqual(0.283552942, rvalue[i, j], Delta);
                            Assert.AreEqual(0.427239418, pvalue[i, j], Delta);
                            Assert.AreEqual(0.412351891, stderrest[i, j], Delta);
                        }
                    }
                }
            }
        }
    }
}