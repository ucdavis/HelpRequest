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
}
