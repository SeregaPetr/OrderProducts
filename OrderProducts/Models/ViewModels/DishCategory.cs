using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProducts.Models.ViewModels
{
    public class DishCategory
    {
        public List<Dish> Dishes { get; set; }
        public List<MenuCategory> MenuCategories { get; set; }
    }
}