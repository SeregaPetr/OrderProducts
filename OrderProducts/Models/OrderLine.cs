using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderProducts.Models
{
    public class OrderLine
    {
        public int Id { get; set; }

        [Display(Name = "Кол-во")]
        public int Quantity { get; set; }

        [Display(Name = "Сумма")]
        public double OrderLinePrice
        {
            get { return Dish.Price * Quantity; }
        }

        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
