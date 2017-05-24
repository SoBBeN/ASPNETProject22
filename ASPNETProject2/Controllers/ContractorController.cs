using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETProject2.Data;
using ASPNETProject2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace ASPNETProject2.Controllers
{
    public class ContractorController : Controller
    {
        private readonly RatingContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnv; //Bpoirier: for upload

        public ContractorController(RatingContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        // GET: Contractor
        [Authorize]
        public async Task<IActionResult> Index(int? SelectedTrade)
        {
            //get trade drop down from our method
            GetTrade(SelectedTrade);

            if (SelectedTrade == null)
            {
                SelectedTrade = 0;
            }

            //ViewData["Trade"] = viewMode.Contractors = _context.Contractors.Include(t => t.Trade);
            var contractors = await  _context.Contractors.ToListAsync();
            //ViewData["ReviewMessege"] = _context.Reviews.Include(c => c.Contractor).Where(r => r.ContractorID == contractors.Where(id => id.ContractorID));

            //switch case to determine what trade they are searching for
            switch (SelectedTrade)
            {
                //case "Carpenter":
                //    viewMode.Contractors = await _context.Contractors.Where(c => c.TradeID == "1").ToListAsync();
                //    break;
                //all
                case 0:
                       contractors = await _context.Contractors.Include(r => r.Reviews).Include(t => t.Trade).ThenInclude(r => r.Contractors).ToListAsync();
                    break;
                //Carpenter
                case 1:
                       contractors = await _context.Contractors.Include(r => r.Reviews).Where(c => c.TradeID == 1).ToListAsync();
                    break;
                //Plumber
                case 2:
                       contractors = await _context.Contractors.Include(r => r.Reviews).Where(c => c.TradeID == 2).ToListAsync();
                    break;
                //Electrician
                case 3:
                       contractors = await _context.Contractors.Include(r => r.Reviews).Where(c => c.TradeID == 3).ToListAsync();
                    break;
                //Heating and air conditioning
                case 4:
                       contractors = await _context.Contractors.Include(r => r.Reviews).Where(c => c.TradeID == 4).ToListAsync();
                    break;
                //Landscaping
                case 5:
                       contractors = await _context.Contractors.Include(r => r.Reviews).Where(c => c.TradeID == 5).ToListAsync();
                    break;
                //Landscaping
                case 6:
                    contractors = await _context.Contractors.Include(r => r.Reviews).Where(c => c.TradeID == 6).ToListAsync();
                    break;
            }


            return View(contractors);

            //var contractors = _context.Contractors.Include(c => c.Trade);
            //return View(await contractors.ToListAsync());
        }

        //Bpoirier: Building method to get Trades
        private IQueryable<Trade> GetTrade(int? SelectedTrade)
        {
            //Get all department sorted by name
            var trade = _context.Trades.OrderBy(d => d.TradeType).ToList();

            //Add ViewData for use within View
            ViewData["SelectedTrade"] = new SelectList(trade, "TradeID", "TradeType", SelectedTrade); //dropdown
            //Retrieve the value of incoming parameter
            int tradeId = SelectedTrade.GetValueOrDefault();


            IQueryable<Trade> trades = _context.Trades
                //Where()
                //Where(DepartmentID == 1)
                .Where(c => !SelectedTrade.HasValue || c.TradeID == tradeId)
                .OrderBy(d => d.TradeID);
            //.Include(d => d.Department);

            return trades;
        }




        // GET: Contractor/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .Include(c => c.Trade).Include(r => r.Reviews)
                .SingleOrDefaultAsync(m => m.ContractorID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // GET: Contractor/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TradeID"] = new SelectList(_context.Trades, "TradeID", "TradeType");
            return View();
        }

        //// POST: Contractor/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ContractorID,FirstName,LastName,Email,BusinessName,PhoneNumber,TradeID,City,ReviewCount,AverageRating")] Contractor contractor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contractor);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["TradeID"] = new SelectList(_context.Trades, "TradeID", "TradeID", contractor.TradeID);
        //    return View(contractor);
        //}

        // POST: Contractor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Contractor model, IList<IFormFile> files) //BPoirier: added file upload
        {

            var filename = "";
            if (ModelState.IsValid)
            {
                //============================ upload profile image =======================//
                foreach (var file in files)
                {
                    //rename the file: 6847sdf456561sdf78.jpg
                     filename = model.FirstName + "_" + model.LastName + System.IO.Path.GetExtension(file.FileName); //this is the new fileName attached with user.id
                                                                                                  //tag on the path where we want to upload the image
                                                                                                  //filename = _hostingEnv.WebRootPath + $"\\images\\users\\{filename}"; //One way to do it
                    filename = _hostingEnv.WebRootPath + $@"\images\users\{filename}";//this would create  \images\users\Benoitpoirier9@outlook.com.jpg

                    using (System.IO.FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush(); // clear the memory like java
                    }
                }


                //============================ End file upload ============================//


                //create a new contractor
                var contractor = new Contractor
                {

                    AverageRating = model.AverageRating,
                    BusinessName = model.BusinessName,
                    City = model.City,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    TradeID = model.TradeID,
                    ReviewStarTotal = model.ReviewStarTotal,
                    ReviewCount = model.ReviewCount,
                    image = model.image
                    
                    //image = filename

                };

            
                _context.Add(contractor);
                                         
                await _context.SaveChangesAsync(); //save the database with new customer

                //Now we need to update our contrator to put the image path in result
                //variable to hold values
                var contractorid = contractor.ContractorID;
                var businessName = contractor.BusinessName;
                var city = contractor.City;
                var email = contractor.Email;
                var firstName = contractor.FirstName;
                var lastName = contractor.LastName;
                var phoneNumber = contractor.PhoneNumber;
                var tradeID = contractor.TradeID;
                var reviewCount = contractor.ReviewCount;
                var starTotal = contractor.ReviewStarTotal;
                var AverageRating = contractor.AverageRating;
                var logo = "";
                if (model.image == null)
                {
                    logo = "imageNotFound" + ".jpg";
                }
                else
                {
                    //var logo = contractor.Email + ".jpg";
                     logo = contractor.FirstName + "_" + contractor.LastName + ".jpg";
                }



                //query the database
                string sqlUpdate = $" UPDATE Contractor" +
                                   $" SET AverageRating = {AverageRating}, BusinessName = '{businessName}', City = '{city}', Email = '{email}', FirstName = '{firstName}'," +
                                   $" LastName = '{lastName}', PhoneNumber = '{phoneNumber}', ReviewCount = {reviewCount}, TradeID = {tradeID} , ReviewStarTotal = {starTotal}, image = '{logo}' " +
                                   $" WHERE ContractorID = {contractorid}";
                //update the database
                _context.Database.ExecuteSqlCommand(sqlUpdate);


                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                //await _signInManager.SignInAsync(user, isPersistent: false);
                //_logger.LogInformation(3, "User created a new account with password.");
                return RedirectToAction("Index");


            }


            ViewData["TradeID"] = new SelectList(_context.Trades, "TradeID", "TradeID");
            return View(model);
        }

        // GET: Contractor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors.SingleOrDefaultAsync(m => m.ContractorID == id);
            if (contractor == null)
            {
                return NotFound();
            }
            ViewData["TradeID"] = new SelectList(_context.Trades, "TradeID", "TradeID", contractor.TradeID);
            return View(contractor);
        }

        // POST: Contractor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractorID,FirstName,LastName,Email,BusinessName,PhoneNumber,TradeID,City,ReviewCount,AverageRating")] Contractor contractor)
        {
            if (id != contractor.ContractorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorExists(contractor.ContractorID))
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
            ViewData["TradeID"] = new SelectList(_context.Trades, "TradeID", "TradeID", contractor.TradeID);
            return View(contractor);
        }

        // GET: Contractor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .Include(c => c.Trade)
                .SingleOrDefaultAsync(m => m.ContractorID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractor = await _context.Contractors.SingleOrDefaultAsync(m => m.ContractorID == id);
            _context.Contractors.Remove(contractor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractors.Any(e => e.ContractorID == id);
        }
    }
}
