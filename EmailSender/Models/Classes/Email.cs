using EmailSender.Models.Extensions;
using EmailSender.Models.Classes;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Models.Classes
{
    public class Email
    {
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmtp;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        public string SenderName { get; set; }

        public Email(EmailParams emailParams)
        {
            _hostSmtp = emailParams.HostSmtp;
            _enableSsl = emailParams.EnableSsl;
            _port = emailParams.Port;
            SenderName = emailParams.SenderName;
            _senderEmail = emailParams.SenderEmail;
            _senderEmailPassword = emailParams.SenderEmailPassword;
        }

        public void Send(string subject, string body, string to)
        {
            _mail = new MailMessage();
            _mail.From = new MailAddress(_senderEmail, SenderName);
            _mail.To.Add(new MailAddress(to));
            _mail.IsBodyHtml = true;
            _mail.Subject = subject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body.StripHTML(), null, MediaTypeNames.Text.Plain));

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString($@"
            <html>
                 <head> 
                 </head>
                 <body>
                    <div style='font-size: 16px padding: 10px; font-family: Arial; line-height: 1.4;'>
                        {body}
                    </div>
                 </body>
            </html>
            ", null, MediaTypeNames.Text.Html));

            _smtp = new SmtpClient
            {
                Host = _hostSmtp,
                EnableSsl = _enableSsl,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword)
            };

            _smtp.SendCompleted += OnSendCompleted;

            _smtp.Send(_mail);
        }

        private void OnSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }
    }
}
