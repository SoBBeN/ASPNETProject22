using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETProject2.Data;
using ASPNETProject2.Models;

namespace ASPNETProject2.Controllers
{
    public class TradeController : Controller
    {
        private readonly RatingContext _context;

        public TradeController(RatingContext context)
        {
            _context = context;    
        }

        // GET: Trade
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trades.ToListAsync());
        }

        // GET: Trade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _context.Trades
                .SingleOrDefaultAsync(m => m.TradeID == id);
            if (trade == null)
            {
                return NotFound();
            }

            return View(trade);
        }

        // GET: Trade/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TradeID,TradeType")] Trade trade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trade);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trade);
        }

        // GET: Trade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _context.Trades.SingleOrDefaultAsync(m => m.TradeID == id);
            if (trade == null)
            {
                return NotFound();
            }
            return View(trade);
        }

        // POST: Trade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TradeID,TradeType")] Trade trade)
        {
            if (id != trade.TradeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TradeExists(trade.TradeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(trade);
        }

        // GET: Trade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _context.Trades
                .SingleOrDefaultAsync(m => m.TradeID == id);
            if (trade == null)
            {
                return NotFound();
            }

            return View(trade);
        }

        // POST: Trade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trade = await _context.Trades.SingleOrDefaultAsync(m => m.TradeID == id);
            _context.Trades.Remove(trade);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TradeExists(int id)
        {
            return _context.Trades.Any(e => e.TradeID == id);
        }
    }
}
