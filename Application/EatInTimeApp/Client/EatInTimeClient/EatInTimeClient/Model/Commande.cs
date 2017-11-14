namespace EatInTimeClient
{
    using EatInTimeClient.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commande")]
    public partial class Commande
    {
        [Key]
        public int Id_Commande { get; set; }

        public DateTime Date_Commande { get; set; }

        public int Id_Avancement { get; set; }

        public int Id_Table { get; set; }

        public virtual Avancement Avancement { get; set; }

        public virtual Tables Tables { get; set; }
    }
}
