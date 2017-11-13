namespace EatInTimeClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ingredient")]
    public partial class Ingredient
    {
        [Key]
        public int Id_Ingredient { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom_Ingredient { get; set; }

        public bool Allergene_Ingredient { get; set; }

        public int Stock_Ingredient { get; set; }
    }
}
