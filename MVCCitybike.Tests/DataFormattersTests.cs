using MVCCitybike.Helpers;
using System.Globalization;
using Xunit;

namespace MVCCitybike.Tests
{
    /// <summary>
    /// Unit tests for DataFormatters helper class
    /// Tests all formatting and calculation methods
    /// </summary>
    public class DataFormattersTests
    {
        public DataFormattersTests()
        {
            // Set culture to en-US to ensure consistent decimal formatting (dot separator)
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
        }
        #region FormatDistanceKm Tests

        [Fact]
        public void FormatDistanceKm_WithZeroMeters_ReturnsZeroKilometers()
        {
            // Arrange
            decimal meters = 0m;

            // Act
            var result = DataFormatters.FormatDistanceKm(meters);

            // Assert
            Assert.Equal("0.00 km", result);
        }

        [Fact]
        public void FormatDistanceKm_WithExactKilometer_ReturnsFormattedString()
        {
            // Arrange
            decimal meters = 1000m;

            // Act
            var result = DataFormatters.FormatDistanceKm(meters);

            // Assert
            Assert.Equal("1.00 km", result);
        }

        [Fact]
        public void FormatDistanceKm_WithDecimalValue_RoundsToTwoDecimals()
        {
            // Arrange
            decimal meters = 2456m;

            // Act
            var result = DataFormatters.FormatDistanceKm(meters);

            // Assert
            Assert.Equal("2.46 km", result);
        }

        [Fact]
        public void FormatDistanceKm_WithLargeValue_FormatsCorrectly()
        {
            // Arrange
            decimal meters = 15750m;

            // Act
            var result = DataFormatters.FormatDistanceKm(meters);

            // Assert
            Assert.Equal("15.75 km", result);
        }

        [Fact]
        public void FormatDistanceKm_WithSmallValue_ShowsDecimals()
        {
            // Arrange
            decimal meters = 250m;

            // Act
            var result = DataFormatters.FormatDistanceKm(meters);

            // Assert
            Assert.Equal("0.25 km", result);
        }

        #endregion

        #region FormatDuration Tests

        [Fact]
        public void FormatDuration_WithZeroSeconds_ReturnsZeroTime()
        {
            // Arrange
            int seconds = 0;

            // Act
            var result = DataFormatters.FormatDuration(seconds);

            // Assert
            Assert.Equal("00:00:00", result);
        }

        [Fact]
        public void FormatDuration_WithExactMinute_ReturnsFormattedTime()
        {
            // Arrange
            int seconds = 60;

            // Act
            var result = DataFormatters.FormatDuration(seconds);

            // Assert
            Assert.Equal("00:01:00", result);
        }

        [Fact]
        public void FormatDuration_WithExactHour_ReturnsFormattedTime()
        {
            // Arrange
            int seconds = 3600;

            // Act
            var result = DataFormatters.FormatDuration(seconds);

            // Assert
            Assert.Equal("01:00:00", result);
        }

        [Fact]
        public void FormatDuration_WithMixedTime_ReturnsFormattedTime()
        {
            // Arrange
            int seconds = 3725; // 1 hour, 2 minutes, 5 seconds

            // Act
            var result = DataFormatters.FormatDuration(seconds);

            // Assert
            Assert.Equal("01:02:05", result);
        }

        [Fact]
        public void FormatDuration_WithTypicalBikeTrip_FormatsCorrectly()
        {
            // Arrange
            int seconds = 1800; // 30 minutes

            // Act
            var result = DataFormatters.FormatDuration(seconds);

            // Assert
            Assert.Equal("00:30:00", result);
        }

        [Fact]
        public void FormatDuration_WithLongDuration_FormatsCorrectly()
        {
            // Arrange
            int seconds = 7325; // 2 hours, 2 minutes, 5 seconds

            // Act
            var result = DataFormatters.FormatDuration(seconds);

            // Assert
            Assert.Equal("02:02:05", result);
        }

        #endregion

        #region CalculateSpeed Tests

        [Fact]
        public void CalculateSpeed_WithZeroSeconds_ReturnsZeroSpeed()
        {
            // Arrange
            decimal meters = 1000m;
            int seconds = 0;

            // Act
            var result = DataFormatters.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("0.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithZeroDistance_ReturnsZeroSpeed()
        {
            // Arrange
            decimal meters = 0m;
            int seconds = 3600;

            // Act
            var result = DataFormatters.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("0.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithTypicalCyclingSpeed_CalculatesCorrectly()
        {
            // Arrange
            decimal meters = 5000m; // 5 km
            int seconds = 1200; // 20 minutes

            // Act
            var result = DataFormatters.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("15.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithSlowSpeed_RoundsToOneDecimal()
        {
            // Arrange
            decimal meters = 2000m; // 2 km
            int seconds = 1800; // 30 minutes

            // Act
            var result = DataFormatters.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("4.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithFastSpeed_CalculatesCorrectly()
        {
            // Arrange
            decimal meters = 10000m; // 10 km
            int seconds = 1800; // 30 minutes

            // Act
            var result = DataFormatters.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("20.0 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithDecimalResult_RoundsToOneDecimal()
        {
            // Arrange
            decimal meters = 3456m;
            int seconds = 900; // 15 minutes

            // Act
            var result = DataFormatters.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("13.8 km/h", result);
        }

        [Fact]
        public void CalculateSpeed_WithShortTrip_CalculatesAccurately()
        {
            // Arrange
            decimal meters = 500m; // 500 meters
            int seconds = 180; // 3 minutes

            // Act
            var result = DataFormatters.CalculateSpeed(meters, seconds);

            // Assert
            Assert.Equal("10.0 km/h", result);
        }

        #endregion

        #region FormatDateTime Tests

        [Fact]
        public void FormatDateTime_WithTypicalDate_FormatsToFinnishFormat()
        {
            // Arrange
            var dateTime = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            var result = DataFormatters.FormatDateTime(dateTime);

            // Assert
            Assert.Equal("15.06.2023 14:30", result);
        }

        [Fact]
        public void FormatDateTime_WithMidnight_FormatsCorrectly()
        {
            // Arrange
            var dateTime = new DateTime(2023, 1, 1, 0, 0, 0);

            // Act
            var result = DataFormatters.FormatDateTime(dateTime);

            // Assert
            Assert.Equal("01.01.2023 00:00", result);
        }

        [Fact]
        public void FormatDateTime_WithSingleDigitDayAndMonth_AddsPadding()
        {
            // Arrange
            var dateTime = new DateTime(2023, 5, 7, 9, 5, 0);

            // Act
            var result = DataFormatters.FormatDateTime(dateTime);

            // Assert
            Assert.Equal("07.05.2023 09:05", result);
        }

        [Fact]
        public void FormatDateTime_WithEndOfYear_FormatsCorrectly()
        {
            // Arrange
            var dateTime = new DateTime(2023, 12, 31, 23, 59, 0);

            // Act
            var result = DataFormatters.FormatDateTime(dateTime);

            // Assert
            Assert.Equal("31.12.2023 23:59", result);
        }

        #endregion

        #region FormatDateTimeWithSeconds Tests

        [Fact]
        public void FormatDateTimeWithSeconds_IncludesSeconds()
        {
            // Arrange
            var dateTime = new DateTime(2023, 6, 15, 14, 30, 45);

            // Act
            var result = DataFormatters.FormatDateTimeWithSeconds(dateTime);

            // Assert
            Assert.Equal("15.06.2023 14:30:45", result);
        }

        [Fact]
        public void FormatDateTimeWithSeconds_WithZeroSeconds_ShowsZero()
        {
            // Arrange
            var dateTime = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            var result = DataFormatters.FormatDateTimeWithSeconds(dateTime);

            // Assert
            Assert.Equal("15.06.2023 14:30:00", result);
        }

        [Fact]
        public void FormatDateTimeWithSeconds_WithSingleDigitSeconds_AddsPadding()
        {
            // Arrange
            var dateTime = new DateTime(2023, 6, 15, 14, 30, 5);

            // Act
            var result = DataFormatters.FormatDateTimeWithSeconds(dateTime);

            // Assert
            Assert.Equal("15.06.2023 14:30:05", result);
        }

        #endregion

        #region FormatCoordinates Tests

        [Fact]
        public void FormatCoordinates_WithTypicalCoordinates_FormatsWithSixDecimals()
        {
            // Arrange
            double lat = 60.170833;
            double lon = 24.9375;

            // Act
            var result = DataFormatters.FormatCoordinates(lat, lon);

            // Assert
            Assert.Equal("60.170833, 24.937500", result);
        }

        [Fact]
        public void FormatCoordinates_WithZeroCoordinates_FormatsCorrectly()
        {
            // Arrange
            double lat = 0.0;
            double lon = 0.0;

            // Act
            var result = DataFormatters.FormatCoordinates(lat, lon);

            // Assert
            Assert.Equal("0.000000, 0.000000", result);
        }

        [Fact]
        public void FormatCoordinates_WithNegativeCoordinates_IncludesSign()
        {
            // Arrange
            double lat = -33.865143;
            double lon = 151.209900;

            // Act
            var result = DataFormatters.FormatCoordinates(lat, lon);

            // Assert
            Assert.Equal("-33.865143, 151.209900", result);
        }

        [Fact]
        public void FormatCoordinates_WithHighPrecision_RoundsToSixDecimals()
        {
            // Arrange
            double lat = 60.1708334567;
            double lon = 24.9375123456;

            // Act
            var result = DataFormatters.FormatCoordinates(lat, lon);

            // Assert
            Assert.Equal("60.170833, 24.937512", result);
        }

        [Fact]
        public void FormatCoordinates_HelsinkiCoordinates_FormatsCorrectly()
        {
            // Arrange (Helsinki coordinates)
            double lat = 60.192059;
            double lon = 24.945831;

            // Act
            var result = DataFormatters.FormatCoordinates(lat, lon);

            // Assert
            Assert.Equal("60.192059, 24.945831", result);
        }

        #endregion
    }
}
