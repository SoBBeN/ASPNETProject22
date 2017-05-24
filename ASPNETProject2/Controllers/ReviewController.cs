using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETProject2.Data;
using ASPNETProject2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ASPNETProject2.Controllers
{
    public class ReviewController : Controller
    {
        private readonly RatingContext _context;
        private readonly UserManager<ApplicationUser> _userManager; //BPoirier: need Identity users

        public ReviewController(RatingContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Review
        [Authorize]
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                var contractorReview = _context.Reviews.Include(r => r.Contractor).Include(r => r.Customer).Where(r => r.ContractorID == id);
                return View(await contractorReview.ToListAsync());
            }
            var ratingContext = _context.Reviews.Include(r => r.Contractor).Include(r => r.Customer);
            return View(await ratingContext.ToListAsync());
        }

        // GET: Review/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Contractor)
                .Include(r => r.Customer)
                .SingleOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            //Get user thats loged in
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return NotFound();
                //to do return error view
            }

            //Locate the logged in user (customer) within the customer Entity
            var customer = await _context.Customers
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Email == user.Email);

            ViewData["CustomerID"] = customer.CustomerID;//used to send to hidden field


            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ContractorID", "BusinessName");
            //ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "City");
            return View();
        }

        //Method we created
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(/*[Bind("ContractorID,CustomerID,Rating,message")]*/ Review review)
        {
            //    UPDATE Customers
            //    SET ContactName = 'Alfred Schmidt', City = 'Frankfurt'
            //    WHERE CustomerID = 1;
            //_context.Database.ExecuteSqlCommand

            review = new Review
            {
                ContractorID = review.ContractorID,
                CustomerID = review.CustomerID,
                Rating = review.Rating,
                message = review.message
                
            };

            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();

                //Now we need to update our contractor table
                //find the contractor
                Contractor contractor = _context.Contractors.Where(c => c.ContractorID == review.ContractorID).SingleOrDefaultAsync().Result;
                //variable to hold values
                var contractorid = contractor.ContractorID;
                var businessName = contractor.BusinessName;
                
                var city = contractor.City;
                var email = contractor.Email;
                var firstName = contractor.FirstName;
                var lastName = contractor.LastName;
                var phoneNumber = contractor.PhoneNumber;
                var tradeID = contractor.TradeID;
                var reviewCount = contractor.ReviewCount + 1;
                var starTotal = contractor.ReviewStarTotal + review.Rating;
                var AverageRating = (double)starTotal / reviewCount;


                //query the database
                string sqlUpdate = $" UPDATE Contractor" +
                                   $" SET AverageRating = {AverageRating}, BusinessName = '{businessName}', City = '{city}', Email = '{email}', FirstName = '{firstName}'," +
                                   $" LastName = '{lastName}', PhoneNumber = '{phoneNumber}', ReviewCount = {reviewCount}, TradeID = {tradeID} , ReviewStarTotal = {starTotal}" +
                                   $" WHERE ContractorID = {contractorid}";
                //update the database
                    _context.Database.ExecuteSqlCommand(sqlUpdate);






                return RedirectToAction("Index");
            }
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ContractorID", "BusinessName", review.ContractorID);
            
            return View(review);
        }

        // GET: Review/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.SingleOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ContractorID", "BusinessName", review.ContractorID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "City", review.CustomerID);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewID,ContractorID,CustomerID,Rating,message")] Review review)
        {
            if (id != review.ReviewID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewID))
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
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ContractorID", "BusinessName", review.ContractorID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "City", review.CustomerID);
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Contractor)
                .Include(r => r.Customer)
                .SingleOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.SingleOrDefaultAsync(m => m.ReviewID == id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }
    }
}
