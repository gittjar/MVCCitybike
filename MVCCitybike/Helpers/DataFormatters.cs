namespace MVCCitybike.Helpers
{
    /// <summary>
    /// Helper class for formatting data in views
    /// Moves logic from Razor views to C# code
    /// </summary>
    public static class DataFormatters
    {
        /// <summary>
        /// Formats distance from meters to kilometers
        /// </summary>
        public static string FormatDistanceKm(decimal meters)
        {
            return $"{(meters / 1000):F2} km";
        }

        /// <summary>
        /// Formats duration from seconds to HH:mm:ss
        /// </summary>
        public static string FormatDuration(int seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Calculates and formats average speed
        /// </summary>
        public static string CalculateSpeed(decimal meters, int seconds)
        {
            if (seconds == 0) return "0.0 km/h";
            var hours = seconds / 3600.0;
            var kilometers = (double)meters / 1000.0;
            return $"{(kilometers / hours):F1} km/h";
        }

        /// <summary>
        /// Formats datetime to Finnish format
        /// </summary>
        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm");
        }

        /// <summary>
        /// Formats datetime with seconds
        /// </summary>
        public static string FormatDateTimeWithSeconds(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }

        /// <summary>
        /// Formats coordinates for display
        /// </summary>
        public static string FormatCoordinates(double lat, double lon)
        {
            return $"{lat:F6}, {lon:F6}";
        }
    }
}
