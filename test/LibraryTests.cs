// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Library")]
    public class LibraryTests
    {
        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void PrintBackendInfoTest()
        {
            string[] infoSplitted;
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                Library.PrintBackendInfo();
                var info = writer.ToString();
                infoSplitted = info.Split(' ');
            }

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
            Assert.AreEqual("ArrayFire", infoSplitted[0]);
        }

        [Test]
        public void GetBackendInfoTest()
        {
            var backendInfo = Library.BackendInfo;
            var infoSplit = backendInfo.Split(' ');
            Assert.AreEqual("ArrayFire", infoSplit[0]);
        }

        [Test]
        public void SetKhivaBackendTest()
        {
            var backends = (int) Library.SupportedBackends;
            var cuda = backends & (int) Library.Backend.KhivaBackendCuda;
            var opencl = backends & (int) Library.Backend.KhivaBackendOpencl;
            var cpu = backends & (int) Library.Backend.KhivaBackendCpu;

            if (cuda != 0)
            {
                Library.CurrentBackend = Library.Backend.KhivaBackendCuda;
                Assert.AreEqual(Library.Backend.KhivaBackendCuda, Library.CurrentBackend);
            }

            if (opencl != 0)
            {
                Library.CurrentBackend = Library.Backend.KhivaBackendOpencl;
                Assert.AreEqual(Library.Backend.KhivaBackendOpencl, Library.CurrentBackend);
            }

            if (cpu == 0) return;
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
            Assert.AreEqual(Library.Backend.KhivaBackendCpu, Library.CurrentBackend);
        }

        [Test]
        public void GetDeviceIdTest()
        {
            var backends = (int) Library.SupportedBackends;
            var cuda = backends & (int) Library.Backend.KhivaBackendCuda;
            var opencl = backends & (int) Library.Backend.KhivaBackendOpencl;
            var cpu = backends & (int) Library.Backend.KhivaBackendCpu;

            if (cuda != 0)
            {
                Library.CurrentBackend = Library.Backend.KhivaBackendCuda;
                for (var i = 0; i < Library.DeviceCount; i++)
                {
                    Library.Device = i;
                    Assert.AreEqual(i, Library.Device);
                }
            }

            if (opencl != 0)
            {
                Library.CurrentBackend = Library.Backend.KhivaBackendOpencl;
                for (var i = 0; i < Library.DeviceCount; i++)
                {
                    Library.Device = i;
                    Assert.AreEqual(i, Library.Device);
                }
            }

            if (cpu == 0) return;
            {
                Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
                for (var i = 0; i < Library.DeviceCount; i++)
                {
                    Library.Device = i;
                    Assert.AreEqual(i, Library.Device);
                }
            }
        }

        [Test]
        public void GetKhivaVersionTest()
        {
            Assert.AreEqual(GetKhivaVersionFromFile(), Library.Version);
        }

        private static string GetKhivaVersionFromFile()
        {
            var version = "";
            var os = Environment.OSVersion.Platform.ToString().ToLower();

            var filePath = os.Contains("win")
                ? "C:/Program Files/Khiva/v0/include/khiva/version.h"
                : "/usr/local/include/khiva/version.h";

            var data = "";

            try
            {
                data = File.ReadAllText(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }

            var matches = Regex.Matches(data, "([0-9]+\\.[0-9]+\\.[0-9]+)");

            if (matches.Count != 0)
            {
                version = matches[0].Groups[1].Value;
            }

            return version;
        }
    }
}