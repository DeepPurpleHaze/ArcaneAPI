namespace ArcaneAPI.Models.GameModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountCharacter")]
    public partial class AccountCharacter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        [StringLength(10)]
        public string Id { get; set; }

        [StringLength(10)]
        public string GameID1 { get; set; }

        [StringLength(10)]
        public string GameID2 { get; set; }

        [StringLength(10)]
        public string GameID3 { get; set; }

        [StringLength(10)]
        public string GameID4 { get; set; }

        [StringLength(10)]
        public string GameID5 { get; set; }

        [StringLength(10)]
        public string GameIDC { get; set; }

        public byte? MoveCnt { get; set; }

        public int ExtClass { get; set; }

        public int ExtWarehouse { get; set; }
    }
}
