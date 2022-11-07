using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IHttpPostService
    {
        Task<string> SendPost(string url, string bodyContent);
    }
}



// private async Task<OrderResult> Logic(OrderItemQueueCommand request)
// {
//     var inputUserProfile = new InputUser(request.userName);
//     var workingFolder = new WorkingFolderInput()
//     {
//         inputTransactionRaw = request.inputTransactionRaw,
//         inputUser = inputUserProfile,
//         includeLog = request.includeLog,
//     };

//     //var service = this.Provider.BuildServiceProvider();
//     //var orderItemMainService = service.GetService<OrderItemMainService>();
//     await orderItemMainService.DirectExecuteFromHandler(workingFolder);
//     OrderResult output = orderItemMainService.OrderResult;
//     return output;
// }

// public async Task<string> Execute(string content)
// {
//     string hostName = "http://localhost:50727";
//     string api = "orderItemMainFunction";
//     var url = $"{hostName}/api/{api}";
//     var output = await this.httpPostService.SendPost(url, content);
//     return output;
// }