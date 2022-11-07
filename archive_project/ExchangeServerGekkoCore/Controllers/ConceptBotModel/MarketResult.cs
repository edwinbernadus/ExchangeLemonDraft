using System;
using Newtonsoft.Json;
//using gekko_bot.BittrexTrade;
//using gekko_bot.WelcomeBittrexTicker;

namespace BlueLight.Main {
    public partial class MarketResult {
        [JsonProperty ("success")]
        public bool Success { get; set; }

        [JsonProperty ("message")]
        public string Message { get; set; }

        [JsonProperty ("result")]
        public MarketResultDetail Result { get; set; }

        //{
        //"success" : true,
        //"message" : "",
        //"result" : null
        //}

        public static string GenerateCancel () {
            var output = new MarketResult () {
                Success = true,
                Message = "",
                Result = null
            };

            var result = JsonConvert.SerializeObject (output);
            return result;
        }

        public static MarketResult GenerateCancelModel () {
            var output = new MarketResult () {
                Success = true,
                Message = "",
                Result = null
            };

            return output;
        }

        public static MarketResult Generate (string id) {

            //var newGuid = guid;
            var output = new MarketResult () {
                Success = true,
                Message = "",
                Result = new MarketResultDetail () {
                Uuid = id.ToString (),
                //OrderUuid = id.ToString(),
                }
            };

            return output;

        }

        public static Tuple<string, string> Generate () {
            var newGuid = Guid.NewGuid ().ToString ().ToLower ();
            var output = new MarketResult () {
                Success = true,
                Message = "",
                Result = new MarketResultDetail () {
                Uuid = newGuid
                }
            };

            var result = JsonConvert.SerializeObject (output);
            var result2 = new Tuple<string, string> (result, newGuid);
            return result2;
        }

        public static Tuple<MarketResult, string> GenerateModel () {
            var newGuid = Guid.NewGuid ().ToString ().ToLower ();
            var output = new MarketResult () {
                Success = true,
                Message = "",
                Result = new MarketResultDetail () {
                Uuid = newGuid
                }
            };

            var result2 = new Tuple<MarketResult, string> (output, newGuid);
            return result2;
        }
    }
}