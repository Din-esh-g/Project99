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


                _context.Update(transaction);
                await _context.SaveChangesAsync();




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


                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
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

        //Transfer
        public IActionResult Transfer(int id)
        {


            ViewData["Id"] = id;
            return View();



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(int id, int amount, int tid, string type)
        {


            try
            {
                Checking checking = new Checking();
                checking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == id);

                if (type == "checking")
                {
                    Checking tochecking = new Checking();
                    tochecking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == tid);

                    if (tochecking != null)
                    {
                        if (checking.Customers != tochecking.Customers)
                        {
                            ViewData["ErrorMessage"] = $"You can only transfer between your own accounts";
                            return View();
                        }
                        else if (checking.Balance < amount)
                        {
                            ViewData["ErrorMessage"] = $"You tried to transfer ${amount} but your balance is only ${checking.Balance}";
                            return View();
                        }
                        else
                        {
                            var newBalance = (checking.Balance - amount);
                            checking.Balance = newBalance;

                            Transaction transaction = new Transaction();
                            transaction.accountNumber = id;
                            transaction.accountType = "checking";
                            transaction.amount = amount;
                            transaction.date = DateTime.Now;
                            transaction.type = "transer out";

                            _context.Update(checking);
                            await _context.SaveChangesAsync();


                            //_context.Update(transaction);
                            //await _context.SaveChangesAsync();


                            var tonewBalance = (tochecking.Balance + amount);
                            tochecking.Balance = tonewBalance;

                            Transaction totransaction = new Transaction();
                            totransaction.accountNumber = tid;
                            totransaction.accountType = "checking";
                            totransaction.amount = amount;
                            totransaction.date = DateTime.Now;
                            totransaction.type = "transfer in";

                            _context.Update(tochecking);
                            await _context.SaveChangesAsync();

                            _context.Update(transaction);
                            await _context.SaveChangesAsync();


                        }
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = $"Please enter a valid account to transfer into.";
                        return View();
                    }
                }
                else
                {
                    Business business = new Business();
                    business = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == tid);



                    if (business != null)
                    {
                        if (checking.Customers != business.Customers)
                        {
                            ViewData["ErrorMessage"] = $"You can only transfer between your own accounts";
                            return View();
                        }
                        else if (checking.Balance < amount)
                        {
                            ViewData["ErrorMessage"] = $"You tried to transfer ${amount} but your balance is only ${checking.Balance}";
                            return View();
                        }
                        else
                        {
                            var newBalance = (checking.Balance - amount);
                            checking.Balance = newBalance;

                            Transaction transaction = new Transaction();
                            transaction.accountNumber = id;
                            transaction.accountType = "checking";
                            transaction.amount = amount;
                            transaction.date = DateTime.Now;
                            transaction.type = "transer out";


                            _context.Update(checking);
                            await _context.SaveChangesAsync();


                            _context.Update(transaction);
                            await _context.SaveChangesAsync();


                            var tonewBalance = (business.Balance + amount);
                            business.Balance = tonewBalance;

                            Transaction totransaction = new Transaction();
                            totransaction.accountNumber = tid;
                            totransaction.accountType = "business";
                            totransaction.amount = amount;
                            totransaction.date = DateTime.Now;
                            totransaction.type = "transfer in";


                            _context.Update(business);
                            await _context.SaveChangesAsync();

                            _context.Update(transaction);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = $"Please enter a valid account to transfer into.";
                        return View();
                    }
                }
            }
            catch
            {
                ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
                return View();
            }
            ViewData["ErrorMessage"] = "Transfer was sucessful";
            return RedirectToAction(nameof(Index));//Home Index


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
                Transaction transaction = new Transaction();
                transaction.accountNumber = checking.accountNumber;
                       
                transaction.accountType = "checking";
                transaction.amount = checking.Balance;
                transaction.date = DateTime.Now;
                transaction.type = "Opening";

                _context.Update(transaction);
                await _context.SaveChangesAsync();




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
