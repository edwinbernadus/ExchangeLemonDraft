namespace BlueLight.Main
{
      public class MvPendingOrderLite
    {
        public decimal Rate {get;set;}
        public decimal Amount {get;set;}
        public decimal LeftAmount {get;set;}
        public bool IsBuy {get;set;}
        public long OrderId {get;set;}
    }
}