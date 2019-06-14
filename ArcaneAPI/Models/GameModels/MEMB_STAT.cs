namespace ArcaneAPI.Models.GameModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MEMB_STAT
    {
        [Key]
        [StringLength(10)]
        public string memb___id { get; set; }

        public byte? ConnectStat { get; set; }

        [StringLength(50)]
        public string ServerName { get; set; }

        [StringLength(15)]
        public string IP { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ConnectTM { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DisConnectTM { get; set; }

        public int? OnlineHours { get; set; }
    }
}
