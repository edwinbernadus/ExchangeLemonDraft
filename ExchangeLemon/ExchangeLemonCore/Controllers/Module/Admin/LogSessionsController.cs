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
    public class LogSessionsController : Controller
    {
        private readonly LoggingExtContext _context;

        public LogSessionsController(LoggingExtContext context)
        {
            _context = context;
        }

        // GET: LogSessions
        public async Task<IActionResult> Index()
        {
            ViewBag.ServerTime = DateTime.Now;
            var items = await _context.LogSessions
                .OrderByDescending(x => x.Id)
                .Take(30)
                .ToListAsync();
            return View(items);
        }

        public async Task<string> Show(int? id)
        {
            var s = await _context.LogSessions.FirstAsync(x => x.Id == id);
            var output = s.StackTrace;
            if (string.IsNullOrEmpty(output))
            {
                return "no-data";
            }
            return output;

        }
        // GET: LogSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logSession = await _context.LogSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logSession == null)
            {
                return NotFound();
            }

            return View(logSession);
        }

        // GET: LogSessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GuidSession,IsStart,CreatedDate")] LogSession logSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logSession);
        }

        // GET: LogSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logSession = await _context.LogSessions.FindAsync(id);
            if (logSession == null)
            {
                return NotFound();
            }
            return View(logSession);
        }

        // POST: LogSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuidSession,IsStart,CreatedDate")] LogSession logSession)
        {
            if (id != logSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogSessionExists(logSession.Id))
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
            return View(logSession);
        }

        // GET: LogSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logSession = await _context.LogSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logSession == null)
            {
                return NotFound();
            }

            return View(logSession);
        }

        // POST: LogSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logSession = await _context.LogSessions.FindAsync(id);
            _context.LogSessions.Remove(logSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogSessionExists(int id)
        {
            return _context.LogSessions.Any(e => e.Id == id);
        }
    }
}
