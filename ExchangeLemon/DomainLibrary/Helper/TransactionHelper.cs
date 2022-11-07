using BlueLight.Main;
using System;
using System.Collections.Generic;
using System.Linq;
// context?.CreateSession();
// context?.UpdateMain(inputOrder,
// oppositeOrder,
// inputOrder.UserProfile,
// oppositeOrder.UserProfile);
// context?.UpdateTransaction();



//private void UpdateOrder(Transaction transaction, Order buyOrder, Order sellOrder)
//{
//    var amount = transaction.Amount;
//    buyOrder.UpdateAmount(amount);
//    sellOrder.UpdateAmount(amount);
//}


public class TransactionHelper
{

    public static string[] usernames = new string[] { "bot_sync@server.com" , "bot_trade@server.com"};

  
    public static void AddHold(UserProfile userProfile, decimal leftAmount, 
        Order order, string currencyCode)
    {
        

        var detail = userProfile.GetUserProfileDetail(currencyCode);

        var amount = leftAmount;
        var availableBalance = UserProfileLogic.GetAvailableBalance(detail);

      
        var logic = new UserProfileDetailLogic();
        var hold = logic.AddHold(detail,leftAmount, order, currencyCode);
        detail.HoldTransactions = detail.HoldTransactions ?? new List<HoldTransaction>();
        detail.HoldTransactions.Add(hold);
    }

    internal static void RemoveHold(UserProfile userProfile, decimal leftAmount, Order order, string currencyCode)
    {
        var detail = userProfile.GetUserProfileDetail(currencyCode);
        var logic = new UserProfileDetailLogic();
        var hold = logic.RemoveHold(detail,leftAmount, order, currencyCode);
        detail.HoldTransactions = detail.HoldTransactions ?? new List<HoldTransaction>();
        detail.HoldTransactions.Add(hold);
    }
}