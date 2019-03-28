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

namespace khiva.dimensionality.tests
{
    [TestFixture]
    public class DimensionalityTests
    {

        [SetUp] public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestPAA()
        {
            double[,] tss = { { 0.0, 0.1, -0.1, 5.0, 6.0, 7.0, 8.1, 9.0, 9.0, 9.0 },
                              { 0.0, 0.1, -0.1, 5.0, 6.0, 7.0, 8.1, 9.0, 9.0, 9.0 } };
            using (array.Array arr = new array.Array(tss), paa = Dimensionality.PAA(arr, 5))
            {
                double[,] expected = { { 0.05, 2.45, 6.5, 8.55, 9.0 },
                                   { 0.05, 2.45, 6.5, 8.55, 9.0 } };
                double[,] paaArr = paa.GetData2D<double>();
                Assert.AreEqual(expected, paaArr);
            }       
        }

        [Test]
        public void TestPIP()
        {
            float[,] tss = { { 0.0F, 1.0F, 2.0F, 3.0F, 4.0F, 5.0F, 6.0F, 7.0F, 8.0F, 9.0F },
                              { 0.0F, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F } };
            using (array.Array arr = new array.Array(tss), pip = Dimensionality.PIP(arr, 6))
            {
                float[,] expected = { { 0.0F, 2.0F, 3.0F, 6.0F, 7.0F, 9.0F },
                                   { 0.0F, -0.1F, 5.0F, 8.1F, 9.0F, 9.0F } };
                float[,] pipArr = pip.GetData2D<float>();
                Assert.AreEqual(expected, pipArr);
            }
        }

        [Test]
        public void TestPLABottomUp()
        {
            float[,] tss = { { 0.0F, 1.0F, 2.0F, 3.0F, 4.0F, 5.0F, 6.0F, 7.0F, 8.0F, 9.0F },
                              { 0.0F, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F } };
            using (array.Array arr = new array.Array(tss), pla = Dimensionality.PLABottomUp(arr, 1))
            {
                float[,] expected = { { 0, 1, 2, 3, 4, 7, 8, 9 },
                                   { 0, 0.1F, -0.1F, 5, 6, 9, 9, 9 } };
                float[,] plaArr = pla.GetData2D<float>();
                Assert.AreEqual(expected, plaArr);
            }
        }

        [Test]
        public void TestPLASlidingWindow()
        {
            float[,] tss = { { 0.0F, 1.0F, 2.0F, 3.0F, 4.0F, 5.0F, 6.0F, 7.0F, 8.0F, 9.0F },
                              { 0.0F, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F } };
            using (array.Array arr = new array.Array(tss), pla = Dimensionality.PLASlidingWindow(arr, 1))
            {
                float[,] expected = { { 0, 2, 3, 7, 8, 9 },
                                   { 0, -0.1F, 5, 9, 9, 9 } };
                float[,] plaArr = pla.GetData2D<float>();
                Assert.AreEqual(expected, plaArr);
            }
        }

        [Test]
        public void TestRamerDouglasPeucker()
        {
            float[,] tss = { { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                              { 0, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F } };
            using (array.Array arr = new array.Array(tss), ramer = Dimensionality.RamerDouglasPeucker(arr, 1.0))
            {
                float[,] expected = { { 0, 2, 3, 6, 9 },
                                   { 0, -0.1F, 5.0F, 8.1F, 9.0F } };
                float[,] ramerArr = ramer.GetData2D<float>();
                Assert.AreEqual(expected, ramerArr);
            }
        }

        [Test]
        public void TestSAX()
        {
            float[,] tss = { { 0.0F, 0.1F, -0.1F, 5.0F, 6.0F },
                              { 7.0F, 8.1F, 9.0F, 9.0F, 9.0F } };
            using (array.Array arr = new array.Array(tss), sax = Dimensionality.SAX(arr, 3))
            {
                float[,] expected = { { 0.0F, 0.1F, -0.1F, 5.0F, 6.0F },
                                   { 0.0F, 1.0F, 2.0F, 2.0F, 2.0F } };
                float[,] saxArr = sax.GetData2D<float>();
                Assert.AreEqual(expected, saxArr);
            }
        }

        [Test]
        public void TestVisvalingam()
        {
            float[,] tss = { { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                              { 0, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F } };
            using (array.Array arr = new array.Array(tss), vis = Dimensionality.Visvalingam(arr, 5))
            {
                float[,] expected = { { 0, 2, 3, 7, 9 },
                                   { 0, -0.1F, 5.0F, 9.0F, 9.0F } };
                float[,] visArr = vis.GetData2D<float>();
                Assert.AreEqual(expected, visArr);
            }
        }
    }
}
