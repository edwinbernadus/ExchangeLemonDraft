using System;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;

namespace BlueLight.Main {
    public class RegisterBitcoinController : Controller {
        private ApplicationContext context;

        public RegisterBitcoinController (IFunctionBitcoinService repo) {
            this.context = null;
            Repo = repo;
            //new ApplicationContext ();
        }

        public IFunctionBitcoinService Repo { get; }

        [HttpPost]
        public async Task<bool> Post () {
            //var c = new BitcoinAddressHelper(context);
            // var repo = new FunctionBitcoinService ();
            var addresses = context.UserProfileDetails
                .Where (x => String.IsNullOrEmpty (x.PublicAddress) == false && x.CurrencyCode == "btc")
                .Select (x => x.PublicAddress).ToList ();
            foreach (var i in addresses) {

                await Repo.Register (i);
                //await c.Register(i);
                //await Register(i);
            }

            return true;

        }

    }
}