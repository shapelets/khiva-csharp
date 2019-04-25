// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Numerics;
using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("KhivaArray")]
    public class KhivaArrayTests
    {
        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestGetType()
        {
            double[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
                Assert.AreEqual(KhivaArray.DType.F64, arr.ArrayType);
        }

        [Test]
        public void TestGetDims()
        {
            double[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
                Assert.AreEqual(new long[] {2, 1, 1, 1}, arr.Dims);
        }

        [Test]
        public void TestZeroDimensionsMismatch()
        {
            try
            {
                using (KhivaArray.CreateZeros<int>(new long[] {1, 2, 3, 4}, 0))
                {
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Number of dimensions must be between 1 and 4", e.Message);
            }
        }

        [Test]
        public void TestCreateFromArray()
        {
            var tss = new[] {1, 2, 3, 4};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(arr))
            {
                arr.Display();
                Assert.AreEqual(arr.GetData1D<int>(), arr2.GetData1D<int>());
            }
        }

        [Test]
        public void TestZero1D()
        {
            using (var arr = KhivaArray.CreateZeros<int>(new long[] {1, 2, 3, 2}, 1))
            {
                Assert.AreEqual(new[] {0}, arr.GetData1D<int>());
            }
        }

        [Test]
        public void TestZero2D()
        {
            using (var arr = KhivaArray.CreateZeros<int>(new long[] {1, 2, 3, 2}, 2))
            {
                Assert.AreEqual(new[,] {{0, 0}}, arr.GetData2D<int>());
            }
        }

        [Test]
        public void TestZero3D()
        {
            using (var arr = KhivaArray.CreateZeros<int>(new long[] {1, 2, 3, 2}, 3))
            {
                Assert.AreEqual(new[,,] {{{0, 0, 0}, {0, 0, 0}}}, arr.GetData3D<int>());
            }
        }

        [Test]
        public void TestZero4D()
        {
            using (var arr = KhivaArray.CreateZeros<int>(new long[] {1, 2, 3, 2}, 4))
            {
                Assert.AreEqual(new[,,,] {{{{0, 0}, {0, 0}, {0, 0}}, {{0, 0}, {0, 0}, {0, 0}}}},
                    arr.GetData4D<int>());
            }
        }

        [Test]
        public void TestDoubleNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((double[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestFloatNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((float[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestComplexDoubleNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((Complex[]) null, true))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestComplexFloatNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((Complex[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestBooleanNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((bool[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestShortNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((short[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestUshortNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((ushort[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestByteNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((byte[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestLongNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((long[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestUlongNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((ulong[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestIntNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((int[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestUintNull()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                using (KhivaArray.Create((uint[]) null))
                {
                }
            });
            Assert.AreEqual("Null elems object provided", ex.Message);
        }

        [Test]
        public void TestDoubleD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                double[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<double>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestFloatD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                float[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<float>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestComplexDoubleD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                Complex[,] tss = {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}};
                using (var arr = KhivaArray.Create(tss, true))
                    arr.GetData1D<Complex>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestComplexFloatD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                Complex[,] tss = {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<Complex>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestBooleanD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                bool[,] tss = {{true, false}, {false, false}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<bool>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestShortD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                short[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<short>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestUshortD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                ushort[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<ushort>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestByteD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                byte[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<byte>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestLongD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                long[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<long>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestUlongD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                ulong[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<ulong>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestIntD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                int[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<int>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestUintD1MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                uint[,] tss = {{1, 2}, {3, 4}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData1D<uint>();
            });
            Assert.AreEqual("The array must be have at most 1 dimensions for using this method but have 2",
                ex.Message);
        }

        [Test]
        public void TestDoubleD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                double[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<double>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestFloatD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                float[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<float>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestComplexDoubleD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                Complex[,,] tss =
                {
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                };
                using (var arr = KhivaArray.Create(tss, true))
                    arr.GetData2D<Complex>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestComplexFloatD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                Complex[,,] tss =
                {
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                };
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<Complex>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestBooleanD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                bool[,,] tss = {{{true, false}, {false, false}}, {{true, false}, {false, false}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<bool>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestShortD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                short[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<short>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestUshortD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                ushort[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<ushort>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestByteD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                byte[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<byte>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestLongD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                long[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<long>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestUlongD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                ulong[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<ulong>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestIntD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                int[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<int>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestUintD2MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                uint[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData2D<uint>();
            });
            Assert.AreEqual("The array must be have at most 2 dimensions for using this method but have 3",
                ex.Message);
        }

        [Test]
        public void TestDoubleD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                double[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<double>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestFloatD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                float[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<float>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestComplexDoubleD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                Complex[,,,] tss =
                {
                    {
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                    },
                    {
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                    }
                };
                using (var arr = KhivaArray.Create(tss, true))
                    arr.GetData3D<Complex>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestComplexFloatD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                Complex[,,,] tss =
                {
                    {
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                    },
                    {
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                        {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                    }
                };
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<Complex>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestBooleanD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                bool[,,,] tss =
                {
                    {{{true, false}, {false, false}}, {{true, false}, {false, false}}},
                    {{{true, false}, {false, false}}, {{true, false}, {false, false}}}
                };
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<bool>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestShortD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                short[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<short>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestUshortD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                ushort[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<ushort>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestByteD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                byte[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<byte>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestLongD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                long[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<long>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestUlongD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                ulong[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<ulong>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestIntD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                int[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<int>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestUintD3MismatchDims()
        {
            var ex = Assert.Throws<Exception>(delegate
            {
                uint[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
                using (var arr = KhivaArray.Create(tss))
                    arr.GetData3D<uint>();
            });
            Assert.AreEqual("The array must be have at most 3 dimensions for using this method but have 4",
                ex.Message);
        }

        [Test]
        public void TestDoubleD1OkDims()
        {
            double[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<double>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestFloatD1OkDims()
        {
            float[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<float>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexDoubleD1OkDims()
        {
            Complex[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss, true))
            {
                var data = arr.GetData1D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexFloatD1OkDims()
        {
            Complex[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestBooleanD1OkDims()
        {
            bool[] tss = {true, false};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<bool>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestShortD1OkDims()
        {
            short[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<short>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUshortD1OkDims()
        {
            ushort[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<ushort>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestByteD1OkDims()
        {
            byte[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<byte>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestLongD1OkDims()
        {
            long[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<long>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUlongD1OkDims()
        {
            ulong[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<ulong>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestIntD1OkDims()
        {
            int[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<int>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUintD1OkDims()
        {
            uint[] tss = {1, 2};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData1D<uint>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestDoubleD2OkDims()
        {
            double[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<double>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestFloatD2OkDims()
        {
            float[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<float>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexDoubleD2OkDims()
        {
            Complex[,] tss = {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}};
            using (var arr = KhivaArray.Create(tss, true))
            {
                var data = arr.GetData2D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexFloatD2OkDims()
        {
            Complex[,] tss = {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestBooleanD2OkDims()
        {
            bool[,] tss = {{true, false}, {false, false}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<bool>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestShortD2OkDims()
        {
            short[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<short>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUshortD2OkDims()
        {
            ushort[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<ushort>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestByteD2OkDims()
        {
            byte[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<byte>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestLongD2OkDims()
        {
            long[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<long>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUlongD2OkDims()
        {
            ulong[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<ulong>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestIntD2OkDims()
        {
            int[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<int>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUintD2OkDims()
        {
            uint[,] tss = {{1, 2}, {3, 4}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData2D<uint>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestDoubleD3OkDims()
        {
            double[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<double>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestFloatD3OkDims()
        {
            float[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<float>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexDoubleD3OkDims()
        {
            Complex[,,] tss =
            {
                {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
            };
            using (var arr = KhivaArray.Create(tss, true))
            {
                var data = arr.GetData3D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexFloatD3OkDims()
        {
            Complex[,,] tss =
            {
                {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
            };
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestBooleanD3OkDims()
        {
            bool[,,] tss = {{{true, false}, {false, false}}, {{true, false}, {false, false}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<bool>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestShortD3OkDims()
        {
            short[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<short>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUshortD3OkDims()
        {
            ushort[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<ushort>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestByteD3OkDims()
        {
            byte[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<byte>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestLongD3OkDims()
        {
            long[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<long>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUlongD3OkDims()
        {
            ulong[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<ulong>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestIntD3OkDims()
        {
            int[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<int>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUintD3OkDims()
        {
            uint[,,] tss = {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData3D<uint>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestDoubleD4OkDims()
        {
            double[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<double>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestFloatD4OkDims()
        {
            float[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<float>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexDoubleD4OkDims()
        {
            Complex[,,,] tss =
            {
                {
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                },
                {
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                }
            };
            using (var arr = KhivaArray.Create(tss, true))
            {
                var data = arr.GetData4D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexFloatD4OkDims()
        {
            Complex[,,,] tss =
            {
                {
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                },
                {
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}},
                    {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}}
                }
            };
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestBooleanD4OkDims()
        {
            bool[,,,] tss =
            {
                {{{true, false}, {false, false}}, {{true, false}, {false, false}}},
                {{{true, false}, {false, false}}, {{true, false}, {false, false}}}
            };
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<bool>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestShortD4OkDims()
        {
            short[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<short>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUshortD4OkDims()
        {
            ushort[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<ushort>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestByteD4OkDims()
        {
            byte[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<byte>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestLongD4OkDims()
        {
            long[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<long>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUlongD4OkDims()
        {
            ulong[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<ulong>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestIntD4OkDims()
        {
            int[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<int>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestUintD4OkDims()
        {
            uint[,,,] tss = {{{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}, {{{1, 2}, {3, 4}}, {{1, 2}, {3, 4}}}};
            using (var arr = KhivaArray.Create(tss))
            {
                var data = arr.GetData4D<uint>();
                Assert.AreEqual(tss, data);
            }
        }


        [Test]
        public void TestPlusOperator()
        {
            uint[] tss = {1, 2, 3, 4};
            uint[] tss2 = {1, 2, 3, 4};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr + arr2)
            {
                Assert.AreEqual(new uint[] {2, 4, 6, 8}, arr3.GetData1D<uint>());
            }
        }

        [Test]
        public void TestPlusSelfOperator()
        {
            uint[] tss = {1, 2, 3, 4, 5};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = arr + arr)
            {
                Assert.AreEqual(new uint[] {2, 4, 6, 8, 10}, arr2.GetData1D<uint>());
            }
        }

        [Test]
        public void TestSubOperator()
        {
            double[] tss = {1, 2, 3, 4};
            double[] tss2 = {1, 2, 3, 4};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr - arr2)
            {
                Assert.AreEqual(new double[] {0, 0, 0, 0}, arr3.GetData1D<double>());
            }
        }

        [Test]
        public void TestMulOperator()
        {
            float[] tss = {1, 2, 3, 4};
            float[] tss2 = {1, 2, 3, 4};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr * arr2)
            {
                Assert.AreEqual(new float[] {1, 4, 9, 16}, arr3.GetData1D<float>());
            }
        }


        [Test]
        public void TestDivOperator()
        {
            float[] tss = {1, 2, 3, 4};
            float[] tss2 = {1, 2, 3, 4};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr / arr2)
            {
                Assert.AreEqual(new float[] {1, 1, 1, 1}, arr3.GetData1D<float>());
            }
        }

        [Test]
        public void TestModOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {1, 2, 3, 4};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr % arr2)
            {
                Assert.AreEqual(new long[] {0, 0, 0, 0}, arr3.GetData1D<long>());
            }
        }

        [Test]
        public void TestPowOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {2, 2, 2, 2};
            using (KhivaArray arr = KhivaArray.Create(tss),
                arr2 = KhivaArray.Create(tss2),
                arrPow = KhivaArray.Pow(arr, arr2))
            {
                Assert.AreEqual(new long[] {1, 4, 9, 16}, arrPow.GetData1D<long>());
            }
        }

        [Test]
        public void TestAndOperator()
        {
            byte[] tss = {1, 1, 1, 1};
            byte[] tss2 = {1, 0, 1, 0};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr & arr2)
            {
                Assert.AreEqual(new byte[] {1, 0, 1, 0}, arr3.GetData1D<byte>());
            }
        }

        [Test]
        public void TestOrOperator()
        {
            byte[] tss = {1, 1, 1, 1};
            byte[] tss2 = {1, 0, 1, 0};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr | arr2)
            {
                Assert.AreEqual(new byte[] {1, 1, 1, 1}, arr3.GetData1D<byte>());
            }
        }

        [Test]
        public void TestXorOperator()
        {
            bool[] tss = {true, true, true, true};
            bool[] tss2 = {true, false, true, false};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arr3 = arr ^ arr2)
            {
                Assert.AreEqual(new[] {false, true, false, true}, arr3.GetData1D<bool>());
            }
        }

        [Test]
        public void TestBitShiftRightOperator()
        {
            int[] tss = {2, 4, 6, 8};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = arr >> 1)
            {
                Assert.AreEqual(new[] {1, 2, 3, 4}, arr2.GetData1D<int>());
            }
        }

        [Test]
        public void TestBitShiftLeftOperator()
        {
            int[] tss = {2, 4, 6, 8};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = arr << 1)
            {
                Assert.AreEqual(new[] {4, 8, 12, 16}, arr2.GetData1D<int>());
            }
        }

        [Test]
        public void TestNegOperator()
        {
            int[] tss = {1, 2, 3, 4};
            using (KhivaArray arr = KhivaArray.Create(tss), arrNeg = -arr)
            {
                Assert.AreEqual(new[] {-1, -2, -3, -4}, arrNeg.GetData1D<int>());
            }
        }

        [Test]
        public void TestNotOperator()
        {
            bool[] tss = {true, true, true, false};
            using (KhivaArray arr = KhivaArray.Create(tss), arrNot = !arr)
                Assert.AreEqual(new[] {false, false, false, true}, arrNot.GetData1D<bool>());
        }

        [Test]
        public void TestLtOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {2, 2, 2, 2};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arrLt = arr < arr2)
                Assert.AreEqual(new[] {true, false, false, false}, arrLt.GetData1D<bool>());
        }

        [Test]
        public void TestGtOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {2, 2, 2, 2};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arrGt = arr > arr2)
                Assert.AreEqual(new[] {false, false, true, true}, arrGt.GetData1D<bool>());
        }

        [Test]
        public void TestLeOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {2, 2, 2, 2};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arrLe = arr <= arr2)
                Assert.AreEqual(new[] {true, true, false, false}, arrLe.GetData1D<bool>());
        }

        [Test]
        public void TestGeOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {2, 2, 2, 2};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arrGe = arr >= arr2)
                Assert.AreEqual(new[] {false, true, true, true}, arrGe.GetData1D<bool>());
        }

        [Test]
        public void TestEqOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {2, 2, 2, 2};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arrEq = arr == arr2)
                Assert.AreEqual(new[] {false, true, false, false}, arrEq.GetData1D<bool>());
        }

        [Test]
        public void TestNeOperator()
        {
            long[] tss = {1, 2, 3, 4};
            long[] tss2 = {2, 2, 2, 2};
            using (KhivaArray arr = KhivaArray.Create(tss), arr2 = KhivaArray.Create(tss2), arrNe = arr != arr2)
                Assert.AreEqual(new[] {true, false, true, true}, arrNe.GetData1D<bool>());
        }

        [Test]
        public void TestTransposeNotConjugate()
        {
            int[,] tss = {{1, 2}, {3, 4}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrTrans = arr.Transpose())
                Assert.AreEqual(new[,] {{1, 3}, {2, 4}}, arrTrans.GetData2D<int>());
        }

        [Test]
        public void TestTransposeConjugate()
        {
            Complex[,] tss = {{new Complex(1, 2), new Complex(3, 4)}, {new Complex(1, 2), new Complex(3, 4)}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrTrans = arr.Transpose(true))
                Assert.AreEqual(
                    new[,] {{new Complex(1, -2), new Complex(1, -2)}, {new Complex(3, -4), new Complex(3, -4)}},
                    arrTrans.GetData2D<Complex>());
        }

        [Test]
        public void TestCol()
        {
            int[,] tss = {{1, 2}, {3, 4}, {5, 6}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrCol = arr.Col(0))
                Assert.AreEqual(new[,] {{1, 2}}, arrCol.GetData2D<int>());
        }

        [Test]
        public void TestCols()
        {
            int[,] tss = {{1, 2}, {3, 4}, {5, 6}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrCol = arr.Cols(0, 1))
                Assert.AreEqual(new[,] {{1, 2}, {3, 4}}, arrCol.GetData2D<int>());
        }

        [Test]
        public void TestRow()
        {
            int[,] tss = {{1, 2}, {3, 4}, {5, 6}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrRow = arr.Row(0))
                Assert.AreEqual(new[,] {{1}, {3}, {5}}, arrRow.GetData2D<int>());
        }

        [Test]
        public void TestRows()
        {
            int[,] tss = {{1, 2}, {3, 4}, {5, 6}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrRows = arr.Rows(0, 1))
                Assert.AreEqual(new[,] {{1, 2}, {3, 4}, {5, 6}}, arrRows.GetData2D<int>());
        }

        [Test]
        public void TestMatMul()
        {
            float[,] tss = {{1, 2}, {3, 4}, {5, 6}};
            float[,] tss2 = {{1, 1, 1}, {1, 1, 1}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                arr2 = KhivaArray.Create(tss2),
                arrMatMul = arr.MatMul(arr2))
                Assert.AreEqual(new float[,] {{9, 12}, {9, 12}}, arrMatMul.GetData2D<float>());
        }

        [Test]
        public void TestCopy()
        {
            int[,] tss = {{1, 2}, {3, 4}, {5, 6}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrCopy = arr.Copy())
                Assert.AreNotSame(arr, arrCopy, "They are not copies");
        }

        [Test]
        public void TestAs()
        {
            int[,] tss = {{1, 2}, {3, 4}, {5, 6}};
            using (KhivaArray arr = KhivaArray.Create(tss), arrAs = arr.As((int) KhivaArray.DType.F64))
                Assert.AreEqual(KhivaArray.DType.F64, arrAs.ArrayType);
        }
    }
}