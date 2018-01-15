namespace EatInTimeClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Plat")]
    public partial class Plat
    {
        [Key]
        public int Id_Plat { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom_Plat { get; set; }

        public decimal Prix_Plat { get; set; }

        public int Id_Type_Plat { get; set; }

        [StringLength(100)]
        public string Chemin_Image { get; set; }
    }
}
