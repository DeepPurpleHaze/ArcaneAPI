namespace ArcaneAPI.Models.GameModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("warehouse")]
    public partial class warehouse
    {
        [Key]
        [StringLength(10)]
        public string AccountID { get; set; }

        [MaxLength(3840)]
        public byte[] Items { get; set; }

        public int? Money { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EndUseDate { get; set; }

        public byte? DbVersion { get; set; }

        public short? pw { get; set; }

        public int Number { get; set; }
    }
}
