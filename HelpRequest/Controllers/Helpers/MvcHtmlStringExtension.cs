using System.Text;
using System.Web.Mvc;
using UCDArch.Core.Utils;

namespace HelpRequest.Controllers.Helpers
{
    public static class MvcHtmlStringExtension
    {
        public static MvcHtmlString Humanize(this MvcHtmlString source, string extraText = "", bool splitCamels = true, bool addColon=true)
        {
            var sourceString = source.ToHtmlString();
            var startIndex = sourceString.IndexOf('>') + 1;
            var endIndex = sourceString.LastIndexOf('<');            
            var labelText = sourceString.Substring(startIndex, endIndex - startIndex);            

            if (splitCamels)
            {
                labelText = Inflector.Titleize(labelText);
            }
            var sb = new StringBuilder(sourceString.Substring(0, startIndex));
            sb.Append(labelText);
            sb.Append(extraText);

            //labelText = labelText + extraText;

            if (addColon && !labelText.Contains(":"))
            {
                //labelText = labelText + ":";
                sb.Append(":");
            }

            sb.Append(sourceString.Substring(endIndex));

            //return MvcHtmlString.Create(sourceString.Substring(0, startIndex) + labelText + sourceString.Substring(endIndex));
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}