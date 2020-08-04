using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProducts.DAL.IRepository
{
    public interface IRepository<T> : IDisposable
         where T : class
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}

