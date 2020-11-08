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

        [Required(ErrorMessage = "please select apartment")]
        public string UserId { get; set; }

        public decimal TotalDiscount { get; set; }

        [Required(ErrorMessage = "Please enter total cost")]
        public decimal TotalCost { get; set; }

        public string OrderNotes { get; set; }

        public int Status { get; set; }

        public decimal? PaidAmount { get; set; }

        [StringLength(255)]
        public string PaidNote { get; set; }


        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Phone number is required. ")]
        [StringLength(11)]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Enter pickup person name")]
        [StringLength(100)]
        public string PickUpPerson { get; set; }

        [StringLength(11)]
        public string PickUpPersonPhone { get; set; }

        public DateTime PickUpDateTime { get; set; }

        public OrderItemDto Items { get; set; }
    }
}