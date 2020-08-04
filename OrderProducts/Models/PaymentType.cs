using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models
{
    public class PaymentType
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Тип оплаты")]
        public string NamePaymentType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public PaymentType()
        {
            Orders = new List<Order>();
        }
    }
}