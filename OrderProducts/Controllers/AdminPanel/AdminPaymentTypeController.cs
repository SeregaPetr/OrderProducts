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
    [Authorize(Roles = "admin")]
    public class AdminPaymentTypeController : Controller
    {
        private readonly IPaymentTypeRepository paymentTypeRepository;

        public AdminPaymentTypeController(IPaymentTypeRepository paymentType)
        {
            paymentTypeRepository = paymentType;
            ViewBag.SelectedMenu = "admin";
        }

        public ActionResult Sort(bool direction = true)
        {
            ViewBag.Direction = !direction;
            List<PaymentType> paymentTypes;

            if (direction)
                paymentTypes = paymentTypeRepository.PaymentTypes.OrderBy(p => p.NamePaymentType).ToList();
            else
                paymentTypes = paymentTypeRepository.PaymentTypes.OrderByDescending(p => p.NamePaymentType).ToList();

            return View("Index", paymentTypes);
        }

        // GET: AdminPaymentType
        public ActionResult Index()
        {
            List<PaymentType> paymentTypes = paymentTypeRepository.PaymentTypes.ToList();
            return View(paymentTypes);
        }

        // GET: AdminPaymentType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = paymentTypeRepository.PaymentTypes.First(p => p.Id == id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // GET: AdminPaymentType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPaymentType/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NamePaymentType")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                paymentTypeRepository.Add(paymentType);
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: AdminPaymentType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = paymentTypeRepository.PaymentTypes.First(p => p.Id == id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: AdminPaymentType/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NamePaymentType")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                paymentTypeRepository.Update(paymentType);
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: AdminPaymentType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = paymentTypeRepository.PaymentTypes.First(p => p.Id == id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: AdminPaymentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paymentTypeRepository.Delete(id);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                paymentTypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
