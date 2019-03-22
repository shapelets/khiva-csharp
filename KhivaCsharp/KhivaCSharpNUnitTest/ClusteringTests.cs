using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva.clustering.Tests
{
    [TestFixture]
    public class ClusteringTests
    {
        [Test]
        public void TestKMeans()
        {
            double[,] tss = new double[,]{{ 0.0, 1.0, 2.0, 3.0 },
                                        { 6.0, 7.0, 8.0, 9.0 },
                                        { 2.0, -2.0, 4.0, -4.0 },
                                        { 8.0, 5.0, 3.0, 1.0 },
                                        { 15.0, 10.0, 5.0, 0.0 },
                                        { 7.0, -7.0, 1.0, -1.0 }
                                        };
            array.Array arr = new array.Array(tss);

            double[,] expected = new double[,] { { 0.0, 0.1667, 0.3333, 0.5 },
                                               { 1.5, -1.5, 0.8333, -0.8333 },
                                               { 4.8333, 3.6667, 2.6667, 1.6667 }
                                            };

            Tuple<array.Array, array.Array> result = clustering.Clustering.KMeans(arr, 3);
            array.Array result_c = result.Item1;
            double[,] data = result_c.GetData2D<double>();

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(expected[0, i] + expected[1, i] + expected[2, i],
                                data[0, i] + data[1, i] + data[2, i],
                                1e-3);
            }
        }

        [Test]
        public void TestKShape()
        {
            double[,] tss = new double[,]{  { 1.0,   2.0,   3.0,  4.0,  5.0,  6.0, 7.0 },
                                            { 0.0,  10.0,   4.0,  5.0,  7.0, -3.0, 0.0 },
                                            { -1.0, 15.0, -12.0,  8.0,  9.0,  4.0, 5.0 },
                                            { 2.0,   8.0,   7.0, -6.0, -1.0,  2.0, 9.0 },
                                            { -5.0, -5.0,  -6.0,  7.0,  9.0,  9.0, 0.0 },
                                            };
            array.Array arr = new array.Array(tss);

            double[,] expected_c = new double[,] { { -0.5234, 0.1560, -0.3627, -1.2764, -0.7781,  0.9135,  1.8711 },
                                                   { -0.7825, 1.5990,  0.1701,  0.4082,  0.8845, -1.4969, -0.7825 },
                                                   { -0.6278, 1.3812, -2.0090,  0.5022,  0.6278,  0.0000,  0.1256 }
                                                };
            int[] expected_l = new int[] { 0, 1, 2, 0, 0 };

            Tuple<array.Array, array.Array> result = clustering.Clustering.KShape(arr, 3);

            double[,] centroids = result.Item1.GetData2D<double>();
            int[] labels = result.Item2.GetData1D<int>();

            for (int i = 0; i < centroids.GetLength(0); i++)
            {
                for (int j = 0; j < centroids.GetLength(1); j++)
                {
                    Assert.AreEqual(expected_c[i,j], centroids[i,j], 1e-3);

                }
            }

            for (int i = 0; i < labels.Length; i++)
            {
                Assert.AreEqual(expected_l[i], labels[i], 1e-3);
            }

        }
    }
}
