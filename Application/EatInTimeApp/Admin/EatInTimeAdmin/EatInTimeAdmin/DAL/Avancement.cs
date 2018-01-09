namespace EatInTimeAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Avancement")]
    public partial class Avancement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Avancement()
        {
            Commande = new HashSet<Commande>();
        }

        [Key]
        public int Id_Avancement { get; set; }

        [Required]
        [StringLength(25)]
        public string Nom_Avancement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commande { get; set; }
    }
}
