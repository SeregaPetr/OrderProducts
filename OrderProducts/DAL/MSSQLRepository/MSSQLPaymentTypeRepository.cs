using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLPaymentTypeRepository : IPaymentTypeRepository
    {
        private OrderProductContext db;

        public MSSQLPaymentTypeRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<PaymentType> PaymentTypes => db.PaymentTypes;

        public void Add(PaymentType paymentType)
        {
            db.PaymentTypes.Add(paymentType);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            PaymentType paymentType = db.PaymentTypes.Find(id);
            db.PaymentTypes.Remove(paymentType);
            db.SaveChanges();
        }

        public void Update(PaymentType paymentType)
        {
            db.Entry(paymentType).State = EntityState.Modified;
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