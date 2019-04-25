// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("LinAlg")]
    public class LinAlgTests
    {
        private const double Delta = 1e-6;

        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestLls()
        {
            float[,] tss = {{4, 3}, {-1, -2}};
            float[] tss2 = {3, 1};
            using (KhivaArray a = KhivaArray.Create(tss), b = KhivaArray.Create(tss2), lls = LinAlg.Lls(a, b))
            {
                var result = lls.GetData2D<float>();
                Assert.AreEqual(1, result[0, 0], Delta);
                Assert.AreEqual(1, result[0, 1], Delta);
            }
        }
    }
}