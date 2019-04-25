// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Features")]
    public class FeaturesTests
    {
        private const double Delta = 1e-6;

        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestAbsEnergy()
        {
            var tss = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            using (KhivaArray arr = KhivaArray.Create(tss), absEnergy = Features.AbsEnergy(arr))
            {
                var result = absEnergy.GetData1D<int>();
                Assert.AreEqual(385, result[0], Delta);
            }
        }

        [Test]
        public void TestAbsoluteSumOfChanges()
        {
            var tss = new[,] {{0, 1, 2, 3}, {4, 6, 8, 10}, {11, 14, 17, 20}};
            using (KhivaArray arr = KhivaArray.Create(tss), absoluteSumOfChanges = Features.AbsoluteSumOfChanges(arr))
            {
                var result = absoluteSumOfChanges.GetData2D<float>();
                Assert.AreEqual(3, result[0, 0]);
                Assert.AreEqual(6, result[1, 0]);
                Assert.AreEqual(9, result[2, 0]);
            }
        }

        [Test]
        public void TestAggregatedAutocorrelation()
        {
            var tss = new float[,] {{1, 2, 3, 4, 5, 6}, {7, 8, 9, 10, 11, 12}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                aggregatedAutocorrelation = Features.AggregatedAutoCorrelation(arr, 0))
            {
                var result = aggregatedAutocorrelation.GetData2D<float>();
                Assert.AreEqual(-0.6571428571428571F, result[0, 0], Delta);
                Assert.AreEqual(-0.6571428571428571F, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestAggregatedLinearTrendMean()
        {
            var tss = new float[] {2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5};
            using (var arr = KhivaArray.Create(tss))
            {
                var (slopeArr, interceptArr, rvalueArr, pValueArr, stderrestArr) =
                    Features.AggregatedLinearTrend(arr, 3, 0);
                using (slopeArr)
                using (interceptArr)
                using (rvalueArr)
                using (pValueArr)
                using (stderrestArr)
                {
                    var slope = slopeArr.GetData1D<float>();
                    var intercept = interceptArr.GetData1D<float>();
                    var rvalue = rvalueArr.GetData1D<float>();
                    var pvalue = pValueArr.GetData1D<float>();
                    var stderrest = stderrestArr.GetData1D<float>();
                    Assert.AreEqual(1, slope[0], Delta);
                    Assert.AreEqual(2, intercept[0], Delta);
                    Assert.AreEqual(1, rvalue[0], Delta);
                    Assert.AreEqual(0, pvalue[0], Delta);
                    Assert.AreEqual(0, stderrest[0], Delta);
                }
            }
        }

        [Test]
        public void TestApproximateEntropy()
        {
            var tss = new float[,] {{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, {11, 12, 13, 14, 15, 16, 17, 18, 19, 20}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                approximateEntropy = Features.ApproximateEntropy(arr, 4, 0.5F))
            {
                var result = approximateEntropy.GetData1D<float>();
                Assert.AreEqual(0.13484281753639338, result[0], Delta);
                Assert.AreEqual(0.13484281753639338, result[1], Delta);
            }
        }

        [Test]
        public void TestCrossCovariance()
        {
            var tss = new float[,] {{0, 1, 2, 3}, {10, 11, 12, 13}};
            var tss2 = new float[,] {{4, 6, 8, 10, 12}, {14, 16, 18, 20, 22}};
            using (KhivaArray xss = KhivaArray.Create(tss),
                yss = KhivaArray.Create(tss2),
                approximateEntropy = Features.CrossCovariance(xss, yss, false))
            {
                var result = approximateEntropy.GetData3D<float>();
                var flatten = new float[result.Length];
                Flatten3D(ref flatten, result);
                for (var i = 0; i < 4; i++)
                {
                    Assert.AreEqual(2.5, flatten[i * 5], Delta);
                    Assert.AreEqual(2.5, flatten[i * 5 + 1], Delta);
                    Assert.AreEqual(0.25, flatten[i * 5 + 2], Delta);
                    Assert.AreEqual(-1.25, flatten[i * 5 + 3], Delta);
                    Assert.AreEqual(-1.5, flatten[i * 5 + 4], Delta);
                }
            }
        }

        private static void Flatten3D<T>(ref T[] flattenArr, T[,,] arr)
        {
            for (var i = 0; i < arr.GetLength(0); i++)
            {
                for (var j = 0; j < arr.GetLength(1); j++)
                {
                    for (var k = 0; k < arr.GetLength(2); k++)
                    {
                        flattenArr[i * arr.GetLength(1) * arr.GetLength(2) + j * arr.GetLength(2) + k] = arr[i, j, k];
                    }
                }
            }
        }

        [Test]
        public void TestAutoCovariance()
        {
            float[,] tss = {{0, 1, 2, 3}, {10, 11, 12, 13}};
            using (KhivaArray arr = KhivaArray.Create(tss), autoCovariance = Features.AutoCovariance(arr))
            {
                var result = autoCovariance.GetData2D<float>();
                Assert.AreEqual(1.25, result[0, 0], Delta);
                Assert.AreEqual(0.3125, result[0, 1], Delta);
                Assert.AreEqual(-0.375, result[0, 2], Delta);
                Assert.AreEqual(-0.5625, result[0, 3], Delta);
                Assert.AreEqual(1.25, result[1, 0], Delta);
                Assert.AreEqual(0.3125, result[1, 1], Delta);
                Assert.AreEqual(-0.375, result[1, 2], Delta);
                Assert.AreEqual(-0.5625, result[1, 3], Delta);
            }
        }

        [Test]
        public void TestCrossCorrelation()
        {
            double[] tss = {1, 2, 3, 4};
            double[] tss2 = {4, 6, 8, 10, 12};
            using (KhivaArray xss = KhivaArray.Create(tss),
                yss = KhivaArray.Create(tss2),
                crossCorrelation = Features.CrossCorrelation(xss, yss, false))
            {
                var result = crossCorrelation.GetData1D<double>();
                Assert.AreEqual(0.790569415, result[0], Delta);
                Assert.AreEqual(0.790569415, result[1], Delta);
                Assert.AreEqual(0.0790569415, result[2], Delta);
                Assert.AreEqual(-0.395284707, result[3], Delta);
                Assert.AreEqual(-0.474341649, result[4], Delta);
            }
        }

        [Test]
        public void TestAutoCorrelation()
        {
            float[,] tss = {{0, 1, 2, 3}, {10, 11, 12, 13}};
            using (KhivaArray arr = KhivaArray.Create(tss), autoCorrelation = Features.AutoCorrelation(arr, 4, false))
            {
                var result = autoCorrelation.GetData2D<float>();
                Assert.AreEqual(1, result[0, 0], Delta);
                Assert.AreEqual(0.25, result[0, 1], Delta);
                Assert.AreEqual(-0.3, result[0, 2], Delta);
                Assert.AreEqual(-0.45, result[0, 3], Delta);
                Assert.AreEqual(1, result[1, 0], Delta);
                Assert.AreEqual(0.25, result[1, 1], Delta);
                Assert.AreEqual(-0.3, result[1, 2], Delta);
                Assert.AreEqual(-0.45, result[1, 3], Delta);
            }
        }

        [Test]
        public void TestBinnedEntropy()
        {
            double[,] tss =
            {
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
                    14, 15, 16, 17, 18, 19, 20
                },
                {
                    1, 1, 3, 10, 5, 6, 1, 8, 9, 10, 11, 1, 13, 14, 10, 16,
                    17, 10, 19, 20
                }
            };
            using (KhivaArray arr = KhivaArray.Create(tss), binnedEntropy = Features.BinnedEntropy(arr, 5))
            {
                var result = binnedEntropy.GetData2D<double>();
                Assert.AreEqual(1.6094379124341005, result[0, 0], Delta);
                Assert.AreEqual(1.5614694247763998, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestC3()
        {
            float[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (KhivaArray arr = KhivaArray.Create(tss), c3 = Features.C3(arr, 2))
            {
                var result = c3.GetData2D<float>();
                Assert.AreEqual(7.5, result[0, 0]);
                Assert.AreEqual(586.5, result[1, 0]);
            }
        }

        [Test]
        public void TestCidCe()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                cidCeFalse = Features.CidCe(arr, false),
                cidCeTrue = Features.CidCe(arr, true))
            {
                var resultFalse = cidCeFalse.GetData2D<double>();
                var resultTrue = cidCeTrue.GetData2D<double>();
                Assert.AreEqual(2.23606797749979, resultFalse[0, 0], Delta);
                Assert.AreEqual(2.23606797749979, resultFalse[1, 0], Delta);
                Assert.AreEqual(1.30930734141595, resultTrue[0, 0], Delta);
                Assert.AreEqual(1.30930734141595, resultTrue[1, 0], Delta);
            }
        }

        [Test]
        public void TestCountAboveMean()
        {
            float[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (KhivaArray arr = KhivaArray.Create(tss), countAboveMean = Features.CountAboveMean(arr))
            {
                var result = countAboveMean.GetData2D<uint>();
                Assert.AreEqual(3, result[0, 0], Delta);
                Assert.AreEqual(3, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestCountBellowMean()
        {
            float[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (KhivaArray arr = KhivaArray.Create(tss), countBellowMean = Features.CountBelowMean(arr))
            {
                var result = countBellowMean.GetData2D<uint>();
                Assert.AreEqual(3, result[0, 0], Delta);
                Assert.AreEqual(3, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestCwtCoefficients()
        {
            double[,] tss = {{0.1, 0.2, 0.3}, {0.1, 0.2, 0.3}};
            int[] width = {1, 2, 3};
            using (KhivaArray arr = KhivaArray.Create(tss),
                widthArr = KhivaArray.Create(width),
                cwtCoeff = Features.CwtCoefficients(arr, widthArr, 2, 2))
            {
                var result = cwtCoeff.GetData2D<float>();
                Assert.AreEqual(0.26517161726951599, result[0, 0], Delta);
                Assert.AreEqual(0.26517161726951599, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestEnergyRatioByChunks()
        {
            float[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                energyRatioByChunks0 = Features.EnergyRatioByChunks(arr, 2, 0),
                energyRatioByChunks1 = Features.EnergyRatioByChunks(arr, 2, 1))
            {
                var result0 = energyRatioByChunks0.GetData2D<float>();
                var result1 = energyRatioByChunks1.GetData2D<float>();
                Assert.AreEqual(0.090909091, result0[0, 0], Delta);
                Assert.AreEqual(0.330376940, result0[1, 0], Delta);
                Assert.AreEqual(0.909090909, result1[0, 0], Delta);
                Assert.AreEqual(0.669623060, result1[1, 0], Delta);
            }
        }

        [Test]
        public void TestFftAggregated()
        {
            float[,] tss =
            {
                {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
                {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), fftAggregated = Features.FftAggregated(arr))
            {
                var result = fftAggregated.GetData2D<float>();
                Assert.AreEqual(1.135143, result[0, 0], 1e-4);
                Assert.AreEqual(2.368324, result[0, 1], 1e-4);
                Assert.AreEqual(1.248777, result[0, 2], 1e-4);
                Assert.AreEqual(3.642666, result[0, 3], 1e-4);
                Assert.AreEqual(1.135143, result[1, 0], 1e-4);
                Assert.AreEqual(2.368324, result[1, 1], 1e-4);
                Assert.AreEqual(1.248777, result[1, 2], 1e-4);
                Assert.AreEqual(3.642666, result[1, 3], 1e-4);
            }
        }

        [Test]
        public void TestFftCoefficient()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (var arr = KhivaArray.Create(tss))
            {
                var (realArr, imagArr, absoluteArr, angleArr) = Features.FftCoefficient(arr, 0);
                using (realArr)
                using (imagArr)
                using (absoluteArr)
                using (angleArr)
                {
                    var real = realArr.GetData2D<double>();
                    var imag = imagArr.GetData2D<double>();
                    var absolute = absoluteArr.GetData2D<double>();
                    var angle = angleArr.GetData2D<double>();
                    Assert.AreEqual(15, real[0, 0], Delta);
                    Assert.AreEqual(51, real[1, 0], Delta);

                    Assert.AreEqual(0, imag[0, 0], Delta);
                    Assert.AreEqual(0, imag[1, 0], Delta);

                    Assert.AreEqual(15, absolute[0, 0], Delta);
                    Assert.AreEqual(51, absolute[1, 0], Delta);

                    Assert.AreEqual(0, angle[0, 0], Delta);
                    Assert.AreEqual(0, angle[1, 0], Delta);
                }
            }
        }

        [Test]
        public void TestFirstLocationOfMaximum()
        {
            float[,] tss =
            {
                {5, 4, 3, 5, 0, 1, 5, 3, 2, 1},
                {2, 4, 3, 5, 2, 5, 4, 3, 5, 2}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), firstLocationOfMax = Features.FirstLocationOfMaximum(arr))
            {
                var result = firstLocationOfMax.GetData2D<float>();
                Assert.AreEqual(0, result[0, 0], Delta);
                Assert.AreEqual(0.3, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestFirstLocationOfMinimum()
        {
            float[,] tss =
            {
                {5, 4, 3, 0, 0, 1},
                {5, 4, 3, 0, 2, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), firstLocationOfMin = Features.FirstLocationOfMinimum(arr))
            {
                var result = firstLocationOfMin.GetData2D<float>();
                Assert.AreEqual(0.5, result[0, 0], Delta);
                Assert.AreEqual(0.5, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestFriedrichCoefficients()
        {
            double[,] tss =
            {
                {0, 1, 2, 3, 4, 5},
                {0, 1, 2, 3, 4, 5}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), friedrichCoeff = Features.FriedrichCoefficients(arr, 4, 2))
            {
                var result = friedrichCoeff.GetData2D<double>();
                double[,] expected =
                {
                    {
                        -0.0009912563255056738, -0.0027067768387496471, -0.00015192681166809052,
                        0.10512571036815643, 0.89872437715530396
                    },
                    {
                        -0.0009912563255056738, -0.0027067768387496471, -0.00015192681166809052,
                        0.10512571036815643, 0.89872437715530396
                    }
                };
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
        public void TestHasDuplicates()
        {
            int[,] tss =
            {
                {5, 4, 3, 0, 0, 1},
                {5, 4, 3, 0, 2, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), hasDuplicates = Features.HasDuplicates(arr))
            {
                var result = hasDuplicates.GetData2D<bool>();
                Assert.AreEqual(true, result[0, 0]);
                Assert.AreEqual(false, result[1, 0]);
            }
        }

        [Test]
        public void TestHasDuplicateMax()
        {
            int[,] tss =
            {
                {5, 4, 3, 0, 5, 1},
                {5, 4, 3, 0, 2, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), hasDuplicateMax = Features.HasDuplicateMax(arr))
            {
                var result = hasDuplicateMax.GetData2D<bool>();
                Assert.AreEqual(true, result[0, 0]);
                Assert.AreEqual(false, result[1, 0]);
            }
        }

        [Test]
        public void TestHasDuplicateMin()
        {
            int[,] tss =
            {
                {5, 4, 3, 0, 0, 1},
                {5, 4, 3, 0, 2, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), hasDuplicateMin = Features.HasDuplicateMin(arr))
            {
                var result = hasDuplicateMin.GetData2D<bool>();
                Assert.AreEqual(true, result[0, 0]);
                Assert.AreEqual(false, result[1, 0]);
            }
        }

        [Test]
        public void TestIndexMassQuantile()
        {
            float[,] tss =
            {
                {5, 4, 3, 0, 0, 1},
                {5, 4, 0, 0, 2, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), indexMassQuantile = Features.IndexMassQuantile(arr, 0.5F))
            {
                var result = indexMassQuantile.GetData2D<float>();
                Assert.AreEqual(0.333333333, result[0, 0], Delta);
                Assert.AreEqual(0.333333333, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestKurtosis()
        {
            float[,] tss =
            {
                {0, 1, 2, 3, 4, 5},
                {2, 2, 2, 20, 30, 25}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), kurtosis = Features.Kurtosis(arr))
            {
                var result = kurtosis.GetData2D<float>();
                Assert.AreEqual(-1.2, result[0, 0], Delta);
                Assert.AreEqual(-2.66226722, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestLargeStandardDeviation()
        {
            int[,] tss =
            {
                {-1, -1, -1, 1, 1, 1},
                {4, 6, 8, 4, 5, 4}
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                largeStandardDeviation = Features.LargeStandardDeviation(arr, 0.4F))
            {
                var result = largeStandardDeviation.GetData2D<bool>();
                Assert.AreEqual(true, result[0, 0]);
                Assert.AreEqual(false, result[1, 0]);
            }
        }

        [Test]
        public void TestLastLocationOfMaximum()
        {
            double[,] tss =
            {
                {0, 4, 3, 5, 5, 1},
                {0, 4, 3, 2, 5, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), lastLocationOfMaximum = Features.LastLocationOfMaximum(arr))
            {
                var result = lastLocationOfMaximum.GetData2D<double>();
                Assert.AreEqual(0.8333333333333334, result[0, 0], Delta);
                Assert.AreEqual(0.8333333333333334, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestLastLocationOfMinimum()
        {
            double[,] tss =
            {
                {0, 4, 3, 5, 5, 1, 0, 4},
                {3, 2, 5, 1, 4, 5, 1, 2}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), lastLocationOfMinimum = Features.LastLocationOfMinimum(arr))
            {
                var result = lastLocationOfMinimum.GetData2D<double>();
                Assert.AreEqual(0.875, result[0, 0], Delta);
                Assert.AreEqual(0.875, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestLength()
        {
            long[,] tss =
            {
                {0, 4, 3, 5, 5, 1},
                {0, 4, 3, 2, 5, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), length = Features.Length(arr))
            {
                var result = length.GetData2D<int>();
                Assert.AreEqual(6, result[0, 0]);
                Assert.AreEqual(6, result[0, 1]);
            }
        }

        [Test]
        public void TestLinearTrend()
        {
            double[,] tss =
            {
                {0, 4, 3, 5, 5, 1},
                {2, 4, 1, 2, 5, 3}
            };
            using (var arr = KhivaArray.Create(tss))
            {
                var (pvalueArr, rvalueArr, interceptArr, slopeArr, stderrArr) = Features.LinearTrend(arr);
                using (pvalueArr)
                using (rvalueArr)
                using (interceptArr)
                using (slopeArr)
                using (stderrArr)
                {
                    var pvalue = pvalueArr.GetData2D<double>();
                    var rvalue = rvalueArr.GetData2D<double>();
                    var intercept = interceptArr.GetData2D<double>();
                    var slope = slopeArr.GetData2D<double>();
                    var stdErr = stderrArr.GetData2D<double>();

                    Assert.AreEqual(0.6260380997892747, pvalue[0, 0], Delta);
                    Assert.AreEqual(0.5272201945463578, pvalue[1, 0], Delta);

                    Assert.AreEqual(0.2548235957188128, rvalue[0, 0], Delta);
                    Assert.AreEqual(0.3268228676411533, rvalue[1, 0], Delta);

                    Assert.AreEqual(2.2857142857142856, intercept[0, 0], Delta);
                    Assert.AreEqual(2.1904761904761907, intercept[1, 0], Delta);

                    Assert.AreEqual(0.2857142857142857, slope[0, 0], Delta);
                    Assert.AreEqual(0.2571428571428572, slope[1, 0], Delta);

                    Assert.AreEqual(0.5421047417431507, stdErr[0, 0], Delta);
                    Assert.AreEqual(0.37179469135129783, stdErr[1, 0], Delta);
                }
            }
        }

        [Test]
        public void TestLocalMaximals()
        {
            float[,] tss =
                {{0.0F, 4.0F, 3.0F, 5.0F, 4.0F, 1.0F, 0.0F, 4.0F}, {0.0F, 4.0F, 3.0F, 5.0F, 4.0F, 1.0F, 0.0F, 4.0F}};
            using (KhivaArray arr = KhivaArray.Create(tss), localMaximals = Features.LocalMaximals(arr))
            {
                var result = localMaximals.GetData2D<float>();
                float[,] expected =
                {
                    {0.0F, 1.0F, 0.0F, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F}, {0.0F, 1.0F, 0.0F, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F}
                };
                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        public void TestLongestStrikeAboveMean()
        {
            double[,] tss =
            {
                {
                    20, 20, 20, 1, 1, 1, 20, 20, 20, 20, 1, 1, 1, 1,
                    1, 1, 1, 1, 20, 20
                },
                {
                    20, 20, 20, 1, 1, 1, 20, 20, 20, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 20, 20
                }
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                longestStrikeAboveMean = Features.LongestStrikeAboveMean(arr))
            {
                var result = longestStrikeAboveMean.GetData2D<double>();
                Assert.AreEqual(4, result[0, 0]);
                Assert.AreEqual(3, result[1, 0]);
            }
        }

        [Test]
        public void TestLongestStrikeBelowMean()
        {
            float[,] tss =
            {
                {
                    20, 20, 20, 1, 1, 1, 20, 20, 20, 20, 1, 1, 1, 1,
                    1, 1, 1, 1, 20, 20
                },
                {
                    20, 20, 20, 1, 1, 1, 20, 20, 20, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 20, 20
                }
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                longestStrikeBelowMean = Features.LongestStrikeBelowMean(arr))
            {
                var result = longestStrikeBelowMean.GetData2D<float>();
                Assert.AreEqual(8, result[0, 0]);
                Assert.AreEqual(9, result[1, 0]);
            }
        }

        [Test]
        public void TestMaxLangevinFixedPoint()
        {
            float[,] tss =
            {
                {0, 1, 2, 3, 4, 5},
                {0, 1, 2, 3, 4, 5}
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                maxLangevinFixedPoint = Features.MaxLangevinFixedPoint(arr, 7, 2))
            {
                var result = maxLangevinFixedPoint.GetData2D<float>();
                Assert.AreEqual(4.562970585, result[0, 0], 1e-4);
                Assert.AreEqual(4.562970585, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestMaximum()
        {
            long[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 1, 1, 5, 1, 20, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), maximum = Features.Maximum(arr))
            {
                var result = maximum.GetData2D<long>();
                Assert.AreEqual(50, result[0, 0]);
                Assert.AreEqual(30, result[1, 0]);
            }
        }

        [Test]
        public void TestMean()
        {
            float[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 1, 1, 5, 1, 20, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), mean = Features.Mean(arr))
            {
                var result = mean.GetData2D<float>();
                Assert.AreEqual(18.55, result[0, 0], 1e-4);
                Assert.AreEqual(12.7, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestMeanAbsoluteChange()
        {
            float[,] tss =
            {
                {0, 1, 2, 3, 4, 5},
                {8, 10, 12, 14, 16, 18}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), meanAbsoluteChange = Features.MeanAbsoluteChange(arr))
            {
                var result = meanAbsoluteChange.GetData2D<float>();
                const double r = 5.0 / 6.0;
                Assert.AreEqual(r, result[0, 0], Delta);
                Assert.AreEqual(r * 2, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestMeanChange()
        {
            float[,] tss =
            {
                {0, 1, 2, 3, 4, 5},
                {8, 10, 12, 14, 16, 18}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), meanChange = Features.MeanChange(arr))
            {
                var result = meanChange.GetData2D<float>();
                const double r = 5.0 / 6.0;
                Assert.AreEqual(r, result[0, 0], 1e-4);
                Assert.AreEqual(r * 2, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestMeanSecondDerivativeCentral()
        {
            double[,] tss =
            {
                {1, 3, 7, 4, 8},
                {2, 5, 1, 7, 4}
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                meanSecondDerivativeCentral = Features.MeanSecondDerivativeCentral(arr))
            {
                var result = meanSecondDerivativeCentral.GetData2D<double>();
                Assert.AreEqual(1.0 / 5.0, result[0, 0], Delta);
                Assert.AreEqual(-3.0 / 5.0, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestMedian()
        {
            float[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 1, 1, 5, 1, 20, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), median = Features.Median(arr))
            {
                var result = median.GetData2D<float>();
                Assert.AreEqual(20, result[0, 0], Delta);
                Assert.AreEqual(18.5, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestMinimum()
        {
            int[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 13, 15, 5, 16, 20, 20},
                {20, 20, 20, 2, 19, 4, 20, 20, 20, 4, 15, 6, 30, 7, 9, 18, 4, 10, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), minimum = Features.Minimum(arr))
            {
                var result = minimum.GetData2D<int>();
                Assert.AreEqual(1, result[0, 0], Delta);
                Assert.AreEqual(2, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestNumberCrossingM()
        {
            long[,] tss =
            {
                {1, 2, 1, 1, -3, -4, 7, 8, 9, 10, -2, 1, -3, 5, 6, 7, -10},
                {1, 2, 1, 1, -3, -4, 7, 8, 9, 10, -2, 1, -3, 5, 6, 7, -10}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), numberCrossingM = Features.NumberCrossingM(arr, 0))
            {
                var result = numberCrossingM.GetData2D<long>();
                Assert.AreEqual(7, result[0, 0], Delta);
                Assert.AreEqual(7, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestNumberCwtPeaks()
        {
            double[,] tss =
            {
                {1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), numberCwtPeaks = Features.NumberCwtPeaks(arr, 2))
            {
                var result = numberCwtPeaks.GetData2D<double>();
                Assert.AreEqual(2, result[0, 0]);
                Assert.AreEqual(2, result[1, 0]);
            }
        }

        [Test]
        public void TestNumberPeaks()
        {
            float[,] tss =
            {
                {3, 0, 0, 4, 0, 0, 13},
                {3, 0, 0, 4, 0, 0, 13}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), numberPeaks = Features.NumberPeaks(arr, 2))
            {
                var result = numberPeaks.GetData2D<float>();
                Assert.AreEqual(1, result[0, 0], 1e-4);
                Assert.AreEqual(1, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestPartialAutocorrelation()
        {
            const double numel = 3000.0F;
            const double step = 1 / (numel - 1);
            var tss = new double[2, (int) numel];
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < (int) numel; j++)
                {
                    tss[i, j] = j * step;
                }
            }

            int[] lags = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            using (KhivaArray arr = KhivaArray.Create(tss),
                lagsArr = KhivaArray.Create(lags),
                partialAutocorrelation = Features.PartialAutocorrelation(arr, lagsArr))
            {
                double[,] expected =
                {
                    {
                        1.0F, 0.9993331432342529F, -0.0006701064994559F, -0.0006701068487018F, -0.0008041285327636F,
                        -0.0005360860959627F, -0.0007371186511591F, -0.0004690756904893F, -0.0008041299879551F,
                        -0.0007371196406893F
                    },
                    {
                        1.0F, 0.9993331432342529F, -0.0006701064994559F, -0.0006701068487018F, -0.0008041285327636F,
                        -0.0005360860959627F, -0.0007371186511591F, -0.0004690756904893F, -0.0008041299879551F,
                        -0.0007371196406893F
                    }
                };
                var result = partialAutocorrelation.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], 1e-3);
                    }
                }
            }
        }

        [Test]
        public void TestPercentageOfReoccurringDatapointsToAllDatapoints()
        {
            float[,] tss = {{3, 0, 0, 4, 0, 0, 13}, {3, 0, 0, 4, 0, 0, 13}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                percentage = Features.PercentageOfReoccurringDatapointsToAllDatapoints(arr))
            {
                var result = percentage.GetData2D<float>();
                Assert.AreEqual(0.25, result[0, 0], 1e-4);
                Assert.AreEqual(0.25, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestPercentageOfReoccurringValuesToAllValues()
        {
            float[,] tss = {{1, 1, 2, 3, 4, 4, 5, 6}, {1, 2, 2, 3, 4, 5, 6, 7}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                percentage = Features.PercentageOfReoccurringValuesToAllValues(arr))
            {
                var result = percentage.GetData2D<float>();
                Assert.AreEqual(4.0 / 8.0, result[0, 0], 1e-4);
                Assert.AreEqual(2.0 / 8.0, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestQuantile()
        {
            float[,] tss =
            {
                {0, 0, 0, 0, 3, 4, 13},
                {0, 0, 0, 0, 3, 4, 13}
            };
            float[] q = {0.6F};
            using (KhivaArray arr = KhivaArray.Create(tss),
                qArr = KhivaArray.Create(q),
                quantile = Features.Quantile(arr, qArr))
            {
                var result = quantile.GetData2D<float>();
                Assert.AreEqual(1.79999999, result[0, 0], 1e-4);
                Assert.AreEqual(1.79999999, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestRangeCount()
        {
            int[,] tss =
            {
                {3, 0, 0, 4, 0, 0, 13},
                {3, 0, 5, 4, 0, 0, 13}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), rangeCount = Features.RangeCount(arr, 2, 12))
            {
                var result = rangeCount.GetData2D<int>();
                Assert.AreEqual(2, result[0, 0]);
                Assert.AreEqual(3, result[1, 0]);
            }
        }

        [Test]
        public void TestRatioBeyondRSigma()
        {
            double[,] tss =
            {
                {3, 0, 0, 4, 0, 0, 13},
                {3, 0, 0, 4, 0, 0, 13}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), ratioBeyondRSigma = Features.RatioBeyondRSigma(arr, 0.5F))
            {
                var result = ratioBeyondRSigma.GetData2D<double>();
                Assert.AreEqual(0.7142857142857143, result[0, 0], 1e-4);
                Assert.AreEqual(0.7142857142857143, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestRatioValueNumberToTimeSeriesLength()
        {
            double[,] tss =
            {
                {3, 0, 0, 4, 0, 0, 13},
                {3, 5, 0, 4, 6, 0, 13}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), ratio = Features.RatioValueNumberToTimeSeriesLength(arr))
            {
                var result = ratio.GetData2D<double>();
                Assert.AreEqual(4.0 / 7.0, result[0, 0], 1e-4);
                Assert.AreEqual(6.0 / 7.0, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestSampleEntropy()
        {
            float[,] tss =
            {
                {3, 0, 0, 4, 0, 0, 13},
                {3, 0, 0, 4, 0, 0, 13}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), sampleEntropy = Features.SampleEntropy(arr))
            {
                var result = sampleEntropy.GetData2D<float>();
                Assert.AreEqual(1.2527629, result[0, 0], 1e-4);
                Assert.AreEqual(1.2527629, result[0, 1], 1e-4);
            }
        }

        [Test]
        public void TestSkewness()
        {
            float[,] tss =
            {
                {3, 0, 0, 4, 0, 0, 13},
                {3, 0, 0, 4, 0, 0, 13}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), skewness = Features.Skewness(arr))
            {
                var result = skewness.GetData2D<float>();
                Assert.AreEqual(2.038404735373753, result[0, 0], 1e-4);
                Assert.AreEqual(2.038404735373753, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestSpktWelchDensity()
        {
            float[,] tss =
            {
                {0, 1, 1, 3, 4, 5, 6, 7, 8, 9},
                {0, 1, 1, 3, 4, 5, 6, 7, 8, 9}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), spktWelchDensity = Features.SpktWelchDensity(arr, 0))
            {
                var result = spktWelchDensity.GetData2D<float>();
                Assert.AreEqual(1.6666667, result[0, 0], 1e-5);
                Assert.AreEqual(1.6666667, result[1, 0], 1e-5);
            }
        }

        [Test]
        public void TestStandardDeviation()
        {
            double[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 1, 1, 5, 1, 20, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), standardDeviation = Features.StandardDeviation(arr))
            {
                var result = standardDeviation.GetData2D<double>();
                Assert.AreEqual(12.363150892875165, result[0, 0], 1e-4);
                Assert.AreEqual(9.51367436903324, result[1, 0], 1e-4);
            }
        }

        [Test]
        public void TestSumOfReoccurringDatapoints()
        {
            short[,] tss =
            {
                {3, 3, 0, 4, 0, 13, 13},
                {3, 3, 0, 4, 0, 13, 13}
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                sumOfReoccurringDatapoints = Features.SumOfReoccurringDatapoints(arr))
            {
                var result = sumOfReoccurringDatapoints.GetData2D<short>();
                Assert.AreEqual(32, result[0, 0]);
                Assert.AreEqual(32, result[1, 0]);
            }
        }

        [Test]
        public void TestSumOfReoccurringValues()
        {
            short[,] tss =
            {
                {4, 4, 6, 6, 7},
                {4, 7, 7, 8, 8}
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                sumOfReoccurringValues = Features.SumOfReoccurringValues(arr))
            {
                var result = sumOfReoccurringValues.GetData2D<short>();
                Assert.AreEqual(10, result[0, 0]);
                Assert.AreEqual(15, result[1, 0]);
            }
        }

        [Test]
        public void TestSumValues()
        {
            float[,] tss =
            {
                {1, 2, 3, 4.1F},
                {-1.2F, -2, -3, -4}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), sumValues = Features.SumValues(arr))
            {
                var result = sumValues.GetData2D<float>();
                Assert.AreEqual(10.1, result[0, 0], Delta);
                Assert.AreEqual(-10.2, result[1, 0], Delta);
            }
        }

        [Test]
        public void TestSymmetryLooking()
        {
            float[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 1, 1, 5, 1, 20, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), symmetryLooking = Features.SymmetryLooking(arr, 0.1F))
            {
                var result = symmetryLooking.GetData2D<bool>();
                Assert.AreEqual(true, result[0, 0]);
                Assert.AreEqual(false, result[1, 0]);
            }
        }

        [Test]
        public void TestTimeReversalAsymmetryStatistic()
        {
            float[,] tss =
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                timeReversalAsymmetryStatistic = Features.TimeReversalAsymmetryStatistic(arr, 2))
            {
                var result = timeReversalAsymmetryStatistic.GetData2D<float>();
                Assert.AreEqual(1052, result[0, 0]);
                Assert.AreEqual(-150.625, result[1, 0]);
            }
        }

        [Test]
        public void TestValueCount()
        {
            float[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 1, 1, 5, 1, 20, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), valueCount = Features.ValueCount(arr, 20))
            {
                var result = valueCount.GetData2D<uint>();
                Assert.AreEqual(9, result[0, 0]);
                Assert.AreEqual(8, result[1, 0]);
            }
        }

        [Test]
        public void TestVariance()
        {
            float[,] tss =
            {
                {1, 1, -1, -1},
                {1, 2, -2, -1}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), variance = Features.Variance(arr))
            {
                var result = variance.GetData2D<float>();
                Assert.AreEqual(1, result[0, 0]);
                Assert.AreEqual(2.5, result[1, 0]);
            }
        }

        [Test]
        public void TestVarianceLargerThanStandardDeviation()
        {
            float[,] tss =
            {
                {20, 20, 20, 18, 25, 19, 20, 20, 20, 20, 40, 30, 1, 50, 1, 1, 5, 1, 20, 20},
                {20, 20, 20, 2, 19, 1, 20, 20, 20, 1, 15, 1, 30, 1, 1, 18, 4, 1, 20, 20}
            };
            using (KhivaArray arr = KhivaArray.Create(tss),
                varianceLargerThanStandardDeviation = Features.VarianceLargerThanStandardDeviation(arr))
            {
                var result = varianceLargerThanStandardDeviation.GetData2D<bool>();
                Assert.AreEqual(true, result[0, 0]);
                Assert.AreEqual(true, result[1, 0]);
            }
        }
    }
}