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

    public class LogDetailsController : Controller
    {
        private readonly DashboardContext _context;

        public LogDetailsController(DashboardContext context)
        {
            _context = context;
        }

        // GET: LogDetails
        public async Task<IActionResult> Index()
        {
            var output = await _context.LogDetails.ToListAsync();
            var output3 = output.Select(x => x.Content).Distinct().ToList();
            var output4 = output3.Select(x => new LogDetail()
            {
                ModuleName = "Module1",
                Content = x,
                CreatedDate = DateTime.Now,
                Id = -1

            }).ToList();

            var result = output4;
            return View(result);
        }

        // GET: LogDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logDetail = await _context.LogDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logDetail == null)
            {
                return NotFound();
            }

            return View(logDetail);
        }

        // GET: LogDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModuleName,Content,CreatedDate")] LogDetail logDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logDetail);
        }

        // GET: LogDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logDetail = await _context.LogDetails.FindAsync(id);
            if (logDetail == null)
            {
                return NotFound();
            }
            return View(logDetail);
        }

        // POST: LogDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModuleName,Content,CreatedDate")] LogDetail logDetail)
        {
            if (id != logDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogDetailExists(logDetail.Id))
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
            return View(logDetail);
        }

        // GET: LogDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logDetail = await _context.LogDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logDetail == null)
            {
                return NotFound();
            }

            return View(logDetail);
        }

        // POST: LogDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logDetail = await _context.LogDetails.FindAsync(id);
            _context.LogDetails.Remove(logDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogDetailExists(int id)
        {
            return _context.LogDetails.Any(e => e.Id == id);
        }
    }
}
