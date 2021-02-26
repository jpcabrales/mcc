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

namespace MoulaChallenge.Controllers
{
    public class PaymentController : ApiController
    {
        private MoulaServiceContext db = new MoulaServiceContext();

        // GET: api/Payments
        public IQueryable<Payment> GetPayments()
        {
            return db.Payments;
        }

        // GET: api/Payments/5
        [ResponseType(typeof(Payment))]
        public async Task<IHttpActionResult> GetPayments(int id)
        {
            Payment payments = await db.Payments.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }

            return Ok(payments);
        }

        // PUT: api/Payments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPayments(int id, Payment payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payments.Id)
            {
                return BadRequest();
            }

            db.Entry(payments).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentsExists(id))
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

        // POST: api/Payments
        [ResponseType(typeof(Payment))]
        public async Task<IHttpActionResult> PostPayments(Payment payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(payments);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = payments.Id }, payments);
        }

        // DELETE: api/Payments/5
        [ResponseType(typeof(Payment))]
        public async Task<IHttpActionResult> DeletePayments(int id)
        {
            Payment payments = await db.Payments.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payments);
            await db.SaveChangesAsync();

            return Ok(payments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentsExists(int id)
        {
            return db.Payments.Count(e => e.Id == id) > 0;
        }
    }
}