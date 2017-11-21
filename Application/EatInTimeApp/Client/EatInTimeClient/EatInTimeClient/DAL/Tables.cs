namespace EatInTimeClient.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tables
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tables()
        {
            Commande = new HashSet<Commande>();
        }

        [Key]
        public int Id_Table { get; set; }

        public int Numero_Table { get; set; }

        public int? Capacite_Table { get; set; }

        public int Id_Emplacement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commande { get; set; }

        public virtual Emplacement Emplacement { get; set; }
    }
}
