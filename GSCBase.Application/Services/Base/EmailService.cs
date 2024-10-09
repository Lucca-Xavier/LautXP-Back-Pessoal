using GSCBase.Application.IServices.Base;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSCBase.Application.Service.Base
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
        }


        public void SendEmail(string mailName, string toAddress, string body, string subject)
        {
            this.SendEmail(mailName, new List<string> { toAddress }, body, subject,new List<Attachment>());
        }

        public void SendEmail(string mailName, List<string> toAddresses, string body, string subject,List<Attachment> attachments)
        {
            try
            {
                var client = new SendGridClient("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");
                var from = new EmailAddress("gerenciar@gerenciarsc.com.br", mailName);
                var to = new List<EmailAddress>();
                if (toAddresses.Any())
                {
                    foreach (var toAddress in toAddresses)
                    {
                        if (!string.IsNullOrEmpty(toAddress))
                            to.Add(new EmailAddress(toAddress, toAddress));
                    }
                    var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, body, body);
                    if (attachments.Any())
                    {
                        foreach(var item in attachments)
                        {
                            msg.AddAttachment(item);
                        }
                    }
                    
                    var response = client.SendEmailAsync(msg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao enviar e-mail");
            }
        }
    }
}
