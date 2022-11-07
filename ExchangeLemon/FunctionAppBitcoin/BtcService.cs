
using NBitcoin;
using QBitNinja.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{

    public class BtcService 
    {
        public static Network network = Network.TestNet;
        // public static string BaseAddress = "http://localhost:51977";
        public static string BaseAddress = "http://tapi.qbit.ninja/";


        //string ConvertSatoshiToBtc(long inputSatoshi)
        //{
        //    var money = new Money(inputSatoshi);
        //    var output = money.ToUnit(MoneyUnit.BTC);
        //    var result = output.ToString();
        //    //var output2 = (decimal)((double)inputSatoshi / 100000000);
        //    //var result = output2.ToString();
        //    return result;

        //}
        public Tuple<string, string> GenerateAddress()
        {
            var key = new Key();
            var privateKey = key.GetWif(network).ToWif();
            var address = key.PubKey.GetAddress(network).ToString();
            var output = new Tuple<string, string>(privateKey, address);
            return output;
        }

        public string GeneratePubKeyFromPrivateKey(string input)
        {
            var sourceKey = new BitcoinSecret(input);
            var address = sourceKey.PubKey.GetAddress(network).ToString();

            //var key = new Key();
            //var privateKey = input;
            //var address = key.PubKey.GetAddress(network).ToString();

            return address;
        }



        public decimal GetBalance(string inputAddr)
        {


            var url = BtcService.BaseAddress;

            var uri = new Uri(url);
            var client = new QBitNinjaClient(baseAddress: uri, network: network);

            BitcoinPubKeyAddress addr = new BitcoinPubKeyAddress(inputAddr);

            var balanceModel = client.GetBalance(dest: addr, unspentOnly: true).Result;

            if (balanceModel.Operations.Count == 0)
                return -1;
            // throw new Exception("No coins to spend");
            var unspentCoins = new List<Coin>();
            foreach (var operation in balanceModel.Operations)
                unspentCoins.AddRange(operation.ReceivedCoins.Select(coin => coin as Coin));
            var balance = unspentCoins.Sum(x => x.Amount.ToDecimal(MoneyUnit.BTC));
            return balance;
        }

    }
}






//List<Coin> GetUnspentCoins(BitcoinAddress _address, QBitNinjaClient client)
//{

//    var balanceModel = client.GetBalance(_address, true).Result;
//    if (balanceModel.Operations.Count == 0)
//    {
//        return null;
//    }
//    List<Coin> unspentCoins = new List<Coin>();
//    foreach (var operation in balanceModel.Operations)
//    {
//        unspentCoins.AddRange(operation.ReceivedCoins.Select(coin => coin as Coin));
//    }
//    var balance = unspentCoins.Sum(x => x.Amount.ToDecimal(MoneyUnit.BTC));
//    return unspentCoins;
//}



//public async Task<(string transactionId, string transactionRaw)>
//    SendMoneyDraft(string sourcePrivateKey,
//      string targetAddress,
//      long amountSatoshi,
//      long amountFeeSatoshi)
//{
//    await Task.Delay(0);
//    // Create a client
//    NBitcoin.Network m_network = BtcService.network;
//    QBitNinjaClient clientQBitNinja =
//    new QBitNinjaClient(BtcService.BaseAddress, m_network);

//    // CUSTOMER KEY
//    var sourceKey = new BitcoinSecret(sourcePrivateKey);
//    var sourcePubKey = sourceKey.PubKey;
//    var sourceAddress = sourceKey.GetAddress();

//    // SELLER KEY
//    BitcoinAddress targetAddressObject = BitcoinAddress.Create(targetAddress, m_network);



//    var sourceUnspentCoins = GetUnspentCoins(sourceAddress, clientQBitNinja);

//    var amount = ConvertSatoshiToBtc(amountSatoshi);
//    var txBuilder = new TransactionBuilder();
//    var transaction = txBuilder
//        .AddCoins(sourceUnspentCoins)
//        .AddKeys(sourceKey.PrivateKey)
//        .Send(targetAddressObject, amount)
//        .SendFees(ConvertSatoshiToBtc(amountFeeSatoshi))
//        .SetChange(sourceKey.GetAddress())
//        .BuildTransaction(true);
//    var verification = txBuilder.Verify(transaction); //check fully signed


//    var m = transaction.ToHex();

//    (string transactionId, string transactionRaw) output = (m, transaction.ToString());
//    return output;
//}

//async Task<string> OldSendMoney(string sourcePrivateKey,
//    string targetAddress,
//    long amountSatoshi,
//    long amountFeeSatoshi)
//{
//    await Task.Delay(0);
//    // Create a client
//    NBitcoin.Network m_network = BtcService.network;
//    string BaseAddress = BtcService.BaseAddress;

//    QBitNinjaClient clientQBitNinja =
//    new QBitNinjaClient(BaseAddress, m_network);
//    //new QBitNinjaClient("http://api.qbit.ninja/", m_network);

//    // CUSTOMER KEY
//    var sourceKey = new BitcoinSecret(sourcePrivateKey);
//    var sourcePubKey = sourceKey.PubKey;
//    var sourceAddress = sourceKey.GetAddress();

//    // SELLER KEY
//    BitcoinAddress targetAddressObject = BitcoinAddress.Create(targetAddress, m_network);


//    //BalanceModel balanceModel = clientQBitNinja.GetBalance(dest: sourceAddress).Result;
//    //var items = balanceModel.Operations.ToList();

//    //var item = items.First();
//    //List<TxOut> txOuts = item.ReceivedCoins.Select(x => x.TxOut).ToList();




//    var sourceUnspentCoins = GetUnspentCoins(sourceAddress, clientQBitNinja);

//    var amount = ConvertSatoshiToBtc(amountSatoshi);
//    var txBuilder = new TransactionBuilder();
//    var transaction = txBuilder
//        .AddCoins(sourceUnspentCoins)
//        .AddKeys(sourceKey.PrivateKey)
//        .Send(targetAddressObject, amount)
//        .SendFees(ConvertSatoshiToBtc(amountFeeSatoshi))
//        .SetChange(sourceKey.GetAddress())
//        .BuildTransaction(true);
//    var verification = txBuilder.Verify(transaction); //check fully signed


//    // ErrorCode INVALID Sucess true  BroadcastResponse

//    var m = transaction.ToHex();


//    //var b = BtcCloudService.Generate();
//    //string rawContent = transaction.ToString();
//    //await b.PushRawTransaction(m, rawContent);


//    // BROADCAST TRANSACTION
//    BroadcastResponse broadcastResponse = clientQBitNinja.Broadcast(transaction).Result;
//    var transactionId = transaction.GetHash();

//    GetTransactionResponse resultTransaction = clientQBitNinja.GetTransaction(transactionId).Result;
//    var result = resultTransaction.TransactionId.ToString();
//    return result;
//}

