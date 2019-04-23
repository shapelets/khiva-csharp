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
using System.IO;
using System.Text.RegularExpressions;

namespace khiva.tests
{
    [TestFixture, Category("Library")]
    public class LibraryTests
    {

        [SetUp]
        public void Init()
        {
            Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
        }

        [Test]
        public void PrintBackendInfoTest()
        {
            string[] info_splitted;
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Khiva.PrintBackendInfo();
                string info = writer.ToString();
                info_splitted = info.Split(' ');
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
            Assert.AreEqual("ArrayFire", info_splitted[0]);
        }

        [Test]
        public void GetBackendInfoTest()
        {
            String backend_info = Khiva.BackendInfo;
            string[] info_split = backend_info.Split(' ');
            Assert.AreEqual("ArrayFire", info_split[0]);
        }

        [Test]
        public void SetKhivaBackendTest()
        {
            int backends = (int)Khiva.SupportedBackends;
            int cuda = backends & (int)Khiva.Backend.KHIVA_BACKEND_CUDA;
            int opencl = backends & (int)Khiva.Backend.KHIVA_BACKEND_OPENCL;
            int cpu = backends & (int)Khiva.Backend.KHIVA_BACKEND_CPU;

            if (cuda != 0)
            {
                Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CUDA;
                Assert.AreEqual(Khiva.Backend.KHIVA_BACKEND_CUDA, Khiva.ActualBackend);
            }

            if (opencl != 0)
            {
                Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_OPENCL;
                Assert.AreEqual(Khiva.Backend.KHIVA_BACKEND_OPENCL, Khiva.ActualBackend);
            }

            if (cpu != 0)
            {
                Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
                Assert.AreEqual(Khiva.Backend.KHIVA_BACKEND_CPU, Khiva.ActualBackend);
            }
        }



        [Test]
        public void GetDeviceIDTest()
        {
            int backends = (int)Khiva.SupportedBackends;
            int cuda = backends & (int)Khiva.Backend.KHIVA_BACKEND_CUDA;
            int opencl = backends & (int)Khiva.Backend.KHIVA_BACKEND_OPENCL;
            int cpu = backends & (int)Khiva.Backend.KHIVA_BACKEND_CPU;

            if (cuda != 0)
            {
                Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CUDA;
                for (int i = 0; i < Khiva.DeviceCount; i++)
                {
                    Khiva.Device = i;
                    Assert.AreEqual(i, Khiva.Device);
                }
            }

            if (opencl != 0)
            {
                Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_OPENCL;
                for (int i = 0; i < Khiva.DeviceCount; i++)
                {
                    Khiva.Device = i;
                    Assert.AreEqual(i, Khiva.Device);
                }
            }

            if (cpu != 0)
            {
                Khiva.ActualBackend = Khiva.Backend.KHIVA_BACKEND_CPU;
                for (int i = 0; i < Khiva.DeviceCount; i++)
                {
                    Khiva.Device = i;
                    Assert.AreEqual(i, Khiva.Device);
                }
            };
        }

        [Test]
        public void GetKhivaVersionTest()
        {
            Assert.AreEqual(GetKhivaVersionFromFile(), Khiva.Version);
        }

        private String GetKhivaVersionFromFile()
        {
            String version = "";
            String filePath;
            String Os = System.Environment.OSVersion.Platform.ToString().ToLower();

            if (Os.Contains("win"))
            {
                filePath = "C:/Program Files/Khiva/v0/include/khiva/version.h";
            }
            else
            {
                filePath = "/usr/local/include/khiva/version.h";
            }

            String data = "";

            try
            {
                data = File.ReadAllText(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }

            MatchCollection matches = Regex.Matches(data, "([0-9]+\\.[0-9]+\\.[0-9]+)");

            if (matches.Count != 0)
            {
                version = matches[0].Groups[1].Value;
            }

            return version;

        }
    }
}