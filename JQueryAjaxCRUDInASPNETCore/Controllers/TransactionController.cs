using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JQueryAjaxCRUDInASPNETCore.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace JQueryAjaxCRUDInASPNETCore.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransactionController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }



        // GET: Transaction/AddOrEdit
        [Helper.NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new TransactionModal());

            }
            else
            {
                var transactionModal = await _context.Transactions.FindAsync(id);
                if (transactionModal == null)
                {
                    return NotFound();
                }
                return View(transactionModal);
            }
        }




        // POST: Transaction/AddOrEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("TransActionId,AccountNumber,BeneficiaryName,BankName,WWIFTCode,Amount,Date")] TransactionModal transactionModal)
        {
        

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    transactionModal.Date = DateTime.Now;
                        _context.Add(transactionModal);
                        await _context.SaveChangesAsync();
               
                }
                else
                {
                    try
                    {
                        _context.Update(transactionModal);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModalExists(transactionModal.TransActionId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

           
                return Json(new {isValid=true,html=Helper.RenderRazorViewToString(this,"_ViewAll",_context.Transactions.ToList())});
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModal) });
        }



        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModal = await _context.Transactions.FindAsync(id);
            if (transactionModal != null)
            {
                _context.Transactions.Remove(transactionModal);
            }

            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
        }

        private bool TransactionModalExists(int id)
        {
            return _context.Transactions.Any(e => e.TransActionId == id);
        }
    }
}
