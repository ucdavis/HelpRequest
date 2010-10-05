using FluentNHibernate.Mapping;
using HelpRequest.Core.Domain;

namespace HelpRequest.Core.Mappings
{
    public class CatbertApplicationMap : ClassMap<CatbertApplication>
    {
        public CatbertApplicationMap()
        {
            Table("vCatbertApplications");
            ReadOnly();
            Id(x => x.Id, "ApplicationId");

            Map(x => x.Name);
            Map(x => x.Abbr);
            Map(x => x.Location);
        }
    }
}
