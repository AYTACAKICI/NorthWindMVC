using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YesilEvMVC.UI.Models.Entity;
using YesilEvMVC.UI.Models.VMs;

namespace YesilEvMVC.UI.Models.DAL
{
    public class CustomerOrderDAL:IDisposable
        
    {
        public void Dispose()
        {
          
        }
        List<Customer_OrderVMs> table;
        public List<Customer_OrderVMs> FindData()
        {
          
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                table = (from customer in db.Customers
                         join order in db.Orders on customer.CustomerID equals order.CustomerID
                         join orderDetails in db.Order_Details on order.OrderID equals orderDetails.OrderID
                         group order by customer.CompanyName into g
                         select new Customer_OrderVMs
                         {
                             Customer = g.Key,
                             TotalOrderQuantity = g.Count(),
                           //  TotalOrderAmount = g.Sum(x=>x.Order_Details.Sum(a=>a.Quantity*a.UnitPrice))      
                         }).ToList();
            }
            return table;
        }
        public List<Customer_OrderVMs> FindDataFilteredbyEmployee(string EmployeeName) 
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                table = (from custumer in db.Customers
                         join order in db.Orders on custumer.CustomerID equals order.CustomerID
                         join orderDetails in db.Order_Details on order.OrderID equals orderDetails.OrderID
                         join Employee in db.Employees on order.EmployeeID equals Employee.EmployeeID
                         where Employee.FirstName == EmployeeName
                         group order by custumer.CompanyName into g
                         select new Customer_OrderVMs
                         {
                             Customer = g.Key,
                             TotalOrderQuantity = g.Count(),
                             TotalOrderAmount = g.Sum(x => x.Order_Details.Sum(a => a.Quantity * a.UnitPrice))
                         }).ToList();

            }
            return table;
        }

        public List<Customer_OrderVMs> FindDateFilteredbyCompany(string CompanyName)
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                table = (from custumer in db.Customers where custumer.CompanyName==CompanyName
                         join order in db.Orders on custumer.CustomerID equals order.CustomerID
                         join orderDetails in db.Order_Details on order.OrderID equals orderDetails.OrderID                         
                         group order by custumer.CompanyName into g
                         select new Customer_OrderVMs
                         {
                             Customer = g.Key,
                             TotalOrderQuantity = g.Count(),
                             TotalOrderAmount = g.Sum(x => x.Order_Details.Sum(a => a.Quantity * a.UnitPrice))
                         }).ToList();

               

            }
            return table;
        }
        public List<Customer_OrderVMs> FindDataFİlteredbyShipper(string Shipper)
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                table = (from custumer in db.Customers
                         join order in db.Orders on custumer.CustomerID equals order.CustomerID
                         join orderDetails in db.Order_Details on order.OrderID equals orderDetails.OrderID
                         join shipper in db.Shippers on order.ShipVia equals shipper.ShipperID
                         where shipper.CompanyName == Shipper
                         group order by custumer.CompanyName into g
                         select new Customer_OrderVMs
                         {
                             Customer = g.Key,
                             TotalOrderQuantity = g.Count(),
                             TotalOrderAmount = g.Sum(x => x.Order_Details.Sum(a => (a.Quantity * a.UnitPrice) * (1 - Convert.ToDecimal(a.Discount))))
                         }).ToList();

            }
            return table;
        }

        public List<SelectListItem> FindCustomers()
        {
            List<SelectListItem> customers;
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                customers = (from costumers in db.Customers
                             select new SelectListItem
                             {Text = costumers.CompanyName,
                             Value = costumers.CustomerID

                             }).ToList();
            }

            return customers;
        
        }
        public List<SelectListItem> FindEmployee()
        {
            List<SelectListItem> employees;
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                employees = (from e in db.Employees
                             select new SelectListItem
                             {
                                 Text=e.FirstName,
                                 Value=e.EmployeeID.ToString()
                             }).ToList();
            }

            return employees;

        }
        public List<SelectListItem> FindShippers()
        {
            List<SelectListItem> shippers;
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                shippers = (from e in db.Shippers
                             select new SelectListItem
                             {
                                 Text = e.CompanyName,
                                 Value = e.ShipperID.ToString()
                             }).ToList();
            }

            return shippers;


        }
        public List<SelectListItem> FindProduct()
        {
            List<SelectListItem> product;
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                product = (from e in db.Products
                            select new SelectListItem
                            {
                                Text = e.ProductName,
                                Value = e.ProductID.ToString()
                            }).ToList();
            }

            return product;


        }
        public void NewOrder(Tuple<Orders, Order_Details> ord)
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                //var customer = db.Customers.Where(x => x.CustomerID == ord.Item1.CustomerID).FirstOrDefault();
                //ord.Item1.Customers = customer;
                //var employee = db.Employees.Where(x => x.EmployeeID == ord.Item1.EmployeeID).FirstOrDefault();
                //ord.Item1.Employees = employee;
                //var shipper = db.Shippers.Where(x => x.ShipperID == ord.Item1.ShipVia).FirstOrDefault();
                //ord.Item1.Shippers = shipper;
                Orders addedOrder = db.Orders.Add(new Orders()
                { 
                  CustomerID = ord.Item1.CustomerID,
                  EmployeeID= ord.Item1.EmployeeID,
                  RequiredDate= ord.Item1.RequiredDate,
                  ShipAddress= ord.Item1.ShipAddress,
                  ShipCity= ord.Item1.ShipCity,
                  ShipCountry= ord.Item1.ShipCountry,
                  ShippedDate= ord.Item1.ShippedDate,
                  ShipPostalCode = ord.Item1.ShipPostalCode,
                  ShipName= ord.Item1.ShipName,
                  ShipRegion= ord.Item1.ShipRegion,
                  ShipVia= ord.Item1.ShipVia,
                  
                });
                db.SaveChanges();
                int AddedOrderID = addedOrder.OrderID;



                var product = db.Products.Where(x => x.ProductID == ord.Item2.ProductID).FirstOrDefault();
                ord.Item2.Products = product;

                db.Order_Details.Add(new Order_Details
                {
                    OrderID = AddedOrderID,
                    ProductID = ord.Item2.ProductID,
                    Quantity = ord.Item2.Quantity,
                    UnitPrice = ord.Item2.UnitPrice,
                    Discount = ord.Item2.Discount
                });
                db.SaveChanges();
                
            }

        }

    }
}