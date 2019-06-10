// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Matrix")]
    public class MatrixTests
    {
        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestStompSelfJoin()
        {
            float[,] tss =
            {
                {10, 10, 11, 11, 10, 11, 10, 10, 11, 11, 10, 11, 10, 10},
                {11, 10, 10, 11, 10, 11, 11, 10, 11, 11, 10, 10, 11, 10}
            };
            using (var arr = KhivaArray.Create(tss))
            {
                var (p, i) = Matrix.StompSelfJoin(arr, 3);
                using (p)
                using (i)
                {
                    float[] expectedIndex =
                        {6, 7, 8, 9, 10, 11, 0, 1, 2, 3, 4, 5, 9, 10, 11, 6, 7, 8, 3, 4, 5, 0, 1, 2};
                    var pVal = p.GetData2D<float>();
                    var iVal = i.GetData2D<uint>();
                    for (var index = 0; index < 6; index++)
                    {
                        Assert.AreEqual(0.0, pVal[0, index], 1e-2);
                        Assert.AreEqual(expectedIndex[index], iVal[0, index]);
                    }
                }
            }
        }

        [Test]
        public void TestStomp()
        {
            double[,] tss =
            {
                {10, 11, 10, 11},
                {10, 11, 10, 11}
            };
            double[,] tss2 =
            {
                {10, 11, 10, 11, 10, 11, 10, 11},
                {10, 11, 10, 11, 10, 11, 10, 11}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2))
            {
                var (pArr, iArr) = Matrix.Stomp(arr, arr2, 3);
                using (pArr)
                using (iArr)
                {
                    var p = pArr.GetData3D<double>();
                    var index = iArr.GetData3D<uint>();
                    uint[,,] expectedIndex =
                    {
                        {
                        {0, 1, 0, 1, 0, 1},
                        {0, 1, 0, 1, 0, 1}
                        },
                        {
                        {0, 1, 0, 1, 0, 1},
                        {0, 1, 0, 1, 0, 1}
                        }
                    };

                    for (var i = 0; i < p.GetLength(0); i++)
                    {
                        for (var j = 0; j < p.GetLength(1); j++)
                        {
                            for (var k = 0; k < p.GetLength(2); k++)
                            {
                                Assert.AreEqual(0, p[i, j, k], 1e-2);
                                Assert.AreEqual(expectedIndex[i, j, k], index[i, j, k]);
                            }
                        }
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNDiscords()
        {
            double[] tss = {11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11};
            double[] tss2 = {9, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 9};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2))
            {
                var (pArr, iArr) = Matrix.Stomp(arr, arr2, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNDiscords(pArr, iArr, 3, 2);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var subsequence = subsequenceArr.GetData1D<uint>();
                        Assert.AreEqual(0, subsequence[0]);
                        Assert.AreEqual(10, subsequence[1]);
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNDiscordsMultipleProfiles()
        {
            double[,] tss =
            {
                {11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11},
                {11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11}
            };
            double[,] tss2 =
            {
                {9, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 9},
                {9, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 9}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2))
            {
                var (pArr, iArr) = Matrix.Stomp(arr, arr2, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNDiscords(pArr, iArr, 3, 2);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var subsequence = subsequenceArr.GetData3D<uint>();
                        Assert.AreEqual(new uint[,,] {{{0, 10}, {0, 10}}, {{0, 10}, {0, 10}}}, subsequence);
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNDiscordsMirror()
        {
            float[] tss = {10, 11, 10, 10, 11, 10};
            using (var arr = KhivaArray.Create(tss))
            {
                var (pArr, iArr) = Matrix.StompSelfJoin(arr, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNDiscords(pArr, iArr, 3, 1, true);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var indices = indicesArr.GetData1D<uint>();
                        var subsequence = subsequenceArr.GetData1D<uint>();
                        for (var i = 0; i < indices.Length; i++)
                        {
                            Assert.AreEqual(3, indices[i]);
                            Assert.AreEqual(1, subsequence[i]);
                        }
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNDiscordsConsecutive()
        {
            float[] tss = {10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 9.999F, 9.998F};
            using (var arr = KhivaArray.Create(tss))
            {
                var (pArr, iArr) = Matrix.StompSelfJoin(arr, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNDiscords(pArr, iArr, 3, 2, true);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var subsequence = subsequenceArr.GetData1D<uint>();
                        Assert.AreEqual(12, subsequence[0]);
                        var travis = Environment.GetEnvironmentVariable("TRAVIS");
                        if (travis != null)
                        {
                            Assert.AreEqual(11, subsequence[1]);
                        }
                        else
                        {
                            Assert.AreNotEqual(11, subsequence[1]);
                        }
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNMotifs()
        {
            float[] tss = {10, 10, 10, 10, 10, 10, 9, 10, 10, 10, 10, 10, 11, 10, 9};
            float[] tss2 = {10, 11, 10, 9};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2))
            {
                var (pArr, iArr) = Matrix.Stomp(arr, arr2, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNMotifs(pArr, iArr, 3, 1);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var indices = indicesArr.GetData1D<uint>();
                        var subsequence = subsequenceArr.GetData1D<uint>();
                        for (var i = 0; i < indices.Length; i++)
                        {
                            Assert.AreEqual(12, indices[i]);
                            Assert.AreEqual(1, subsequence[i]);
                        }
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNMotifsMultipleProfiles()
        {
            float[,] tss =
            {
                {10, 10, 10, 10, 10, 10, 9, 10, 10, 10, 10, 10, 11, 10, 9},
                {10, 10, 10, 10, 10, 10, 9, 10, 10, 10, 10, 10, 11, 10, 9}
            };
            float[,] tss2 = {{10, 11, 10, 9}, {10, 11, 10, 9}};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2))
            {
                var (pArr, iArr) = Matrix.Stomp(arr, arr2, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNMotifs(pArr, iArr, 3, 1);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var indices = indicesArr.GetData3D<uint>();
                        var subsequence = subsequenceArr.GetData3D<uint>();
                        Assert.AreEqual(new uint[,,] {{{12}, {12}}, {{12}, {12}}}, indices);
                        Assert.AreEqual(new uint[,,] {{{1}, {1}}, {{1}, {1}}}, subsequence);
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNMotifsMirror()
        {
            float[] tss = {10.1F, 11, 10.2F, 10.15F, 10.775F, 10.1F, 11, 10.2F};
            using (var arr = KhivaArray.Create(tss))
            {
                var (pArr, iArr) = Matrix.StompSelfJoin(arr, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNMotifs(pArr, iArr, 3, 2, true);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var indices = indicesArr.GetData2D<uint>();
                        var subsequence = subsequenceArr.GetData2D<uint>();
                        Assert.AreEqual(new uint[,] {{0, 0}}, indices);
                        Assert.AreEqual(new uint[,] {{5, 3}}, subsequence);
                    }
                }
            }
        }

        [Test]
        public void TestFindBestNMotifsConsecutive()
        {
            float[] tss = {10.1F, 11, 10.1F, 10.15F, 10.075F, 10.1F, 11, 10.1F, 10.15F};
            using (var arr = KhivaArray.Create(tss))
            {
                var (pArr, iArr) = Matrix.StompSelfJoin(arr, 3);
                using (pArr)
                using (iArr)
                {
                    var (distancesArr, indicesArr, subsequenceArr) = Matrix.FindBestNMotifs(pArr, iArr, 3, 2);
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        var indices = indicesArr.GetData2D<uint>();
                        var subsequence = subsequenceArr.GetData2D<uint>();
                        Assert.AreEqual(6, indices[0, 1]);
                        Assert.AreEqual(3, subsequence[0, 1]);
                    }
                }
            }
        } 
    }
}