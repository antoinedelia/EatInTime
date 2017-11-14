namespace EatInTimeClient
{
    using EatInTimeClient.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Type_Plat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Type_Plat()
        {
            Plat = new HashSet<Plat>();
        }

        [Key]
        public int Id_Type_Plat { get; set; }

        [Required]
        [StringLength(25)]
        public string Nom_Type_Plat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plat> Plat { get; set; }
    }
}
