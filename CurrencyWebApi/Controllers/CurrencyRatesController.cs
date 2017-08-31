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
using CurrencyWebApi.Models;

namespace CurrencyWebApi.Controllers
{
    public class CurrencyRatesController : ApiController
    {
        private CurrencyWebApiContext db = new CurrencyWebApiContext();

        // GET: api/CurrencyRates
        public IQueryable<CurrencyRate> GetCurrencyRates()
        {
            return db.CurrencyRates;
        }

        // GET: api/CurrencyRates/5
        [ResponseType(typeof(CurrencyRate))]
        public async Task<IHttpActionResult> GetCurrencyRate(int id)
        {
            CurrencyRate currencyRate = await db.CurrencyRates.FindAsync(id);
            if (currencyRate == null)
            {
                return NotFound();
            }

            return Ok(currencyRate);
        }

        // PUT: api/CurrencyRates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCurrencyRate(int id, CurrencyRate currencyRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currencyRate.Id)
            {
                return BadRequest();
            }

            db.Entry(currencyRate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyRateExists(id))
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

        // POST: api/CurrencyRates
        [ResponseType(typeof(CurrencyRate))]
        public async Task<IHttpActionResult> PostCurrencyRate(CurrencyRate currencyRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CurrencyRates.Add(currencyRate);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = currencyRate.Id }, currencyRate);
        }

        // DELETE: api/CurrencyRates/5
        [ResponseType(typeof(CurrencyRate))]
        public async Task<IHttpActionResult> DeleteCurrencyRate(int id)
        {
            CurrencyRate currencyRate = await db.CurrencyRates.FindAsync(id);
            if (currencyRate == null)
            {
                return NotFound();
            }

            db.CurrencyRates.Remove(currencyRate);
            await db.SaveChangesAsync();

            return Ok(currencyRate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurrencyRateExists(int id)
        {
            return db.CurrencyRates.Count(e => e.Id == id) > 0;
        }
    }
}