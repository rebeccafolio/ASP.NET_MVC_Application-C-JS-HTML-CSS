using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ITP245_Model;

namespace ITP245_2021_Fall.HtmlHelpers
{
    public static class HtmlExtensions
    {
        //public static MvcHtmlString SreachBox(this HtmlHelper html, string message)
        //{
        //    var label = new TagBuilder("label") { InnerHtml = "Search" };
        //    label.MergeAttribute("id", "SearchLabel");
        //    label.MergeAttribute("style", "margin-left: 15px; margin-right: 2px;");
        //    string labelBox = label.ToString(TagRenderMode.Normal);

        //    var tag = new TagBuilder("input");
        //    tag.MergeAttribute("id", "Search");
        //    tag.MergeAttribute("size", "36");
        //    tag.MergeAttribute("style", "margin-left: 4px");
        //    tag.MergeAttribute("type", "text");
        //    tag.MergeAttribute("value", String.Empty);
        //    tag.MergeAttribute("placeholder", $"{message} Press Tab");
        //    tag.MergeAttribute("onchange", "SearchByName();");
        //    string searchBox = tag.ToString(TagRenderMode.Normal);

        //    return new MvcHtmlString(labelBox + searchBox);
        //}

        public static MvcHtmlString Email(this HtmlHelper html, IEmail item)
        {
            
            //var indexmailto = new TagBuilder("a") { InnerHtml = "Email Contact" };
            var indexmailto = new TagBuilder("a") { InnerHtml = item.Email };
            indexmailto.MergeAttribute("class", "email");
            indexmailto.MergeAttribute("href", $"mailto:{item.Email}?subject={item.Contact}");
            //indexmailto.MergeAttribute("class", "badge bg-info");
            string mailtoLink = indexmailto.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(mailtoLink);
                                      
                    
        }
    }
}