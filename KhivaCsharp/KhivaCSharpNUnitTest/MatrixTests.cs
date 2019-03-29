using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva.matrix.tests
{
    [TestFixture]
    public class MatrixTests
    {
        double DELTA = 1e-6;

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
            using (array.Array arr = new array.Array(tss))
            {
                var (p, i) = Matrix.StompSelfJoin(arr, 3);
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
        }


        [Test]
        public void TestFindbestNDiscords()
        {
        }

        [Test]
        public void TestFindbestNMotifs()
        {
        }

    }
}
