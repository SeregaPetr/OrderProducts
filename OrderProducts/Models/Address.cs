using OrderProducts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "№ дома")]
        public string HouseNumber { get; set; }

        [Required]
        [Display(Name = "№ кв.")]
        public int ApartmentNumber { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Address()
        {
            Users = new List<User>();
            Orders = new List<Order>();
        }
    }
}
