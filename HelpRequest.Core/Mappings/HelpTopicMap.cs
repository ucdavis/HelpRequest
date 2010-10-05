using FluentNHibernate.Mapping;
using HelpRequest.Core.Domain;

namespace HelpRequest.Core.Mappings
{
    public class HelpTopicMap : ClassMap<HelpTopic>
    {
        public HelpTopicMap()
        {
            Id(x => x.Id);

            Map(x => x.Question);
            Map(x => x.Answer);
            Map(x => x.IsActive);
            Map(x => x.AvailableToPublic);
            Map(x => x.AppFilter);
            Map(x => x.NumberOfReads);
            Map(x => x.IsVideo);
            Map(x => x.VideoName);
        }
    }
}
