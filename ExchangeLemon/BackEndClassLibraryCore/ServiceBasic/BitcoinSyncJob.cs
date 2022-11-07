
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BlueLight.Main
{

    public class BitcoinSyncJob
    {

        public BitcoinSyncJob(
            //IFunctionBitcoinService functionBitcoinService,
            ApplicationContext context,
            BitcoinAddressService bitcoinAddressService,
            BtcBusinessLogicNew bitcoinFunction)
        {
            //_functionBitcoinService = functionBitcoinService;
            _context = context;
            _bitcoinAddressService = bitcoinAddressService;
            BitcoinFunction = bitcoinFunction;
        }

        public decimal ResultDiffAmount { get; internal set; }

        //public IFunctionBitcoinService _functionBitcoinService { get; }
        public ApplicationContext _context { get; }
        public BitcoinAddressService _bitcoinAddressService { get; }
        public BtcBusinessLogicNew BitcoinFunction { get; }

        string currencyCode = "btc";
        public async Task<bool> ExecuteFromAddressAsync(string bitcoinPublicAddress)
        {
            
            var detail = await GetDetailFromPublicAddress(bitcoinPublicAddress);
            if (detail == null)
            {
                return false;
            }

            //var userProfile = detail.UserProfile;
            await Logic(detail, currencyCode);
            return true;
        }

        private async Task<UserProfileDetail> GetDetailFromPublicAddress(string bitcoinPublicAddress)
        {
            UserProfileDetail detail = await _context.UserProfileDetails
                .Include(x => x.UserProfile)
                .Where(x => x.CurrencyCode == currencyCode)
            .FirstOrDefaultAsync(x => x.PublicAddress == bitcoinPublicAddress);
            return detail;
        }

        public async Task ExecuteAsync(string userName)
        {
           
            UserProfileDetail detail = await GetDetail(userName);
            await Logic(detail, currencyCode);
        }

        private async Task<UserProfileDetail> GetDetail(string userName)
        {
            var userProfile = await _context.UserProfiles
               .Include(x => x.UserProfileDetails)
           .FirstAsync(x => x.username == userName);

            UserProfileDetail detail = userProfile.GetUserProfileDetail(currencyCode);
            return detail;
        }

        async Task Logic(UserProfileDetail detail,string currencyCode)
        {
            UserProfile userProfile = detail.UserProfile;
            var detail2 = await _bitcoinAddressService.GetOrCreatePublicAddress(detail);
            var walletAddress = detail2.Item1;

            var lastItems = await _context.RemittanceIncomingTransactions
                .Where(x => x.UserProfileDetail.Id == detail.Id).ToListAsync();
            var lastItems2 = lastItems.Select(x => x.TransactionId).ToList();


            var balance = detail.IncomingRemittance;

            var output = await this.BitcoinFunction.InquiryDiff(walletAddress, balance, lastItems2);

            if (output.HasNewItem && output.NewDiffAmount > 0)
            {
                
                var newDiffAmount = output.NewDiffAmount;

                userProfile.IncomingTransfer(newDiffAmount, currencyCode);

                detail.RemittanceIncomingTransactions = detail.RemittanceIncomingTransactions ?? new List<RemittanceIncomingTransaction>();
                foreach (var item in output.NewTransactions)
                {
                    //var amount = item.Amount.ToUnit(NBitcoin.MoneyUnit.BTC);
                    //var transactionId = item.TransactionId.ToString();
                    var transactionId = item;
                    var amount = -1;
                    var input = new RemittanceIncomingTransaction()
                    {
                        Amount = amount,
                        TransactionId = transactionId,
                        UserProfileDetail = detail
                    };
                    detail.RemittanceIncomingTransactions.Add(input);
                }

                await _context.SaveChangesAsync();
                ResultDiffAmount = newDiffAmount;
            }
        }



    }


}