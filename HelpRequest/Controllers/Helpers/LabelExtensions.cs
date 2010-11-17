using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using UCDArch.Core.Utils;

namespace HelpRequest.Controllers.Helpers
{

    public static class LabelExtensions
    {
        public static MvcHtmlString Label(this HtmlHelper html, string expression, string id = "", bool generatedId = false)
        {
            return LabelHelper(html,
                               ModelMetadata.FromStringExpression(expression, html.ViewData),
                               expression, DisplayOptions.None, "", id, generatedId);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DisplayOptions displayOptions, string extraText = "", string id = "", bool generatedId = false)
        {
            return LabelHelper(html,
                               ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                               ExpressionHelper.GetExpressionText(expression), displayOptions, extraText, id, generatedId);
        }

        //public static MvcHtmlString LabelForModel(this HtmlHelper html)
        //{
        //    return LabelHelper(html, html.ViewData.ModelMetadata, String.Empty);
        //}

        internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, DisplayOptions displayOptions, string extraText, string id, bool generatedId)
        {
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }
            var sb = new StringBuilder();
            if (displayOptions == DisplayOptions.Humanize || displayOptions == DisplayOptions.HumanizeAndColon)
            {
                sb.Append(Inflector.Titleize(labelText));
            }
            else
            {
                sb.Append(labelText);
            }
            sb.Append(extraText);
            if (displayOptions == DisplayOptions.HumanizeAndColon && labelText.Substring(labelText.Length - 1) != ":")
            {
                sb.Append(":");
            }

            var tag = new TagBuilder("label");
            if (!string.IsNullOrWhiteSpace(id))
            {
                tag.Attributes.Add("id", id);
            }
            else if (generatedId)
            {
                tag.Attributes.Add("id", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName) + "_Label");    
            }
            
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(sb.ToString());

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        
    }
    public enum DisplayOptions
    {
        Humanize,
        HumanizeAndColon,
        None
    }
}