using System.Collections.Generic;
using System.Linq;
using BlueLight.Main;
using Xunit;

public class UnitTestBalance
{
    [Fact]
    public void SetBalance()
    {
        var userProfile = new UserProfile()
        {
            UserProfileDetails = new List<UserProfileDetail>()
        };
        var detail = new UserProfileDetail()
        {
            CurrencyCode = "btc",
            Balance = 3,
            HoldBalance = 1,
            AdjustmentTransactions = new List<AdjustmentTransaction>()
        };

        userProfile.UserProfileDetails.Add(detail);

        userProfile.SetBalanceTesting("btc", 5);

        var history = detail.AdjustmentTransactions.First();
        Assert.Equal(2, history.AdjustmentAmount);
        Assert.Equal(1, history.PrevHoldBalance);
        Assert.Equal(5, history.RunningBalance);

        Assert.Equal(1, detail.AdjustmentTransactions.Count);



    }
}
