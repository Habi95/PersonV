using SecurityData.Enum;
using SecurityData.model;
using SecurityData.repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Resources;
using Attachment = System.Net.Mail.Attachment;

namespace SecurityController
{
    public class EmailController
    {
        public static void SendEmail(User user, string? email, Entities entities)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            //Get Username &Password from Server file

            var resourceManager = new ResourceManager(typeof(Properties.Resources));
            var url = resourceManager.GetString("User_Password");
            string text = File.ReadAllText(url);
            string[] split = text.Split(";");
            string userName = /*"test@digitalcampusvorarlberg.at";*/ split[0];
            string password = /*"37tfr6ykuC2voasq";*/ split[1];

            MailMessage message = new MailMessage(userName, email);
            message.Sender = new MailAddress(userName);
            message.Subject = emailTemplate.document_type.ToString();
            var emailTemplateForText = entities.email_template.FirstOrDefault(x => x.id == 10);

            string body = emailTemplateForText.text;
            body = body.Replace("{Email}", email);
            body = body.Replace("{Password}", user.password);
            body = body.Replace("{SecurityWord}", user.security_word);
            body = body.Replace("{admin}", user.admin ? "Du hast Admin Rechte." : "Du hast keine Admin Rechte.");
            body = body.Replace("{authentication}", user.authentication ? "Du darfst lesen und schreiben im Programm." : "Du darfst im Programm nur lesen.");
            message.Body = body;

            SmtpClient oSmtp = new SmtpClient("w01959cb.kasserver.com");
            oSmtp.UseDefaultCredentials = false;
            oSmtp.Host = "w01959cb.kasserver.com";
            oSmtp.Credentials = new NetworkCredential(userName, password);
            oSmtp.EnableSsl = true;
            oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            oSmtp.Port = 25;
            oSmtp.Send(message);
        }
    }
}