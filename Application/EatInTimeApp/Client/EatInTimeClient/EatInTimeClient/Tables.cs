namespace EatInTimeClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tables
    {
        [Key]
        public int Id_Table { get; set; }

        public int Numero_Table { get; set; }

        public int? Capacite_Table { get; set; }

        public int Id_Emplacement { get; set; }

        public bool? Est_Occupee { get; set; }

        public bool? Alerte { get; set; }
    }
}
