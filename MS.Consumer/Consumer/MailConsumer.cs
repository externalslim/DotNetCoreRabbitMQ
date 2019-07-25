using Microsoft.Extensions.Configuration;
using MS.Consumer.RMQConnection;
using MS.Data.Model;
using MS.Logic.MailLogic;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Consumer.Consumer
{
    public class MailConsumer 
    {

        private readonly RabbitMQConnection _rabbitMQService;
        private static string _queueName;

        public MailConsumer(IConfigurationRoot configuration)
        {
            _rabbitMQService = new RabbitMQConnection();
            _queueName = configuration.GetSection("RabbitMQ").GetSection("Queue").Value;

            using (var connection = _rabbitMQService.GetRabbitMQConnection(configuration))
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);

                    // Received event'i sürekli listen modunda olacaktır.
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var mailParam = JsonConvert.DeserializeObject<MailInput>(message);
                        var sender = new MailSender();
                        sender.MailSend(mailParam);
                        Console.WriteLine("{0} isimli queue üzerinden gelen mesaj: \"{1}\"", _queueName, message);
                    };

                    channel.BasicConsume(_queueName, true, consumer);
                    Console.ReadLine();
                }
            }
        }
    }
}
