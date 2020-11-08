namespace LaundryManagerWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int Id { get; set; }

        public string OrderReference { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Display(Name = "Total Discount")]
        public decimal TotalDiscount { get; set; }

        [Required]
        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Notes")]
        public string OrderNotes { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public int Status { get; set; }

        [Display(Name = "Paid Amount")]
        public decimal? PaidAmount { get; set; }

        [Display(Name = "Payment Notes")]
        [StringLength(255)]
        public string PaidNote { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Phone Number")]
        [Required]
        [StringLength(11)]
        public string CustomerPhone { get; set; }

        [Display(Name = "Pickup Person Name")]
        [Required]
        [StringLength(100)]
        public string PickUpPerson { get; set; }

        [Display(Name = "Pickup Person Phone Number")]
        [StringLength(11)]
        public string PickUpPersonPhone { get; set; }

        [Display(Name = "Pickup Date")]
        public DateTime PickUpDateTime { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
