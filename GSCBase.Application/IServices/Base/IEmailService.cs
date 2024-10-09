using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace GSCBase.Application.IServices.Base
{
    public interface IEmailService
    {
        void SendEmail(string mailName,string toAddress, string body, string subject);
        void SendEmail(string mailName, List<string> toAddresses, string body, string subject, List<Attachment> attachments);
    }
}
