using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Domain.VirtualAccount;
using Repository.Data;

namespace ScholarshipManagementSystem.Controllers.VirtualAccount
{
    public class PaymentDisbursementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentDisbursementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentDisbursements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PaymentDisbursement.Include(p => p.Applicant).Include(p => p.PaymentMethod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentDisbursements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDisbursement = await _context.PaymentDisbursement
                .Include(p => p.Applicant)
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(m => m.PaymentDisbursementId == id);
            if (paymentDisbursement == null)
            {
                return NotFound();
            }

            return View(paymentDisbursement);
        }

        // GET: PaymentDisbursements/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId");
            return View();
        }

        // POST: PaymentDisbursements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentDisbursementId,TrancheDocumentId,ApplicantReferenceNo,DDNo,ChequeNo,DDReceiver,DDReceiverCNIC,DDRelationWithScholar,DDReceiverContactNo,TransactionId,TransactionStatus,CustomerName,CustomerCnic,MobileNumber,District,DisbursementAmount,TransactionType,TransactionAmount,TransactionDate,DirectAgentId,AgentName,City,ApplicantId,PaymentMethodId,DDScannedCopy,ChequeScannedCopy,OtherDocumentScannedCopy")] PaymentDisbursement paymentDisbursement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentDisbursement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", paymentDisbursement.ApplicantId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", paymentDisbursement.PaymentMethodId);
            return View(paymentDisbursement);
        }

        // GET: PaymentDisbursements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDisbursement = await _context.PaymentDisbursement.FindAsync(id);
            if (paymentDisbursement == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", paymentDisbursement.ApplicantId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", paymentDisbursement.PaymentMethodId);
            return View(paymentDisbursement);
        }

        // POST: PaymentDisbursements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentDisbursementId,TrancheDocumentId,ApplicantReferenceNo,DDNo,ChequeNo,DDReceiver,DDReceiverCNIC,DDRelationWithScholar,DDReceiverContactNo,TransactionId,TransactionStatus,CustomerName,CustomerCnic,MobileNumber,District,DisbursementAmount,TransactionType,TransactionAmount,TransactionDate,DirectAgentId,AgentName,City,ApplicantId,PaymentMethodId,DDScannedCopy,ChequeScannedCopy,OtherDocumentScannedCopy")] PaymentDisbursement paymentDisbursement)
        {
            if (id != paymentDisbursement.PaymentDisbursementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentDisbursement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentDisbursementExists(paymentDisbursement.PaymentDisbursementId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Applicant, "ApplicantId", "ApplicantReferenceNo", paymentDisbursement.ApplicantId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", paymentDisbursement.PaymentMethodId);
            return View(paymentDisbursement);
        }

        // GET: PaymentDisbursements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDisbursement = await _context.PaymentDisbursement
                .Include(p => p.Applicant)
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(m => m.PaymentDisbursementId == id);
            if (paymentDisbursement == null)
            {
                return NotFound();
            }

            return View(paymentDisbursement);
        }

        // POST: PaymentDisbursements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentDisbursement = await _context.PaymentDisbursement.FindAsync(id);
            _context.PaymentDisbursement.Remove(paymentDisbursement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentDisbursementExists(int id)
        {
            return _context.PaymentDisbursement.Any(e => e.PaymentDisbursementId == id);
        }
    }
}
