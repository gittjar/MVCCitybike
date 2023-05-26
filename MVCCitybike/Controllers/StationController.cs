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
        public async Task<IActionResult> Index(string stationKaupunki, string searchItem)
        {

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

            //return View(await stations.ToListAsync());

            if (!string.IsNullOrEmpty(stationKaupunki))
            {
                stations = stations.Where(x => x.Kaupunki == stationKaupunki);
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
        public async Task<IActionResult> Create([Bind("FID,ID,Nimi,Namn,Name,Osoite,Adress,Kaupunki,Stad,Operaattor,Kapasiteet,x,y")] Station station)
        {
            if (ModelState.IsValid)
            {
                _context.Add(station);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("FID,ID,Nimi,Namn,Name,Osoite,Adress,Kaupunki,Stad,Operaattor,Kapasiteet,x,y")] Station station)
        {
            if (id != station.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(station);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
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
            var station = await _context.Station.FindAsync(id);
            if (station != null)
            {
                _context.Station.Remove(station);
            }
            
            await _context.SaveChangesAsync();
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
