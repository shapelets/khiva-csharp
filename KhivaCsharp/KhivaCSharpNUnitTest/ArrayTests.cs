using NUnit.Framework;
using khiva.array;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva.array.Tests
{
    [TestFixture]
    public class ArrayTests
    {
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
        public unsafe void TestDoubleOkDims()
        {
            double[] tss = { 1, 2 };
            long[] dims = { 1, 2 };
            try
            {
                Array arr = new Array(tss, dims);
                void* data = arr.GetData();
                IntPtr dataPtr = new IntPtr(data); // Search how to get double from IntPtr
                Assert.AreEqual(tss, dataPtr);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Mismatching dims and array size");
            }
        }
    }
}