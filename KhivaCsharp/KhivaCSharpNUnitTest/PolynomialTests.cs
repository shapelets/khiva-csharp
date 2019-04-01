// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace khiva.polynomial.tests
{
    [TestFixture]
    public class PolynomialTests
    {
        double DELTA = 1e-6;

        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void TestPolyfit1()
        {
            double[] tss = { 0, 1, 2, 3, 4, 5 };
            using(array.Array arr = new array.Array(tss), arr2 = new array.Array(tss), polyfit = Polynomial.Polyfit(arr, arr2, 1))
            {
                double[] expected = { 1.0, 0.0 };
                double[] result = polyfit.GetData1D<double>();
                for (int i = 0; i<result.Length; i++)
                {
                    Assert.AreEqual(expected[i], result[i], DELTA);
                }
            }
        }

        [Test]
        public void TestPolyfit3()
        {
            double[] tss = { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0 };
            double[] tss2 = { 0.0, 0.8, 0.9, 0.1, -0.8, -1.0 };
            using (array.Array arr = new array.Array(tss), arr2 = new array.Array(tss2), polyfit = Polynomial.Polyfit(arr, arr2, 3))
            {
                double[] expected = { 0.08703704, -0.81349206, 1.69312169, -0.03968254 };
                double[] result = polyfit.GetData1D<double>();
                for (int i = 0; i < result.Length; i++)
                {
                    Assert.AreEqual(expected[i], result[i], 1e-5);
                }
            }
        }

        [Test]
        public void TestRoots()
        {
            float[] tss = { 5, -20, 5, 50, -20, -40 };
            using (array.Array arr = new array.Array(tss), roots = Polynomial.Roots(arr))
            {
                Complex[] expected = { new Complex(2,0), new Complex(2, 0), new Complex(2, 0), new Complex(-1, 0), new Complex(-1, 0) };
                Complex[] result = roots.GetData1D<Complex>();
                for (int i = 0; i < result.Length; i++)
                {
                    Assert.AreEqual(expected[i].Real, result[i].Real, 1e-2);
                    Assert.AreEqual(expected[i].Imaginary, result[i].Imaginary, 1e-2);
                }
            }
        }
    }
}
