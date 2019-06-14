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
    [AllowAnonymous]
    [RoutePrefix("api/Characters")]
    public class CharactersController : ApiController
    {
        private MainContext db = new MainContext();
        private GenericRepository<Character> Repository = new GenericRepository<Character>();

        // GET: api/Characters
        [ResponseType(typeof(IEnumerable<Character>))]
        public IHttpActionResult GetCharacter()
        {
            return Ok(Repository.Get(includeProperties: "GuildMember").Select(d => d.DTO));
            //return db.Character;
        }

        // GET: api/Characters/5
        [ResponseType(typeof(Character))]
        public IHttpActionResult GetCharacter(string id)
        {
            Character character = db.Character.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // PUT: api/Characters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCharacter(string id, Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != character.Name)
            {
                return BadRequest();
            }

            db.Entry(character).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/Characters
        [ResponseType(typeof(Character))]
        public IHttpActionResult PostCharacter(Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Character.Add(character);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CharacterExists(character.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = character.Name }, character);
        }

        // DELETE: api/Characters/5
        [ResponseType(typeof(Character))]
        public IHttpActionResult DeleteCharacter(string id)
        {
            Character character = db.Character.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            db.Character.Remove(character);
            db.SaveChanges();

            return Ok(character);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharacterExists(string id)
        {
            return db.Character.Count(e => e.Name == id) > 0;
        }
    }
}