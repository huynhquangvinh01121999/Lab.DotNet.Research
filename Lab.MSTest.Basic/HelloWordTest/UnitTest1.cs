using HelloWordCore.Models;
using HelloWordTest.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace HelloWordTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string Expected1 = "Xin chào!";
        private const string Expected2 = "Hello World!";

        [TestMethod]
        [DataRow(1, 2, DisplayName = "Functional Case FC100.1")]
        public void TestMethod(int i, int j) { }

        [TestMethod]
        [DataRow(null)]
        public void TestMethod1(object o)
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                HelloWordCore.Program.Main();
                var result = sw.ToString().Trim();
                Assert.AreNotEqual(Expected1, result);
            }
        }

        [TestMethod]
        [CustomDataRowAttribute(1)]
        public void TestMethod2(int i) {
            Assert.AreEqual(1, i);
        }

        //[TestMethod]
        //public void TestMethod3()
        //{
        //    using (var sw = new StringWriter())
        //    {
        //        Console.SetOut(sw);
        //        HelloWordCore.Program.Main();
        //        var result = sw.ToString().Trim();
        //        Assert.AreEqual(Expected2, result);
        //    }
        //}

        //[TestMethod]
        //[DataRow(1, "message", true, 2.0)]
        //public void TestMethod4(int i, string s, bool b, float f) { }

        //[TestMethod]
        //[DataRow(new string[] { "line1", "line2" })]
        //public void TestMethod5(string[] lines) { }

        //[TestMethod]
        //[DataRow(null)]
        //public void TestMethod6(object o) { }

        [TestMethod]
        public void TestMethodModel(MethodModel model)
        {

        }
    }
}
