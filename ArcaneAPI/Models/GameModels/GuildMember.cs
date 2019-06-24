namespace ArcaneAPI.Models.GameModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GuildMember")]
    public partial class GuildMember
    {
        [Key]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(8)]
        public string G_Name { get; set; }

        public byte? G_Level { get; set; }

        public byte G_Status { get; set; }

        public byte G_WareStatus { get; set; }

        public virtual Character Character { get; set; }

        public virtual Guild Guild { get; set; }

        internal GuildMemberDTO DTO
        {
            get { return new GuildMemberDTO(this); }
        }
    }

    internal class GuildMemberDTO
    {
        public GuildMemberDTO(GuildMember item)
        {
            Name = item.Name;
            G_Name = item.G_Name;
        }

        public string Name { get; set; }

        public string G_Name { get; set; }
    }
}
