using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace HelpRequest.Core.Domain
{
    public class User : DomainObject
    {
        public virtual string LoginId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmployeeId { get; set; }
        public virtual string UserKey { get; set; }
        public virtual string Sid { get; set; }

        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("vUsers");
            Id(x => x.Id, "UserId");
            ReadOnly();

            Map(x => x.LoginId);
            Map(x => x.Email);
            Map(x => x.Phone);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.EmployeeId);
            Map(x => x.UserKey);
            Map(x => x.Sid);
        }
    }
}
