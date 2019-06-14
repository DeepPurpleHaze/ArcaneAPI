namespace ArcaneAPI.Models.GameModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MEMB_INFO
    {
        public MEMB_INFO() { }

        public MEMB_INFO(string login, string pass, string mail)
        {
            memb_guid = 0;
            memb___id = login;
            memb__pwd = pass;
            mail_addr = mail;
            fpas_answ = "";
            fpas_ques = "";
            bloc_code = "0";
            ctl1_code = "0";
            AccountLevel = 0;
            AccountExpireDate = DateTime.Now;
            memb_name = "site";
            sno__numb = "1";
            mail_chek = "0";
        }

        [Key]
        public int memb_guid { get; set; }

        [Required]
        [StringLength(10)]
        public string memb___id { get; set; }

        [Required]
        [StringLength(10)]
        public string memb__pwd { get; set; }

        [Required]
        [StringLength(10)]
        public string memb_name { get; set; }

        [Required]
        [StringLength(18)]
        public string sno__numb { get; set; }

        [StringLength(6)]
        public string post_code { get; set; }

        [StringLength(50)]
        public string addr_info { get; set; }

        [StringLength(50)]
        public string addr_deta { get; set; }

        [StringLength(20)]
        public string tel__numb { get; set; }

        [StringLength(15)]
        public string phon_numb { get; set; }

        [StringLength(50)]
        public string mail_addr { get; set; }

        [StringLength(50)]
        public string fpas_ques { get; set; }

        [StringLength(50)]
        public string fpas_answ { get; set; }

        [StringLength(2)]
        public string job__code { get; set; }

        public DateTime? appl_days { get; set; }

        public DateTime? modi_days { get; set; }

        public DateTime? out__days { get; set; }

        public DateTime? true_days { get; set; }

        [StringLength(1)]
        public string mail_chek { get; set; }

        [Required]
        [StringLength(1)]
        public string bloc_code { get; set; }

        [Required]
        [StringLength(1)]
        public string ctl1_code { get; set; }

        public int AccountLevel { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime AccountExpireDate { get; set; }
    }
}


