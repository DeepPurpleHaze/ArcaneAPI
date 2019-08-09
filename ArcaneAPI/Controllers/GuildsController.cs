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
    [RoutePrefix("api/Guilds")]
    public class GuildsController : ApiController
    {
        private GuildRepository Repository = new GuildRepository();
                
        [HttpGet]
        [Route("GetAll")]
        [ResponseType(typeof(IEnumerable<GuildDTO>))]
        public IHttpActionResult GetGuilds()
        {
            try
            {
                return Ok(Repository.GetWithIncludes().Select(d => d.DTO));
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
        [Route("GetById")]
        [ResponseType(typeof(GuildDTO))]
        public IHttpActionResult GetGuild(string id)
        {
            try
            {
                Guild guild = Repository.GetByID(id);
                return Ok(guild.DTO);
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