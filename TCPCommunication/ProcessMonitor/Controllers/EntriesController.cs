using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProcessMonitor.DAL;

namespace ProcessMonitor.Controllers
{
    public class EntriesController : Controller
    {
        private readonly EntriesContext _context;

        public EntriesController(EntriesContext context)
        {
            _context = context;
        }

        // GET: Entries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Logs.ToListAsync());
        }

        // GET: Entries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entries = await _context.Logs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (entries == null)
            {
                return NotFound();
            }

            return View(entries);
        }

        // GET: Entries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Timespan,Stare")] Entries entries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entries);
        }

        // GET: Entries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entries = await _context.Logs.SingleOrDefaultAsync(m => m.Id == id);
            if (entries == null)
            {
                return NotFound();
            }
            return View(entries);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Timespan,Stare")] Entries entries)
        {
            if (id != entries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntriesExists(entries.Id))
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
            return View(entries);
        }

        // GET: Entries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entries = await _context.Logs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (entries == null)
            {
                return NotFound();
            }

            return View(entries);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entries = await _context.Logs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Logs.Remove(entries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntriesExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}
