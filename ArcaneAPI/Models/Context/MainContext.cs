using ArcaneAPI.Models.CustomModels;
using ArcaneAPI.Models.GameModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ArcaneAPI.Models.Context
{
    public class MainContext: DbContext
    {
        public MainContext() : base("name=DefaultConnection") { }

        public IEnumerable<T> ExecuteStoredProcedure<T>(string query, params object[] parameters)
        {
            Database.CommandTimeout = 180;
            return Database.SqlQuery<T>(query, parameters).ToList();
        }

        //Game tables

        public virtual DbSet<Guild> Guild { get; set; }

        public virtual DbSet<GuildMember> GuildMember { get; set; }

        public virtual DbSet<MEMB_STAT> MEMB_STAT { get; set; }

        public virtual DbSet<Character> Character { get; set; }

        public virtual DbSet<MEMB_INFO> MEMB_INFO { get; set; }

        public virtual DbSet<WZ_CW_INFO> WZ_CW_INFO { get; set; }

        public virtual DbSet<warehouse> warehouse { get; set; }

        public virtual DbSet<AccountCharacter> AccountCharacter { get; set; }

        //Custom tables

        public virtual DbSet<News> News { get; set; }

        public virtual DbSet<FAQ> FAQ { get; set; }

        //Model creating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guild>()
                .Property(e => e.G_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Guild>()
                .Property(e => e.G_Master)
                .IsUnicode(false);

            modelBuilder.Entity<Guild>()
                .Property(e => e.G_Notice)
                .IsUnicode(false);

            modelBuilder.Entity<GuildMember>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<GuildMember>()
                .Property(e => e.G_Name)
                .IsUnicode(false);

            modelBuilder.Entity<MEMB_STAT>()
                .Property(e => e.memb___id)
                .IsUnicode(false);

            modelBuilder.Entity<MEMB_STAT>()
                .Property(e => e.ServerName)
                .IsUnicode(false);

            modelBuilder.Entity<MEMB_STAT>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<Character>()
                .HasOptional(e => e.GuildMember)
                .WithRequired(e => e.Character);

            modelBuilder.Entity<Character>()
               .HasRequired(e => e.MEMB_STAT)
               .WithMany(e => e.Characters)
               .HasForeignKey(e => e.AccountID);

            modelBuilder.Entity<Guild>()
                .HasMany(e => e.GuildMembers)
                .WithRequired(e => e.Guild)
                .HasForeignKey(e => e.G_Name);
        }

        public bool IsDetached(object entityToDelete)
        {
            return Entry(entityToDelete).State == EntityState.Detached;
        }

        public void SetPropertyModifiedTrue(object obj, string item)
        {
            Entry(obj).Property(item).IsModified = true;
        }

        public void MarkAsModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}