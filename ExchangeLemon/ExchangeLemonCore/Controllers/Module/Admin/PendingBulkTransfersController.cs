using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlueLight.Main;

namespace ExchangeLemonCore.Controllers.Admin
{
    public class PendingBulkTransfersController : Controller
    {
        private readonly ApplicationContext _context;

        public PendingBulkTransfersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: PendingBulkTransfers
        public async Task<IActionResult> Index()
        {
            //var p = new PendingBulkTransferDummyFactory();
            //var items = p.Generate();

            //var items = await _context.PendingBulkTransfers.ToListAsync();

            var  items = await _context.PendingBulkTransfers
                .Select(x => new MvPendingBulkList()
                {
                    CreatedDate = x.CreatedDate,
                    TotalAmount= x.Collection.Sum( y => y.Amount),
                    Id = x.Id
                })
                .ToListAsync();
            return View(items);
        }

        // GET: PendingBulkTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingBulkTransfer = await _context.PendingBulkTransfers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pendingBulkTransfer == null)
            {
                return NotFound();
            }

            return View(pendingBulkTransfer);
        }

        // GET: PendingBulkTransfers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PendingBulkTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,Status")] PendingBulkTransfer pendingBulkTransfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pendingBulkTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pendingBulkTransfer);
        }

        // GET: PendingBulkTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingBulkTransfer = await _context.PendingBulkTransfers.FindAsync(id);
            if (pendingBulkTransfer == null)
            {
                return NotFound();
            }
            return View(pendingBulkTransfer);
        }

        // POST: PendingBulkTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,Status")] PendingBulkTransfer pendingBulkTransfer)
        {
            if (id != pendingBulkTransfer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pendingBulkTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PendingBulkTransferExists(pendingBulkTransfer.Id))
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
            return View(pendingBulkTransfer);
        }

        // GET: PendingBulkTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingBulkTransfer = await _context.PendingBulkTransfers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pendingBulkTransfer == null)
            {
                return NotFound();
            }

            return View(pendingBulkTransfer);
        }

        // POST: PendingBulkTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pendingBulkTransfer = await _context.PendingBulkTransfers.FindAsync(id);
            _context.PendingBulkTransfers.Remove(pendingBulkTransfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PendingBulkTransferExists(int id)
        {
            return _context.PendingBulkTransfers.Any(e => e.Id == id);
        }
    }
}
