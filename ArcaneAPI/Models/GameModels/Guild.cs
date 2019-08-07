namespace ArcaneAPI.Models.GameModels
{
    using ArcaneAPI.Models.Context;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Linq.Expressions;

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

        [NotMapped]
        public ICollection<Guild> Alliance { get; set; }

        internal GuildDTO DTO { get { return new GuildDTO(this); } }

        internal Ally Ally { get { return new Ally(this); } }
    }

    internal class GuildDTO
    {
        public GuildDTO() { }

        public GuildDTO(Guild item)
        {
            G_Name = item.G_Name;
            G_Mark = item.G_Mark;
            G_Score = item.G_Score;
            G_Master = item.G_Master;
            Number = item.Number;
            G_Rival = item.G_Rival;
            G_Union = item.G_Union;
            MemberCount = item.MemberCount;
            GuildMembers = item.GuildMembers?.Select(d => d.DTO);
            Alliance = item.Alliance?.Select(d => d.Ally);
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
        
        public IEnumerable<GuildMemberDTO> GuildMembers { get; set; }

        public IEnumerable<Ally> Alliance { get; set; }
    }

    internal class Ally
    {
        public Ally() { }

        public Ally(Guild item)
        {
            Head = item.G_Union == item.Number;
            G_Name = item.G_Name;
        }

        public bool Head { get; set; }

        public string G_Name { get; set; }
    }

    internal class GuildRepository : ModelRepository<Guild>
    {
        public GuildRepository() : base(IPS) { }

        public static string IPS => @"GuildMembers";

        internal override IEnumerable<Guild> GetWithIncludes(Expression<Func<Guild, bool>> filter = null, Func<IQueryable<Guild>, IOrderedQueryable<Guild>> orderBy = null, int? take = null)
        {
            var guilds = base.GetWithIncludes(filter, orderBy, take);

            foreach (var item in guilds)
            {
                if (item.G_Union != 0)
                {
                    item.Alliance = GetAlliance(item.G_Name, item.G_Union).ToList();
                }
            }

            return guilds;
        }

        internal IEnumerable<Guild> GetAlliance(string guild, int g_union)
        {
            return Get(d => d.G_Union == g_union && d.G_Name != guild, e => e.OrderBy(x => x.G_Name));
        }
    }
}
