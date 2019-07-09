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
using ArcaneAPI.Models.GameModels;

namespace ArcaneAPI.Controllers
{
    public class GuildsController : ApiController
    {
        private GuildRepository Repository = new GuildRepository();

        // GET: api/Guilds
        [ResponseType(typeof(IEnumerable<GuildDTO>))]
        public IHttpActionResult GetGuild()
        {
            return Ok(Repository.GetWithIncludes());
        }

        // GET: api/Guilds/5
        [ResponseType(typeof(GuildDTO))]
        public IHttpActionResult GetGuild(string id)
        {
            Guild guild = db.Guild.Find(id);
            if (guild == null)
            {
                return NotFound();
            }

            return Ok(guild);
        }

        // PUT: api/Guilds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuild(string id, Guild guild)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guild.G_Name)
            {
                return BadRequest();
            }

            db.Entry(guild).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuildExists(id))
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

        // POST: api/Guilds
        [ResponseType(typeof(Guild))]
        public IHttpActionResult PostGuild(Guild guild)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guild.Add(guild);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GuildExists(guild.G_Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = guild.G_Name }, guild);
        }

        // DELETE: api/Guilds/5
        [ResponseType(typeof(Guild))]
        public IHttpActionResult DeleteGuild(string id)
        {
            Guild guild = db.Guild.Find(id);
            if (guild == null)
            {
                return NotFound();
            }

            db.Guild.Remove(guild);
            db.SaveChanges();

            return Ok(guild);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuildExists(string id)
        {
            return db.Guild.Count(e => e.G_Name == id) > 0;
        }
    }
}