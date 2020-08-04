using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название блюда")]
        public string NameDish { get; set; }

        [Display(Name = "Соостав")]
        public string CompositionDish { get; set; }

        [Required]
        [Display(Name = "Вес")]
        public double Weight { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }
        //public byte[] Photo { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public double Price { get; set; }

        public int MenuCategoryId { get; set; }
        public virtual MenuCategory MenuCategory { get; set; }

       // public virtual ICollection<Carousel> Carousels { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }


        public Dish()
        {
        //    Carousels = new List<Carousel>();
            OrderLines = new List<OrderLine>();
        }

    }
}