// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Dimensionality")]
    public class DimensionalityTests
    {
        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestPaa()
        {
            double[,] tss =
            {
                {0.0, 0.1, -0.1, 5.0, 6.0, 7.0, 8.1, 9.0, 9.0, 9.0},
                {0.0, 0.1, -0.1, 5.0, 6.0, 7.0, 8.1, 9.0, 9.0, 9.0}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), paa = Dimensionality.Paa(arr, 5))
            {
                double[,] expected =
                {
                    {0.05, 2.45, 6.5, 8.55, 9.0},
                    {0.05, 2.45, 6.5, 8.55, 9.0}
                };
                var paaArr = paa.GetData2D<double>();
                Assert.AreEqual(expected, paaArr);
            }
        }

        [Test]
        public void TestPip()
        {
            float[,] tss =
            {
                {0.0F, 1.0F, 2.0F, 3.0F, 4.0F, 5.0F, 6.0F, 7.0F, 8.0F, 9.0F},
                {0.0F, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), pip = Dimensionality.Pip(arr, 6))
            {
                float[,] expected =
                {
                    {0.0F, 2.0F, 3.0F, 6.0F, 7.0F, 9.0F},
                    {0.0F, -0.1F, 5.0F, 8.1F, 9.0F, 9.0F}
                };
                var pipArr = pip.GetData2D<float>();
                Assert.AreEqual(expected, pipArr);
            }
        }

        [Test]
        public void TestPlaBottomUp()
        {
            float[,] tss =
            {
                {0.0F, 1.0F, 2.0F, 3.0F, 4.0F, 5.0F, 6.0F, 7.0F, 8.0F, 9.0F},
                {0.0F, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), pla = Dimensionality.PlaBottomUp(arr, 1))
            {
                float[,] expected =
                {
                    {0, 1, 2, 3, 4, 7, 8, 9},
                    {0, 0.1F, -0.1F, 5, 6, 9, 9, 9}
                };
                var plaArr = pla.GetData2D<float>();
                Assert.AreEqual(expected, plaArr);
            }
        }

        [Test]
        public void TestPlaSlidingWindow()
        {
            float[,] tss =
            {
                {0.0F, 1.0F, 2.0F, 3.0F, 4.0F, 5.0F, 6.0F, 7.0F, 8.0F, 9.0F},
                {0.0F, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), pla = Dimensionality.PlaSlidingWindow(arr, 1))
            {
                float[,] expected =
                {
                    {0, 2, 3, 7, 8, 9},
                    {0, -0.1F, 5, 9, 9, 9}
                };
                var plaArr = pla.GetData2D<float>();
                Assert.AreEqual(expected, plaArr);
            }
        }

        [Test]
        public void TestRamerDouglasPeucker()
        {
            float[,] tss =
            {
                {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
                {0, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), ramer = Dimensionality.RamerDouglasPeucker(arr, 1.0))
            {
                float[,] expected =
                {
                    {0, 2, 3, 6, 9},
                    {0, -0.1F, 5.0F, 8.1F, 9.0F}
                };
                var ramerArr = ramer.GetData2D<float>();
                Assert.AreEqual(expected, ramerArr);
            }
        }

        [Test]
        public void TestSax()
        {
            float[,] tss =
            {
                {0.0F, 0.1F, -0.1F, 5.0F, 6.0F},
                {7.0F, 8.1F, 9.0F, 9.0F, 9.0F}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), sax = Dimensionality.SAX(arr, 3))
            {
                float[,] expected =
                {
                    {0.0F, 0.1F, -0.1F, 5.0F, 6.0F},
                    {0.0F, 1.0F, 2.0F, 2.0F, 2.0F}
                };
                var saxArr = sax.GetData2D<float>();
                Assert.AreEqual(expected, saxArr);
            }
        }

        [Test]
        public void TestVisvalingam()
        {
            float[,] tss =
            {
                {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
                {0, 0.1F, -0.1F, 5.0F, 6.0F, 7.0F, 8.1F, 9.0F, 9.0F, 9.0F}
            };
            using (KhivaArray arr = KhivaArray.Create(tss), vis = Dimensionality.Visvalingam(arr, 5))
            {
                float[,] expected =
                {
                    {0, 2, 3, 7, 9},
                    {0, -0.1F, 5.0F, 9.0F, 9.0F}
                };
                var visArr = vis.GetData2D<float>();
                Assert.AreEqual(expected, visArr);
            }
        }
    }
}