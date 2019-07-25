using System;
using System.Collections.Generic;

namespace MS.Data.EFDatabase
{
    public partial class Mail
    {
        public int Id { get; set; }
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public string MailCc { get; set; }
        public string MailBcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool? IsSend { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
