using Microsoft.AspNet.Identity.EntityFramework;
using OrderProducts.DAL.EF;
using OrderProducts.DAL.Entities;
using OrderProducts.DAL.Identity;
using OrderProducts.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrderProducts.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext db;
        public ApplicationUserManager UserManager { get; }
        public IClientManager ClientManager { get; }
        public ApplicationRoleManager RoleManager { get; }

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            ClientManager = new ClientManager(db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    ClientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}