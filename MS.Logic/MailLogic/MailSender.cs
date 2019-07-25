
using System;
using System.Net;
using System.Net.Mail;
using System.Security;
using MS.Data.EFDatabase;
using MS.Data.Model;
using MS.Logic.DatabaseOperations;

namespace MS.Logic.MailLogic
{
    public class MailSender
    {

        private IMailDBOperations _mailDBOperations;
        private string _to;
        private string _cc;
        private string _bcc;

        public void MailSend(MailInput mail)
        {
            _mailDBOperations = new MailDBOperations();

            try
            { 

                MailMessage _mail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.EnableSsl = false;
                client.Host = "xx.xx.xx.xx";
                string smtpUserName = "xx@xx";
                string smtpPassword = "xx";

                client.Port = 587;
                client.Credentials = new NetworkCredential(smtpUserName, smtpPassword);

                _mail = MailMessageGenerator(mail);
                _mail.IsBodyHtml = true;
                client.ServicePoint.Expect100Continue = false;
                client.Send(_mail);

                var mailLog = new Mail
                {
                    Subject = mail.Subject,
                    Body = mail.Body,
                    MailFrom = mail.From,
                    MailTo = _to,
                    MailCc = _cc,
                    MailBcc = _bcc,
                    CreationTime = DateTime.Now
                };
  
                _mailDBOperations.Create(mailLog);

            }
            catch (Exception ex)
            {
                var mailLog = new Mail
                {
                    Subject = mail.Subject,
                    Body = mail.Body,
                    MailFrom = mail.From,
                    MailTo = _to,
                    MailCc = _cc,
                    MailBcc = _bcc
                };

                _mailDBOperations.Log(ex, mailLog);
            }
        }


        /// <summary>
        /// Mail Generator => Generates mail with main mail parameters and set ready to send with SmtpClient.
        /// </summary>
        /// <param name="mail">MailInput</param>
        /// <returns>MailMessage</returns>
        private MailMessage MailMessageGenerator(MailInput mail)
        {
            var mailMessage = new MailMessage();
            #region Subject, Body, From
            mailMessage.Subject = mail.Subject;
            mailMessage.From = new MailAddress("mobi-fi@netas.com.tr");
            mailMessage.Body = mail.Body;
            #endregion

            #region To
            if (mail.To != null && mail.To.Count > 0)
            {
                foreach (var to in mail.To)
                {
                    if (IsValid(to))
                    {
                        mailMessage.To.Add(new MailAddress(to));
                        _to += to + ";";
                    }
                }
            }
            #endregion

            #region CC
            if (mail.CC != null && mail.CC.Count > 0)
            {
                foreach (var cc in mail.CC)
                {
                    if (IsValid(cc))
                    {
                        mailMessage.To.Add(new MailAddress(cc));
                        _cc += cc + ";";
                    }
                }
            }
            #endregion

            #region BCC
            if (mail.BCC != null && mail.BCC.Count > 0)
            {
                foreach (var bcc in mail.BCC)
                {
                    if (IsValid(bcc))
                    {
                        mailMessage.To.Add(new MailAddress(bcc));
                        _bcc += bcc + ";";
                    }
                }
            }
            #endregion

            return mailMessage;
        }

        /// <summary>
        /// returns email address format's eligible status.
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns>true/false</returns>
        private bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
