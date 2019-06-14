using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArcaneAPI.Models.CustomModels
{
    public class News
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [StringLength(4000)]
        public string Body { get; set; }

        [StringLength(150)]
        public string Title { get; set; }
    }
}