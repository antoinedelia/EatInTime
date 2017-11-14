using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace EatInTimeClient.Model
{
    [Table("Plat")]
    public partial class Plat : INotifyPropertyChanged
    {
        [Key]
        public int Id_Plat { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom_Plat { get; set; }

        public decimal Prix_Plat { get; set; }

        public int Id_Type_Plat { get; set; }

        public virtual Type_Plat Type_Plat { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
