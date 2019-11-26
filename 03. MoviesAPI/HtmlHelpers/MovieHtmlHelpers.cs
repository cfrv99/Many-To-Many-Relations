using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.HtmlHelpers
{
    public static class MovieHtmlHelpers
    {
        //public static IHtmlContent MovieLink(this IHtmlHelper htmlHelper)
        //{
        //    //string link = "<a href=\"https://www.google.com\">Google</a>";
        //    //return link;

        //    string link = "<script>window.location.href = \"http://www.w3schools.com\"</script>";
        //    return new HtmlString(link);
        //}

        //public static IHtmlContent EmailLink(this IHtmlHelper htmlHelper, string email, string title = null)
        //{
        //    string link = $"<a href=\"mailto:{email}\">{title ?? email}</a>";
        //    return new HtmlString(link);
        //}

        public static IHtmlContent EmailLink(this IHtmlHelper htmlHelper, string email, string title = null)
        {
            var link = new TagBuilder("a");
            link.InnerHtml.Append(title ?? email);
            link.Attributes.Add("href", $"mailto:{email}");
            link.AddCssClass("text-danger");
            return link;
        }
    }
}
