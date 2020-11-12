namespace LaundryManagerWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
