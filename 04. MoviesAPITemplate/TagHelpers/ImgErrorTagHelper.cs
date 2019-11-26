using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.TagHelpers
{
    //src=""  error-src="/placeholder.png"
    [HtmlTargetElement("img", Attributes = "error-src", TagStructure = TagStructure.WithoutEndTag)]
    public class ImgErrorTagHelper : TagHelper
    {
        public string ErrorSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("onerror", $"event.target.src='{ErrorSrc}'");
        }
    }
}
