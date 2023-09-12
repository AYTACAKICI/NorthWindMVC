using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YesilEvMVC.UI.Models.VMs
{
    public class Customer_OrderVMs
    {
        public string Customer { get; set; }
        public decimal TotalOrderAmount { get; set; }
        public int TotalOrderQuantity { get; set; }

    }
}