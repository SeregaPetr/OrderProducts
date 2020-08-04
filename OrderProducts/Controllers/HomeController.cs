using OrderProducts.DAL;
using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderProducts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarouselRepository carouselRepository;
        private readonly IMenuCategoryRepository menuCategoryRepository;

        public HomeController(ICarouselRepository carousel, IMenuCategoryRepository menuCategory)
        {
            carouselRepository = carousel;
            menuCategoryRepository = menuCategory;
        }

        public ActionResult Index()
        {
            return View();
        }

        //[ChildActionOnly]
        //public ActionResult Account()
        //{
        //    return PartialView("_Account");
        //}

        [ChildActionOnly]
        public ActionResult Carousel()
        {
            Carousel carousel = new Carousel
            {
                Photos = new List<string>
                {
                     "akcii.jpg","dostavka.jpg","pizza-2.jpg"
                }
            };
            return PartialView("_Carousel", carousel);
        }

        [ChildActionOnly]
        public ActionResult MenuCategory()
        {
            List<MenuCategory> menuCategories = menuCategoryRepository.MenuCategories.ToList();
            return PartialView("_MenuCategory", menuCategories);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                carouselRepository.Dispose();
                menuCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
