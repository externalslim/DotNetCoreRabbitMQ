using MS.Data.EFDatabase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Logic.DatabaseOperations
{
    public class MailDBOperations : IMailDBOperations
    {
        private NotificationApplicationContext _connection;
        private Mail _mail;
        public void Create(Mail mail)
        {
            _connection = new NotificationApplicationContext();
            _mail = mail;
            using (var connection = _connection)
            {
                try
                {
                    connection.Mail.Add(mail);
                    connection.SaveChanges();
                    connection.Dispose();
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    Log(ex, mail);
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Log(Exception exception, Mail mail)
        {
            if (_connection == null)
            {
                _connection = new NotificationApplicationContext();
            }

            var mailModel = _mail != null ? JsonConvert.SerializeObject(_mail) : JsonConvert.SerializeObject(mail);

            using (var connection = _connection)
            {
                try
                {
                    var errorAll = JsonConvert.SerializeObject(exception);
                    var errorLog = new ErrorLog
                    {
                        StackTrace = errorAll,
                        Message = exception.Message,
                        MailParameter = mailModel,
                        CreationTime = DateTime.Now
                    };
                    connection.ErrorLog.Add(errorLog);
                    connection.SaveChanges();
                    connection.Dispose();
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    this.Log(ex, null);
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
