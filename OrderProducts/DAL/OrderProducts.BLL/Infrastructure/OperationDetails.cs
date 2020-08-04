﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL.OrderProducts.BLL.Infrastructure
{
    public class OperationDetails
    {
        public bool Succedeed { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }

        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }
    }
}