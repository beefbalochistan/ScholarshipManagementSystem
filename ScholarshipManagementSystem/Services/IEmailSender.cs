using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Services
{
    public interface IEmailSender
    {
        bool SendEmail(string email, string subject, string message);
    }
}
