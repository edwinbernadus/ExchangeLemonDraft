using System;
using System.Threading.Tasks;

namespace BlueLight.Main {
    internal class BittrexClient : IDisposable {
        private BittrexClientOptions option;

        public BittrexClient () { }

        public BittrexClient (BittrexClientOptions option) {
            this.option = option;
        }

        public void Dispose () {
            //throw new NotImplementedException();
        }

        internal Task<dynamic> GetMarketHistoryAsync (string market) {
            throw new NotImplementedException ();
        }

        internal Task<dynamic> GetOpenOrdersAsync (string market) {
            throw new NotImplementedException ();
        }

        internal Task<dynamic> GetTickerAsync (string market) {
            throw new NotImplementedException ();
        }

        internal Task<Balance> GetBalancesAsync () {
            throw new NotImplementedException ();
        }

        internal Task<dynamic> GetOrderAsync (Guid guid) {
            throw new NotImplementedException ();
        }
    }

}