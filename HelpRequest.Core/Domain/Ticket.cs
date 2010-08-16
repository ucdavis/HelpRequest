﻿using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace HelpRequest.Core.Domain
{
    /// <summary>
    /// Note: This does not exist in the database and is never persisted.
    /// </summary>
    public class Ticket : DomainObject
    {
        public Ticket()
        {
            SetDefaults();
        }

        public virtual void SetDefaults()
        {
            Availability = new List<string>();
            EmailCCs = new List<string>();
            Attachments = new List<Attachment>();
        }

        
        public virtual User User { get; set; }
        public virtual string FromEmail { get; set; } //When not using CAS/Catbert Authentication to get the email.
        [Required(Message = "You must select an Urgency Level")]
        public virtual string UrgencyLevel { get; set; }
        [Required(Message = "You must select a Support Department")]
        public virtual string SupportDepartment { get; set; }
        public virtual string SupportDepartmentOther { get; set; }
        public virtual string ForApplication { get; set; }
        public virtual string ForWebSite { get; set; }
        
        [Required(Message = "You must enter the Subject.")]
        public string Subject { get; set; }
        [Required(Message = "You must enter the Message Body.")]
        public string MessageBody { get; set; }
        public List<string> Availability { get; set; }
        public List<string> EmailCCs { get; set; }

        public List<Attachment> Attachments { get; set; }
    }
}