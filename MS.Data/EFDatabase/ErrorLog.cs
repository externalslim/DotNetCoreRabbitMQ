using System;
using System.Collections.Generic;

namespace MS.Data.EFDatabase
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public string MailParameter { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
