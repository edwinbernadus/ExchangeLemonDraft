using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Xunit;
public class UnitTestValidation
{

    [Fact]
    public async Task TestValidationOne()
    {

        var orderInput = new OrderInput();
        var order = OrderFactory.Generate(orderInput);
        var orders = new List<Order>();
        var orders2 = orders.AsQueryable();

        var orderMatchService = new DevOrderMatchService(orders2);
        //var orderMatchService = new DevOrderMatchService();
        //orderMatchService.Init(orders2);
        //orderMatchService.orders = orders2;
        var item = await orderMatchService.BuyMatchMaker(order);
        Assert.True(item == null);
    }

    [Fact]
    public void TestValidationBalance()
    {
        var validationBusinessLogic = new ValidationBusinessLogic();
        UserProfile user = new UserProfile()
        {
            UserProfileDetails = new List<UserProfileDetail>()
        };
        user.UserProfileDetails.Add(new UserProfileDetail()
        {
            CurrencyCode = "usd",
            Balance = 20000
        });
        var orderInput = new OrderInput()
        {
            CurrencyPair = "btc_usd",
            Amount = 2,
            RequestRate = 9000
        };
        var order = OrderFactory.Generate(orderInput);
        bool isValid = validationBusinessLogic.CheckBalanceSubmitOrder(user, order);
        Assert.True(isValid);
    }

    [Fact]
    public void TestValidationBalanceOne()
    {
        var validationBusinessLogic = new ValidationBusinessLogic();
        UserProfile user = new UserProfile()
        {
            UserProfileDetails = new List<UserProfileDetail>()
        };
        user.UserProfileDetails.Add(new UserProfileDetail()
        {
            CurrencyCode = "usd",
            Balance = 18000
        });
        var orderInput = new OrderInput()
        {
            CurrencyPair = "btc_usd",
            Amount = 2,
            RequestRate = 9000
        };
        var order = OrderFactory.Generate(orderInput);
        bool isValid = validationBusinessLogic.CheckBalanceSubmitOrder(user, order);
        Assert.True(isValid);
    }


    [Fact]
    public void TestValidationBalanceTwoNegative()
    {
        var validationBusinessLogic = new ValidationBusinessLogic();
        UserProfile user = new UserProfile()
        {
            UserProfileDetails = new List<UserProfileDetail>()
        };
        user.UserProfileDetails.Add(new UserProfileDetail()
        {
            CurrencyCode = "usd",
            Balance = 17999
        });
        var orderInput = new OrderInput()
        {
            CurrencyPair = "btc_usd",
            Amount = 2,
            RequestRate = 9000
        };
        var order = OrderFactory.Generate(orderInput);
        bool isValid = validationBusinessLogic.CheckBalanceSubmitOrder(user, order);
        Assert.False(isValid);
    }
}
