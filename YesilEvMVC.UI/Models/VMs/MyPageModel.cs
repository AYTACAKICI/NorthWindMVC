using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YesilEvMVC.UI.Models.VMs
{
    public class MyPageModel
    {
        public List<CustomerOrderFilterVMs> customerOrderFilterVMs { get; set; }
        public List<Customer_OrderVMs> customer_OrderVMs { get; set; }
    }
}