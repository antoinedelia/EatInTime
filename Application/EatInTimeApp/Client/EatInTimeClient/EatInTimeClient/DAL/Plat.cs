namespace EatInTimeClient.DAL
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Plat")]
    public partial class Plat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plat()
        {
            Commande = new ObservableCollection<Commande>();
            Ingredients = new ObservableCollection<Ingredient>();
        }

        [Key]
        public int Id_Plat { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom_Plat { get; set; }

        public decimal Prix_Plat { get; set; }

        public int Id_Type_Plat { get; set; }

        public virtual Type_Plat Type_Plat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Commande> Commande { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Ingredient> Ingredients { get; set; }
        [NotMapped]
        public string String_Ingredients { get; set; }
        [NotMapped]
        public bool Is_In_Menu { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
