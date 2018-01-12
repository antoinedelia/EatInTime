namespace EatInTimeClient.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tables
    {
        private Tables _tables;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tables()
        {
            Commande = new HashSet<Commande>();
        }

        public Tables(Tables tables)
        {
            this._tables = tables;
        }

        [Key]
        public int Id_Table { get; set; }

        public int Numero_Table { get; set; }

        public int? Capacite_Table { get; set; }

        public int Id_Emplacement { get; set; }

        public bool Est_Occupee { get; set; }

        public bool? Alerte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commande { get; set; }

        public virtual Emplacement Emplacement { get; set; }
    }
}
