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

namespace khiva.regularization.tests
{
    [TestFixture, Category("Regularization")]
    public class RegularizationTests
    {
        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestGroupBySingleColumn()
        {
            int[,] tss = { { 0, 1, 1, 2, 2, 3 }, { 0, 3, 3, 1, 1, 2 } };
            using(KhivaArray arr = KhivaArray.Create(tss), groupBy = Regularization.GroupBy(arr, 0))
            {
                int[] expected = { 0, 3, 1, 2 };
                int[] result = groupBy.GetData1D<int>();
                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        public void TestGroupByDoubleKeyColumn()
        {
            float[,] tss = { { 0, 1, 1, 2, 2, 3 }, { 1, 2, 2, 3, 3, 4 }, { 0, 3, 3, 1, 1, 2 } };
            using (KhivaArray arr = KhivaArray.Create(tss), groupBy = Regularization.GroupBy(arr, 0, 2, 1))
            {
                float[] expected = { 0, 3, 1, 2 };
                float[] result = groupBy.GetData1D<float>();
                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        public void TestGroupByDoubleKeyColumn2()
        {
            float[,] tss = { { 0, 0, 1, 1, 1 }, { 0, 1, 0, 0, 1 }, { 1, 2, 3, 4, 5 } };
            using (KhivaArray arr = KhivaArray.Create(tss), groupBy = Regularization.GroupBy(arr, 0, 2, 1))
            {
                float[] expected = { 1, 2, 3.5F, 5 };
                float[] result = groupBy.GetData1D<float>();
                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        public void TestGroupByDoubleKeyDoubleValueColumn()
        {
            float[,] tss = { { 0, 0, 0, 2, 2 }, { 2, 2, 2, 4, 4 }, { 0, 1, 2, 3, 4 }, { 1, 1, 1, 1, 1 } };
            using (KhivaArray arr = KhivaArray.Create(tss), groupBy = Regularization.GroupBy(arr, 0, 2, 2))
            {
                float[,] expected = { { 1, 3.5F }, { 1, 1 } };
                float[,] result = groupBy.GetData2D<float>();
                Assert.AreEqual(expected, result);
            }
        }

    }
}
