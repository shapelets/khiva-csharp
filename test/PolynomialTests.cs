// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Numerics;
using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Polynomial")]
    public class PolynomialTests
    {
        private const double Delta = 1e-6;

        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestPolyFit1()
        {
            double[] tss = {0, 1, 2, 3, 4, 5};
            using (KhivaArray arr = KhivaArray.Create(tss),
                arr2 = KhivaArray.Create(tss),
                polyFit = Polynomial.PolyFit(arr, arr2, 1))
            {
                double[] expected = {1.0, 0.0};
                var result = polyFit.GetData1D<double>();
                for (var i = 0; i < result.Length; i++)
                {
                    Assert.AreEqual(expected[i], result[i], Delta);
                }
            }
        }

        [Test]
        public void TestPolyFit3()
        {
            double[] tss = {0.0, 1.0, 2.0, 3.0, 4.0, 5.0};
            double[] tss2 = {0.0, 0.8, 0.9, 0.1, -0.8, -1.0};
            using (KhivaArray arr = KhivaArray.Create(tss),
                arr2 = KhivaArray.Create(tss2),
                polyFit = Polynomial.PolyFit(arr, arr2, 3))
            {
                double[] expected = {0.08703704, -0.81349206, 1.69312169, -0.03968254};
                var result = polyFit.GetData1D<double>();
                for (var i = 0; i < result.Length; i++)
                {
                    Assert.AreEqual(expected[i], result[i], 1e-5);
                }
            }
        }

        [Test]
        public void TestRoots()
        {
            float[] tss = {5, -20, 5, 50, -20, -40};
            using (KhivaArray arr = KhivaArray.Create(tss), roots = Polynomial.Roots(arr))
            {
                Complex[] expected =
                    {new Complex(2, 0), new Complex(2, 0), new Complex(2, 0), new Complex(-1, 0), new Complex(-1, 0)};
                var result = roots.GetData1D<Complex>();
                for (var i = 0; i < result.Length; i++)
                {
                    Assert.AreEqual(expected[i].Real, result[i].Real, 1e-2);
                    Assert.AreEqual(expected[i].Imaginary, result[i].Imaginary, 1e-2);
                }
            }
        }
    }
}