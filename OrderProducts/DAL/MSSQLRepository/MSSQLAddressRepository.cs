using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLAddressRepository : IAddressRepository
    {
        private OrderProductContext db;

        public MSSQLAddressRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<Address> Addresses => db.Addresses;

        public void Add(Address address)
        {
            db.Addresses.Add(address);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Address address)
        {
            db.Entry(address).State = EntityState.Modified;
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