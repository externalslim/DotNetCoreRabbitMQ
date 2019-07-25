using MS.Data.EFDatabase;
using System;

namespace MS.Logic.DatabaseOperations
{
    public interface IMailDBOperations
    {
        void Create(Mail mail);
        void Log(Exception exception, Mail mail);
    }
}
