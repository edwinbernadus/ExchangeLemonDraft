using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlueLight.Main;

namespace ExchangeLemonCore.Controllers
{
    public class LogReceiveRawsController : Controller
    {
        private readonly LoggingContext _context;

        public LogReceiveRawsController(LoggingContext context)
        {
            _context = context;
        }

        // GET: LogReceiveRaws
        public async Task<IActionResult> Index()
        {
            var output = await _context.LogReceiveRaws
                .Take(10)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return View(output);
        }

        // GET: LogReceiveRaws/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logReceiveRaw = await _context.LogReceiveRaws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logReceiveRaw == null)
            {
                return NotFound();
            }

            return View(logReceiveRaw);
        }

        // GET: LogReceiveRaws/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogReceiveRaws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SessionId,EventType,Content,CreatedDate")] LogReceiveRaw logReceiveRaw)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logReceiveRaw);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logReceiveRaw);
        }

        // GET: LogReceiveRaws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logReceiveRaw = await _context.LogReceiveRaws.FindAsync(id);
            if (logReceiveRaw == null)
            {
                return NotFound();
            }
            return View(logReceiveRaw);
        }

        // POST: LogReceiveRaws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionId,EventType,Content,CreatedDate")] LogReceiveRaw logReceiveRaw)
        {
            if (id != logReceiveRaw.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logReceiveRaw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogReceiveRawExists(logReceiveRaw.Id))
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
            return View(logReceiveRaw);
        }

        // GET: LogReceiveRaws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logReceiveRaw = await _context.LogReceiveRaws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logReceiveRaw == null)
            {
                return NotFound();
            }

            return View(logReceiveRaw);
        }

        // POST: LogReceiveRaws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logReceiveRaw = await _context.LogReceiveRaws.FindAsync(id);
            _context.LogReceiveRaws.Remove(logReceiveRaw);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogReceiveRawExists(int id)
        {
            return _context.LogReceiveRaws.Any(e => e.Id == id);
        }
    }
}
