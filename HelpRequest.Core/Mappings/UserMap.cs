using FluentNHibernate.Mapping;
using HelpRequest.Core.Domain;

namespace HelpRequest.Core.Mappings
{
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
