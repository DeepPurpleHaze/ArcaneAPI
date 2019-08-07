using ArcaneAPI.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArcaneAPI.Models.CustomModels
{
    [Table("Article")]
    public class Article
    {
        public Article() { }

        public Article(ArticleDTO item)
        {
            Id = item.Id;
            Date = item.Date;
            Body = item.Body;
            Title = item.Title;
            Description = item.Description;
            ArticleTypeId = item.ArticleTypeId;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Body { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int ArticleTypeId { get; set; }

        public virtual ArticleType ArticleType { get; set; }

        public ArticleDTO DTO { get { return new ArticleDTO(this); } }
    }

    public class ArticleDTO
    {
        public ArticleDTO() { }

        public ArticleDTO(Article item)
        {
            Id = item.Id;
            Date = item.Date;
            Body = item.Body;
            Title = item.Title;
            Description = item.Description;
            ArticleTypeId = item.ArticleTypeId;
            ArticleType = item.ArticleType?.DTO;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Body { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int ArticleTypeId { get; set; }

        public ArticleTypeDTO ArticleType { get; set; }

        public Article ToModel()
        {
            return new Article(this);
        }
    }

    internal class ArticleRepository : ModelRepository<Article>
    {
        public ArticleRepository() : base(ips) { }

        internal static string ips = @"ArticleType";

        internal IEnumerable<Article> GetByArticleType(int articleTypeId)
        {
            return Get(d => d.ArticleTypeId == articleTypeId, d => d.OrderByDescending(q => q.Date));
        }

        internal new Article Insert(Article item)
        {
            base.Insert(item);
            return item;
        }
    }
}