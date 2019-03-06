using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomationTest;


namespace EbayUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() => EbayTest.AutomationEbay();
    }
}
