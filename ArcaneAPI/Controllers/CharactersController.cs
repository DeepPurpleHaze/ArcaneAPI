using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ArcaneAPI.Models.GameModels;

namespace ArcaneAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Characters")]
    public class CharactersController : ApiController
    {
        private CharacterRepository Repository = new CharacterRepository();

        [HttpGet]
        [Route("GetById")]
        [ResponseType(typeof(CharacterDTO))]
        public IHttpActionResult GetCharacter(string id)
        {
            try
            {
                CharacterDTO character = Repository.GetById(id).DTO;
                return Ok(character);
            }            
            catch (NullReferenceException nre)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("Top")]
        [ResponseType(typeof(IEnumerable<Character>))]
        public IHttpActionResult Top(int race = 0)
        {
            try
            { 
                return Ok(Repository.Top(race).Select(d => d.DTO));
            }            
            catch (NullReferenceException nre)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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