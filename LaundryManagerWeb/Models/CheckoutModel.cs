using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryManagerWeb.Models
{
    public class CheckoutModel
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string CustomerPhone { get; set; }


        [Required]
        [Display(Name = "Pickup Person Name")]
        public string PickupPerson { get; set; }

        [Required]
        [Display(Name = "Pickup Person Contact Number")]
        public string PickupPersonPhone { get; set; }

        [Required]
        [Display(Name = "Pickup Date/Time")]
        public string PickupDateTime { get; set; }

        public List<CartItemModel> CartItemModel { get; set; }

    }
}