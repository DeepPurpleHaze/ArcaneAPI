namespace ArcaneAPI.Models.GameModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public partial class WZ_CW_INFO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAP_SVR_GROUP { get; set; }

        public int? CRYWOLF_OCCUFY { get; set; }

        public int? CRYWOLF_STATE { get; set; }
    }
}
