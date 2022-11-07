namespace BlueLight.Main
{
    public static class UserProfileDebugExtension
    {
        public static bool IsAvailableBalanceNegative(this UserProfile userProfile)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var details = UserProfileDetails;
            var output = false;
            foreach (var detail in details)
            {

                var availableBalance = UserProfileLogic.GetAvailableBalance(detail);
                var detected = availableBalance < 0;
                if (detected)
                {
                    output = true;
                }

            }
            return output;
        }

        public static bool IsHoldBalanceNegative(this UserProfile userProfile)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var details = UserProfileDetails;
            var output = false;
            foreach (var detail in details)
            {

                var holdBalance = detail.HoldBalance;
                var detected = holdBalance < 0;
                if (detected)
                {
                    output = true;
                }

            }
            return output;
        }

    }

}