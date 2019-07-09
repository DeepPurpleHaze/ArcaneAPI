namespace ArcaneAPI.Models.GameModels
{
    using ArcaneAPI.Models.Context;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Guild")]
    public partial class Guild
    {
        [Key]
        [StringLength(8)]
        public string G_Name { get; set; }

        [MaxLength(32)]
        public byte[] G_Mark { get; set; }

        public int? G_Score { get; set; }

        [StringLength(10)]
        public string G_Master { get; set; }

        public int? G_Count { get; set; }

        [StringLength(60)]
        public string G_Notice { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public int G_Type { get; set; }

        public int G_Rival { get; set; }

        public int G_Union { get; set; }

        public int? MemberCount { get; set; }

        [MaxLength(3840)]
        public byte[] Warehouse { get; set; }

        public long Money { get; set; }

        public virtual ICollection<GuildMember> GuildMembers { get; set; }

        internal GuildDTO DTO { get { return new GuildDTO(this); } }
    }

    internal class GuildDTO
    {
        public GuildDTO() { }

        public GuildDTO(Guild item)
        {

        }

        public string G_Name { get; set; }

        [MaxLength(32)]
        public byte[] G_Mark { get; set; }

        public int? G_Score { get; set; }

        [StringLength(10)]
        public string G_Master { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public int G_Rival { get; set; }

        public int G_Union { get; set; }

        public int? MemberCount { get; set; }
        
        public ICollection<GuildMember> GuildMembers { get; set; }

        public ICollection<Guild> Alliance { get; set; }
    }

    internal class GuildRepository : ModelRepository<Guild>
    {
        public GuildRepository() : base(IPS) { }

        public static string IPS => @"GuildMember";


    }
}
