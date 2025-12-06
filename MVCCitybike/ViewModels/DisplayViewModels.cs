using MVCCitybike.Models;

namespace MVCCitybike.ViewModels
{
    /// <summary>
    /// View Model for BikeTrip with formatted display properties
    /// Moves formatting logic from Razor to C#
    /// </summary>
    public class BiketripDisplayViewModel
    {
        public int ID { get; set; }
        public string DepartureStationName { get; set; } = "";
        public int DepartureStationId { get; set; }
        public string ReturnStationName { get; set; } = "";
        public int ReturnStationId { get; set; }
        public string FormattedDistance { get; set; } = "";
        public string FormattedDuration { get; set; } = "";
        public string FormattedDepartureTime { get; set; } = "";
        public string FormattedReturnTime { get; set; } = "";
        public string AverageSpeed { get; set; } = "";
        
        // Raw values for calculations/editing
        public decimal CoveredDistanceM { get; set; }
        public int DurationSec { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }

        public static BiketripDisplayViewModel FromModel(BiketripsMay2021 trip)
        {
            return new BiketripDisplayViewModel
            {
                ID = trip.ID,
                DepartureStationName = trip.Departure_station_name ?? "",
                DepartureStationId = trip.Departure_station_id,
                ReturnStationName = trip.Return_station_name ?? "",
                ReturnStationId = trip.Return_station_id,
                CoveredDistanceM = trip.Covered_distance_m,
                DurationSec = trip.Duration_sec,
                Departure = trip.Departure,
                Return = trip.Return,
                FormattedDistance = Helpers.DataFormatters.FormatDistanceKm(trip.Covered_distance_m),
                FormattedDuration = Helpers.DataFormatters.FormatDuration(trip.Duration_sec),
                FormattedDepartureTime = Helpers.DataFormatters.FormatDateTime(trip.Departure),
                FormattedReturnTime = Helpers.DataFormatters.FormatDateTime(trip.Return),
                AverageSpeed = Helpers.DataFormatters.CalculateSpeed(trip.Covered_distance_m, trip.Duration_sec)
            };
        }

        public static IEnumerable<BiketripDisplayViewModel> FromModels(IEnumerable<BiketripsMay2021> trips)
        {
            return trips.Select(FromModel);
        }
    }

    /// <summary>
    /// View Model for Station with formatted display properties
    /// </summary>
    public class StationDisplayViewModel
    {
        public int ID { get; set; }
        public int FID { get; set; }
        public string Nimi { get; set; } = "";
        public string Osoite { get; set; } = "";
        public string Kaupunki { get; set; } = "";
        public string Operaattor { get; set; } = "";
        public int Kapasiteet { get; set; }
        public string FormattedCoordinates { get; set; } = "";
        
        // All station properties for details view
        public string? Namn { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public string? Stad { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public string? Kuva { get; set; }

        public static StationDisplayViewModel FromModel(Station station)
        {
            return new StationDisplayViewModel
            {
                ID = station.ID,
                FID = station.FID,
                Nimi = station.Nimi ?? "",
                Osoite = station.Osoite ?? "",
                Kaupunki = station.Kaupunki ?? "",
                Operaattor = station.Operaattor ?? "",
                Kapasiteet = station.Kapasiteet,
                FormattedCoordinates = Helpers.DataFormatters.FormatCoordinates((double)station.y, (double)station.x),
                Namn = station.Namn,
                Name = station.Name,
                Adress = station.Adress,
                Stad = station.Stad,
                X = station.x,
                Y = station.y,
                Kuva = station.Kuva
            };
        }

        public static IEnumerable<StationDisplayViewModel> FromModels(IEnumerable<Station> stations)
        {
            return stations.Select(FromModel);
        }
    }
}
