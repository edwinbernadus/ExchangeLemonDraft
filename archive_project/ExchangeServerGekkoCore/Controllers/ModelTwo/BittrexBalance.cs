namespace BlueLight.Main {
    internal class BittrexBalance {
        public BittrexBalance () { }

        //public class BalanceDetail
        //{
        public string Currency { get; internal set; }
        public double? Balance { get; internal set; }
        public double? Available { get; internal set; }
        public int Pending { get; internal set; }
        public string CryptoAddress { get; internal set; }
        //}
    }
}