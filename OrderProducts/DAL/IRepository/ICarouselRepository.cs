using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProducts.DAL.IRepository
{
    public interface ICarouselRepository : IRepository<Carousel>
    {
        IQueryable<Carousel> Carousels { get; }
    }
}
