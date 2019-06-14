using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArcaneAPI.Models.CustomModels
{
    public class FAQ
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        public string Body { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}