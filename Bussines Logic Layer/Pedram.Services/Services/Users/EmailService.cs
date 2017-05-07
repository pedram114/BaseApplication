﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Users
    {
    public class EmailService : IIdentityMessageService
        {
        public Task SendAsync( IdentityMessage message )
            {
            // Credentials:
            var credentialUserName = "Pedram.gilaniniya@mail.com";
            var sentFrom = "Pedram.gilaniniya@mail.com";
            var pwd = "12aghebaT12";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient( "smtp.mail.com" );

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential( credentialUserName, pwd );

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage( sentFrom, message.Destination );

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send:
            return client.SendMailAsync( mail );
            }
        }
    }
