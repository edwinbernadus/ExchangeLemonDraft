using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class AddExternalManuallyCommandHandler : IRequestHandler<AddExternalManuallyCommand, bool>
    {
        private readonly ApplicationContext _applicationContext;

        public AddExternalManuallyCommandHandler(ApplicationContext applicationContext,RepoUser repoUser)
        {
            _applicationContext = applicationContext;
            RepoUser = repoUser;
        }

        public RepoUser RepoUser { get; }

        public async Task<bool> Handle(AddExternalManuallyCommand request, CancellationToken cancellationToken)
        {
            
            var userName = request.userName;
            var userProfile = await this.RepoUser.GetUser(userName);
            var currencyCode = request.currencyCode;
            var amount2 = request.amount2;

            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == currencyCode);




            var newBalance = detail.Balance + amount2;
            var accountTransaction = new AccountTransaction()
            {
                Amount = amount2,
                RunningBalance = detail.Balance + amount2,
                CurrencyCode = currencyCode,
                IsExternal = true
            };

            detail.AccountTransactions = detail.AccountTransactions ?? new List<AccountTransaction>();
            detail.AccountTransactions.Add(accountTransaction);

            detail.Balance = newBalance;

            await this._applicationContext.SaveChangesAsync();
            return true;
        }
    }
}