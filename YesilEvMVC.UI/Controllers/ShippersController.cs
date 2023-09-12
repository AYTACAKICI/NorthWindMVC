using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YesilEvMVC.UI.Models.Entity;

namespace YesilEvMVC.UI.Controllers
{
    public class ShippersController : Controller
    {
        // GET: Shippers
        public ActionResult Index()
        {
            List<Shippers> degerler;
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                 degerler = db.Shippers.ToList();
            }
            
            return View(degerler);
        }

        [HttpGet]
        public ActionResult NewShippers() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewShippers(Shippers spr) 
        {
            NORTHWNDEntities db = new NORTHWNDEntities();
            
                db.Shippers.Add(spr);
                db.SaveChanges();

            return RedirectToAction("Index");
            
        }
        public ActionResult DeleteShippers(int id)
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                var shipper = db.Shippers.Find(id);
                db.Shippers.Remove(shipper);
                int dönenVeri = db.SaveChanges();
                if (dönenVeri > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Hata");
                }

            }         
        }

        public ActionResult FindShippers(int id) 
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                var shipper = db.Shippers.Find(id);


                return View(shipper);
            }
        }

        public ActionResult UpdateShippers(Shippers shipper)
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
               var shipperupdated = db.Shippers.Find(shipper.ShipperID);
                shipperupdated.CompanyName = shipper.CompanyName;
                shipperupdated.Phone = shipper.Phone;
                var hede = db.SaveChanges();
                if (hede!=0)
                {
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Hata");
                }

            }
            
        }

    }
}