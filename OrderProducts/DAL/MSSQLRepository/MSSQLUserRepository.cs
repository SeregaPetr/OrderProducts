using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OrderProducts.DAL.OrderProducts.BLL.Services;

namespace OrderProducts.DAL.MSSQLRepository
{
    public class MSSQLUserRepository : IUserRepository
    {
        private OrderProductContext db;

        public MSSQLUserRepository()
        {
            db = new OrderProductContext();
        }

        public IQueryable<User> Users => db.Users;

        public void Add(User user, Address address)
        {
            Address addressDb = db.Addresses.First(f => f.Id == address.Id);
            user.Addresses.Add(addressDb);

            db.SaveChanges();
        }

        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
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