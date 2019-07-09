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
            return Ok(Repository.GetWithIncludes().Select(d => d.DTO));
        }

        // GET: api/Guilds/5
        [ResponseType(typeof(GuildDTO))]
        public IHttpActionResult GetGuild(string id)
        {
            Guild guild = Repository.GetByID(id);
            if (guild == null)
            {
                return NotFound();
            }

            return Ok(guild);
        }        

        // DELETE: api/Guilds/5
        //[ResponseType(typeof(Guild))]
        //public IHttpActionResult DeleteGuild(string id)
        //{
        //    Guild guild = db.Guild.Find(id);
        //    if (guild == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Guild.Remove(guild);
        //    db.SaveChanges();

        //    return Ok(guild);
        //}

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