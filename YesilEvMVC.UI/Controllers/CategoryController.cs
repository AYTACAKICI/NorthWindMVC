using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YesilEvMVC.UI.Models;
using YesilEvMVC.UI.Models.Entity;

namespace YesilEvMVC.UI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        NORTHWNDEntities db = new NORTHWNDEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.Categories select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(x => x.CategoryName.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.Categories.ToList();            
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult NewCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult NewCategory(Categories cat) 
        {
            if (!ModelState.IsValid)
            {
                return View("NewCategory");

            }
            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            var cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FindCategory(int id) 
        {
           var ktgr = db.Categories.Find(id);
            return View("FindCategory", ktgr);
        }
        public ActionResult Update(Categories k)
        {
            var ctgr = db.Categories.Find(k.CategoryID);
            ctgr.CategoryName = k.CategoryName;
            ctgr.Description = k.Description;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //public ActionResult kategoriUrun()
        //{
           

        //    //var kategorilerDekiUrunler = (from ctgr in db.Categories
        //    //                              join urn in db.Products on ctgr equals urn.CategoryID into   )

           

        //    //var kategorilerDekiUrunler = (from c in db.Categories
        //    //                              join p in db.Products 
        //    //                              on c.CategoryID equals p.CategoryID
        //    //                              into urunsayisi
                                          
        //    //                              select new KategoriProduct() { Kategori = c, Urun = p == null ? "(Urun Yok)" : p. }).ToList();
        //    //return View(kategorilerDekiUrunler);
        //}
    }
}