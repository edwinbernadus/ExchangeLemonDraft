using BlueLight.Main;
using ExchangeLemonCore.Models.ReceiveModels;
using ExchangeLemonCore.Models.SendModels;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ExchangeLemonCore.Controllers
{
    public class WebHookRoutingService
    {
        public WebhookReceiveTransferService WebhookReceiveTransfer { get; }
        public ReceiveLogCaptureService ReceiveLogCapture { get; }
        public WebhookInquirySentTransferService WebhookInquirySentTransferService { get; }

        public WebHookRoutingService(
           WebhookReceiveTransferService WebhookReceiveTransfer,
           ReceiveLogCaptureService receiveLogCapture,
           WebhookInquirySentTransferService webhookInquirySentTransferService)
        {
            this.WebhookReceiveTransfer = WebhookReceiveTransfer;
            this.ReceiveLogCapture = receiveLogCapture;
            WebhookInquirySentTransferService = webhookInquirySentTransferService;
        }

        public async Task<string> Execute(string content, string event1)
        {
            var sessionId = Guid.NewGuid();
            await ReceiveLogCapture.SaveRaw(sessionId, content, event1);
            if (event1 == "unconfirmed-tx")
            {
                var contentObject = JsonConvert.DeserializeObject<BtcWebHookReceiveResult>(content);
                if (contentObject == null)
                {
                    return "receiver_failed_to_parse";
                }
                await WebhookReceiveTransfer.ReceiveLogic(contentObject, sessionId, event1);
                return "receiver_post";
            }
            else if (event1 == "tx-confirmation")
            {
                var contentObject = JsonConvert.DeserializeObject<BtcWebHookSendResult>(content);
                if (contentObject == null)
                {
                    return "send_failed_to_parse";
                }
                await WebhookInquirySentTransferService.InquiryLogic(contentObject, sessionId, event1);
                return "send_post";
            }

            return "no_criteria";
            

            
        }
         
    }
}
