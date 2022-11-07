using BlueLight.Main;
using System;
using System.Threading.Tasks;
using Xunit;

public class UnitTestParseError
{

    [Fact]
    public async Task TestValidationNegativeNull()
    {
        await Task.Delay(0);
        var inputTransaction = new InputTransaction();
        var userProfile = new UserProfile();

        Exception result = null;
        try
        {
            OrderFactory.Generate(inputTransaction, userProfile, true);
        }
        catch (System.Exception ex)
        {
            result = ex;
            var m2 = ex.Message;

        }

        var seperator = Environment.NewLine;
        // Assert.Equal($"Value cannot be null.\r\nParameter name: s", result.Message);
        Assert.Equal($"Value cannot be null.{seperator}Parameter name: s", result.Message);
        //System.ArgumentNullException: 'Value cannot be null.'



    }

    [Fact]
    public async Task TestValidationNegativeEmpty()
    {
        await Task.Delay(0);
        var inputTransaction = new InputTransaction()
        {
            Amount = "",
            Rate = ""
        };
        var userProfile = new UserProfile();

        Exception result = null;
        try
        {
            OrderFactory.Generate(inputTransaction, userProfile, true);
        }
        catch (System.Exception ex)
        {
            result = ex;
            var m2 = ex.Message;

        }

        Assert.Equal("Input string was not in a correct format.", result.Message);
        //System.ArgumentNullException: 'Value cannot be null.'



    }
}
