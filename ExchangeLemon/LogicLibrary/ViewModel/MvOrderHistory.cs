namespace BlueLight.Main
{
    public class MvOrderHistory
    {
        public int Id { get; set; }


        public decimal RunningAmount { get; set; }
        public decimal RunningLeftAmount { get; set; }

        public decimal RequestRate { get; set; }
        public string CurrencyPair { get; set; }
        public bool IsBuy { get; set; }

        public int TransactionId { get; set; }
        public int OrderId { get; set; }

        public MvOrderHistory()
        {

        }

        public MvOrderHistory(OrderHistory input)
        {
            this.Id = input.Id;

            this.RunningAmount = input.RunningAmount;
            this.RunningLeftAmount = input.RunningLeftAmount;

            this.RequestRate = input.RequestRate;
            this.IsBuy = input.IsBuy;
            this.CurrencyPair = input.CurrencyPair;

            this.TransactionId = input.Transaction?.Id ?? -1;
            this.OrderId = input.Order?.Id ?? -1;
        }
    }
}