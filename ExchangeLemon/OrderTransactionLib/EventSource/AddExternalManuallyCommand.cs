using MediatR;

namespace BlueLight.Main
{
    public class AddExternalManuallyCommand : IRequest<bool>
    {
        // public static void AddExternalManually(this UserProfile userProfile, decimal amount2, string currencyCode)
        // public UserProfile userProfile { get; set; }
        public string userName { get; set; }
        public decimal amount2 { get; set; }
        public string currencyCode { get; set; }
    }
}