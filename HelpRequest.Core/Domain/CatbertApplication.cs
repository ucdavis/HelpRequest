using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;


namespace HelpRequest.Core.Domain
{
    public class CatbertApplication : DomainObject
    {        
        public virtual string Name { get; set; }
        public virtual string Abbr { get; set; }
        public virtual string Location { get; set; }
    }

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
