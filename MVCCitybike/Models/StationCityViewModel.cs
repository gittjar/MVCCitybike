using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCCitybike.Models
{
	public class StationCityViewModel
	{
		public List<Station>? Stations { get; set; }
		public SelectList? Kaupungit { get; set; }
		public string? StationKaupunki { get; set; }
		public string? SearchItem { get; set; }
	}
}

