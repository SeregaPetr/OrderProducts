using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrderProducts.DAL;
using OrderProducts.DAL.IRepository;
using OrderProducts.Helpers;
using OrderProducts.Models;

namespace OrderProducts.Controllers.AdminPanel
{
    [Authorize(Roles = "admin")]
    public class AdminMenuCategorieController : Controller
    {
        private readonly IMenuCategoryRepository menuCategoryRepository;

        public AdminMenuCategorieController(IMenuCategoryRepository menuCategory)
        {
            menuCategoryRepository = menuCategory;
            ViewBag.SelectedMenu = "admin";
        }

        public ActionResult Sort(string sort, bool direction = true)
        {
            ViewBag.Direction = !direction;
            List<MenuCategory> menuCategories;
            switch (sort)
            {
                case "nameCategory":
                    if (direction)
                        menuCategories = menuCategoryRepository.MenuCategories.OrderBy(m => m.NameCategory).ToList();
                    else
                        menuCategories = menuCategoryRepository.MenuCategories.OrderByDescending(m => m.NameCategory).ToList();
                    break;
                case "comment":
                    if (direction)
                        menuCategories = menuCategoryRepository.MenuCategories.OrderBy(m => m.Comment).ToList();
                    else
                        menuCategories = menuCategoryRepository.MenuCategories.OrderByDescending(m => m.Comment).ToList();
                    break;
                case "photo":
                    if (direction)
                        menuCategories = menuCategoryRepository.MenuCategories.OrderBy(m => m.Photo).ToList();
                    else
                        menuCategories = menuCategoryRepository.MenuCategories.OrderByDescending(m => m.Photo).ToList();
                    break;
                default:
                    menuCategories = menuCategoryRepository.MenuCategories.ToList();
                    break;
            }
            return View("Index", menuCategories);
        }

        // GET: AdminMenuCategorie
        public ActionResult Index()
        {
            List<MenuCategory> menuCategories = menuCategoryRepository.MenuCategories.ToList();
            return View(menuCategories);
        }

        // GET: AdminMenuCategorie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = menuCategoryRepository.MenuCategories.First(m => m.Id == id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // GET: AdminMenuCategorie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminMenuCategorie/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameCategory,Comment")] MenuCategory menuCategory, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                string directory = Server.MapPath(Url.Content("~/Content/Images"));
                menuCategory.Photo = ImageFiles.AddFile(directory, uploadedFile);

                menuCategoryRepository.Add(menuCategory);
                return RedirectToAction("Index");
            }
            return View(menuCategory);
        }

        // GET: AdminMenuCategorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = menuCategoryRepository.MenuCategories.First(m => m.Id == id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // POST: AdminMenuCategorie/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameCategory,Photo,Comment")] MenuCategory menuCategory, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                string directory = Server.MapPath(Url.Content("~/Content/Images"));
                ImageFiles.DeleteFile(directory, menuCategory.Photo);
                menuCategory.Photo = ImageFiles.AddFile(directory, uploadedFile);

                menuCategoryRepository.Update(menuCategory);
                return RedirectToAction("Index");
            }
            return View(menuCategory);
        }

        // GET: AdminMenuCategorie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = menuCategoryRepository.MenuCategories.First(m => m.Id == id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // POST: AdminMenuCategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string photo)
        {
            string directoryToDelete = Server.MapPath(Url.Content("~/Content/Images"));
            ImageFiles.DeleteFile(directoryToDelete, photo);

            menuCategoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                menuCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
