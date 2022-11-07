using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestGreyZoneNumber
    {
        [Fact]
        public void TestOne()
        {
            {
                var number1 = -0.00000001m;
                var result = OrderItemMainHelper.IsValidGreyZoneNumber(number1);
                Assert.True(result);
            }
        }


        [Fact]
        public void TestTwo()

        {
            var number1 = -0.00000005m;
            var result = OrderItemMainHelper.IsValidGreyZoneNumber(number1);
            Assert.True(result);
        }


        [Fact]
        public void TestThree()
        {
            var number1 = -0.00000006m;
            var result = OrderItemMainHelper.IsValidGreyZoneNumber(number1);
            Assert.False(result);
        }


        [Fact]
        public void TestFour()
        {
            var number1 = 0.00000001m;
            var result = OrderItemMainHelper.IsValidGreyZoneNumber(number1);
            Assert.True(result);
        }

        [Fact]
        public void TestFive()
        {
            var number1 = 0.00000005m;
            var result = OrderItemMainHelper.IsValidGreyZoneNumber(number1);
            Assert.True(result);
        }


        [Fact]
        public void TestSix()
        {
            var number1 = 0.00000006m;
            var result = OrderItemMainHelper.IsValidGreyZoneNumber(number1);
            Assert.False(result);
        }


    }
}
