using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLOrderLineRepository : IOrderLineRepository
    {
        private OrderProductContext db;

        public MSSQLOrderLineRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<OrderLine> OrderLines => db.OrderLines;

        public void Add(OrderLine orderLine)
        {
            db.OrderLines.Add(orderLine);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderLine orderLine)
        {
            throw new NotImplementedException();
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