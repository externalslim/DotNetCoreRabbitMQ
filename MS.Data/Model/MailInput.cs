using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Data.Model
{
    public class MailInput
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
