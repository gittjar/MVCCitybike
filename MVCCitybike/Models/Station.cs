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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // autoupdate increase +1 id
        public int ID { get; set; }
        [Comment("Citybike aseman nimi")]
        [Required]
        public string? Nimi { get; set; }
        public string? Namn { get; set; }
        public string? Name { get; set; }
        public string? Osoite { get; set; }
        public string? Adress { get; set; }
        [Required]
        public string? Kaupunki { get; set; }
        public string? Stad { get; set; }
        public string? Operaattor { get; set; }
        public int Kapasiteet { get; set; }
        [Column(TypeName = "decimal(8, 6)")]
        public decimal x { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        public decimal y { get; set; }
    }
}

