namespace EatInTimeClient.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EatInTimeContext : DbContext
    {
        public EatInTimeContext()
            : base("name=EatInTimeContext")
        {
        }

        public virtual DbSet<Avancement> Avancement { get; set; }
        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<Emplacement> Emplacement { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Plat> Plat { get; set; }
        public virtual DbSet<Sysdiagrams> Sysdiagrams { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
        public virtual DbSet<Type_Plat> Type_Plat { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avancement>()
                .Property(e => e.Nom_Avancement)
                .IsUnicode(false);

            modelBuilder.Entity<Avancement>()
                .HasMany(e => e.Commande)
                .WithRequired(e => e.Avancement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Commande>()
                .HasMany(e => e.Plat)
                .WithMany(e => e.Commande)
                .Map(m => m.ToTable("contient").MapLeftKey("Id_Commande").MapRightKey("Id_Plat"));

            modelBuilder.Entity<Emplacement>()
                .Property(e => e.Nom_Emplacement)
                .IsUnicode(false);

            modelBuilder.Entity<Emplacement>()
                .HasMany(e => e.Tables)
                .WithRequired(e => e.Emplacement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ingredient>()
                .Property(e => e.Nom_Ingredient)
                .IsUnicode(false);

            modelBuilder.Entity<Ingredient>()
                .HasMany(e => e.Plat)
                .WithMany(e => e.Ingredients)
                .Map(m => m.ToTable("est_compose").MapLeftKey("Id_Ingredient").MapRightKey("Id_Plat"));

            modelBuilder.Entity<Plat>()
                .Property(e => e.Nom_Plat)
                .IsUnicode(false);

            modelBuilder.Entity<Plat>()
                .Property(e => e.Prix_Plat)
                .HasPrecision(25, 0);

            modelBuilder.Entity<Tables>()
                .HasMany(e => e.Commande)
                .WithRequired(e => e.Tables)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type_Plat>()
                .Property(e => e.Nom_Type_Plat)
                .IsUnicode(false);

            modelBuilder.Entity<Type_Plat>()
                .HasMany(e => e.Plat)
                .WithRequired(e => e.Type_Plat)
                .WillCascadeOnDelete(false);
        }
    }
}
