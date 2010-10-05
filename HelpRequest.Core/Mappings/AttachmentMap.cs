using FluentNHibernate.Mapping;
using HelpRequest.Core.Domain;

namespace HelpRequest.Core.Mappings
{
    /// <summary>
    /// Attachment Map
    /// </summary>
    public class AttachmentMap : ClassMap<Attachment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentMap"/> class.
        /// </summary>
        public AttachmentMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.DateCreated);
            Map(x => x.Contents);
            Map(x => x.ContentType);
        }
    }
}
