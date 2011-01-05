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
            SetDefaults();
        }
        public Application(int sortOrder, string abbr, string application)
        {
            SetDefaults();
            SortOrder = sortOrder;
            Abbr = abbr;
            ApplicationName = application;
        }

        public virtual void SetDefaults()
        {
            SortOrder = 0;
        }

        public virtual int SortOrder { get; set; }
        [Required]
        [Length(50)]
        public virtual string Abbr { get; set; }
        [Required]
        [Length(50)]
        public virtual string ApplicationName { get; set; }
    }

    public class ApplicationMap : ClassMap<Application>
    {
        public ApplicationMap()
        {
            Id(x => x.Id);
            Map(x => x.SortOrder);
            Map(x => x.Abbr);
            Map(x => x.ApplicationName);
        }
    }
}
