using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLMenuCategoryRepository : IMenuCategoryRepository
    {
        private OrderProductContext db;

        public MSSQLMenuCategoryRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<MenuCategory> MenuCategories => db.MenuCategories;

        public void Add(MenuCategory menuCategory)
        {
            db.MenuCategories.Add(menuCategory);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            MenuCategory menuCategory = db.MenuCategories.Find(id);
            db.MenuCategories.Remove(menuCategory);
            db.SaveChanges();
        }

        public void Update(MenuCategory menuCategory)
        {
            db.Entry(menuCategory).State = EntityState.Modified;
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}