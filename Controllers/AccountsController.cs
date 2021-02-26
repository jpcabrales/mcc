using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MoulaChallenge.Models;
using Newtonsoft.Json;

namespace MoulaChallenge.Controllers
{
    public class AccountsController : ApiController
    {
        private MoulaServiceContext db = new MoulaServiceContext();

        // GET: api/Accounts
        public IQueryable<Account> GetAccounts()
        {
            return db.Accounts;
        }

        // GET: api/GetAccountsPaymentList/5
        [ResponseType(typeof(AccountDetailDTO))]
        public async Task<IHttpActionResult> GetAccountBalancePayment(int id)
        {
            var account = db.Accounts.Select(a =>
               new AccountDetailDTO()
               {
                   Id = a.Id,
                   Name = a.Name,
                   Balance = a.Balance
               }).SingleOrDefault(a => a.Id == id);

            account.Payments = db.Payments.Select(p =>
                new PaymentDTO()
                {
                    Id = p.Id,
                    Date = p.Date,
                    Amount = p.Amount,
                    Status = (p.Status == 1 ? "Open" : "Closed"),
                    Reason = p.Reason,
                    AccountId = p.AccountId
                }).Where(p => p.AccountId == id).OrderByDescending(p => p.Date).ToList();

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAccount(int id, Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.Id)
            {
                return BadRequest();
            }

            db.Entry(account).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Accounts
        [ResponseType(typeof(Account))]
        public async Task<IHttpActionResult> PostAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accounts.Add(account);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Account))]
        public async Task<IHttpActionResult> DeleteAccount(int id)
        {
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(account);
            await db.SaveChangesAsync();

            return Ok(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountExists(int id)
        {
            return db.Accounts.Count(e => e.Id == id) > 0;
        }
    }
}