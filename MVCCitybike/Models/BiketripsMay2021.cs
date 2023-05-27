using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVCCitybike.Models
{
    public class BiketripsMay2021
    {
        public int ID { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public int Departure_station_id { get; set; }
        public string? Departure_station_name { get; set; }
        public int Return_station_id { get; set; }
        public string? Return_station_name { get; set; }
        public decimal Covered_distance_m { get; set; }
        public int Duration_sec { get; set; }
    }
    // Add context and controller
    // dotnet aspnet-codegenerator controller -name BiketripsMay2021Controller -m BiketripsMay2021 -dc MvcBiketripsMay2021.Data.MvcBiketripsMay2021Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries -sqlite
    // export PATH=$HOME/.dotnet/tools:$PATH
    // dotnet ef migrations add InitialCreate --context MvcBiketripsMay2021Context
    //
    // Change in Program.cs right connection string!
}

