using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLOrderRepository : IOrderRepository
    {
        private OrderProductContext db;

        public MSSQLOrderRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<Order> Orders => db.Orders;

        public void Add(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            db.Entry(order).State = EntityState.Detached;
        }

        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
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