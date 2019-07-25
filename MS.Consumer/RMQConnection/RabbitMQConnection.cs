using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Consumer.RMQConnection
{
    public class RabbitMQConnection 
    {
        private string _hostUrl;
        private string _userName;
        private string _password;
        private string _queue;
         
        public IConnection GetRabbitMQConnection(IConfigurationRoot configuration)
        {
            _hostUrl    = configuration.GetSection("RabbitMQ").GetSection("HostUrl").Value;
            _userName   = configuration.GetSection("RabbitMQ").GetSection("UserName").Value;
            _password   = configuration.GetSection("RabbitMQ").GetSection("Password").Value;
            _queue      = configuration.GetSection("RabbitMQ").GetSection("Queue").Value;

            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostUrl,
                UserName = _userName,
                Password = _password
            };

            return connectionFactory.CreateConnection();
        }
    }
}
