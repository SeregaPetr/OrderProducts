using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models
{
    public class MenuCategory
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Категория блюд")]
        public string NameCategory { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Comment { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }
        //  public byte[] Photo { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public MenuCategory()
        {
            Dishes = new List<Dish>();
        }
    }
}