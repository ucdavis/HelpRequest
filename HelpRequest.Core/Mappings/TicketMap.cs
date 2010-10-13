using FluentNHibernate.Mapping;
using HelpRequest.Core.Domain;

namespace HelpRequest.Core.Mappings
{
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

            References(x => x.User).Column("UserId").Cascade.None();
            HasMany(x => x.Attachments).Table("AttachmentDoesNotExist").Cascade.AllDeleteOrphan();

        }
    }
}
