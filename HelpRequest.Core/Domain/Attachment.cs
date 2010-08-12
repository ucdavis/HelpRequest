using System;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace HelpRequest.Core.Domain
{
    public class Attachment : DomainObject 
    {
        public Attachment(string name, string fileName)
        {
            SetDefaults();

            Name = name;
            FileName = fileName;
        }

        /// <summary>
        /// Required by nhibernate, use the parameterized constructor
        /// </summary>
        protected Attachment() { SetDefaults(); }

        protected void SetDefaults()
        {
            DateCreated = DateTime.Now;

        }

        [Required]
        [Length(100)]
        public virtual string Name { get; set; }
        public virtual DateTime DateCreated { get; set; }
        [Required]
        [Length(100)]
        public virtual string FileName { get; set; } 
        [NotNull]
        public virtual byte[] Contents { get; set; } 
        public virtual string ContentType { get; set; }
    }
}
