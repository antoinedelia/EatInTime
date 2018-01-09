namespace EatInTimeAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ingredient")]
    public partial class Ingredient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingredient()
        {
            Plat = new HashSet<Plat>();
        }

        [Key]
        public int Id_Ingredient { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom_Ingredient { get; set; }

        public bool Allergene_Ingredient { get; set; }

        public int Stock_Ingredient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plat> Plat { get; set; }
    }
}
