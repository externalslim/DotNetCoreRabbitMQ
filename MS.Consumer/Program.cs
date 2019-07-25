using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Consumer.Consumer;
using MS.Consumer.RMQConnection;
using MS.Logic.DatabaseOperations;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MS.Consumer
{
    class Program
    {
        #region Private Objects
        private static MailConsumer _consumer;
        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        #endregion


        private static IConfigurationBuilder ProgramSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder;
        }

        static void Main(string[] args)
        {

            Task.Factory.StartNew(() => {
                while (true)
                {
                    var builder = ProgramSettings();
                    IConfigurationRoot _configuration = builder.Build();

                    _consumer = new MailConsumer(_configuration);

                    Thread.Sleep(30000);
                }
            });
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            _closing.WaitOne();
        }

        protected static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Exit");
            _closing.Set();

           
        }
    }
}
