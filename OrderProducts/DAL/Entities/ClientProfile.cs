using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required]
        public string LastName { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

     //   public virtual ICollection<Address> Addresses { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }


        public ClientProfile()
        {
           // Addresses = new List<Address>();
           // Orders = new List<Order>();
        }
    }
}