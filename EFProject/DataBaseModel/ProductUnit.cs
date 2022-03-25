namespace EFProject.DataBaseModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductUnit")]
    public partial class ProductUnit
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Unit { get; set; }

        public virtual Product Product { get; set; }
    }
}