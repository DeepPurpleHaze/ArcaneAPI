using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ArcaneAPI.Models.Context;
using ArcaneAPI.Models.CustomModels;

namespace ArcaneAPI.Controllers
{
    public class FAQsController : ApiController
    {
        private MainContext db = new MainContext();

        // GET: api/FAQs
        public IQueryable<FAQ> GetFAQ()
        {
            return db.FAQ;
        }

        // GET: api/FAQs/5
        [ResponseType(typeof(FAQ))]
        public IHttpActionResult GetFAQ(int id)
        {
            FAQ fAQ = db.FAQ.Find(id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return Ok(fAQ);
        }

        // PUT: api/FAQs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFAQ(int id, FAQ fAQ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fAQ.Id)
            {
                return BadRequest();
            }

            db.Entry(fAQ).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FAQExists(id))
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

        // POST: api/FAQs
        [ResponseType(typeof(FAQ))]
        public IHttpActionResult PostFAQ(FAQ fAQ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FAQ.Add(fAQ);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fAQ.Id }, fAQ);
        }

        // DELETE: api/FAQs/5
        [ResponseType(typeof(FAQ))]
        public IHttpActionResult DeleteFAQ(int id)
        {
            FAQ fAQ = db.FAQ.Find(id);
            if (fAQ == null)
            {
                return NotFound();
            }

            db.FAQ.Remove(fAQ);
            db.SaveChanges();

            return Ok(fAQ);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FAQExists(int id)
        {
            return db.FAQ.Count(e => e.Id == id) > 0;
        }
    }
}