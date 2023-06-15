using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCitybike.Models;
using MvcBiketripsMay2021.Data;
using X.PagedList;

namespace MVCCitybike.Controllers
{
    public class BiketripsMay2021Controller : Controller
    {
        private readonly MvcBiketripsMay2021Context _context;

        public BiketripsMay2021Controller(MvcBiketripsMay2021Context context)
        {
            _context = context;
        }

        // GET: BiketripsMay2021
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string searchString)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["DateSortParmReturn"] = sortOrder == "ReturnDate" ? "return_date_desc" : "ReturnDate";
            ViewData["DistanceSortParm"] = sortOrder == "Distance" ? "Distance_desc" : "Distance";
            ViewData["CurrentFilter"] = searchString;

            var biketripsmay2021 = from s in _context.BiketripsMay2021
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                biketripsmay2021 = biketripsmay2021.Where(s => s.Return_station_name.Contains(searchString)
                                       || s.Departure_station_name.Contains(searchString));
            }

            switch (sortOrder)
            {
                // departure
                case "Date":
                    biketripsmay2021 = biketripsmay2021.OrderBy(s => s.Departure);
                    break;
                case "date_desc":
                    biketripsmay2021 = biketripsmay2021.OrderByDescending(s => s.Departure);
                    break;

                // return
                case "ReturnDate":
                    biketripsmay2021 = biketripsmay2021.OrderBy(s => s.Return);
                    break;
                case "return_date_desc":
                    biketripsmay2021 = biketripsmay2021.OrderByDescending(s => s.Return);
                    break;

                // distance_m
                case "Distance":
                    biketripsmay2021 = biketripsmay2021.OrderBy(s => s.Covered_distance_m);
                    break;
                case "Distance_desc":
                    biketripsmay2021 = biketripsmay2021.OrderByDescending(s => s.Covered_distance_m);
                    break;
            }




            int pageSize = 50;
            return View(await PaginatedList<BiketripsMay2021>.CreateAsync(biketripsmay2021.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: BiketripsMay2021/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BiketripsMay2021 == null)
            {
                return NotFound();
            }

            var biketripsMay2021 = await _context.BiketripsMay2021
                .FirstOrDefaultAsync(m => m.ID == id);
            if (biketripsMay2021 == null)
            {
                return NotFound();
            }

            return View(biketripsMay2021);
        }

        // GET: BiketripsMay2021/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BiketripsMay2021/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Departure,Return,Departure_station_id,Departure_station_name,Return_station_id,Return_station_name,Covered_distance_m,Duration_sec")] BiketripsMay2021 biketripsMay2021)
        {
            if (ModelState.IsValid)
            {
                _context.Add(biketripsMay2021);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(biketripsMay2021);
        }

        // GET: BiketripsMay2021/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BiketripsMay2021 == null)
            {
                return NotFound();
            }

            var biketripsMay2021 = await _context.BiketripsMay2021.FindAsync(id);
            if (biketripsMay2021 == null)
            {
                return NotFound();
            }
            return View(biketripsMay2021);
        }

        // POST: BiketripsMay2021/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Departure,Return,Departure_station_id,Departure_station_name,Return_station_id,Return_station_name,Covered_distance_m,Duration_sec")] BiketripsMay2021 biketripsMay2021)
        {
            if (id != biketripsMay2021.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biketripsMay2021);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiketripsMay2021Exists(biketripsMay2021.ID))
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
            return View(biketripsMay2021);
        }

        // GET: BiketripsMay2021/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BiketripsMay2021 == null)
            {
                return NotFound();
            }

            var biketripsMay2021 = await _context.BiketripsMay2021
                .FirstOrDefaultAsync(m => m.ID == id);
            if (biketripsMay2021 == null)
            {
                return NotFound();
            }

            return View(biketripsMay2021);
        }

        // POST: BiketripsMay2021/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BiketripsMay2021 == null)
            {
                return Problem("Entity set 'MvcBiketripsMay2021Context.BiketripsMay2021'  is null.");
            }
            var biketripsMay2021 = await _context.BiketripsMay2021.FindAsync(id);
            if (biketripsMay2021 != null)
            {
                _context.BiketripsMay2021.Remove(biketripsMay2021);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BiketripsMay2021Exists(int id)
        {
          return (_context.BiketripsMay2021?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
