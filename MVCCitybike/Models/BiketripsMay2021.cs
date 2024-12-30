using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MVCCitybike.Models
{
    public class BiketripsMay2021
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Departure is required")]
        public DateTime Departure { get; set; }
        
        [Required(ErrorMessage = "Return is required")]
        public DateTime Return { get; set; }
        
        [Required(ErrorMessage = "Departure station ID is required")]
        public int Departure_station_id { get; set; }
        
        [Required(ErrorMessage = "Departure station name is required")]
        public string? Departure_station_name { get; set; }
        
        [Required(ErrorMessage = "Return station ID is required")]
        public int Return_station_id { get; set; }
        
        [Required(ErrorMessage = "Return station name is required")]
        public string? Return_station_name { get; set; }
        
        [Required(ErrorMessage = "Covered distance is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Covered distance must be a positive number")]
        public decimal Covered_distance_m { get; set; }
        
        [Required(ErrorMessage = "Duration is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Duration must be a positive number")]
        public int Duration_sec { get; set; }
    }
}