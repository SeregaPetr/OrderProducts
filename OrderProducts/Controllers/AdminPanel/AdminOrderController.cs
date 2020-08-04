using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrderProducts.DAL;
using OrderProducts.DAL.IRepository;
using OrderProducts.Models;

namespace OrderProducts.Controllers.AdminPanel
{
    [Authorize(Roles = "admin, employee")]
    public class AdminOrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public AdminOrderController(IOrderRepository order)
        {
            orderRepository = order;
            ViewBag.SelectedMenu = "orders";
        }

        public ActionResult SortOrder(string status,string sort, bool direction = true )
        {
            StatusTitle(status);
            ViewBag.Direction = !direction;
            List<Order> orders;
            switch (sort)
            {
                case "orderNumber":
                    if (direction)
                        orders = orderRepository.Orders.Where(s=>s.StatusOrder== status).OrderBy(o => o.Id).ToList();
                    else
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).OrderByDescending(o => o.Id).ToList();
                    break;
                case "orderDate":
                    if (direction)
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).OrderBy(o => o.OrderDate).ToList();
                    else
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).OrderByDescending(o => o.OrderDate).ToList();
                    break;
                case "orderDeliveryTime":
                    if (direction)
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).OrderBy(o => o.OrderDeliveryTime).ToList();
                    else
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).OrderByDescending(o => o.OrderDeliveryTime).ToList();
                    break;
                case "orderPrice":
                    if (direction)
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).ToList().OrderBy(o => o.OrderPrice).ToList();
                    else
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).ToList().OrderByDescending(o => o.OrderPrice).ToList();
                    break;
                case "comment":
                    if (direction)
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).OrderBy(o => o.Comment).ToList();
                    else
                        orders = orderRepository.Orders.Where(s => s.StatusOrder == status).OrderByDescending(o => o.Comment).ToList();
                    break;
                default:
                    orders = orderRepository.Orders.ToList();
                    break;
            }
            return View("Index", orders);
        }

        public ActionResult Index(string status = "new")
        {
            StatusTitle(status);
            var orders = orderRepository.Orders.Where(o => o.StatusOrder == status).OrderBy(d => d.OrderDate).ToList();

            return View(orders);
        }

        private void StatusTitle(string status)
        {
            ViewBag.Status = status;
            switch (status)
            {
                case "new":
                    ViewBag.Title = "Новые заказы";
                    break;
                case "process":
                    ViewBag.Title = "Обработанные заказы";
                    break;
                case "delivery":
                    ViewBag.Title = "Заказы на доставке";
                    break;
                case "closed":
                    ViewBag.Title = "Закрытые";
                    break;
            }
        }

        public ActionResult Details(int? id, string status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderRepository.Orders.First(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            switch (status)
            {
                case "new":
                    ViewBag.ButtonName = "Обработать заказ";
                    break;
                case "process":
                    ViewBag.ButtonName = "Оформить доставку";
                    break;
                case "delivery":
                    ViewBag.ButtonName = "Подтвердить доставку";
                    break;
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Process(int id, string status)
        {
            Order order = orderRepository.Orders.First(o => o.Id == id);
            switch (status)
            {
                case "new":
                    order.StatusOrder = "process";
                    break;
                case "process":
                    order.StatusOrder = "delivery";
                    break;
                case "delivery":
                    order.StatusOrder = "closed";
                    break;
            }
            orderRepository.Update(order);
            return RedirectToAction("Index", new { status });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string status)
        {
            orderRepository.Delete(id);

            return RedirectToAction("Index", new { status });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                orderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
