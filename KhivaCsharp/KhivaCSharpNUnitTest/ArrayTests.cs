using NUnit.Framework;
using khiva.array;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace khiva.array.Tests
{
    [TestFixture]
    public class ArrayTests
    {
        [Test]
        public void TestGetType()
        {
            double[] tss = { 1, 2 };
            long[] dims = { 1, 2, 1, 1 };
            Array arr = new Array(tss, dims);
            Assert.AreEqual(Array.Dtype.f64, arr.GetArrayType()); 
        }

        [Test]
        public void TestGetDims()
        {
            double[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            Assert.AreEqual(new long[]{1,2,1,1}, arr.GetDims());
        }

        [Test]
        public void TestDoubleNull()
        {
            double[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestDoubleMismatchingDims()
        {
            double[] tss = {1, 2};
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestFloatNull()
        {
            float[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestFloatMismatchingDims()
        {
            float[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestComplexDoubleNull()
        {
            Complex[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims, true);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestComplexDoubleMismatchingDims()
        {
            Complex[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims, true);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestComplexFloatNull()
        {
            Complex[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims, false);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestComplexFloatMismatchingDims()
        {
            Complex[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims, false);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestBooleanNull()
        {
            bool[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestBooleanMismatchingDims()
        {
            bool[] tss = { true, false };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestShortNull()
        {
            short[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestShortMismatchingDims()
        {
            short[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestUshortNull()
        {
            ushort[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestUshortMismatchingDims()
        {
            ushort[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestByteNull()
        {
            byte[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestByteMismatchingDims()
        {
            byte[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestLongNull()
        {
            long[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestLongMismatchingDims()
        {
            long[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestUlongNull()
        {
            ulong[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestUlongMismatchingDims()
        {
            ulong[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestIntNull()
        {
            int[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestIntMismatchingDims()
        {
            int[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestUintNull()
        {
            uint[] tss = null;
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Null elems object provided");
            }
        }

        [Test]
        public void TestUintMismatchingDims()
        {
            uint[] tss = { 1, 2 };
            long[] dims = { 1, 1, 1, 1 };
            try
            {
                new Array(tss, dims);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }

        [Test]
        public void TestDoubleOkDims()
        {
            double[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            double[] data = arr.GetData<double>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestFloatOkDims()
        {
            float[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            float[] data = arr.GetData<float>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexDoubleOkDims()
        {
            Complex[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims, true);
            Complex[] data = arr.GetData<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestComplexFloatOkDims()
        {
            Complex[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims, false);
            Complex[] data = arr.GetData<Complex>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestBooleanOkDims()
        {
            bool[] tss = { true, false };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            bool[] data = arr.GetData<bool>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestShortOkDims()
        {
            short[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            short[] data = arr.GetData<short>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUshortOkDims()
        {
            ushort[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            ushort[] data = arr.GetData<ushort>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestByteOkDims()
        {
            byte[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            byte[] data = arr.GetData<byte>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestLongOkDims()
        {
            long[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            long[] data = arr.GetData<long>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUlongOkDims()
        {
            ulong[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            ulong[] data = arr.GetData<ulong>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestIntOkDims()
        {
            int[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            int[] data = arr.GetData<int>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestUintOkDims()
        {
            uint[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            Array arr = new Array(tss, dims);
            uint[] data = arr.GetData<uint>();
            Assert.AreEqual(tss, data);
        }

        [Test]
        public void TestPlusOperator()
        {
            uint[] tss = { 1, 2, 3, 4 };
            uint[] tss2 = { 1, 2, 3, 4 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr += arr2;
            Assert.AreEqual(new uint[]{2, 4, 6, 8}, arr.GetData<uint>());
        }

        [Test]
        public void TestPlusSelfOperator()
        {
            uint[] tss = { 1, 2, 3, 4, 5};
            long[] dims = { 1, 5 };
            Array arr = new Array(tss, dims);
            arr += arr;
            Assert.AreEqual(new uint[] {2, 4, 6, 8, 10}, arr.GetData<uint>());
        }

        [Test]
        public void TestSubOperator()
        {
            double[] tss = { 1, 2, 3, 4 };
            double[] tss2 = { 1, 2, 3, 4 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr -= arr2;
            Assert.AreEqual(new double[] {0, 0, 0, 0}, arr.GetData<double>());
        }

        [Test]
        public void TestMulOperator()
        {
            float[] tss = { 1, 2, 3, 4 };
            float[] tss2 = { 1, 2, 3, 4 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr *= arr2;
            Assert.AreEqual(new float[] {1, 4, 9, 16}, arr.GetData<float>());
        }

        

        [Test]
        public void TestTrueDivOperator()
        {
            float[] tss = { 1, 2, 3, 4 };
            float[] tss2 = { 1, 2, 3, 4 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr /= arr2;
            Assert.AreEqual(new float[] {1, 1, 1, 1}, arr.GetData<float>());
        }

        [Test]
        public void TestDivOperator()
        {
            short[] tss = { 1, 2, 3, 4 };
            short[] tss2 = { 1, 2, 3, 4 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            Array arrDiv = arr / arr2;
            Assert.AreEqual(new short[] { 1, 1, 1, 1 }, arrDiv.GetData<short>());
        }

        [Test]
        public void TestModOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 1, 2, 3, 4 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr %= arr2;
            Assert.AreEqual(new long[] { 0, 0, 0, 0 }, arr.GetData<long>());
        }

        [Test]
        public void TestPowOperator()
        {
            long[] tss = { 1, 2, 3, 4 };
            long[] tss2 = { 2, 2, 2, 2 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            Array arrPow = arr.Pow(arr2);
            Assert.AreEqual(new long[] { 1, 4, 9, 16 }, arrPow.GetData<long>());
        }

        [Test]
        public void TestAndOperator()
        {
            byte[] tss = { 1, 1, 1, 1 };
            byte[] tss2 = { 1, 0, 1, 0 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr &= arr2;
            Assert.AreEqual(new byte[] { 1, 0, 1, 0 }, arr.GetData<byte>());
        }

        [Test]
        public void TestOrOperator()
        {
            byte[] tss = { 1, 1, 1, 1 };
            byte[] tss2 = { 1, 0, 1, 0 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr |= arr2;
            Assert.AreEqual(new byte[] { 1, 1, 1, 1 }, arr.GetData<byte>());
        }

        [Test]
        public void TestXorOperator()
        {
            bool[] tss = { true, true, true, true };
            bool[] tss2 = { true, false, true, false };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arr2 = new Array(tss2, dims);
            arr ^= arr2;
            Assert.AreEqual(new bool[] { false, true, false, true }, arr.GetData<bool>());
        }

        [Test]
        public void TestBitShiftRightOperator()
        {
            int[] tss = { 2, 4, 6, 8 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            arr >>= 1;
            Assert.AreEqual(new int[] { 1, 2, 3, 4 }, arr.GetData<int>());
        }

        [Test]
        public void TestBitShiftLeftOperator()
        {
            int[] tss = { 2, 4, 6, 8 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            arr <<= 1;
            Assert.AreEqual(new int[] { 4, 8, 12, 16 }, arr.GetData<int>());
        }

        [Test]
        public void TestNegOperator()
        {
            int[] tss = { 1, 2, 3, 4 };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arrNeg = -arr;
            Assert.AreEqual(new int[] { -1, -2, -3, -4}, arrNeg.GetData<int>());
        }

        [Test]
        public void TestNotOperator()
        {
            bool[] tss = { true, true, true, false };
            long[] dims = { 1, 4 };
            Array arr = new Array(tss, dims);
            Array arrNot = !arr;
            Assert.AreEqual(new bool[] { false, false, false, true }, arrNot.GetData<bool>());
        }

    }
}