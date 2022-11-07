using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlueLight.Main
{

    public class RpcBackEnd : IDisposable
    {
        //static IConnection connection;

        //static IModel channel;

        //static EventingBasicConsumer consumer;


        public static IConnection connection;

        IModel channel;

        EventingBasicConsumer consumer;


        public IHttpPostService httpPostService { get; private set; }
        public IApplicationLifetime AppLifetime { get; }

        string hostName = "";


        string api = "orderItemMainFunction";
        // public OrderItemMainService orderItemMainService { get; }
        public RpcBackEnd(IHttpPostService httpPostService)
        {
            this.httpPostService = httpPostService;
            
            
            this.hostName = GetHostName();
        }

        public static string GetHostName()
        {
            var hostName = "";
            // hostName = "http://localhost";

#if DEBUG
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                hostName = "http://localhost:5000";
            }
            else
            {
                // hostName = "http://localhost:50727";
                hostName = "http://localhost:5000";
            }
#endif

#if RELEASE
            // hostName = "http://localhost";
            hostName = "http://localhost:5000";
#endif
            return hostName;
        }

        public void InitBackend()
        {
            if (connection != null)
            {
                return;
            }

            var factory = new ConnectionFactory() { HostName = "localhost" };
            
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            {
                channel.QueueDeclare(queue: "rpc_queue_transaction", durable: false,
                  exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                //EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "rpc_queue_transaction",
                  autoAck: false, consumer: consumer);
                Debug.WriteLine("[RpcBackEnd] [x] Awaiting RPC requests");


                consumer.Received += async (model, ea) =>
                {
                    

                    string response = null;

                    var body = ea.Body;
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    try
                    {
                        var content = Encoding.UTF8.GetString(body);
                        Debug.WriteLine($"[RpcBackEnd] {content}");

                        var url = $"{hostName}/api/{api}";

                        var output = await this.httpPostService.SendPost(url, content);
                        //TODO:105 - capture to item (always positive)

                        var output2 = new RpcOutput()
                        {
                            Content = output,
                        };
                        var output3 = JsonConvert.SerializeObject(output2);
                        response = output3;

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(" [.] " + e.Message);
                        // response = "";
                        var output2 = new RpcOutput()
                        {
                            Content = e.Message,
                            IsError = true
                        };
                        var output3 = JsonConvert.SerializeObject(output2);
                        response = output3;
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                          basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag,
                          multiple: false);
                    }
                };


            }
        }


        
        public async Task<RpcOutput> TestDebug()
        {
            var api2 = "DebugApi";
            var url = $"{hostName}/api/{api2}";

            var content = "hello world";
            var output = await this.httpPostService.SendPost(url, content);
            //TODO:105 - capture to item (always positive)

            var output2 = new RpcOutput()
            {
                Content = output,
            };

            return output2;
        }

        public void Dispose()
        {

            try
            {
                consumer.HandleBasicCancelOk("one");
                consumer = null;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }

            try
            {
                channel.Dispose();
                channel = null;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }


            try
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }

        }
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