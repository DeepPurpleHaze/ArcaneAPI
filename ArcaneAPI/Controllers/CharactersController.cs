using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private CharacterRepository Repository = new CharacterRepository();

        // GET: api/Characters
        [ResponseType(typeof(IEnumerable<CharacterDTO>))]
        public IHttpActionResult GetCharacter()
        {
            return Ok(Repository.GetWithIncludes().Select(d => d.DTO));
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

        // GET: api/Characters/Top
        [HttpGet]
        [Route("Top")]
        [ResponseType(typeof(IEnumerable<Character>))]
        public IHttpActionResult Top(int race = 0)
        {
            return Ok(Repository.Get(orderBy: q => q.OrderByDescending(c => c.MasterResetCount).ThenByDescending(c => c.ResetCount).ThenByDescending(c => c.cLevel).ThenBy(c => c.Name), includeProperties: "GuildMember, MEMB_STAT", take: 100).Select(d => d.DTO));
            //return db.Character;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}