using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCitybike.Models;
using MvcStation.Data;

namespace MVCCitybike.Controllers
{
    public class StationController : Controller
    {
        private readonly MvcStationContext _context;

        public StationController(MvcStationContext context)
        {
            _context = context;
        }

        // GET: Station
        public async Task<IActionResult> Index(string[] cities, string searchItem, string sortOrder)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SearchItem"] = searchItem; // Pass search term back to view

            /*
           return _context.Station != null ?
          View(await _context.Station.ToListAsync()) :
          Problem("Entity set 'MvcStationContext.Station'  is null.");
            */



            if (_context.Station == null)
            {
                return Problem("Entity set 'MvcStationContext.Station'  is null.");
            }

            // Use LINQ to get list of cities
            IQueryable<string> kaupunkiQuery = from m in _context.Station
                                               orderby m.Kaupunki
                                               select m.Kaupunki;
            var stations = from m in _context.Station
                           select m;
            //

            if (!string.IsNullOrEmpty(searchItem))
            {
                stations = stations.Where(s => s.Nimi!.Contains(searchItem));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    stations = stations.OrderByDescending(s => s.Nimi);
                    break;
                default:
                    stations = stations.OrderBy(s => s.Nimi);
                    break;
            }

            //return View(await stations.ToListAsync());

            // Filter by multiple selected cities
            if (cities != null && cities.Length > 0)
            {
                stations = stations.Where(x => cities.Contains(x.Kaupunki));
                ViewData["SelectedCities"] = cities;
            }

            var stationKaupunkiVM = new StationCityViewModel
            {
                Kaupungit = new SelectList(await kaupunkiQuery.Distinct().ToListAsync()),
                Stations = await stations.ToListAsync()
            };

            return View(stationKaupunkiVM);

        }

        // GET: Station/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Station == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .FirstOrDefaultAsync(m => m.ID == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // GET: Station/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Station/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FID,ID,Nimi,Namn,Name,Osoite,Adress,Kaupunki,Stad,Operaattor,Kapasiteet,x,y,Kuva")] Station station)
        {
            if (ModelState.IsValid)
            {
                // Set default values for nullable fields if they're empty
                if (string.IsNullOrWhiteSpace(station.Namn))
                    station.Namn = station.Nimi;
                if (string.IsNullOrWhiteSpace(station.Name))
                    station.Name = station.Nimi;
                if (string.IsNullOrWhiteSpace(station.Adress))
                    station.Adress = station.Osoite;
                if (string.IsNullOrWhiteSpace(station.Stad))
                    station.Stad = station.Kaupunki;
                if (string.IsNullOrWhiteSpace(station.Operaattor))
                    station.Operaattor = "CityBike Finland";
                
                try
                {
                    _context.Add(station);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Asema '{station.Nimi}' luotu onnistuneesti!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = $"Tietokantavirhe: {ex.InnerException?.Message ?? ex.Message}";
                    return View(station);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Virhe tallennettaessa: {ex.Message}";
                    return View(station);
                }
            }
            TempData["ErrorMessage"] = "Aseman luominen epäonnistui. Tarkista lomakkeen tiedot.";
            return View(station);
        }
        
        
        // GET: Station/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Station == null)
            {
                return NotFound();
            }

            var station = await _context.Station.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        // POST: Station/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FID,ID,Nimi,Namn,Name,Osoite,Adress,Kaupunki,Stad,Operaattor,Kapasiteet,x,y,Kuva")] Station station)
        {
            if (id != station.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Set default values for nullable fields if they're empty
                if (string.IsNullOrWhiteSpace(station.Namn))
                    station.Namn = station.Nimi;
                if (string.IsNullOrWhiteSpace(station.Name))
                    station.Name = station.Nimi;
                if (string.IsNullOrWhiteSpace(station.Adress))
                    station.Adress = station.Osoite;
                if (string.IsNullOrWhiteSpace(station.Stad))
                    station.Stad = station.Kaupunki;
                if (string.IsNullOrWhiteSpace(station.Operaattor))
                    station.Operaattor = "CityBike Finland";

                try
                {
                    _context.Update(station);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Asema '{station.Nimi}' päivitetty onnistuneesti!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.ID))
                    {
                        TempData["ErrorMessage"] = "Asemaa ei löytynyt. Se on saatettu poistaa.";
                        return NotFound();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Samanaikainen muokkaus havaittiin. Yritä uudelleen.";
                        return View(station);
                    }
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = $"Tietokantavirhe: {ex.InnerException?.Message ?? ex.Message}";
                    return View(station);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Virhe tallennettaessa: {ex.Message}";
                    return View(station);
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Aseman päivitys epäonnistui. Tarkista lomakkeen tiedot.";
            return View(station);
        }
    

        // GET: Station/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Station == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .FirstOrDefaultAsync(m => m.ID == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // POST: Station/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Station == null)
            {
                return Problem("Entity set 'MvcStationContext.Station'  is null.");
            }
            
            try
            {
                var station = await _context.Station.FindAsync(id);
                if (station != null)
                {
                    var stationName = station.Nimi;
                    _context.Station.Remove(station);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Asema '{stationName}' poistettu onnistuneesti!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Asemaa ei löytynyt poistettavaksi.";
                }
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = $"Tietokantavirhe poistettaessa: {ex.InnerException?.Message ?? ex.Message}";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Virhe poistettaessa: {ex.Message}";
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(int id)
        {
          return (_context.Station?.Any(e => e.ID == id)).GetValueOrDefault();
        }
        // For search testing
        /*
        [HttpPost]
        public string Index(string searchItem, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchItem;
        }
        */
    }
}
