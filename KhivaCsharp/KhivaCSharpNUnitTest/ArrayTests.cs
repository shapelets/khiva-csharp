// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;
using khiva.array;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace khiva.array.tests
{
    [TestFixture]
    public class ArrayTests
    {
        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestGetType()
        {
            double[] tss = { 1, 2 };
            long[] dims = { 1, 2, 1, 1 };
            using(Array arr = new Array(tss))
                Assert.AreEqual(Array.Dtype.f64, arr.ArrayType); 
        }

        [Test]
        public void TestGetDims()
        {
            double[] tss = { 1, 2 };
            using (Array arr = new Array(tss))
                Assert.AreEqual(new long[] { 2, 1, 1, 1 }, arr.Dims);
        }

        [Test]
        public void TestDoubleNull()
        {
            double[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestFloatNull()
        {
            float[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestComplexDoubleNull()
        {
            Complex[] tss = null;
            try
            {
                using (new Array(tss, true)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestComplexFloatNull()
        {
            Complex[] tss = null;
            try
            {
                using (new Array(tss, false)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestBooleanNull()
        {
            bool[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestShortNull()
        {
            short[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestUshortNull()
        {
            ushort[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestByteNull()
        {
            byte[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestLongNull()
        {
            long[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestUlongNull()
        {
            ulong[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestIntNull()
        {
            int[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestUintNull()
        {
            uint[] tss = null;
            try
            {
                using (new Array(tss)) { }
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null elems object provided", e.Message);
            }
        }

        [Test]
        public void TestDoubleD1MismatchDims()
        {
            double[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                {
                    double[] data = arr.GetData1D<double>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestFloatD1MismatchDims()
        {
            float[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                { 
                float[] data = arr.GetData1D<float>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestComplexDoubleD1MismatchDims()
        {
            Complex[,] tss = { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } };
            using (Array arr = new Array(tss, true))
                try
                {
                    Complex[] data = arr.GetData1D<Complex>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestComplexFloatD1MismatchDims()
        {
            Complex[,] tss = { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } };
            using (Array arr = new Array(tss, false))
                try
                {
                    Complex[] data = arr.GetData1D<Complex>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
}

        [Test]
        public void TestBooleanD1MismatchDims()
        {
            bool[,] tss = { { true, false }, { false, false } };
            using (Array arr = new Array(tss))
                try
                {
                    bool[] data = arr.GetData1D<bool>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestShortD1MismatchDims()
        {
            short[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                {
                    short[] data = arr.GetData1D<short>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestUshortD1MismatchDims()
        {
            ushort[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                {
                    ushort[] data = arr.GetData1D<ushort>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestByteD1MismatchDims()
        {
            byte[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                {
                    byte[] data = arr.GetData1D<byte>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestLongD1MismatchDims()
        {
            long[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                { 
                long[] data = arr.GetData1D<long>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestUlongD1MismatchDims()
        {
            ulong[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                { 
                ulong[] data = arr.GetData1D<ulong>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestIntD1MismatchDims()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                { 
                int[] data = arr.GetData1D<int>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestUintD1MismatchDims()
        {
            uint[,] tss = { { 1, 2 }, { 3, 4 } };
            using (Array arr = new Array(tss))
                try
                { 
                uint[] data = arr.GetData1D<uint>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 1 dimensions for using this method and have 2", e.Message);
                }
        }

        [Test]
        public void TestDoubleD2MismatchDims()
        {
            double[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                double[,] data = arr.GetData2D<double>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestFloatD2MismatchDims()
        {
            float[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                float[,] data = arr.GetData2D<float>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestComplexDoubleD2MismatchDims()
        {
            Complex[,,] tss = { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } };
            using (Array arr = new Array(tss, true))
                try
                { 
                Complex[,] data = arr.GetData2D<Complex>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestComplexFloatD2MismatchDims()
        {
            Complex[,,] tss = { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } };
            using (Array arr = new Array(tss, false))
                try
                { 
                Complex[,] data = arr.GetData2D<Complex>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestBooleanD2MismatchDims()
        {
            bool[,,] tss = { { { true, false }, { false, false } }, { { true, false }, { false, false } } };
            using (Array arr = new Array(tss))
                try
                { 
                bool[,] data = arr.GetData2D<bool>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestShortD2MismatchDims()
        {
            short[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                short[,] data = arr.GetData2D<short>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestUshortD2MismatchDims()
        {
            ushort[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                ushort[,] data = arr.GetData2D<ushort>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestByteD2MismatchDims()
        {
            byte[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                byte[,] data = arr.GetData2D<byte>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestLongD2MismatchDims()
        {
            long[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                long[,] data = arr.GetData2D<long>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestUlongD2MismatchDims()
        {
            ulong[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                ulong[,] data = arr.GetData2D<ulong>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestIntD2MismatchDims()
        {
            int[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                int[,] data = arr.GetData2D<int>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestUintD2MismatchDims()
        {
            uint[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            using (Array arr = new Array(tss))
                try
                { 
                    uint[,] data = arr.GetData2D<uint>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 2 dimensions for using this method and have 3", e.Message);
                }
        }

        [Test]
        public void TestDoubleD3MismatchDims()
        {
            double[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                double[,,] data = arr.GetData3D<double>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestFloatD3MismatchDims()
        {
            float[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                float[,,] data = arr.GetData3D<float>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestComplexDoubleD3MismatchDims()
        {
            Complex[,,,] tss = { { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } }, { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } } };
            using (Array arr = new Array(tss, true))
                try
                { 
                Complex[,,] data = arr.GetData3D<Complex>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestComplexFloatD3MismatchDims()
        {
            Complex[,,,] tss = { { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } }, { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } } };
            using (Array arr = new Array(tss, false))
                try
                { 
                Complex[,,] data = arr.GetData3D<Complex>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestBooleanD3MismatchDims()
        {
            bool[,,,] tss = { { { { true, false }, { false, false } }, { { true, false }, { false, false } } }, { { { true, false }, { false, false } }, { { true, false }, { false, false } } } };
            using (Array arr = new Array(tss))
                try
                { 
                bool[,,] data = arr.GetData3D<bool>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestShortD3MismatchDims()
        {
            short[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                short[,,] data = arr.GetData3D<short>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestUshortD3MismatchDims()
        {
            ushort[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                ushort[,,] data = arr.GetData3D<ushort>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestByteD3MismatchDims()
        {
            byte[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                byte[,,] data = arr.GetData3D<byte>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestLongD3MismatchDims()
        {
            long[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                long[,,] data = arr.GetData3D<long>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestUlongD3MismatchDims()
        {
            ulong[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                ulong[,,] data = arr.GetData3D<ulong>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestIntD3MismatchDims()
        {
            int[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                int[,,] data = arr.GetData3D<int>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestUintD3MismatchDims()
        {
            uint[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            using (Array arr = new Array(tss))
                try
                { 
                uint[,,] data = arr.GetData3D<uint>();
                }
                catch (Exception e)
                {
                    Assert.AreEqual("The array must be have at most 3 dimensions for using this method and have 4", e.Message);
                }
        }

        [Test]
        public void TestDoubleD1OkDims()
        {
            double[] tss = { 1, 2 };
            using (Array arr = new Array(tss))
            {
                double[] data = arr.GetData1D<double>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestFloatD1OkDims()
        {
            float[] tss = { 1, 2 };
            using (Array arr = new Array(tss))
            {
                float[] data = arr.GetData1D<float>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexDoubleD1OkDims()
        {
            Complex[] tss = { 1, 2 };
            using (Array arr = new Array(tss, true))
            {
                Complex[] data = arr.GetData1D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestComplexFloatD1OkDims()
        {
            Complex[] tss = { 1, 2 };
            using (Array arr = new Array(tss, false))
            {
                Complex[] data = arr.GetData1D<Complex>();
                Assert.AreEqual(tss, data);
            }
        }

        [Test]
        public void TestBooleanD1OkDims()
        {
            bool[] tss = { true, false };
            using (Array arr = new Array(tss))
            {
                bool[] data = arr.GetData1D<bool>();
                Assert.AreEqual(tss, data);
            }    
        }

        [Test]
        public void TestShortD1OkDims()
        {
            short[] tss = { 1, 2 };
            Array arr = new Array(tss);
            short[] data = arr.GetData1D<short>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUshortD1OkDims()
        {
            ushort[] tss = { 1, 2 };
            Array arr = new Array(tss);
            ushort[] data = arr.GetData1D<ushort>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestByteD1OkDims()
        {
            byte[] tss = { 1, 2 };
            Array arr = new Array(tss);
            byte[] data = arr.GetData1D<byte>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestLongD1OkDims()
        {
            long[] tss = { 1, 2 };
            Array arr = new Array(tss);
            long[] data = arr.GetData1D<long>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUlongD1OkDims()
        {
            ulong[] tss = { 1, 2 };
            Array arr = new Array(tss);
            ulong[] data = arr.GetData1D<ulong>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestIntD1OkDims()
        {
            int[] tss = { 1, 2 };
            Array arr = new Array(tss);
            int[] data = arr.GetData1D<int>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUintD1OkDims()
        {
            uint[] tss = { 1, 2 };
            Array arr = new Array(tss);
            uint[] data = arr.GetData1D<uint>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestDoubleD2OkDims()
        {
            double[,] tss = { { 1, 2 }, { 3, 4} };
            Array arr = new Array(tss);
            double[,] data = arr.GetData2D<double>();
            Assert.AreEqual(tss, data);
        }
        
        [Test]
        public void TestFloatD2OkDims()
        {
            float[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            float[,] data = arr.GetData2D<float>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexDoubleD2OkDims()
        {
            Complex[,] tss = { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } };
            Array arr = new Array(tss, true);
            Complex[,] data = arr.GetData2D<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexFloatD2OkDims()
        {
            Complex[,] tss = { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } };
            Array arr = new Array(tss, false);
            Complex[,] data = arr.GetData2D<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestBooleanD2OkDims()
        {
            bool[,] tss = { { true, false }, { false, false } };
            Array arr = new Array(tss);
            bool[,] data = arr.GetData2D<bool>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestShortD2OkDims()
        {
            short[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            short[,] data = arr.GetData2D<short>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUshortD2OkDims()
        {
            ushort[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            ushort[,] data = arr.GetData2D<ushort>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestByteD2OkDims()
        {
            byte[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            byte[,] data = arr.GetData2D<byte>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestLongD2OkDims()
        {
            long[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            long[,] data = arr.GetData2D<long>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUlongD2OkDims()
        {
            ulong[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            ulong[,] data = arr.GetData2D<ulong>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestIntD2OkDims()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            int[,] data = arr.GetData2D<int>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUintD2OkDims()
        {
            uint[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            uint[,] data = arr.GetData2D<uint>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestDoubleD3OkDims()
        {
            double[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            double[,,] data = arr.GetData3D<double>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestFloatD3OkDims()
        {
            float[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            float[,,] data = arr.GetData3D<float>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexDoubleD3OkDims()
        {
            Complex[,,] tss = { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } };
            Array arr = new Array(tss, true);
            Complex[,,] data = arr.GetData3D<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexFloatD3OkDims()
        {
            Complex[,,] tss = { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } };
            Array arr = new Array(tss, false);
            Complex[,,] data = arr.GetData3D<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestBooleanD3OkDims()
        {
            bool[,,] tss = { { { true, false }, { false, false } }, { { true, false }, { false, false } } };
            Array arr = new Array(tss);
            bool[,,] data = arr.GetData3D<bool>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestShortD3OkDims()
        {
            short[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            short[,,] data = arr.GetData3D<short>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUshortD3OkDims()
        {
            ushort[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            ushort[,,] data = arr.GetData3D<ushort>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestByteD3OkDims()
        {
            byte[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            byte[,,] data = arr.GetData3D<byte>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestLongD3OkDims()
        {
            long[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            long[,,] data = arr.GetData3D<long>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUlongD3OkDims()
        {
            ulong[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            ulong[,,] data = arr.GetData3D<ulong>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestIntD3OkDims()
        {
            int[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            int[,,] data = arr.GetData3D<int>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUintD3OkDims()
        {
            uint[,,] tss = { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } };
            Array arr = new Array(tss);
            uint[,,] data = arr.GetData3D<uint>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestDoubleD4OkDims()
        {
            double[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            double[,,,] data = arr.GetData4D<double>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestFloatD4OkDims()
        {
            float[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            float[,,,] data = arr.GetData4D<float>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexDoubleD4OkDims()
        {
            Complex[,,,] tss = { { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } }, { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } } };
            Array arr = new Array(tss, true);
            Complex[,,,] data = arr.GetData4D<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexFloatD4OkDims()
        {
            Complex[,,,] tss = { { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } }, { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } }, { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } } } };
            Array arr = new Array(tss, false);
            Complex[,,,] data = arr.GetData4D<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestBooleanD4OkDims()
        {
            bool[,,,] tss = { { { { true, false }, { false, false } }, { { true, false }, { false, false } } }, { { { true, false }, { false, false } }, { { true, false }, { false, false } } } };
            Array arr = new Array(tss);
            bool[,,,] data = arr.GetData4D<bool>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestShortD4OkDims()
        {
            short[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            short[,,,] data = arr.GetData4D<short>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUshortD4OkDims()
        {
            ushort[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            ushort[,,,] data = arr.GetData4D<ushort>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestByteD4OkDims()
        {
            byte[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            byte[,,,] data = arr.GetData4D<byte>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestLongD4OkDims()
        {
            long[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            long[,,,] data = arr.GetData4D<long>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUlongD4OkDims()
        {
            ulong[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            ulong[,,,] data = arr.GetData4D<ulong>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestIntD4OkDims()
        {
            int[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            int[,,,] data = arr.GetData4D<int>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUintD4OkDims()
        {
            uint[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } }, { { { 1, 2 }, { 3, 4 } }, { { 1, 2 }, { 3, 4 } } } };
            Array arr = new Array(tss);
            uint[,,,] data = arr.GetData4D<uint>();
            Assert.AreEqual(tss, data);
        }
        

        [Test]
        public void TestPlusOperator()
        {
            uint[] tss = { 1, 2, 3, 4 };
            uint[] tss2 = { 1, 2, 3, 4 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr += arr2;
            Assert.AreEqual(new uint[]{2, 4, 6, 8}, arr.GetData1D<uint>());
        }

        [Test]
        public void TestPlusSelfOperator()
        {
            uint[] tss = { 1, 2, 3, 4, 5};
            Array arr = new Array(tss);
            arr += arr;
            Assert.AreEqual(new uint[] {2, 4, 6, 8, 10}, arr.GetData1D<uint>());
        }

        [Test]
        public void TestSubOperator()
        {
            double[] tss = { 1, 2, 3, 4 };
            double[] tss2 = { 1, 2, 3, 4 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr -= arr2;
            Assert.AreEqual(new double[] {0, 0, 0, 0}, arr.GetData1D<double>());
        }

        [Test]
        public void TestMulOperator()
        {
            float[] tss = { 1, 2, 3, 4 };
            float[] tss2 = { 1, 2, 3, 4 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr *= arr2;
            Assert.AreEqual(new float[] {1, 4, 9, 16}, arr.GetData1D<float>());
        }

        

        [Test]
        public void TestTrueDivOperator()
        {
            float[] tss = { 1, 2, 3, 4 };
            float[] tss2 = { 1, 2, 3, 4 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr /= arr2;
            Assert.AreEqual(new float[] {1, 1, 1, 1}, arr.GetData1D<float>());
        }

        [Test]
        public void TestDivOperator()
        {
            short[] tss = { 1, 2, 3, 4 };
            short[] tss2 = { 1, 2, 3, 4 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrDiv = arr / arr2;
            Assert.AreEqual(new short[] { 1, 1, 1, 1 }, arrDiv.GetData1D<short>());
        }

        [Test]
        public void TestModOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 1, 2, 3, 4 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr %= arr2;
            Assert.AreEqual(new long[] { 0, 0, 0, 0 }, arr.GetData1D<long>());
        }

        [Test]
        public void TestPowOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrPow = arr.Pow(arr2);
            Assert.AreEqual(new long[] { 1, 4, 9, 16 }, arrPow.GetData1D<long>());
        }

        [Test]
        public void TestAndOperator()
        {
            byte[] tss = { 1, 1, 1, 1 };
            byte[] tss2 = { 1, 0, 1, 0 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr &= arr2;
            Assert.AreEqual(new byte[] { 1, 0, 1, 0 }, arr.GetData1D<byte>());
        }

        [Test]
        public void TestOrOperator()
        {
            byte[] tss = { 1, 1, 1, 1 };
            byte[] tss2 = { 1, 0, 1, 0 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr |= arr2;
            Assert.AreEqual(new byte[] { 1, 1, 1, 1 }, arr.GetData1D<byte>());
        }

        [Test]
        public void TestXorOperator()
        {
            bool[] tss = { true, true, true, true };
            bool[] tss2 = { true, false, true, false };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            arr ^= arr2;
            Assert.AreEqual(new bool[] { false, true, false, true }, arr.GetData1D<bool>());
        }

        [Test]
        public void TestBitShiftRightOperator()
        {
            int[] tss = { 2, 4, 6, 8 };
            Array arr = new Array(tss);
            arr >>= 1;
            Assert.AreEqual(new int[] { 1, 2, 3, 4 }, arr.GetData1D<int>());
        }

        [Test]
        public void TestBitShiftLeftOperator()
        {
            int[] tss = { 2, 4, 6, 8 };
            Array arr = new Array(tss);
            arr <<= 1;
            Assert.AreEqual(new int[] { 4, 8, 12, 16 }, arr.GetData1D<int>());
        }

        [Test]
        public void TestNegOperator()
        {
            int[] tss = { 1, 2, 3, 4 };
            Array arr = new Array(tss);
            Array arrNeg = -arr;
            Assert.AreEqual(new int[] { -1, -2, -3, -4 }, arrNeg.GetData1D<int>());
        }

        [Test]
        public void TestNotOperator()
        {
            bool[] tss = { true, true, true, false };
            Array arr = new Array(tss);
            Array arrNot = !arr;
            Assert.AreEqual(new bool[] { false, false, false, true }, arrNot.GetData1D<bool>());
        }

        [Test]
        public void TestLtOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrLt = arr < arr2;
            Assert.AreEqual(new bool[] { true, false, false, false }, arrLt.GetData1D<bool>());
        }

        [Test]
        public void TestGtOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrGt = arr > arr2;
            Assert.AreEqual(new bool[] { false, false, true, true }, arrGt.GetData1D<bool>());
        }

        [Test]
        public void TestLeOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrLe = arr <= arr2;
            Assert.AreEqual(new bool[] { true, true, false, false }, arrLe.GetData1D<bool>());
        }
        [Test]
        public void TestGeOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrGe = arr >= arr2;
            Assert.AreEqual(new bool[] { false, true, true, true }, arrGe.GetData1D<bool>());
        }
        [Test]
        public void TestEqOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrEq = arr == arr2;
            Assert.AreEqual(new bool[] { false, true, false, false }, arrEq.GetData1D<bool>());
        }
        [Test]
        public void TestNeOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrNe = arr != arr2;
            Assert.AreEqual(new bool[] { true, false, true, true }, arrNe.GetData1D<bool>());
        }

        [Test]
        public void TestTransposeNotConjugate()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 } };
            Array arr = new Array(tss);
            Array arrTrans = arr.Transpose();
            Assert.AreEqual(new int[,] { { 1, 3 }, { 2, 4 } }, arrTrans.GetData2D<int>());
        }

        [Test]
        public void TestTransposeConjugate()
        {
            Complex[,] tss = { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(1, 2), new Complex(3, 4) } };
            Array arr = new Array(tss, false);
            Array arrTrans = arr.Transpose(true);
            Assert.AreEqual(new Complex[,] { { new Complex(1, -2), new Complex(1, -2) }, { new Complex(3, -4), new Complex(3, -4) } }, arrTrans.GetData2D<Complex>());
        }

        [Test]
        public void TestCol()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Array arr = new Array(tss);
            Array arrCol = arr.Col(0);
            Assert.AreEqual(new int[,] { { 1, 2 } }, arrCol.GetData2D<int>());
        }

        [Test]
        public void TestCols()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Array arr = new Array(tss);
            Array arrCol = arr.Cols(0,1);
            Assert.AreEqual(new int[,] { { 1 , 2 }, { 3, 4 } }, arrCol.GetData2D<int>());
        }

        [Test]
        public void TestRow()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Array arr = new Array(tss);
            Array arrRow = arr.Row(0);
            Assert.AreEqual(new int[,] { { 1 }, { 3 } , { 5 } }, arrRow.GetData2D<int>());
        }

        [Test]
        public void TestRows()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Array arr = new Array(tss);
            Array arrRows = arr.Rows(0, 1);
            Assert.AreEqual(new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } }, arrRows.GetData2D<int>());
        }

        [Test]
        public void TestMatmul()
        {
            float[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            float[,] tss2 = { { 1, 1, 1 }, { 1, 1, 1 } };
            Array arr = new Array(tss);
            Array arr2 = new Array(tss2);
            Array arrMatul = arr.Matmul(arr2);
            Assert.AreEqual(new float[,] { { 9, 12 }, { 9, 12 } }, arrMatul.GetData2D<float>());
        }

        [Test]
        public void TestCopy()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Array arr = new Array(tss);
            Array arrCopy = arr.Copy();
            Assert.AreNotSame(arr, arrCopy, "They are not copies");
        }

        [Test]
        public void TestAs()
        {
            int[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Array arr = new Array(tss);
            Array arrAs = arr.As((int)Array.Dtype.f64);
            Assert.AreEqual(Array.Dtype.f64, arrAs.ArrayType);
        }
    }
}