namespace EatInTimeAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Plat")]
    public partial class Plat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plat()
        {
            Commande = new HashSet<Commande>();
            Ingredient = new HashSet<Ingredient>();
        }

        [Key]
        public int Id_Plat { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom_Plat { get; set; }

        public decimal Prix_Plat { get; set; }

        public int Id_Type_Plat { get; set; }

        public virtual Type_Plat Type_Plat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commande { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingredient> Ingredient { get; set; }
    }
}
