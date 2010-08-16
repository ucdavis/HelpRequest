using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
