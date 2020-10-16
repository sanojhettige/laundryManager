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

        public decimal TotalDiscount { get; set; }

        public decimal TotalCost { get; set; }

        public string OrderNotes { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public int Status { get; set; }

        public decimal? PaidAmount { get; set; }

        [StringLength(255)]
        public string PaidNote { get; set; }
    }
}
