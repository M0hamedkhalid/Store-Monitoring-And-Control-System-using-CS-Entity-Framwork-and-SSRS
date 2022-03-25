namespace EFProject.DataBaseModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            CustomerBills = new HashSet<CustomerBill>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public double Mobile { get; set; }

        public double? Fax { get; set; }

        public double? Phone { get; set; }

        [StringLength(50)]
        public string Mail { get; set; }

        [StringLength(20)]
        public string Website { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), System.ComponentModel.Browsable(false)]
        public virtual ICollection<CustomerBill> CustomerBills { get; set; }
    }
}