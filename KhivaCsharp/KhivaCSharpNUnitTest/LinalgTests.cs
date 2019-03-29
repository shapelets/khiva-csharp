using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva.linalg.tests
{
    [TestFixture]
    public class LinalgTests
    {
        double DELTA = 1e-6;

        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void Testlls()
        {
            float[,] tss = { { 4, 3 }, { -1, -2 } };
            float[] tss2 = { 3, 1 };
            using (array.Array a = new array.Array(tss), b = new array.Array(tss2), lls = Linalg.Lls(a, b))
            {
                float[,] result = lls.GetData2D<float>();
                Assert.AreEqual(1, result[0, 0], DELTA);
                Assert.AreEqual(1, result[0, 1], DELTA);
            }

        }
    }
}
