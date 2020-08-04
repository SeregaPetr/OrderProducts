using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLDishRepository : IDishRepository
    {
        private OrderProductContext db;

        public MSSQLDishRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<Dish> Dishes => db.Dishes;

        public void Add(Dish dish)
        {
            db.Dishes.Add(dish);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
            db.SaveChanges();
        }

        public void Update(Dish dish)
        {
            db.Entry(dish).State = EntityState.Modified;
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