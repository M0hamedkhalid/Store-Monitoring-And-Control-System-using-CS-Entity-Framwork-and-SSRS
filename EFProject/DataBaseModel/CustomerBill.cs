namespace EFProject.DataBaseModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CustomerBill")]
    public partial class CustomerBill
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime BillDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WarehouseID { get; set; }

        public int Count { get; set; }

        [System.ComponentModel.Browsable(false)]
        public virtual Customer Customer { get; set; }

        [System.ComponentModel.Browsable(false)]
        public virtual Product Product { get; set; }

        [System.ComponentModel.Browsable(false)]
        public virtual Warehouse Warehouse { get; set; }
    }
}