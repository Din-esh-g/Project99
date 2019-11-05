using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project99.Models;
using Project99.Models.Class;

namespace Project99.Controllers
{
    [Authorize]
    public class BusinessesController : Controller
    {
        private readonly BankContext _context;

        public BusinessesController(BankContext context)
        {
            _context = context;
        }

        // GET: Businesses
        public async Task<IActionResult> Index()
        {

            return View(await _context.Business.ToListAsync());
        }






        public async Task<IActionResult> withCustomerId(int id)
        {
            try
            {


                var customers = await _context.Customers.Where(b => b.Id == id).ToListAsync();


                if (customers.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("withCustomerId");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }




        //Deposit Withdraw transfer





        /// <summary>
        /// //////////////////
        /// </summary>
        /// <returns></returns>



        public IActionResult Withdraw(int id)
        {


            ViewData["Id"] = id;
            return View();



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id, int amount)
        {


            try
            {
                Business business = new Business();
                business = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == id);
                                
                var newBalance = (business.Balance - amount);
                business.Balance = newBalance;

                Transaction transaction = new Transaction();
                transaction.accountNumber = id;
                transaction.accountType = "business";
                transaction.amount = amount;
                transaction.date = DateTime.Now;
                transaction.type = "withdraw";

                _context.Update(business);
                await _context.SaveChangesAsync();


                //_context.Update(transaction);
                //await _context.SaveChangesAsync();



            }
            catch
            {
                ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
                return View();
            }
            return RedirectToAction(nameof(Business));


        }

        //public IActionResult Deposit(int id)
        //{



        //    ViewData["Id"] = id;
        //    return View();



        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Deposit(int id, int amount)
        //{



        //    try
        //    {
        //        Business business = new Business();
        //        business = await _context.Business.FirstOrDefaultAsync(c => c.Id == id);


        //        var newBalance = (business.Balance + amount);
        //        business.Balance = newBalance;


        //        Transaction transaction = new Transaction();
        //        transaction.accountId = id;
        //        transaction.accountType = "business";
        //        transaction.amount = amount;
        //        transaction.date = DateTime.Now;
        //        transaction.type = "deposit";

        //        _context.Update(business);
        //        await _context.SaveChangesAsync();


        //        _context.Update(transaction);
        //        await _context.SaveChangesAsync();

        //    }
        //    catch
        //    {
        //        ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
        //        return View();
        //    }
        //    return RedirectToAction(nameof(MyBusiness));


        //}

        //public IActionResult Transfer(int id)
        //{


        //    ViewData["Id"] = id;
        //    return View();



        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Transfer(int id, int amount, int tid, string type)
        //{


        //    try
        //    {
        //        Business business = new Business();
        //        business = await _context.Business.FirstOrDefaultAsync(c => c.Id == id);

        //        if (type == "checking")
        //        {
        //            Checking tochecking = new Checking();
        //            tochecking = await _context.Checking.FirstOrDefaultAsync(c => c.Id == tid);

        //            if (tochecking != null)
        //            {
        //                if (business.CustomerId != tochecking.CustomerId)
        //                {
        //                    ViewData["ErrorMessage"] = $"You can only transfer between your own accounts";
        //                    return View();
        //                }
        //                else
        //                {

        //                    var newBalance = (business.Balance - amount);
        //                    business.Balance = newBalance;


        //                    Transaction transaction = new Transaction();
        //                    transaction.accountId = id;
        //                    transaction.accountType = "business";
        //                    transaction.amount = amount;
        //                    transaction.date = DateTime.Now;
        //                    transaction.type = "transfer out";

        //                    _context.Update(business);
        //                    await _context.SaveChangesAsync();


        //                    _context.Update(transaction);
        //                    await _context.SaveChangesAsync();



        //                    var tonewBalance = (tochecking.Balance + amount);
        //                    tochecking.Balance = tonewBalance;


        //                    Transaction totransaction = new Transaction();
        //                    totransaction.accountId = tid;
        //                    totransaction.accountType = "checking";
        //                    totransaction.amount = amount;
        //                    totransaction.date = DateTime.Now;
        //                    totransaction.type = "transfer in";


        //                    _context.Update(tochecking);
        //                    await _context.SaveChangesAsync();


        //                    _context.Update(totransaction);
        //                    await _context.SaveChangesAsync();
        //                }



        //            }
        //            else
        //            {
        //                ViewData["ErrorMessage"] = $"Please enter a valid account to transfer into.";
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            Business tobusiness = new Business();
        //            tobusiness = await _context.Business.FirstOrDefaultAsync(c => c.Id == tid);



        //            if (tobusiness != null)
        //            {
        //                if (business.CustomerId != tobusiness.CustomerId)
        //                {
        //                    ViewData["ErrorMessage"] = $"You can only transfer between your own accounts";
        //                    return View();
        //                }
        //                else
        //                {

        //                    var newBalance = (business.Balance - amount);
        //                    business.Balance = newBalance;



        //                    Transaction transaction = new Transaction();
        //                    transaction.accountId = id;
        //                    transaction.accountType = "business";
        //                    transaction.amount = amount;
        //                    transaction.date = DateTime.Now;
        //                    transaction.type = "transfer out";

        //                    _context.Update(business);
        //                    await _context.SaveChangesAsync();


        //                    _context.Update(transaction);
        //                    await _context.SaveChangesAsync();


        //                    var tonewBalance = (tobusiness.Balance + amount);
        //                    tobusiness.Balance = tonewBalance;

        //                    Transaction totransaction = new Transaction();
        //                    totransaction.accountId = tid;
        //                    totransaction.accountType = "business";
        //                    totransaction.amount = amount;
        //                    totransaction.date = DateTime.Now;
        //                    totransaction.type = "transfer in";

        //                    _context.Update(tobusiness);
        //                    await _context.SaveChangesAsync();


        //                    _context.Update(totransaction);
        //                    await _context.SaveChangesAsync();

        //                }


        //            }
        //            else
        //            {
        //                ViewData["ErrorMessage"] = $"Please enter a valid account to transfer into.";
        //                return View();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
        //        return View();
        //    }
        //    return RedirectToAction(nameof(MyBusiness));


        //}










        // GET: Businesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Business
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (business == null)
            {
                return NotFound();
            }

            return View(business);
        }

        // GET: Businesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("type,accountNumber,InterestRate,Balance,createdAt")] Business business)
        {
            if (ModelState.IsValid)
            {
                _context.Add(business);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(business);
        }

        // GET: Businesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Business.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }
            return View(business);
        }

        // POST: Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("type,accountNumber,InterestRate,Balance,createdAt")] Business business)
        {
            if (id != business.accountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(business);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessExists(business.accountNumber))
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
            return View(business);
        }

        // GET: Businesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Business
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (business == null)
            {
                return NotFound();
            }

            return View(business);
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var business = await _context.Business.FindAsync(id);
            _context.Business.Remove(business);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessExists(int id)
        {
            return _context.Business.Any(e => e.accountNumber == id);
        }
        public int? sessionGetId()
        {
            var val = HttpContext.Session.GetInt32("Id");
            return val;
        }
    }
}
