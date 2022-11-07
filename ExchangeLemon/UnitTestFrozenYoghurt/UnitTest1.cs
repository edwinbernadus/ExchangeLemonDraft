using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueLight.Main;

namespace UnitTestFrozenYoghurt
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = DevHelper.Log("one", "module1");
            //[{module}]: {message}
            Assert.AreEqual("[module1]: one", s);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var s = DevHelper.IsDebug();
            Assert.AreEqual(true, s);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var input = "btc_usd";
            var s = DisplayHelper.GetFirstPair(input);
            Assert.AreEqual("BTC", s);
        }
    }
}
