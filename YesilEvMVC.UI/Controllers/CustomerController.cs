using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YesilEvMVC.UI.Models;
using YesilEvMVC.UI.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace YesilEvMVC.UI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        NORTHWNDEntities db = new NORTHWNDEntities();
        public ActionResult Index(int sayfa=1)
        {
            var deger = db.Customers.ToList().ToPagedList(sayfa,10);
            return View(deger);
        }
        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCustomer( Customers cst)
        {
            db.Customers.Add(cst);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCustomer(string id)
        {
            var silinecekData = db.Customers.Find(id);
           db.Customers.Remove(silinecekData);
            int donenVeri = db.SaveChanges();
            if (donenVeri > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Hata");
            }
        }
        public ActionResult FindCustomer(string id) 
        {
            var cstmr = db.Customers.Find(id);
            return View("FindCustomer",cstmr);
        }
        public ActionResult Update(Customers c) 
        {
            var cstmr = db.Customers.Where(x => x.CustomerID == c.CustomerID).FirstOrDefault();
            
            cstmr.CompanyName = c.ContactName;
            cstmr.Address = c.Address;
            cstmr.City = c.City;
            cstmr.ContactTitle = c.ContactTitle;
            cstmr.Phone = c.Phone;
            db.SaveChanges();
            return RedirectToAction("Index");

            //var ct = db.Customers.Select(x => x.CustomerID)

        }

    }
}