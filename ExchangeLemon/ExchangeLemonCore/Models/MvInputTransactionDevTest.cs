using BlueLight.Main;
public class MvInputTransactionDevTest
{
    public string id { get; set; }
    public double amount { get; set; }
    public double rate { get; set; }
    public bool isBuy { get; set; }

    public InputTransactionRaw Export()
    {
        var input = this;
        var output = new InputTransactionRaw()
        {
            rate = input.rate.ToString(),
            amount = input.amount.ToString(),
            mode = input.isBuy ? "buy" : "sell",
            current_pair = "btc_usd"
        };
        return output;
    }
}