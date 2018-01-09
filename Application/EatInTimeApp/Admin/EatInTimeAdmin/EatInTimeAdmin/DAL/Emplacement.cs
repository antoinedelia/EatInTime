namespace EatInTimeAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Emplacement")]
    public partial class Emplacement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Emplacement()
        {
            Tables = new HashSet<Tables>();
        }

        [Key]
        public int Id_Emplacement { get; set; }

        [Required]
        [StringLength(25)]
        public string Nom_Emplacement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tables> Tables { get; set; }
    }
}
