using MediatR;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class OrderItemMainTestService
    {
        
        public OrderItemMainTestService(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task Test(InputTransactionRaw input, UserProfile userProfile)
        {
            FeatureRepo.UseTransaction = false;
            
            ParamSpecial.IsForceStop = false;
            
            var inputUserProfile = new InputUser(userProfile);

            //var workingFolder = new WorkingFolder();
            //workingFolder.input = input;
            //workingFolder.inputMode = inputUserProfile;
            //workingFolder.includeLog = false;

            //await ItemMainService.Execute(workingFolder);

            var command = new OrderItemUserCommand()
            {
                inputTransactionRaw = input,
                userProfile = userProfile,
                includeLog = false
            };
            OrderResult result = await Mediator.Send(command);
            this.OrderResult = result;



        }

        public OrderResult OrderResult { get; set; }
        //public OrderResult OrderResult
        //{
        //    get
        //    {
        //        return this.ItemMainService.OrderResult;
        //    }
        //}

        public IMediator Mediator { get; }
    }
}