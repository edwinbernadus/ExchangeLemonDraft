//using ExchangeLemonCore.Data;
//using BackEndStandard;
//using ExchangeLemonCore.Data;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ExchangeLemonCore.Controllers
{
    [HtmlTargetElement("amp-img", Attributes = ImgAttributeName)]
    public class AmpImgTagHelper : TagHelper
    {
        private const string ImgAttributeName = "src";
        


        [HtmlAttributeName(ImgAttributeName)]
        public string ImageSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("img", $"http://localhost:53252/{ImageSrc}");
        }
    }
}