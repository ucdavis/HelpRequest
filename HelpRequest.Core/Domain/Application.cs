using System.Collections.Generic;
using System.ComponentModel;
using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace HelpRequest.Core.Domain
{
    public class Application : DomainObject
    {
        public Application()
        {

        }
        public Application(string abbr, string application)
        {
            Abbr = abbr;
            ApplicationName = application;
        }


        [Required]
        [Length(50)]
        public virtual string Abbr { get; set; }
        [Required]
        [Length(50)]
        public virtual string ApplicationName { get; set; }

        public virtual bool HideOtherFaq { get; set; }
    }

    public class ApplicationMap : ClassMap<Application>
    {
        public ApplicationMap()
        {
            Id(x => x.Id);
            Map(x => x.Abbr);
            Map(x => x.ApplicationName);
            Map(x => x.HideOtherFaq);
        }
    }
}
