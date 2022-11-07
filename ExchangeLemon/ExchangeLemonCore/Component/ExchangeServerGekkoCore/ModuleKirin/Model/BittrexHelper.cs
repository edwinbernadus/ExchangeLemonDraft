//using gekko_bot.BittrexTrade;
//using gekko_bot.WelcomeBittrexTicker;
//using Bittrex.Net.Objects;
//using CryptoExchange.Net.Authentication;
using BlueLight.Main;
using Microsoft.AspNetCore.Http;
//using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlueLight.KirinLogic
{
    public static class BittrexHelper
    {

        public static bool UseOldSystem = false;

        public static string EnsurePairName(string market)
        {
            return "btc_usd";
            // if (market == "USDT-BTC")
            // {
            //     //var url = $"http://localhost:52494/api/v1/market/selllimit?apikey=409534a6876044b393bb675915a6f40a&nonce=1522950315&market=USDT-BTC&quantity=0.001&rate={rate}";
            //     return "btc_usd";
            // }
            // else
            // {
            //     return market;
            // }

        }

        public static async Task EnsureHasUser(string userName, ApplicationContext context)
        {
            var isExist = context.UserProfiles.Any(x => x.username == userName);
            if (isExist == false)
            {
                var user = new UserProfile();
                PopulateCurrency(user);
                user.username = userName;
                context.UserProfiles.Add(user);
                await context.SaveChangesAsync();
            }
        }

        public static void PopulateCurrency(UserProfile userProfile)
        {

            userProfile.UserProfileDetails = new List<UserProfileDetail>();
            var userProfileDetails = userProfile.UserProfileDetails;

            var currencies = UserProfile.GetDefaultCurrencies();
            foreach (var currency in currencies)
            {
                var detail = new UserProfileDetail()
                {
                    CurrencyCode = currency,
                    HoldTransactions = new List<HoldTransaction>(),
                    AccountTransactions = new List<AccountTransaction>(),
                    RemittanceTransactions = new List<RemittanceTransaction>(),
                    AdjustmentTransactions = new List<AdjustmentTransaction>()
                };

                userProfileDetails.Add(detail);


            }
        }

        public static string GetUserName(HttpRequestMessage request)
        {
            var default1 = "NoHeader";
            default1 = "guest2@server.com";
            var output = request.Headers.From ?? default1;
            return output;
        }

        public static string GetUserName(HttpRequest request)
        {
            return "bot_trade@server.com";
        }

        public static BittrexClientOptions GenerateCredential()
        {

            var option = new BittrexClientOptions()
            {
                ApiCredentials = new ApiCredentials("409534a6876044b393bb675915a6f40a", "7011570f10f841f98f1471cb350abd57")
            };
            return option;
        }

        internal static string LimitDisplay(bool isBuy)
        {
            var output = isBuy ? "LIMIT_BUY" : "LIMIT_SELL";
            return output;
        }
    }
}