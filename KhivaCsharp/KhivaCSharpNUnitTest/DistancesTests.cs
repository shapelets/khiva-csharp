﻿// Copyright (c) 2019 Shapelets.io
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

namespace khiva.distances.tests
{
    [TestFixture]
    public class DistancesTests
    {
        [SetUp]
        public void Init()
        {
            library.Library.SetKhivaBackend(library.Library.Backend.KHIVA_BACKEND_CPU);
        }

        [Test]
        public void TestDTW()
        {
            int[,] tss = { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 4, 4, 4, 4, 4 }, { 5, 5, 5, 5, 5 } };
            int[,] expected = { { 0, 0, 0, 0, 0 }, { 5, 0, 0, 0, 0 }, { 10, 5, 0, 0, 0 }, { 15, 10, 5, 0, 0 }, { 20, 15, 10, 5, 0 } };
            array.Array arr = new array.Array(tss);
            array.Array DTWResult = Distances.DTW(arr);
            int[,] result = DTWResult.GetData2D<int>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestEuclidean()
        {
            int[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };
            float[,] expected = { { 0, 0, 0 }, { 8, 0, 0 }, {16, 8, 0 } };
            array.Array arr = new array.Array(tss);
            array.Array euclideanResult = Distances.Euclidean(arr);
            float[,] result = euclideanResult.GetData2D<float>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestHamming()
        {
            int[,] tss = { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 4, 4, 4, 4, 4 }, { 5, 5, 5, 5, 5 } };
            int[,] expected = { { 0, 0, 0, 0, 0 }, { 5, 0, 0, 0, 0 }, { 5, 5, 0, 0, 0 }, { 5, 5, 5, 0, 0 }, { 5, 5, 5, 5, 0 } };
            array.Array arr = new array.Array(tss);
            array.Array HammingResult = Distances.Hamming(arr);
            int[,] result = HammingResult.GetData2D<int>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestManhattan()
        {
            int[,] tss = { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 4, 4, 4, 4, 4 }, { 5, 5, 5, 5, 5 } };
            int[,] expected = { { 0, 0, 0, 0, 0 }, { 5, 0, 0, 0, 0 }, { 10, 5, 0, 0, 0 }, { 15, 10, 5, 0, 0 }, { 20, 15, 10, 5, 0 } };
            array.Array arr = new array.Array(tss);
            array.Array ManhattanResult = Distances.Manhattan(arr);
            int[,] result = ManhattanResult.GetData2D<int>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestSBD()
        {
            float[,] tss = { { 1, 2, 3, 4, 5 }, { 1, 1, 0, 1, 1 }, { 10, 12, 0, 0, 1 } };
            float[,] expected = { { 0, 0, 0 }, { 0.505025F, 0, 0 }, { 0.458583F, 0.564093F, 0 } };
            array.Array arr = new array.Array(tss);
            array.Array SBDResult = Distances.SBD(arr);
            float[,] result = SBDResult.GetData2D<float>();
            for (int i = 0; i<expected.GetLength(0); i++)
            {
                for (int j = 0; j<expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i,j], result[i,j], 1e-6);
                }
            }
        }

        [Test]
        public void TestSquaredEuclidean()
        {
            int[,] tss = { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };
            int[,] expected = { { 0, 0, 0 }, { 64, 0, 0 }, { 256, 64, 0 } };
            array.Array arr = new array.Array(tss);
            array.Array SquaredEuclideanResult = Distances.SquaredEuclidean(arr);
            int[,] result = SquaredEuclideanResult.GetData2D<int>();
            Assert.AreEqual(expected, result);
        }

    }
}
