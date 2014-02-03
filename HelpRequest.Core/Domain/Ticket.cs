using System.Collections.Generic;
using System.ComponentModel;
using FluentNHibernate.Mapping;
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

        [Required(Message = "You must enter your Phone Number")]
        public virtual string YourPhoneNumber { get; set; }
        
        [Required(Message = "You must enter the Subject.")]
        //[DisplayName("Subject:")]
        public virtual string Subject { get; set; }

        [Required(Message = "You must enter the Message Body.")]
        public virtual string MessageBody { get; set; }
        public virtual List<string> Availability { get; set; }
        public virtual List<string> EmailCCs { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; } //Note: if IList is used instead, a different mapping test is used.
    }

    public class TicketMap : ClassMap<Ticket>
    {
        public TicketMap()
        {
            Table("DoesNotExist"); //This table is only in memory, if we want to persist it, it will need to be created.
            Id(x => x.Id).UnsavedValue(0);

            Map(x => x.FromEmail);
            Map(x => x.UrgencyLevel);
            Map(x => x.SupportDepartment);
            Map(x => x.SupportDepartmentOther);
            Map(x => x.Subject);
            Map(x => x.MessageBody);
            Map(x => x.Availability);
            Map(x => x.ForApplication);
            Map(x => x.ForWebSite);
            Map(x => x.EmailCCs);
            Map(x => x.YourPhoneNumber);

            References(x => x.User).Column("UserId").Cascade.None();
            HasMany(x => x.Attachments).Table("AttachmentDoesNotExist").Cascade.AllDeleteOrphan();

        }
    }
}
