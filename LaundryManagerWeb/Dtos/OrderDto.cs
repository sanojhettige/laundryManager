using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LaundryManagerWeb.Dtos
{
    public class OrderDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }

        public string OrderReference { get; set; }

        public decimal TotalDiscount { get; set; }
        public decimal TotalCost { get; set; }

        public string OrderNotes { get; set; }

        public int Status { get; set; }

        public decimal? PaidAmount { get; set; }

        public string PaidNote { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public string PickUpPerson { get; set; }

        public string PickUpPersonPhone { get; set; }

        public DateTime PickUpDateTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderItemDto Items { get; set; }
    }
}