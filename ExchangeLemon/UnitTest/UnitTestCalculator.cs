using BlueLight.Main;
using System;
using System.Threading.Tasks;
using Xunit;

public class UnitTestCalculator
{

    [Fact]
    public async Task Scenario1()
    {
        await Task.Delay(0);
        decimal rate = 0.0001m;
        decimal amount = 0.0001m;

        var output = AmountCalculator.CalcRoundSum(amount, rate);
        //var valid = output == 0.00000001;


        Assert.Equal(0.00000001m, output);

    }


    [Fact]
    public async Task Scenario2()
    {

        await Task.Delay(0);

        decimal rate = 0.1m;
        decimal amount = 0.1m;

        var output = AmountCalculator.CalcRoundSum(amount, rate);



        //double after1 = Math.Round(output, 8,
        //    MidpointRounding.AwayFromZero); // Rounds "up"

        Assert.Equal(0.01m, output);

    }

    [Fact]
    public async Task Scenario3()
    {
        await Task.Delay(0);
        decimal rate = 0.12345678m;
        decimal amount = 0.12345678m;

        var output = AmountCalculator.CalcRoundSum(amount, rate);

        var z = 0.01524158m;
        Assert.Equal(z, output);

    }


}
