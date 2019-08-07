using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ArcaneAPI.Models.CustomModels;

namespace ArcaneAPI.Controllers
{
    [RoutePrefix("api/Articles")]
    public class ArticlesController : ApiController
    {
        private ArticleRepository Repository = new ArticleRepository();

        [AllowAnonymous]
        [HttpGet]
        [Route("GetByType")]
        [ResponseType(typeof(IEnumerable<ArticleDTO>))]
        public IHttpActionResult GetArticlesByType(int articleTypeId)
        {
            try
            {
                var articles = Repository.GetByArticleType(articleTypeId).Select(d => d.DTO);
                return Ok(articles);
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

        [AllowAnonymous]
        [HttpGet]
        [Route("Get")]
        [ResponseType(typeof(ArticleDTO))]
        public IHttpActionResult GetArticle(int id)
        {
            try
            {
                Article article = Repository.GetByID(id);
                return Ok(article.DTO);
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

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("Put")]
        [ResponseType(typeof(ArticleDTO))]
        public IHttpActionResult PutArticle(int id, ArticleDTO article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != article.Id)
                {
                    return BadRequest();
                }

                Repository.Update(article.ToModel());
                Article updated = Repository.GetByID(id);
                return Ok(updated.DTO);
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Post")]
        [ResponseType(typeof(ArticleDTO))]
        public IHttpActionResult PostArticle(ArticleDTO article)
        {
            //ModelState.Remove("Model");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var inserted = Repository.Insert(article.ToModel());
                ArticleDTO dto = inserted.DTO;
                return Ok(dto);
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

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("Delete")]
        [ResponseType(typeof(ArticleDTO))]
        public IHttpActionResult DeleteArticle(int id)
        {
            try
            {
                ArticleDTO dto = Repository.GetAsNoTracking(d => d.Id == id).First().DTO;
                Repository.Delete(id);
                return Ok(dto);
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