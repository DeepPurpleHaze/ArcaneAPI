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
            return Ok(Repository.Get(includeProperties: "GuildMember, MEMB_STAT").Select(d => d.DTO));
            //return db.Character;
        }

        // GET: api/Characters/5
        [ResponseType(typeof(CharacterDTO))]
        public IHttpActionResult GetCharacter(string id)
        {
            var temp = Repository.Get(d => d.Name == id, includeProperties: "GuildMember, MEMB_STAT")?.FirstOrDefault();
            CharacterDTO character = temp?.DTO;

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharacterExists(string id)
        {
            return db.Character.Count(e => e.Name == id) > 0;
        }
    }
}