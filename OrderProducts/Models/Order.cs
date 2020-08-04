using OrderProducts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderProducts.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Дата заказа")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Время доставки")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDeliveryTime { get; set; }

        [Display(Name = "Комментарий")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public string StatusOrder { get; set; } = "new";

        [Display(Name = "Сумма")]
        public double OrderPrice
        {
            get { return OrderLines.Sum(item => item.OrderLinePrice); }
        }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }
    }
}
