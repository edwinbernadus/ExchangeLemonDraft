using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class DevInquiryUserService : IInquiryUserService
    {


        public async Task<UserProfile> GetUser(InputUser input)
        {
            await Task.Delay(0);
            return input.UserProfileTesting;
        }
    }
}