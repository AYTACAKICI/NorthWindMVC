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
    public class ProductController : Controller
    {
        // GET: Product
        NORTHWNDEntities db = new NORTHWNDEntities();
        public ActionResult Index()
        {
            var deger = db.Products.ToList();
            
            return View(deger);
        }
        [HttpGet]
        public ActionResult NewProduct() 
        {
            List<SelectListItem> degerler = (from i in db.Categories.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            List<SelectListItem> uretici = (from i in db.Suppliers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.CompanyName,
                                                Value = i.SupplierID.ToString()
                                            }).ToList();
            ViewBag.urtc = uretici;
            return View();
        }
        [HttpPost]
        public ActionResult NewProduct(Products urun)
        {
            var ktg = db.Categories.Where(x => x.CategoryID == urun.Categories.CategoryID).FirstOrDefault();
            urun.Categories = ktg;
            var uretici = db.Suppliers.Where(x => x.SupplierID == urun.Suppliers.SupplierID).FirstOrDefault();
            urun.Suppliers = uretici;
            db.Products.Add(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int id)
        {
            var silinenUrun = db.Products.Find(id);
            db.Products.Remove(silinenUrun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FindProduct(int id)
        {
            var prdct = db.Products.Find(id);

            List<SelectListItem> degerler = (from i in db.Categories.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            List<SelectListItem> uretici = (from i in db.Suppliers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.CompanyName,
                                                Value = i.SupplierID.ToString()
                                            }).ToList();
            ViewBag.urtc = uretici;

            return View("FindProduct",prdct);
        }
        public ActionResult Update(Products p) 
        {
            var prdc = db.Products.Find(p.ProductID);
            prdc.ProductName = p.ProductName;
            prdc.SupplierID = p.SupplierID;
            prdc.UnitPrice = p.UnitPrice;
            prdc.UnitsInStock = p.UnitsInStock;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}