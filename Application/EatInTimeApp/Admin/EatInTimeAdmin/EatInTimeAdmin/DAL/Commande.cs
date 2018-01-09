namespace EatInTimeAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commande")]
    public partial class Commande
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commande()
        {
            Plat = new HashSet<Plat>();
        }

        [Key]
        public int Id_Commande { get; set; }

        public DateTime Date_Commande { get; set; }

        public int Id_Avancement { get; set; }

        public int Id_Table { get; set; }

        public virtual Avancement Avancement { get; set; }

        public virtual Tables Tables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plat> Plat { get; set; }
    }
}
