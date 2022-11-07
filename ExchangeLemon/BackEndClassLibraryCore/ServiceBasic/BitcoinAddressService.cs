using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class BitcoinAddressService {
        private ApplicationContext db;
        private IFunctionBitcoinService repo;

        public BitcoinAddressService (ApplicationContext db,
            IFunctionBitcoinService repo, SignalDashboard dashboardService) {
            this.db = db;
            this.repo = repo;
            this.dashboardService = dashboardService;
        }

        public SignalDashboard dashboardService { get; }

        public async Task<Tuple<string,bool>> GetOrCreatePublicAddress (UserProfileDetail detail) {

            if (detail.PrivateKey == null) {

                // var repo = new FunctionBitcoinService ();
                var item = await repo.GenerateAddress ();
                //var item = await GetPublicAddress();
                //var w = JsonConvert.DeserializeObject<BitcoinGenerateAddress>(item);

                var privateKey = item.PrivateKey;
                var address = item.PublicAddress;

                detail.PrivateKey = privateKey;
                detail.PublicAddress = address;

                await db.SaveChangesAsync ();

                
                await dashboardService.Submit ("bitcoinAddressGenerate");

                //return address;
                return new Tuple<string, bool>(address, true);
            } else {
                var output = detail.PublicAddress;

                return new Tuple<string,bool>(output,false);

            }

        }
    }
}