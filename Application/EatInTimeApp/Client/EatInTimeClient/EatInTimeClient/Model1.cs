namespace EatInTimeClient
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=EatInTimeContext")
        {
        }

        public virtual DbSet<Plat> Plat { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plat>()
                .Property(e => e.Nom_Plat)
                .IsUnicode(false);

            modelBuilder.Entity<Plat>()
                .Property(e => e.Prix_Plat)
                .HasPrecision(25, 0);

            modelBuilder.Entity<Plat>()
                .Property(e => e.Chemin_Image)
                .IsUnicode(false);
        }
    }
}
