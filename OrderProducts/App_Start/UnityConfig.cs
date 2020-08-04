using OrderProducts.DAL;
using OrderProducts.DAL.IRepository;
using OrderProducts.DAL.MSSQLRepository;
using OrderProducts.Models;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace OrderProducts
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAddressRepository, MSSQLAddressRepository>();
            container.RegisterType<ICarouselRepository, MSSQLCarouselRepository>();
            container.RegisterType<IDishRepository, MSSQLDishRepository>();
            container.RegisterType<IMenuCategoryRepository, MSSQLMenuCategoryRepository>();
            container.RegisterType<IOrderLineRepository, MSSQLOrderLineRepository>();
            container.RegisterType<IOrderRepository, MSSQLOrderRepository>();
            container.RegisterType<IPaymentTypeRepository, MSSQLPaymentTypeRepository>();
            container.RegisterType<IUserRepository, MSSQLUserRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}