using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.EntityFrameworkCore;

namespace BackEndClassLibrary
{
    public class RemittanceOutgoingCheckAllManualService
    {

        private readonly ApplicationContext _applicationContext;

        private readonly IRemittanceOutgoingValidatorService _remittanceOutgoingValidatorService;

        public RemittanceOutgoingCheckAllManualService(ApplicationContext applicationContext, 
            IRemittanceOutgoingValidatorService remittanceOutgoingValidatorService)
        {
            _remittanceOutgoingValidatorService = remittanceOutgoingValidatorService;
            _applicationContext = applicationContext;
        }

        public async Task<string> Execute(long userProfileDetailId)
        {
            //this._remittanceOutgoingValidatorService.IsManual = true;
            var items = await _applicationContext.PendingTransferLists
                .Where(x => x.ConfirmTransfer < 3 && x.UserProfileDetail.Id == userProfileDetailId && 
                string.IsNullOrEmpty(x.TransactionId) == false)
                .OrderBy(x => x.LastCheckTransferDate)
                .ToListAsync();

            var outputSequence = string.Join("-", items.Select(x => x.Id));
            foreach (var item in items)
            {
                await this._remittanceOutgoingValidatorService.Execute(item.Id,true);
            }
            return outputSequence;
        }
    }
}
