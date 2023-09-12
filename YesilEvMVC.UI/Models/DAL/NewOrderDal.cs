using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YesilEvMVC.UI.Models.Entity;

namespace YesilEvMVC.UI.Models.DAL
{
    public class NewOrderDal
    {
        public void NewOrder(Orders order , Order_Details details)
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
              Orders addedOrder = db.Orders.Add(order);
                int AddedOrderID = addedOrder.OrderID;
                db.Order_Details.Add(new Order_Details
                {
                    OrderID = AddedOrderID,
                    ProductID = details.ProductID,
                    Quantity = details.Quantity,
                    UnitPrice = details.UnitPrice,
                    Discount = details.Discount
                });
            }
        
        }
    }
}