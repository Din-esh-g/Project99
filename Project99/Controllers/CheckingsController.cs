using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project99.Models;
using Project99.Models.Class;

namespace Project99.Controllers
{
    public class CheckingsController : Controller
    {
        private readonly BankContext _context;

        public CheckingsController(BankContext context)
        {
            _context = context;
        }

        // GET: Checkings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Checking.ToListAsync());
        }

        //Deposit
        public IActionResult Deposit(int id)
        {


            ViewData["Id"] = id;
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(int id, double amount)
        {


            try
            {
                Checking checking = new Checking();
                checking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == id);


                var newBalance = (checking.Balance + amount);
                checking.Balance = newBalance;

                _context.Update(checking);
                await _context.SaveChangesAsync();

                Transaction transaction = new Transaction();
                transaction.accountNumber = id;
                transaction.accountType = "checking";
                transaction.amount = amount;
                transaction.date = DateTime.Now;
                transaction.type = "deposit";

                //_context.Update(checking);
                //await _context.SaveChangesAsync();


                //_context.Update(transaction);
                //await _context.SaveChangesAsync();




            }
            catch
            {
                ViewData["ErrorMessage"] = "There was a problem with your deposit please try again";
                return View();
            }
            return RedirectToAction(nameof(Index));//Nav to the Home


        }

        //Deposit
        public IActionResult Withdraw(int id)
        {


            ViewData["Id"] = id;
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id, double amount)
        {


            try
            {
                Checking checking = new Checking();
                checking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == id);

                if (checking.Balance>=amount)
                {
                    var newBalance = (checking.Balance - amount);
                    checking.Balance = newBalance;

                    _context.Update(checking);
                    await _context.SaveChangesAsync();

                    Transaction transaction = new Transaction();
                    transaction.accountNumber = id;
                    transaction.accountType = "checking";
                    transaction.amount = amount;
                    transaction.date = DateTime.Now;
                    transaction.type = "deposit";

                    //_context.Update(checking);
                    //await _context.SaveChangesAsync();


                    //_context.Update(transaction);
                    //await _context.SaveChangesAsync();
                }
                else
                {

                    ViewData["ErrorMessage"] = "No Sufficent AMT";
                    return View();
                }




            }
            catch
            {
                ViewData["ErrorMessage"] = "There was a problem with your Withdraw please try again";
                return View();
            }
            return RedirectToAction(nameof(Index));//Nav to the Home


        }



        // GET: Checkings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checking = await _context.Checking
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (checking == null)
            {
                return NotFound();
            }

            return View(checking);
        }

        // GET: Checkings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Checkings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("type,accountNumber,InterestRate,Balance,createdAt")] Checking checking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checking);
        }

        // GET: Checkings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checking = await _context.Checking.FindAsync(id);
            if (checking == null)
            {
                return NotFound();
            }
            return View(checking);
        }

        // POST: Checkings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("type,accountNumber,InterestRate,Balance,createdAt")] Checking checking)
        {
            if (id != checking.accountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckingExists(checking.accountNumber))
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
            return View(checking);
        }

        // GET: Checkings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checking = await _context.Checking
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (checking == null)
            {
                return NotFound();
            }

            return View(checking);
        }

        // POST: Checkings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checking = await _context.Checking.FindAsync(id);
            _context.Checking.Remove(checking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckingExists(int id)
        {
            return _context.Checking.Any(e => e.accountNumber == id);
        }
    }
}
