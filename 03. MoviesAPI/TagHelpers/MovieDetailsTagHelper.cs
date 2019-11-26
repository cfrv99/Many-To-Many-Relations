using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MoviesApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MoviesApiClient.TagHelpers
{
    public enum MovieDetailsType
    { 
        Full,
        Modal
    }

    [HtmlTargetElement("a", Attributes = "movie-model")]
    public class MovieDetailsTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public MovieDetailsTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext Context { get; set; }

        public Movie MovieModel { get; set; }
        public MovieDetailsType MovieType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(Context);

            output.TagName = "a";
            //output.Attributes.Add("class", "btn btn-primary");
            output.AddClass("btn", HtmlEncoder.Default);
            output.AddClass("btn-primary", HtmlEncoder.Default);
            output.Attributes.Add("title", MovieModel.Title);

            string url = "";
            if (MovieType == MovieDetailsType.Full)
            {
                url = urlHelper.ActionLink("Movie", "Home", new { id = MovieModel.imdbID });

                var icon = new TagBuilder("i");

                if (MovieModel.Type == "game")
                    icon.AddCssClass("fas fa-gamepad");
                else if (MovieModel.Type == "series")
                    icon.AddCssClass("fas fa-tv");
                else
                    icon.AddCssClass("fas fa-film");

                output.Content.AppendHtml(icon);

                output.Content.Append(" Details");
            }
            else
            {
                url = urlHelper.ActionLink("MovieModal", "Home", new { id = MovieModel.imdbID });

                var icon = new TagBuilder("i");
                icon.AddCssClass("fas fa-eye");
                output.Content.AppendHtml(icon);

                output.Attributes.Add("data-open-modal", true);
            }

            output.Attributes.Add("href", url);
        }
    }
}
