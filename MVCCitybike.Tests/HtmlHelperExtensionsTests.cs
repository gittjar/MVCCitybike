using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using MVCCitybike.Helpers;
using System.Globalization;
using Xunit;

namespace MVCCitybike.Tests
{
    /// <summary>
    /// Unit tests for HtmlHelperExtensions
    /// Tests HTML generation and formatting methods
    /// </summary>
    public class HtmlHelperExtensionsTests
    {
        public HtmlHelperExtensionsTests()
        {
            // Set culture to en-US to ensure consistent decimal formatting (dot separator)
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
        }
        #region FormatDistance Tests

        [Fact]
        public void FormatDistance_WithZeroMeters_ReturnsZeroKilometers()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 0m;

            // Act
            var result = htmlHelper.FormatDistance(meters);

            // Assert
            Assert.Equal("0.00 km", result);
        }

        [Fact]
        public void FormatDistance_WithExactKilometer_ReturnsFormattedString()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 1000m;

            // Act
            var result = htmlHelper.FormatDistance(meters);

            // Assert
            Assert.Equal("1.00 km", result);
        }

        [Fact]
        public void FormatDistance_WithDecimalValue_RoundsToTwoDecimals()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 2456m;

            // Act
            var result = htmlHelper.FormatDistance(meters);

            // Assert
            Assert.Equal("2.46 km", result);
        }

        [Fact]
        public void FormatDistance_WithTypicalBikeTrip_FormatsCorrectly()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 5000m;

            // Act
            var result = htmlHelper.FormatDistance(meters);

            // Assert
            Assert.Equal("5.00 km", result);
        }

        [Fact]
        public void FormatDistance_WithLongDistance_FormatsCorrectly()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 15750m;

            // Act
            var result = htmlHelper.FormatDistance(meters);

            // Assert
            Assert.Equal("15.75 km", result);
        }

        #endregion

        #region FormatDuration Tests

        [Fact]
        public void FormatDuration_WithZeroSeconds_ReturnsZeroTime()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            int seconds = 0;

            // Act
            var result = htmlHelper.FormatDuration(seconds);

            // Assert
            Assert.Equal("00:00:00", result);
        }

        [Fact]
        public void FormatDuration_WithExactMinute_ReturnsFormattedTime()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            int seconds = 60;

            // Act
            var result = htmlHelper.FormatDuration(seconds);

            // Assert
            Assert.Equal("00:01:00", result);
        }

        [Fact]
        public void FormatDuration_WithExactHour_ReturnsFormattedTime()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            int seconds = 3600;

            // Act
            var result = htmlHelper.FormatDuration(seconds);

            // Assert
            Assert.Equal("01:00:00", result);
        }

        [Fact]
        public void FormatDuration_WithMixedTime_ReturnsFormattedTime()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            int seconds = 3725; // 1 hour, 2 minutes, 5 seconds

            // Act
            var result = htmlHelper.FormatDuration(seconds);

            // Assert
            Assert.Equal("01:02:05", result);
        }

        [Fact]
        public void FormatDuration_WithTypicalBikeTrip_FormatsCorrectly()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            int seconds = 1800; // 30 minutes

            // Act
            var result = htmlHelper.FormatDuration(seconds);

            // Assert
            Assert.Equal("00:30:00", result);
        }

        #endregion

        #region CalculateSpeed Tests

        [Fact]
        public void CalculateSpeed_WithZeroSeconds_ReturnsZeroSpeed()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 1000m;
            int seconds = 0;

            // Act
            var result = htmlHelper.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("0.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithZeroDistance_ReturnsZeroSpeed()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 0m;
            int seconds = 3600;

            // Act
            var result = htmlHelper.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("0.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithTypicalCyclingSpeed_CalculatesCorrectly()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 5000m; // 5 km
            int seconds = 1200; // 20 minutes

            // Act
            var result = htmlHelper.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("15.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithSlowSpeed_RoundsToOneDecimal()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 2000m; // 2 km
            int seconds = 1800; // 30 minutes

            // Act
            var result = htmlHelper.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("4.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithFastSpeed_CalculatesCorrectly()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 10000m; // 10 km
            int seconds = 1800; // 30 minutes

            // Act
            var result = htmlHelper.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("20.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithRealisticBikeSpeed_CalculatesAccurately()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            decimal meters = 3000m; // 3 km
            int seconds = 720; // 12 minutes

            // Act
            var result = htmlHelper.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("15.0 km/h", result);
        }

        #endregion

        #region CitybikeDetailItem Tests

        [Fact]
        public void CitybikeDetailItem_WithLabelAndValue_GeneratesCorrectHtml()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeDetailItem("Station Name", "Kaivopuisto");
            var html = result.ToString();

            // Assert
            Assert.Contains("citybike-detail-item", html);
            Assert.Contains("citybike-detail-label", html);
            Assert.Contains("citybike-detail-value", html);
            Assert.Contains("Station Name", html);
            Assert.Contains("Kaivopuisto", html);
        }

        [Fact]
        public void CitybikeDetailItem_WithNullValue_GeneratesEmptyValue()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeDetailItem("Station Name", null);
            var html = result.ToString();

            // Assert
            Assert.Contains("citybike-detail-item", html);
            Assert.Contains("Station Name", html);
            Assert.Contains("citybike-detail-value\">", html);
        }

        [Fact]
        public void CitybikeDetailItem_WithEmptyValue_GeneratesEmptyValue()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeDetailItem("Label", "");
            var html = result.ToString();

            // Assert
            Assert.Contains("citybike-detail-item", html);
            Assert.Contains("Label", html);
        }

        #endregion

        #region CitybikeActionLink Tests

        [Fact]
        public void CitybikeActionLink_WithIdParameter_GeneratesCorrectUrl()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeActionLink("Edit", "Edit", 123);
            var html = result.ToString();

            // Assert
            Assert.Contains("href=\"/Edit/123\"", html);
            Assert.Contains("citybike-action-link", html);
            Assert.Contains("Edit", html);
        }

        [Fact]
        public void CitybikeActionLink_WithoutIdParameter_GeneratesSimpleUrl()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeActionLink("Create New", "Create");
            var html = result.ToString();

            // Assert
            Assert.Contains("href=\"/Create\"", html);
            Assert.Contains("citybike-action-link", html);
            Assert.Contains("Create New", html);
        }

        [Fact]
        public void CitybikeActionLink_WithDangerFlag_AddsDangerClass()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeActionLink("Delete", "Delete", 123, isDanger: true);
            var html = result.ToString();

            // Assert
            Assert.Contains("citybike-action-link-danger", html);
            Assert.Contains("href=\"/Delete/123\"", html);
        }

        [Fact]
        public void CitybikeActionLink_WithoutDangerFlag_DoesNotAddDangerClass()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeActionLink("Edit", "Edit", 123, isDanger: false);
            var html = result.ToString();

            // Assert
            Assert.DoesNotContain("citybike-action-link-danger", html);
            Assert.Contains("citybike-action-link", html);
        }

        #endregion

        #region CitybikeInfoItem Tests

        [Fact]
        public void CitybikeInfoItem_WithLabelAndValue_GeneratesCorrectHtml()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeInfoItem("Distance", "2.5 km");
            var html = result.ToString();

            // Assert
            Assert.Contains("citybike-info-item", html);
            Assert.Contains("citybike-info-label", html);
            Assert.Contains("citybike-info-value", html);
            Assert.Contains("Distance", html);
            Assert.Contains("2.5 km", html);
        }

        [Fact]
        public void CitybikeInfoItem_WithNullValue_GeneratesEmptyValue()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();

            // Act
            var result = htmlHelper.CitybikeInfoItem("Label", null);
            var html = result.ToString();

            // Assert
            Assert.Contains("citybike-info-item", html);
            Assert.Contains("Label", html);
            Assert.Contains("citybike-info-value\">", html);
        }

        [Fact]
        public void CitybikeInfoItem_WithLongText_GeneratesCorrectly()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper();
            var longValue = "This is a very long station name with lots of details";

            // Act
            var result = htmlHelper.CitybikeInfoItem("Station", longValue);
            var html = result.ToString();

            // Assert
            Assert.Contains("citybike-info-item", html);
            Assert.Contains(longValue, html);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a mock IHtmlHelper for testing extension methods
        /// </summary>
        private IHtmlHelper CreateMockHtmlHelper()
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            
            // Setup ViewContext if needed for more complex tests
            var viewContext = new ViewContext();
            mockHtmlHelper.Setup(h => h.ViewContext).Returns(viewContext);
            
            return mockHtmlHelper.Object;
        }

        #endregion
    }
}
