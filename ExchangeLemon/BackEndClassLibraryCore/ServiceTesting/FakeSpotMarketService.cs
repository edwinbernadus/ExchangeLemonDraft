using System.Collections.Generic;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main {
    public class FakeSpotMarketService {
        private readonly ApplicationContext db;

        public FakeSpotMarketService(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task EnsureDataPopulated () {
            var isExist = await db.SpotMarkets.AnyAsync ();
            if (isExist == false) {
                var output = new List<SpotMarket> ();
                output.Add (new SpotMarket () {
                    Id = 0,
                        //CurrencyPair = "btc_idr",

                        CurrencyPair = "btc_usd",
                        PercentageMovement = 1,
                        LastRate = 9500,

                });
                output.Add (new SpotMarket () {
                    Id = 0,
                        //CurrencyPair = "eth_idr",
                        CurrencyPair = "eth_usd",
                        PercentageMovement = 2,
                        LastRate = 700,
                });
                output.Add (new SpotMarket () {
                    Id = 0,
                        //CurrencyPair = "ltc_idr",

                        CurrencyPair = "ltc_usd",
                        PercentageMovement = 3,
                        LastRate = 190,
                });

                output.Add (new SpotMarket () {
                    Id = 0,
                        //CurrencyPair = "btc_idr",

                        CurrencyPair = "btc_idr",
                        PercentageMovement = 4,
                        LastRate = 7500,

                });

                db.SpotMarkets.AddRange (output);

                output.Add (new SpotMarket () {
                    Id = 0,
                        //CurrencyPair = "ltc_idr",

                        //CurrencyPair = "btc_xlm",
                        CurrencyPair = "xlm_btc",
                        PercentageMovement = 4,
                        //LastRate = 3144,
                        LastRate = 0.00003145m,

                });
                db.SpotMarkets.AddRange (output);

                await db.SaveChangesAsync ();
            }

        }
    }
}