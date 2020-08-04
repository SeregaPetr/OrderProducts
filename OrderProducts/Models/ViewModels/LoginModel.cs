using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserLogin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}