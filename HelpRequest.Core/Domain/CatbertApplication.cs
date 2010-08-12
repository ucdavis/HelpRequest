using UCDArch.Core.DomainModel;


namespace HelpRequest.Core.Domain
{
    public class CatbertApplication : DomainObject
    {        
        public virtual string Name { get; set; }
        public virtual string Abbr { get; set; }
        public virtual string Location { get; set; }
    }
}
