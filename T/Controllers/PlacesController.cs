using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T.Data;
using T.Data.Migrations;
using T.Models;

namespace T.Controllers
{
    public class PlacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private object place;
        private object listAgencies;
        private object transaction;

        public IEnumerable<int> Agencies { get; private set; }

        public PlacesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> PlacesRecords()
        {
            return View(await _context.Places.ToListAsync());

        }

        public async Task<IActionResult> placeDetails(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            ViewData["PlacesAgency"] = await _context.PlacesAgency.ToListAsync();
            ViewData["Agencies"] = await _context.Agency.ToListAsync();
            var applicationDbContext = _context.Places.Include(b => b.PlaceCategory).Where(b => b.Id == Id);
            return View("Index", await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> SearchPlace(string p_name, string c_name)
        {
            // return View("index", await _context.Places.ToListAsync());
            return View();
        }
        // GET: Places
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Places.Include(p => p.PlaceCategory);
            ViewData["Agencies"] = await _context.Agency.ToListAsync();
            ViewData["PlacesAgency"] = await _context.PlacesAgency.ToListAsync();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var places = await _context.Places
                .Include(p => p.PlaceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }

            return View(places);
        }

        // GET: Places/Create
        // [Authorize(Roles = "Admin")]
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Type");
            ViewData["AgencyID"] = new SelectList(_context.Agency, "Id", "Name");
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //  [Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,CityName,Image,Price,CategoryID")] Places places, List<int> Agencies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(places);
                await _context.SaveChangesAsync();

                List<PlacesAgency> placeAgency = new List<PlacesAgency>();
                foreach (int agency in Agencies)
                {
                    placeAgency.Add(new PlacesAgency { AgencyID = agency, PlaceID = places.Id });
                }
                _context.PlacesAgency.AddRange(placeAgency);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Type", places.CategoryID);
            return View(places);
        }

        // GET: Places/Edit/5
        // [Authorize(Roles = "Admin")]
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var places = await _context.Places.FindAsync(id);
            if (places == null)
            {
                return NotFound();
            }



            IList<PlacesAgency> placeAgencies = await _context.PlacesAgency.Where<PlacesAgency>(a => a.PlaceID == places.Id).ToListAsync();
            IList<int> listAgencies = new List<int>();
            foreach (PlacesAgency placesAgency in placeAgencies)
            {
                listAgencies.Add(placesAgency.AgencyID);
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Type", places.CategoryID);
            ViewData["AgencyID"] = new MultiSelectList(_context.Agency, "Id", "Name", listAgencies.ToArray());
            return View(places);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CityName,Image,Price,CategoryID")] Places places)
        {
            if (id != places.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(places);
                    await _context.SaveChangesAsync();
                  
                    List<PlacesAgency> placeAgency = new List<PlacesAgency>();
                    foreach (int agency in Agencies)
                    {
                      
                        placeAgency.Add(new PlacesAgency { AgencyID = agency, PlaceID = places.Id });
                    }
                    List<PlacesAgency> deletePlaceAgencies = await _context.PlacesAgency.Where<PlacesAgency>(a => a.PlaceID == places.Id).ToListAsync();
                    _context.PlacesAgency.RemoveRange(deletePlaceAgencies);
                    _context.SaveChanges();

                    _context.PlacesAgency.UpdateRange(placeAgency);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacesExists(places.Id))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Type", places.CategoryID);
          //  ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Type", places.CategoryID);
           // ViewData["AgencyID"] = new MultiSelectList(_context.Agency, "Id", "Name", listAgencies.ToArray());
            return View(places);
        }

        // // GET: Places/Delete/5
        //[Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var places = await _context.Places
                .Include(p => p.PlaceCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (places == null)
            {
                return NotFound();
            }

            return View(places);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var places = await _context.Places.FindAsync(id);
            _context.Places.Remove(places);
            await _context.SaveChangesAsync();

            List<PlacesAgency> deleteplaceAgency = await _context.PlacesAgency.Where<PlacesAgency>(a => a.PlaceID == places.Id).ToListAsync();
            _context.PlacesAgency.RemoveRange(deleteplaceAgency);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacesExists(int id)
        {
            return _context.Places.Any(e => e.Id == id);
        }
    }
}
