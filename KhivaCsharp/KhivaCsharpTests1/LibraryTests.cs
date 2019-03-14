using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhivaCsharp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhivaCsharp.library.Tests
{
    [TestClass()]
    public class LibraryTests
    {
        [TestMethod]
        public void VersionTest()
        {
            Assert.AreEqual("v0.2.1", Library.Version());
        }
        
        [TestMethod]
        public void GetBackendTest()
        {
            Assert.AreEqual(Library.Backend.KHIVA_BACKEND_OPENCL, Library.GetBackend());
        }
    }
}