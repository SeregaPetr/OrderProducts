using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrderProducts.DAL;
using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using OrderProducts.Models.ViewModels;

namespace OrderProducts.Controllers
{
    public class DishController : Controller
    {
        private readonly IDishRepository dishRepository;

        public DishController(IDishRepository dish)
        {
            dishRepository = dish;
        }

        public ActionResult Dish(int id, string sort = "popular")
        {
            ViewBag.Id = id;
            ViewBag.Sort = sort;

            List<Dish> dishes = SortDishes(id, sort);

            if (dishes == null)
            {
                return HttpNotFound();
            }

            DishCategory dishCategory = new DishCategory
            {
                Dishes = dishes,
            };
            return View(dishCategory);
        }

        [HttpPost]
        public ActionResult DishCard(int id, string sort = "popular")
        {
            ViewBag.Id = id;

            List<Dish> dishes = SortDishes(id, sort);

            if (dishes == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DishCard", dishes);
        }

        private List<Dish> SortDishes(int Id, string sort)
        {
            List<Dish> dishes = null;

            switch (sort)
            {
                case "popular":
                    dishes = dishRepository.Dishes.Where(d => d.MenuCategoryId == Id).OrderByDescending(d => d.OrderLines.Count()).ToList();
                    break;
                case "price_asc":
                    dishes = dishRepository.Dishes.Where(d => d.MenuCategoryId == Id).OrderBy(p => p.Price).ToList();
                    break;
                case "price_desc":
                    dishes = dishRepository.Dishes.Where(d => d.MenuCategoryId == Id).OrderByDescending(p => p.Price).ToList();
                    break;
            }
            return dishes;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dishRepository.Dispose();
                // menuCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
