using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models.ViewModels
{
    public class UserDelivery
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Е-мейл")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "№ дома")]
        public string HouseNumber { get; set; }

        [Required]
        [Display(Name = "№ кв.")]
        public int ApartmentNumber { get; set; }

        [Required]
        [Display(Name = "Тип оплаты")]
        public string NamePaymentType { get; set; }

        [Display(Name = "Время доставки заказа")]
        public DateTime? OrderDeliveryTime { get; set; }

        [Display(Name = "Комментарий к заказу")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}