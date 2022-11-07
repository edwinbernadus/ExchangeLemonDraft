using System;

namespace BlueLight.Main
{
    public class OrderInput
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsBuy { get; set; }
        public decimal RequestRate { get; set; }

        public UserProfile UserProfile { get; set; }
        public string CurrencyPair { get; set; }
        public Guid GuidInput { get;  set; }
    }
}