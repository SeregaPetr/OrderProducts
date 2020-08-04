using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProducts.DAL.IRepository
{
    public interface IDishRepository : IRepository<Dish>
    {
        IQueryable<Dish> Dishes { get; }
    }
}
