﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using HelpRequest.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using Attachment = System.Net.Mail.Attachment;

namespace HelpRequest.Core.Abstractions
{
    public interface IEmailProvider
    {
        void SendHelpRequest(Ticket ticket, bool isPublicEmail, string ticketEmail);        
    }
    public class EmailProvider : IEmailProvider
    {
       public void SendHelpRequest(Ticket ticket, bool isPublicEmail, string ticketEmail)
       {
           Check.Require(ticket != null, "Details are missing.");
           var fromEmail = "";
           if(isPublicEmail)
           {
               fromEmail = ticket.FromEmail;
           }
           else
           {
               Check.Require(ticket.User != null, "Login Details missing.");
               fromEmail = ticket.User.Email;
           }
           Check.Require(!string.IsNullOrEmpty(fromEmail), "Email details missing.");
           Check.Require(!string.IsNullOrEmpty(ticketEmail), "Help Desk Email address not supplied.");

           var bodyBuilder = new StringBuilder();
           bodyBuilder.AppendLine("Original Subject     : " + ticket.Subject);
           bodyBuilder.AppendLine("Urgency Level        : " + ticket.UrgencyLevel);
           bodyBuilder.AppendLine("Support Department   : " + ticket.SupportDepartment);
           if (!string.IsNullOrEmpty(ticket.ForApplication))
           {
               bodyBuilder.AppendLine("For Application      : " + ticket.ForApplication);
           }
           if (!string.IsNullOrEmpty(ticket.ForWebSite))
           {
               bodyBuilder.AppendLine("For Web Site         : " + ticket.ForWebSite);
           }
           if (ticket.Availability != null && ticket.Availability.Count > 0)
           {
               bodyBuilder.AppendLine("Available Times      : ");
               foreach (var availableDate in ticket.Availability)
               {
                   bodyBuilder.AppendLine("   " + availableDate);
               }
           }
           bodyBuilder.AppendLine("");
           bodyBuilder.AppendLine("");
           bodyBuilder.AppendLine("Supplied Message Body :");
           bodyBuilder.AppendLine(ticket.MessageBody);  

           MailMessage message = new MailMessage(fromEmail, ticketEmail,
                                                  ticket.Subject,
                                                  bodyBuilder.ToString());

           foreach (var emailCC in ticket.EmailCCs)
           {
               if (!FilterCruEmail(emailCC))
               {
                   message.CC.Add(emailCC);
               }
           }
           foreach (var attachment in ticket.Attachments)
           {
               var messStream = new MemoryStream(attachment.Contents);
               var messAttach = new Attachment(messStream, attachment.FileName ,attachment.ContentType);
               message.Attachments.Add(messAttach);

           }
           

           message.IsBodyHtml = false;
           SmtpClient client = new SmtpClient("smtp.ucdavis.edu");
           client.Send(message);
       }

        private bool FilterCruEmail(string emailCc)
        {
            var cruEmail = new List<string>(10);
            cruEmail.Add("shuka@ucdavis.edu".ToLower());
            cruEmail.Add("shuka@caes.ucdavis.edu".ToLower());
            cruEmail.Add("smith@caes.ucdavis.edu".ToLower());
            cruEmail.Add("ssmith@ucdavis.edu".ToLower());
            cruEmail.Add("ssmith@caes.ucdavis.edu".ToLower());


            if (cruEmail.Contains(emailCc.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
