using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OrderProducts.DAL;
using OrderProducts.DAL.Entities;
using OrderProducts.DAL.Identity;
using OrderProducts.DAL.IRepository;
using OrderProducts.Helpers;
using OrderProducts.Models;

namespace OrderProducts.Controllers.AdminPanel
{
    [Authorize(Roles = "admin")]
    public class AdminDisheController : Controller
    {
        private readonly IDishRepository dishRepository;
        private readonly IMenuCategoryRepository menuCategoryRepository;

        public AdminDisheController(IDishRepository dish, IMenuCategoryRepository menuCategory)
        {
            dishRepository = dish;
            menuCategoryRepository = menuCategory;
            ViewBag.SelectedMenu = "admin";
        }

        public ActionResult Sort(string sort, bool direction = true)
        {
            ViewBag.Direction = !direction;
            List<Dish> dishes;
            switch (sort)
            {
                case "category":
                    if (direction)
                        dishes = dishRepository.Dishes.OrderBy(d => d.MenuCategory.NameCategory).ToList();
                    else
                        dishes = dishRepository.Dishes.OrderByDescending(d => d.MenuCategory.NameCategory).ToList();
                    break;
                case "nameDish":
                    if (direction)
                        dishes = dishRepository.Dishes.OrderBy(d => d.NameDish).ToList();
                    else
                        dishes = dishRepository.Dishes.OrderByDescending(d => d.NameDish).ToList();
                    break;
                case "composition":
                    if (direction)
                        dishes = dishRepository.Dishes.OrderBy(d => d.CompositionDish).ToList();
                    else
                        dishes = dishRepository.Dishes.OrderByDescending(d => d.CompositionDish).ToList();
                    break;
                case "weight":
                    if (direction)
                        dishes = dishRepository.Dishes.OrderBy(d => d.Weight).ToList();
                    else
                        dishes = dishRepository.Dishes.OrderByDescending(d => d.Weight).ToList();
                    break;
                case "photo":
                    if (direction)
                        dishes = dishRepository.Dishes.OrderBy(d => d.Photo).ToList();
                    else
                        dishes = dishRepository.Dishes.OrderByDescending(d => d.Photo).ToList();
                    break;
                case "price":
                    if (direction)
                        dishes = dishRepository.Dishes.OrderBy(d => d.Price).ToList();
                    else
                        dishes = dishRepository.Dishes.OrderByDescending(d => d.Price).ToList();
                    break;
                default:
                    dishes = dishRepository.Dishes.ToList();
                    break;
            }
            return View("Index", dishes);
        }

        // GET: AdminDishe
        public ActionResult Index()
        {
            var dishes = dishRepository.Dishes.ToList();
            return View(dishes);
        }

        // GET: AdminDishe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = dishRepository.Dishes.First(d => d.Id == id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: AdminDishe/Create
        public ActionResult Create()
        {
            var menuCategories = menuCategoryRepository.MenuCategories.ToList();
            ViewBag.MenuCategoryId = new SelectList(menuCategories, "Id", "NameCategory");
            return View();
        }

        // POST: AdminDishe/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameDish,CompositionDish,Weight,Photo,Price,MenuCategoryId")] Dish dish, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                string directory = Server.MapPath(Url.Content("~/Content/Images"));
                dish.Photo = ImageFiles.AddFile(directory, uploadedFile);

                dishRepository.Add(dish);
                return RedirectToAction("Index");
            }

            var menuCategories = menuCategoryRepository.MenuCategories.ToList();
            ViewBag.MenuCategoryId = new SelectList(menuCategories, "Id", "NameCategory", dish.MenuCategoryId);
            return View(dish);
        }

        // GET: AdminDishe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = dishRepository.Dishes.First(d => d.Id == id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            var menuCategories = menuCategoryRepository.MenuCategories.ToList();
            ViewBag.MenuCategoryId = new SelectList(menuCategories, "Id", "NameCategory", dish.MenuCategoryId);
            return View(dish);
        }

        // POST: AdminDishe/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameDish,CompositionDish,Weight,Price,MenuCategoryId")] Dish dish, string photoName, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                dish.Photo = photoName;

                string directory = Server.MapPath(Url.Content("~/Content/Images"));
                ImageFiles.DeleteFile(directory, photoName);
                dish.Photo = ImageFiles.AddFile(directory, uploadedFile);

                dishRepository.Update(dish);
                return RedirectToAction("Index");
            }
            var menuCategories = menuCategoryRepository.MenuCategories.ToList();
            ViewBag.MenuCategoryId = new SelectList(menuCategories, "Id", "NameCategory", dish.MenuCategoryId);
            return View(dish);
        }

        // GET: AdminDishe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = dishRepository.Dishes.First(d => d.Id == id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: AdminDishe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string photo)
        {
            string directoryToDelete = Server.MapPath(Url.Content("~/Content/Images"));
            ImageFiles.DeleteFile(directoryToDelete, photo);

            dishRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dishRepository.Dispose();
                menuCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
