using ArcaneAPI.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ArcaneAPI.Controllers
{
    [RoutePrefix("api/ArticleTypes")]
    public class ArticleTypesController : ApiController
    {
        private ArticleTypeRepository Repository = new ArticleTypeRepository();

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        [ResponseType(typeof(IEnumerable<ArticleTypeDTO>))]
        public IHttpActionResult GetArticlesByType(int articleTypeId)
        {
            try
            {
                IEnumerable<ArticleTypeDTO> articleTypes = Repository.Get().Select(d => d.DTO);
                return Ok(articleTypes);
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
    }
}
