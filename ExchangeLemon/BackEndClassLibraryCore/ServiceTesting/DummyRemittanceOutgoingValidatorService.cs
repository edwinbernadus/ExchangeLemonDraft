using BlueLight.Main;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BackEndClassLibrary
{

    public class DummyRemittanceOutgoingValidatorService : IRemittanceOutgoingValidatorService
    {
        private RemittanceOutgoingValidatorService remittanceOutgoingValidatorService;

        public DummyRemittanceOutgoingValidatorService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            InitOutgoingService();

        }

        public IServiceProvider ServiceProvider { get; }
        public int fakeOutputConfirm { get;  set; }
        public bool IsManual { get; set; }
        public async Task Execute(long pendingTransferId,bool isManual)
        {
            if (remittanceOutgoingValidatorService.BtcConfirmTransactionInquiry is 
                DummyResultBtcConfirmTransactionInquiry inquiry)
            {
                inquiry.OutputConfirm = fakeOutputConfirm;
            }
            await remittanceOutgoingValidatorService.Execute(pendingTransferId,isManual);
        }

        private void InitOutgoingService()
        {
            var context = this.ServiceProvider.GetService<ApplicationContext>();
            var confirmTransactionInquiry = this.ServiceProvider.GetService<IBtcConfirmTransactionInquiry>();
            var service = new RemittanceOutgoingValidatorService(context, confirmTransactionInquiry);
            this.remittanceOutgoingValidatorService = service;
        }
    }
}
