using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCCitybike.TagHelpers
{
    /// <summary>
    /// Tag Helper for rendering citybike-styled card action buttons
    /// Usage: <citybike-card-actions id="@item.ID" controller="Station" />
    /// </summary>
    [HtmlTargetElement("citybike-card-actions")]
    public class CitybikeCardActionsTagHelper : TagHelper
    {
        public int Id { get; set; }
        public string Controller { get; set; } = "";
        public bool ShowDetails { get; set; } = true;
        public bool ShowEdit { get; set; } = true;
        public bool ShowDelete { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "citybike-card-actions");
            
            var content = new System.Text.StringBuilder();
            
            if (ShowDetails)
            {
                content.Append($@"
                    <a href=""/{Controller}/Details/{Id}"" class=""citybike-action-link"">
                        <span>Lisätiedot</span>
                    </a>");
            }
            
            if (ShowEdit)
            {
                content.Append($@"
                    <a href=""/{Controller}/Edit/{Id}"" class=""citybike-action-link"">
                        <span>Muokkaa</span>
                    </a>");
            }
            
            if (ShowDelete)
            {
                content.Append($@"
                    <a href=""/{Controller}/Delete/{Id}"" class=""citybike-action-link citybike-action-link-danger"">
                        <span>Poista</span>
                    </a>");
            }
            
            output.Content.SetHtmlContent(content.ToString());
        }
    }

    /// <summary>
    /// Tag Helper for rendering citybike action bar with create button
    /// Usage: <citybike-action-bar controller="Station" button-text="Luo Uusi Asema" />
    /// </summary>
    [HtmlTargetElement("citybike-action-bar")]
    public class CitybikeActionBarTagHelper : TagHelper
    {
        public string Controller { get; set; } = "";
        public string ButtonText { get; set; } = "Luo Uusi";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "citybike-action-bar");
            
            output.Content.SetHtmlContent($@"
                <a href=""/{Controller}/Create"" class=""citybike-btn citybike-btn-primary"">
                    <span>+ {ButtonText}</span>
                </a>");
        }
    }

    /// <summary>
    /// Tag Helper for rendering citybike details header with edit and back buttons
    /// Usage: <citybike-details-header title="ASEMAN TIEDOT" id="@Model.ID" controller="Station" />
    /// </summary>
    [HtmlTargetElement("citybike-details-header")]
    public class CitybikeDetailsHeaderTagHelper : TagHelper
    {
        public string Title { get; set; } = "";
        public int Id { get; set; }
        public string Controller { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "citybike-details-header");
            
            output.Content.SetHtmlContent($@"
                <h1 class=""citybike-details-title"">{Title}</h1>
                <div class=""citybike-details-actions"">
                    <a href=""/{Controller}/Edit/{Id}"" class=""citybike-btn citybike-btn-secondary"">
                        <span>Muokkaa</span>
                    </a>
                    <a href=""/{Controller}/Index"" class=""citybike-btn citybike-btn-outline"">
                        <span>Takaisin</span>
                    </a>
                </div>");
        }
    }

    /// <summary>
    /// Tag Helper for rendering citybike form header
    /// Usage: <citybike-form-header title="LUO UUSI ASEMA" subtitle="Täytä aseman tiedot..." />
    /// </summary>
    [HtmlTargetElement("citybike-form-header")]
    public class CitybikeFormHeaderTagHelper : TagHelper
    {
        public string Title { get; set; } = "";
        public string Subtitle { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "citybike-form-header");
            
            output.Content.SetHtmlContent($@"
                <h1 class=""citybike-form-title"">{Title}</h1>
                <p class=""citybike-form-subtitle"">{Subtitle}</p>");
        }
    }

    /// <summary>
    /// Tag Helper for rendering citybike stat box
    /// Usage: <citybike-stat-box value="10.5 km" label="Kokonaismatka" />
    /// </summary>
    [HtmlTargetElement("citybike-stat-box")]
    public class CitybikeStatBoxTagHelper : TagHelper
    {
        public string Value { get; set; } = "";
        public string Label { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "citybike-stat-box");
            
            output.Content.SetHtmlContent($@"
                <span class=""citybike-stat-value-large"">{Value}</span>
                <span class=""citybike-stat-label"">{Label}</span>");
        }
    }
}
