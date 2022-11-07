namespace BlueLight.Main.Tests
{
    public class FakeInsertMoneyHelper
    {
        public static void Execute(UserProfile userProfile)
        {
            foreach (var item in userProfile.UserProfileDetails)
            {
                var currency = item.CurrencyCode;
                userProfile.IncomingTransfer(10 * 10000, currency);
            }
        }

    }
}