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
using khiva.array;

namespace khiva.matrix.tests
{
    [TestFixture, Category("Matrix")]
    public class MatrixTests
    {

        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestStompSelfJoin()
        {
            float[,] tss = { { 10, 10, 11, 11, 10, 11, 10, 10, 11, 11, 10, 11, 10, 10 },
                             { 11, 10, 10, 11, 10, 11, 11, 10, 11, 11, 10, 10, 11, 10 } };
            using (KhivaArray arr = new KhivaArray(tss))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.StompSelfJoin(arr, 3);
                var p = tuple.Item1;
                var i = tuple.Item2;
                using (p)
                using (i)
                {
                    float[] expected_index = { 6, 7, 8, 9, 10, 11, 0, 1, 2, 3, 4, 5, 9, 10, 11, 6, 7, 8, 3, 4, 5, 0, 1, 2 };
                    float[,] pVal = p.GetData2D<float>();
                    uint[,] iVal = i.GetData2D<uint>();
                    for (int index = 0; index < 6; index++)
                    {
                        Assert.AreEqual(0.0, pVal[0, index], 1e-2);
                        Assert.AreEqual(expected_index[index], iVal[0, index]);
                    }
                }
            }
        }

        [Test]
        public void TestStomp()
        {
            double[,] tss = { { 10, 11, 10, 11 }, 
                              { 10, 11, 10, 11 } };
            double[,] tss2 = { { 10, 11, 10, 11, 10, 11, 10, 11 }, 
                               { 10, 11, 10, 11, 10, 11, 10, 11 } };
            using(KhivaArray arr = new KhivaArray(tss), arr2 = new KhivaArray(tss2))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.Stomp(arr, arr2, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    double[,,] p = pArr.GetData3D<double>();
                    uint[,,] index = iArr.GetData3D<uint>();
                    uint[,,] expected_index = { { { 0, 1 }, { 0, 1 }, { 0, 1 }, { 0, 1 }, { 0, 1 }, { 0, 1 } }, { { 0, 1 }, { 0, 1 }, { 0, 1 }, { 0, 1 }, { 0, 1 }, { 0, 1 } } };
                    for(int i = 0; i < p.GetLength(0); i++)
                    {
                        for (int j = 0; j < p.GetLength(1); j++)
                        {
                            for (int k = 0; k < p.GetLength(2); k++)
                            {
                                Assert.AreEqual(0, p[i, j, k], 1e-2);
                                Assert.AreEqual(expected_index[i,j,k], index[i, j, k]);
                            }
                        }
                    }
                }
            }
        }


        [Test]
        public void TestFindbestNDiscords()
        {
            double[] tss = { 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11 };
            double[] tss2 = { 9, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 9 };
            using (KhivaArray arr = new KhivaArray(tss), arr2 = new KhivaArray(tss2))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.Stomp(arr, arr2, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNDiscords(pArr, iArr, 3, 2);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[] subsequence = subsequenceArr.GetData1D<uint>();
                        Assert.AreEqual(0, subsequence[0]);
                        Assert.AreEqual(10, subsequence[1]);
                    }
                }
            }
        }

        [Test]
        public void TestFindbestNDiscordsMultipleProfiles()
        {
            double[,] tss = { { 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11 }, { 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11 } };
            double[,] tss2 = { { 9, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 9 },
                              { 9, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 10.2, 10.1, 9 }};
            using (KhivaArray arr = new KhivaArray(tss), arr2 = new KhivaArray(tss2))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.Stomp(arr, arr2, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNDiscords(pArr, iArr, 3, 2);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[,,] subsequence = subsequenceArr.GetData3D<uint>();
                        Assert.AreEqual(new uint[,,] { { { 0, 10 }, { 0, 10 } }, { { 0, 10 }, { 0, 10 } } }, subsequence);
                    }
                }
            }
        }

        [Test]
        public void TestFindbestNDiscordsMirror()
        {
            float[] tss = { 10, 11, 10, 10, 11, 10 };
            using (KhivaArray arr = new KhivaArray(tss))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.StompSelfJoin(arr, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNDiscords(pArr, iArr, 3, 1, true);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[] indices = indicesArr.GetData1D<uint>();
                        uint[] subsequence = subsequenceArr.GetData1D<uint>();
                        for ( int i = 0; i < indices.Length; i++)
                        {
                            Assert.AreEqual(3, indices[i]);
                            Assert.AreEqual(1, subsequence[i]);
                        }
                    }
                }
            }
        }

        [Test]
        public void TestFindbestNDiscordsConsecutive()
        {
            float[] tss = { 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 11, 10, 9.999F, 9.998F };
            using (KhivaArray arr = new KhivaArray(tss))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.StompSelfJoin(arr, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNDiscords(pArr, iArr, 3, 2, true);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[] subsequence = subsequenceArr.GetData1D<uint>();
                        Assert.AreEqual(12, subsequence[0]);
                        string travis = Environment.GetEnvironmentVariable("TRAVIS");
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
        public void TestFindbestNMotifs()
        {
            float[] tss = { 10, 10, 10, 10, 10, 10, 9, 10, 10, 10, 10, 10, 11, 10, 9 };
            float[] tss2 = { 10, 11, 10, 9 };
            using (KhivaArray arr = new KhivaArray(tss), arr2 = new KhivaArray(tss2))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.Stomp(arr, arr2, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNMotifs(pArr, iArr, 3, 1);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[] indices = indicesArr.GetData1D<uint>();
                        uint[] subsequence = subsequenceArr.GetData1D<uint>();
                        for ( int i = 0; i < indices.Length; i++)
                        {
                            Assert.AreEqual(12, indices[i]);
                            Assert.AreEqual(1, subsequence[i]);
                        }
                    }
                }
            }
        }

        [Test]
        public void TestFindbestNMotifsMultipleProfiles()
        {
            float[,] tss = { { 10, 10, 10, 10, 10, 10, 9, 10, 10, 10, 10, 10, 11, 10, 9 },
                            { 10, 10, 10, 10, 10, 10, 9, 10, 10, 10, 10, 10, 11, 10, 9 } };
            float[,] tss2 = { { 10, 11, 10, 9 }, { 10, 11, 10, 9 } };
            using (KhivaArray arr = new KhivaArray(tss), arr2 = new KhivaArray(tss2))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.Stomp(arr, arr2, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNMotifs(pArr, iArr, 3, 1);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[,,] indices = indicesArr.GetData3D<uint>();
                        uint[,,] subsequence = subsequenceArr.GetData3D<uint>();
                        Assert.AreEqual(new uint[,,] { { { 12, 12 } },{ { 12 ,  12 } } }, indices);
                        Assert.AreEqual(new uint[,,] { { { 1, 1 } }, { { 1, 1 } } }, subsequence);
                    }
                }
            }
        }

        [Test]
        public void TestFindbestNMotifsMirror()
        {
            float[] tss = { 10.1F, 11, 10.2F, 10.15F, 10.775F, 10.1F, 11, 10.2F };
            using (KhivaArray arr = new KhivaArray(tss))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.StompSelfJoin(arr, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNMotifs(pArr, iArr, 3, 2, true);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[,] indices = indicesArr.GetData2D<uint>();
                        uint[,] subsequence = subsequenceArr.GetData2D<uint>();
                        Assert.AreEqual(new uint[,] { {0, 0} }, indices);
                        Assert.AreEqual(new uint[,] { { 5, 3 } }, subsequence);
                    }
                }
            }
        }

        [Test]
        public void TestFindbestNMotifsConsecutive()
        {
            float[] tss = { 10.1F, 11, 10.1F, 10.15F, 10.075F, 10.1F, 11, 10.1F, 10.15F };
            using (KhivaArray arr = new KhivaArray(tss))
            {
                Tuple<KhivaArray, KhivaArray> tuple = Matrix.StompSelfJoin(arr, 3);
                var pArr = tuple.Item1;
                var iArr = tuple.Item2;
                using (pArr)
                using (iArr)
                {
                    Tuple<KhivaArray, KhivaArray, KhivaArray> tuple2 = Matrix.FindbestNMotifs(pArr, iArr, 3, 2);
                    var distancesArr = tuple2.Item1;
                    var indicesArr = tuple2.Item2;
                    var subsequenceArr = tuple2.Item3;
                    using (distancesArr)
                    using (indicesArr)
                    using (subsequenceArr)
                    {
                        uint[,] indices = indicesArr.GetData2D<uint>();
                        uint[,] subsequence = subsequenceArr.GetData2D<uint>();
                        Assert.AreEqual(6, indices[0,1]);
                        Assert.AreEqual(3, subsequence[0,1]);
                    }
                }
            }
        }

    }
}
