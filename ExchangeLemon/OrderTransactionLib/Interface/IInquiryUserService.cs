using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IInquiryUserService
    {
        Task<UserProfile> GetUser(InputUser input);
    }
}