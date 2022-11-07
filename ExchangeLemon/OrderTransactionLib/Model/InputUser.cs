using System;

namespace BlueLight.Main
{
    public class InputUser
    {
        public InputUser(string userName)
        {
            this.UserProfileName = userName;
        }

        public InputUser(UserProfile userProfile)
        {
            UserProfileTesting = userProfile;
        }

        public UserProfile UserProfileTesting { get; set; }
        public string UserProfileName { get; set; }

        internal string GetUserName()
        {
            var output = UserProfileName ?? UserProfileTesting?.username;
            return output;
        }
    }
}