using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class InquiryUserService : IInquiryUserService
    {


        public InquiryUserService(RepoUser repoUser)
        {
            RepoUser = repoUser;
        }

        public RepoUser RepoUser { get; }



        public async Task<UserProfile> GetUser(InputUser input)
        {
            var output = await this.RepoUser.GetUser(input.UserProfileName);
            return output;
        }
    }
}