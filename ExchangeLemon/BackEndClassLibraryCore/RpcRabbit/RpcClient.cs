using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace BlueLight.Main
{
    public class RpcClient
    {

        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }



        internal T ReceiveTransaction<T>(string s)
        {
            var tempOutput = JsonConvert.DeserializeObject<RpcOutput>(s);
            var content = tempOutput.Content;
            if (tempOutput.IsError)
            {
                throw new ArgumentException(content);
            }

            //TODO:105 - move to item encapsuled , if error , show throw
            var output = JsonConvert.DeserializeObject<T>(content);
            return output;
        }

        internal string CallTransactionExt(object request)
        {
            var raw = JsonConvert.SerializeObject(request);
            var item = new RpcItem()
            {
                TypeContent = request.GetType().ToString(),
                content = raw
            };
            var output = JsonConvert.SerializeObject(item);
            string s = CallTransaction(output);
            return s;
        }

        public string Call(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: props,
                body: messageBytes);

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            return respQueue.Take(); ;
        }

        public string CallTransaction(string message)
        {
            Debug.WriteLine("[call transaction] before publish");
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue_transaction",
                basicProperties: props,
                body: messageBytes);

            Debug.WriteLine("[call transaction] before consume");
            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            var output = respQueue.Take();
            
            Debug.WriteLine($"[call transaction] response: {output}");
            return output;
        }

        public void Close()
        {
            // Console.WriteLine("waiting");
            // Console.ReadLine();

            connection.Close();
        }
    }
}
