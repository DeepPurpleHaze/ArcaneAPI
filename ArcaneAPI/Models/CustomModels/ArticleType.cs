using ArcaneAPI.Models.Context;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArcaneAPI.Models.CustomModels
{
    [Table("ArticleType")]
    public class ArticleType
    {
        public ArticleType() { }

        public ArticleType(ArticleTypeDTO item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public ArticleTypeDTO DTO { get { return new ArticleTypeDTO(this); } }
    }

    public class ArticleTypeDTO
    {
        public ArticleTypeDTO() { }

        public ArticleTypeDTO(ArticleType item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }

    internal class ArticleTypeRepository: ModelRepository<ArticleType>
    {

    }
}