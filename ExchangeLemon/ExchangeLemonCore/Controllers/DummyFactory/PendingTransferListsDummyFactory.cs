using System;
using System.Collections.Generic;
using System.Linq;
using BlueLight.Main;

namespace ExchangeLemonCore.Controllers.Admin
{
    public class PendingTransferListsDummyFactory
    {
        public List<MvPendingTransferList> GenerateThree()
        {

            var output = GenerateTwo().Select(x => new MvPendingTransferList(x)).ToList();

            //var output = GenerateTwo().Select(x => new MvPendingTransferList()
            //{
            //    Amount = x.Amount,
            //    CreatedDate = x.CreatedDate,
            //    CurrencyCode = x.AccountTransaction.CurrencyCode,
            //    Id = x.Id,
            //    UserName = x.UserProfileDetail.UserProfile.username
            //}).ToList();


            return output;

        }

        public List<PendingTransferList> GenerateTwo()
        {

            var output = new List<PendingTransferList>();
            output.Add(new PendingTransferList()
            {
                Id = 1,
                UserProfileDetail = new UserProfileDetail()
                {
                    UserProfile = new UserProfile()
                    {
                        username = "user01"
                    }
                },
                AccountTransaction = new AccountTransaction()
                {
                    CurrencyCode = "btc"
                },
                Amount = 1.2m,
                CreatedDate = DateTime.Now
            });

            output.Add(new PendingTransferList()
            {
                Id = 1,
                UserProfileDetail = new UserProfileDetail()
                {
                    UserProfile = new UserProfile()
                    {
                        username = "user02"
                    }
                },
                AccountTransaction = new AccountTransaction()
                {
                    CurrencyCode = "btc"
                },
                Amount = 0.2m,
                CreatedDate = DateTime.Now
            });


            return output;

        }

        public List<MvPendingTransferList> Generate()
        {

            var output = new List<MvPendingTransferList>();
            output.Add(new MvPendingTransferList()
            {
                Id = 1,
                UserName = "user01",
                CurrencyCode = "btc",
                Amount = 1.2m,
                CreatedDate = DateTime.Now
            });

            output.Add(new MvPendingTransferList()
            {
                Id = 2,
                UserName = "user02",
                CurrencyCode = "btc",
                Amount = 0.2m,
                CreatedDate = DateTime.Now
            });

            return output;

        }
    }
}
