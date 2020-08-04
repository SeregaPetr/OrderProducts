using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLCarouselRepository : ICarouselRepository
    {
        private OrderProductContext db;

        public MSSQLCarouselRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<Carousel> Carousels => db.Carousels;

        public void Add(Carousel carousel)
        {
            db.Carousels.Add(carousel);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Carousel carousel = db.Carousels.Find(id);
            // db.Entry(carousel).State = EntityState.Deleted;
            db.Carousels.Remove(carousel);
            db.SaveChanges();
        }

        public void Update(Carousel carousel)
        {
            db.Entry(carousel).State = EntityState.Modified;
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