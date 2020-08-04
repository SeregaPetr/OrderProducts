using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using Microsoft.Ajax.Utilities;
using OrderProducts.DAL;
using OrderProducts.DAL.IRepository;
using OrderProducts.Models;
using OrderProducts.Models.ViewModels;

namespace OrderProducts.Controllers
{
    public class OrderController : Controller
    {
        private readonly IPaymentTypeRepository paymentTypeRepository;
        private readonly IDishRepository dishRepository;
        private readonly IUserRepository userRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderLineRepository orderLineRepository;

        public OrderController(IPaymentTypeRepository paymentType, IDishRepository dish, IUserRepository user,
            IAddressRepository address, IOrderRepository order, IOrderLineRepository orderLine)
        {
            paymentTypeRepository = paymentType;
            dishRepository = dish;
            userRepository = user;
            addressRepository = address;
            orderRepository = order;
            orderLineRepository = orderLine;
        }

        public ActionResult Order()
        {
            ViewBag.ActiveOrder = "";
            ViewBag.PaymentTypes = new SelectList(paymentTypeRepository.PaymentTypes.ToList(), "Id", "NamePaymentType");

            if (!(Session["order"] is Order order) || order.OrderLines.Count == 0)
            {
                ViewBag.ActiveOrder = "disabled";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order([Bind(Include = "FirstName,LastName,Phone,Email,Street,HouseNumber,ApartmentNumber,NamePaymentType,OrderDeliveryTime,Comment")] UserDelivery userAddress)
        {
            List<PaymentType> payments = paymentTypeRepository.PaymentTypes.ToList();
            ViewBag.PaymentTypes = new SelectList(payments, "Id", "NamePaymentType", payments.First(x => x.Id == int.Parse(userAddress.NamePaymentType)));

            if (!(Session["order"] is Order order) || order.OrderLines.Count == 0)
            {
                ModelState.AddModelError("", "Корзина заказов пуста");

                return View(userAddress);
            }

            userAddress.OrderDeliveryTime ??= DateTime.Now.AddHours(1);

            if (DateTime.Now.AddMinutes(59).CompareTo(userAddress.OrderDeliveryTime) > 0)
            {
                ModelState.AddModelError("OrderDeliveryTime", "Время для доставки мин. 1 час");

                return View(userAddress);
            }

            if (!ModelState.IsValid)
            {
                return View(userAddress);
            }

            //-----------------------------------

            Order newOrder = null;
            User user = userRepository.Users.FirstOrDefault(u => u.Phone == userAddress.Phone);

            Address address = addressRepository.Addresses.FirstOrDefault(a => a.Street == userAddress.Street &&
                                                                              a.HouseNumber == userAddress.HouseNumber && 
                                                                              a.ApartmentNumber == userAddress.ApartmentNumber);

            Address newAddress = null;
            if (address == null)
            {
                newAddress = new Address
                {
                    Street = userAddress.Street,
                    HouseNumber = userAddress.HouseNumber,
                    ApartmentNumber = userAddress.ApartmentNumber
                };
            }

            if (user == null)
            {
                user = new User
                {
                    FirstName = userAddress.FirstName,
                    LastName = userAddress.LastName,
                    Phone = userAddress.Phone,
                    Email = userAddress.Email
                };

                if (address == null)
                {
                    user.Addresses.Add(newAddress);

                    newOrder = new Order
                    {
                        User = user,
                        Address = newAddress
                    };
                }
                else
                {
                    address.Users.Add(user);
                    addressRepository.Update(address);

                    newOrder = new Order
                    {
                        UserId = user.Id,
                        AddressId = address.Id
                    };
                }
            }
            else
            {
                if (address == null)
                {
                    user.Addresses.Add(newAddress);
                    userRepository.Update(user);

                    newOrder = new Order
                    {
                        UserId = user.Id,
                        AddressId = newAddress.Id
                    };
                }
                else
                {
                    bool isAddressUser = false;

                    foreach (var item in user.Addresses)
                    {
                        if (item.Street == userAddress.Street && item.HouseNumber == userAddress.HouseNumber &&
                             item.ApartmentNumber == userAddress.ApartmentNumber)
                        {
                            isAddressUser = true;
                            break;
                        }
                    }

                    if (!isAddressUser)
                    {
                        userRepository.Add(user, address);
                    }

                    newOrder = new Order
                    {
                        UserId = user.Id,
                        AddressId = address.Id
                    };
                }
            }

            newOrder.Comment = userAddress.Comment;
            newOrder.OrderDate = DateTime.Now;
            newOrder.OrderDeliveryTime = (DateTime)userAddress.OrderDeliveryTime;
            newOrder.PaymentTypeId = int.Parse(userAddress.NamePaymentType);

            orderRepository.Add(newOrder);

            foreach (var item in order.OrderLines)
            {
                OrderLine orderLine = new OrderLine
                {
                    DishId = item.Dish.Id,
                    Quantity = item.Quantity,
                    OrderId = newOrder.Id
                };

                orderLineRepository.Add(orderLine);
            }
            Session["order"] = null;
            order = orderRepository.Orders.First(o => o.Id == newOrder.Id);

            return View("OrderAccepted", order);
        }

        [ChildActionOnly]
        public ActionResult OrderContents()
        {
            if (!(Session["order"] is Order order))
            {
                order = new Order();
            }
            return PartialView("_OrderContents", order);
        }

        [ChildActionOnly]
        public ActionResult ShowQuantityItemsInCart()
        {
            int quantity = RefreshCartData();
            return PartialView("_Cart", quantity);
        }

        [HttpPost]
        public ActionResult RefreshCart()
        {
            int quantity = RefreshCartData();
            return PartialView("_Cart", quantity);
        }

        private int RefreshCartData()
        {
            Order order = Session["order"] as Order;
            int quantity = order?.OrderLines?.Count() ?? 0;
            return quantity;
        }

        [HttpPost]
        public ActionResult AddDishToCart(int id)
        {
            Order order = AddDish(id);
            int quantity = order.OrderLines.Count();

            return PartialView("_Cart", quantity);
        }

        [HttpPost]
        public ActionResult AddQuantityDishsToOrder(int id)
        {
            Order order = AddDish(id);

            return PartialView("_OrderContents", order);
        }

        private Order AddDish(int id)
        {
            if (!(Session["order"] is Order order))
            {
                order = new Order();
            }
            OrderLine orderLine = order?.OrderLines?.FirstOrDefault(x => x.Dish.Id == id);

            if (orderLine == null)
            {
                Dish dish = dishRepository.Dishes.First(d => d.Id == id);

                orderLine = new OrderLine
                {
                    Dish = dish,
                    Quantity = 1,
                };
                order.OrderLines?.Add(orderLine);
            }
            else
            {
                ++orderLine.Quantity;
            }
            Session["order"] = order;

            return order;
        }

        [HttpPost]
        public ActionResult RemoveQuantityDishToOrder(int Id)
        {
            Order order = Session["order"] as Order;
            OrderLine orderLine = order?.OrderLines.FirstOrDefault(x => x.Dish.Id == Id);

            if (orderLine?.Quantity > 1)
            {
                --orderLine.Quantity;
            }
            else
            {
                order?.OrderLines.Remove(orderLine);
            }
            Session["order"] = order;

            return PartialView("_OrderContents", order);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Order order = Session["order"] as Order;
            OrderLine orderLineDelete = order?.OrderLines.First(x => x.Dish.Id == id);
            order?.OrderLines.Remove(orderLineDelete);

            Session["order"] = order;

            return PartialView("_OrderContents", order);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                paymentTypeRepository.Dispose();
                dishRepository.Dispose();
                userRepository.Dispose();
                addressRepository.Dispose();
                orderRepository.Dispose();
                orderLineRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
