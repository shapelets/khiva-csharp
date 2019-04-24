// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using khiva.array;

namespace khiva.linalg.tests
{
    [TestFixture, Category("Linalg")]
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
            using (KhivaArray a = KhivaArray.Create(tss), b = KhivaArray.Create(tss2), lls = Linalg.Lls(a, b))
            {
                float[,] result = lls.GetData2D<float>();
                Assert.AreEqual(1, result[0, 0], DELTA);
                Assert.AreEqual(1, result[0, 1], DELTA);
            }
        }
    }
}
