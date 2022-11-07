using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestFilterGreyNumber
    {
        [Fact]
        public void TestError()
        {
            {
                var number1 = -0.00000101m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(-0.00000101m, result);
            }
        }

        [Fact]
        public void TestOne()
        {
            {
                var number1 = -0.00000001m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(-0.00000001m,result);
            }
        }

        [Fact]
        public void TestTwo()
        {
            {
                var number1 = -0.00000006m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(-0.00000010m, result);
            }
        }

        [Fact]
        public void TestThree()
        {
            {
                var number1 = -0.00000010m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(-0.00000010m, result);
            }
        }

        [Fact]
        public void TestFour()
        {
            {
                var number1 = -0.00000011m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(-0.00000015m, result);
            }
        }

        [Fact]
        public void TestFive()
        {
            {
                var number1 = 0.00000006m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(0.00000010m, result);
            }
        }

        [Fact]
        public void TestSix()
        {
            {
                var number1 = 0.00000010m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(0.00000010m, result);
            }
        }

        [Fact]
        public void TestSeven()
        {
            {
                var number1 = 0.00000011m;
                var result = OrderItemMainHelper.FilterGreyNumber(number1);
                Assert.Equal(0.00000015m, result);
            }
        }
    }
}
