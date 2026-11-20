using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProjectChaptereight.BlogWeb.TagHelpers
{
    public class OpenModel: TagHelper

    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Class { get; set; } = "btn btn-success";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.Add("onclick", $"OpenModal('{Url}','default_Modal','{Title}')");
            output.Attributes.Add("class", Class);
            base.Process(context, output);
        }
    }
}
