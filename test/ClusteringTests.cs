// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Clustering")]
    public class ClusteringTests
    {
        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestKMeans()
        {
            var tss = new[,]
            {
                {0.0, 1.0, 2.0, 3.0},
                {6.0, 7.0, 8.0, 9.0},
                {2.0, -2.0, 4.0, -4.0},
                {8.0, 5.0, 3.0, 1.0},
                {15.0, 10.0, 5.0, 0.0},
                {7.0, -7.0, 1.0, -1.0}
            };

            var expected = new[,]
            {
                {0.0, 0.1667, 0.3333, 0.5},
                {1.5, -1.5, 0.8333, -0.8333},
                {4.8333, 3.6667, 2.6667, 1.6667}
            };
            using (var arr = KhivaArray.Create(tss))
            {
                var (centroidsArr, labelsArr) = Clustering.KMeans(arr, 3);
                using (centroidsArr)
                {
                    using (labelsArr)
                    {
                        var data = centroidsArr.GetData2D<double>();

                        for (var i = 0; i < 4; i++)
                        {
                            Assert.AreEqual(expected[0, i] + expected[1, i] + expected[2, i],
                                data[0, i] + data[1, i] + data[2, i],
                                1e-3);
                        }
                    }
                }
            }
        }

        [Test]
        public void TestKShape()
        {
            var tss = new[,]
            {
                {1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0},
                {0.0, 10.0, 4.0, 5.0, 7.0, -3.0, 0.0},
                {-1.0, 15.0, -12.0, 8.0, 9.0, 4.0, 5.0},
                {2.0, 8.0, 7.0, -6.0, -1.0, 2.0, 9.0},
                {-5.0, -5.0, -6.0, 7.0, 9.0, 9.0, 0.0}
            };

            var expectedC = new[,]
            {
                {-0.5234, 0.1560, -0.3627, -1.2764, -0.7781, 0.9135, 1.8711},
                {-0.7825, 1.5990, 0.1701, 0.4082, 0.8845, -1.4969, -0.7825},
                {-0.6278, 1.3812, -2.0090, 0.5022, 0.6278, 0.0000, 0.1256}
            };
            var expectedL = new uint[] {0, 1, 2, 0, 0};
            using (var arr = KhivaArray.Create(tss))
            {
                var (centroidsArr, labelsArr) = Clustering.KShape(arr, 3);
                using (centroidsArr)
                using (labelsArr)
                {
                    var centroids = centroidsArr.GetData2D<double>();
                    var labels = labelsArr.GetData1D<uint>();

                    for (var i = 0; i < centroids.GetLength(0); i++)
                    {
                        for (var j = 0; j < centroids.GetLength(1); j++)
                        {
                            Assert.AreEqual(expectedC[i, j], centroids[i, j], 1e-3);
                        }
                    }

                    for (var i = 0; i < labels.Length; i++)
                    {
                        Assert.AreEqual(expectedL[i], labels[i], 1e-3);
                    }
                }
            }
        }
    }
}