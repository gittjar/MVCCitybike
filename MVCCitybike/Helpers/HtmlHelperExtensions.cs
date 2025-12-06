using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq.Expressions;

namespace MVCCitybike.Helpers
{
    /// <summary>
    /// Custom HTML Helper Extensions to reduce repetitive HTML in views
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Generates a citybike-styled form group with label, input, and validation
        /// </summary>
        public static IHtmlContent CitybikeFormGroupFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression,
            string labelText,
            object? htmlAttributes = null,
            string? placeholder = null,
            string? hint = null)
        {
            var inputHtml = htmlHelper.TextBoxFor(expression, htmlAttributes);
            var validationHtml = htmlHelper.ValidationMessageFor(expression, null, new { @class = "citybike-validation" });
            
            var writer = new StringWriter();
            writer.WriteLine("<div class=\"citybike-form-group\">");
            writer.WriteLine($"    <label class=\"citybike-label\">{labelText}</label>");
            
            // Write input
            inputHtml.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            
            // Write validation
            validationHtml.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            
            if (!string.IsNullOrEmpty(hint))
            {
                writer.WriteLine($"    <small class=\"citybike-form-hint\">{hint}</small>");
            }
            
            writer.WriteLine("</div>");
            
            return new HtmlString(writer.ToString());
        }

        /// <summary>
        /// Generates a citybike-styled detail item (label-value pair)
        /// </summary>
        public static IHtmlContent CitybikeDetailItem(
            this IHtmlHelper htmlHelper,
            string label,
            string? value)
        {
            return new HtmlString($@"
                <div class=""citybike-detail-item"">
                    <span class=""citybike-detail-label"">{label}</span>
                    <span class=""citybike-detail-value"">{value ?? ""}</span>
                </div>");
        }

        /// <summary>
        /// Generates a citybike-styled action link
        /// </summary>
        public static IHtmlContent CitybikeActionLink(
            this IHtmlHelper htmlHelper,
            string text,
            string action,
            int? id = null,
            bool isDanger = false)
        {
            var cssClass = isDanger 
                ? "citybike-action-link citybike-action-link-danger" 
                : "citybike-action-link";
            
            var url = id.HasValue ? $"/{action}/{id}" : $"/{action}";
            
            return new HtmlString($@"
                <a href=""{url}"" class=""{cssClass}"">
                    <span>{text}</span>
                </a>");
        }

        /// <summary>
        /// Generates a citybike info item for cards (used in Index views)
        /// </summary>
        public static IHtmlContent CitybikeInfoItem(
            this IHtmlHelper htmlHelper,
            string label,
            string? value)
        {
            return new HtmlString($@"
                <div class=""citybike-info-item"">
                    <span class=""citybike-info-label"">{label}</span>
                    <span class=""citybike-info-value"">{value ?? ""}</span>
                </div>");
        }

        /// <summary>
        /// Formats distance from meters to kilometers with 2 decimal places
        /// </summary>
        public static string FormatDistance(this IHtmlHelper htmlHelper, decimal meters)
        {
            return $"{(meters / 1000):F2} km";
        }

        /// <summary>
        /// Formats duration from seconds to HH:mm:ss format
        /// </summary>
        public static string FormatDuration(this IHtmlHelper htmlHelper, int seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Calculates average speed in km/h
        /// </summary>
        public static string CalculateSpeed(this IHtmlHelper htmlHelper, decimal meters, int seconds)
        {
            if (seconds == 0) return "0.0 km/h";
            var hours = seconds / 3600.0;
            var kilometers = (double)meters / 1000.0;
            return $"{(kilometers / hours):F1} km/h";
        }
    }
}
