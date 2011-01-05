using System;
using HelpRequest.Core.Domain;

namespace HelpRequest.Tests.Core.Helpers
{
    public static class CreateValidEntities
    {
        public static HelpTopic HelpTopic(int? counter)
        {
            var rtValue = new HelpTopic();
            rtValue.Question = "Question" + counter.Extra();
            rtValue.Answer = "Answer" + counter.Extra();
            rtValue.AvailableToPublic = true;
            return rtValue;
        }

        public static Attachment Attachment(int? counter)
        {
            var rtValue = new Attachment("Name"+ counter.Extra(), "FileName"+ counter.Extra());
            rtValue.Contents = new byte[]{0,1,1,0};
            return rtValue;
        }
        

        public static CatbertApplication CatbertApplication(int? counter)
        {
            var rtValue = new CatbertApplication();
            rtValue.Name = "Name" + counter.Extra();
            rtValue.Abbr = "Abbr" + counter.Extra();
            rtValue.Location = "Location" + counter.Extra();

            return rtValue;
        }

        public static Ticket Ticket(int? counter)
        {
            var rtValue = new Ticket();
            rtValue.Subject = "Subject" + counter.Extra();
            rtValue.UrgencyLevel = "UrgencyLevel" + counter.Extra();
            rtValue.SupportDepartment = "SupportDepartment" + counter.Extra();
            rtValue.MessageBody = "MessageBody" + counter.Extra();
            rtValue.FromEmail = "FromEmail" + counter.Extra();
            return rtValue;
        }

        public static User User(int? counter)
        {
            var rtValue = new User();
            rtValue.Email = "Email" + counter.Extra();
            rtValue.LoginId = "LoginId" + counter.Extra();
            rtValue.FirstName = "FirstName" + counter.Extra();
            rtValue.LastName = "LastName" + counter.Extra();
            return rtValue;
        }

        #region Helper Extension

        private static string Extra(this int? counter)
        {
            var extraString = "";
            if (counter != null)
            {
                extraString = counter.ToString();
            }
            return extraString;
        }

        #endregion Helper Extension

        public static Application Application(int? counter)
        {
            var rtValue = new Application(0, "Abbr" + counter.Extra(), "Application" + counter.Extra());
            if (counter != null)
            {
                rtValue.SortOrder = (int) counter;
            }
            return rtValue;
        }
    }
}
