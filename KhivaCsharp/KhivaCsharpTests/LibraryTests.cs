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
        [TestMethod()]
        public void VersionTest()
        {
            /*Console.WriteLine(Library.Version());
            Console.ReadKey();
            Assert.AreEqual("v0.2.1", Library.Version());*/
        }

        [TestMethod()]
        public void GetBackendTest()
        {
            Console.WriteLine(Library.GetBackend());
        }
    }
}