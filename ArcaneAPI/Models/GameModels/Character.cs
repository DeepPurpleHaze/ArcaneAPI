namespace ArcaneAPI.Models.GameModels
{
    using ArcaneAPI.Models.Context;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Character")]
    public partial class Character
    {
        [Required]
        [StringLength(10)]
        public string AccountID { get; set; }

        [Key]
        [StringLength(10)]
        public string Name { get; set; }

        public int? cLevel { get; set; }

        public int? LevelUpPoint { get; set; }

        public byte? Class { get; set; }

        public int? Experience { get; set; }

        public int? Strength { get; set; }

        public int? Dexterity { get; set; }

        public int? Vitality { get; set; }

        public int? Energy { get; set; }

        public int? Leadership { get; set; }

        [MaxLength(3776)]
        public byte[] Inventory { get; set; }

        [MaxLength(180)]
        public byte[] MagicList { get; set; }

        public int? Money { get; set; }

        public float? Life { get; set; }

        public float? MaxLife { get; set; }

        public float? Mana { get; set; }

        public float? MaxMana { get; set; }

        public float? BP { get; set; }

        public float? MaxBP { get; set; }

        public float? Shield { get; set; }

        public float? MaxShield { get; set; }

        public short? MapNumber { get; set; }

        public short? MapPosX { get; set; }

        public short? MapPosY { get; set; }

        public byte? MapDir { get; set; }

        public int? PkCount { get; set; }

        public int? PkLevel { get; set; }

        public int? PkTime { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LDate { get; set; }

        public byte? CtlCode { get; set; }

        public byte? DbVersion { get; set; }

        [MaxLength(50)]
        public byte[] Quest { get; set; }

        public short? ChatLimitTime { get; set; }

        public int? FruitPoint { get; set; }

        [MaxLength(208)]
        public byte[] EffectList { get; set; }

        public int FruitAddPoint { get; set; }

        public int FruitSubPoint { get; set; }

        public int ResetCount { get; set; }

        public int MasterResetCount { get; set; }

        public int ExtInventory { get; set; }

        public int IsMarried { get; set; }

        [StringLength(10)]
        public string MarryName { get; set; }

        public int GensExitTime { get; set; }

        public int QuestIndex { get; set; }

        public int QuestState { get; set; }

        public int QuestVar { get; set; }

        public int QuestStat { get; set; }

        public int Rank { get; set; }

        public int PointsCount { get; set; }

        public int PointsTime { get; set; }

        public virtual GuildMember GuildMember { get; set; }

        public virtual MEMB_STAT MEMB_STAT { get; set; }

        internal CharacterDTO DTO
        {
            get { return new CharacterDTO(this); }
        }
    }

    internal class CharacterDTO
    {
        public CharacterDTO(Character item)
        {
            Name = item.Name;
            cLevel = item.cLevel;
            Class = item.Class;
            Strength = item.Strength;
            Dexterity = item.Dexterity;
            Vitality = item.Vitality;
            Energy = item.Energy;
            Leadership = item.Leadership;
            PkCount = item.PkCount;
            ResetCount = item.ResetCount;
            MasterResetCount = item.MasterResetCount;
            MarryName = item.MarryName;
            Guild = item.GuildMember?.G_Name;
            ConnectStat = (item.MEMB_STAT?.ConnectStat ?? 0) == 0 ? false : true;
            DisñonnectTime = item.MEMB_STAT?.DisConnectTM ?? DateTime.MinValue;
            ConnectTime = item.MEMB_STAT?.ConnectTM ?? DateTime.MinValue;
        }

        public string Name { get; set; }

        public int? cLevel { get; set; }

        public byte? Class { get; set; }

        public int? Strength { get; set; }

        public int? Dexterity { get; set; }

        public int? Vitality { get; set; }

        public int? Energy { get; set; }

        public int? Leadership { get; set; }

        public int? PkCount { get; set; }

        public DateTime? MDate { get; set; }

        public int ResetCount { get; set; }

        public int MasterResetCount { get; set; }

        public string MarryName { get; set; }

        public string Guild { get; set; }

        public bool ConnectStat { get; set; }

        public DateTime DisñonnectTime { get; set; }

        public DateTime ConnectTime { get; set; }
    }

    internal class CharacterRepository: ModelRepository<Character>
    {
        public CharacterRepository() : base(IPS)
        {

        }

        public static string IPS => @"GuildMember, MEMB_STAT";
    }
}
