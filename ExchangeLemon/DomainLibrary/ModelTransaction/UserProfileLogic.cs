namespace BlueLight.Main
{
    public class UserProfileLogic
    {
        public static decimal GetAvailableBalance(UserProfileDetail detail)
        {

            var output = detail.Balance - detail.HoldBalance;
            return output;

        }
    }

}
