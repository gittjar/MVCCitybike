using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVCCitybike.Models
{
    public class Station
    {
        public int FID { get; set; }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Comment("Citybike aseman nimi")]
        [Required(ErrorMessage = "Nimi is required")]
        public string? Nimi { get; set; }
        
        [Required(ErrorMessage = "Osoite is required")]
        public string? Osoite { get; set; }
        
        [Required(ErrorMessage = "Kaupunki is required")]
        public string? Kaupunki { get; set; }
        
        [Column(TypeName = "decimal(8, 6)")]
        public decimal x { get; set; }
        
        [Column(TypeName = "decimal(9, 6)")]
        public decimal y { get; set; }
        
        [Comment("Citybike aseman nimi ruotsiksi")]
        public string? Namn { get; set; }
        
        [Comment("Citybike station name in English")]
        public string? Name { get; set; }
        
        [Comment("Citybike aseman osoite ruotsiksi")]
        public string? Adress { get; set; }
        
        [Comment("Citybike aseman kaupunki ruotsiksi")]
        public string? Stad { get; set; }
        
        [Comment("Citybike aseman operaattori")]
        public string? Operaattor { get; set; }
        
        [Comment("Citybike aseman kapasiteetti")]
        public int Kapasiteet { get; set; }

        [Comment("Citybike aseman kuva")]
        [Required(ErrorMessage = "Kuva is required")]
        public string? Kuva { get; set; }
    }
}