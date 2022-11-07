using System.Collections.Generic;
using System.Linq;
using BlueLight.Main;
using ExchangeLemonSyncBotCore.Controllers;
using Xunit;

public class UnitTestAdjustSync
{
 
    [Fact]
    public void TestShouldBuyOrSell()
    {
        
        var adjustRateLogic = new AdjustRateLogic();
        adjustRateLogic.lemonLastRate = 7656;
        adjustRateLogic.bfxLastRate = 7608;
        var result = adjustRateLogic.ShouldBuyOrSell();
        
        Assert.Equal("sell", result);
    }

    [Fact]
    public void TestShouldBuyOrSellTwo()
    {

        var adjustRateLogic = new AdjustRateLogic();
        adjustRateLogic.lemonLastRate = 6000;
        adjustRateLogic.bfxLastRate = 7500;
        var result = adjustRateLogic.ShouldBuyOrSell();


        Assert.Equal("buy", result);
    }
}
