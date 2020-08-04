using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models
{
    public class Carousel
    {
        public int Id { get; set; }
       // public string  Name { get; set; }
        public List<string> Photos { get; set; }
        //public virtual ICollection<Dish> Dishes { get; set; }

        //public Carousel()
        //{
        //    Dishes = new List<Dish>();
        //}
    }
}