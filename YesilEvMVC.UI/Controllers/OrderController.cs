
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using YesilEvMVC.UI.Models;
using YesilEvMVC.UI.Models.DAL;
using YesilEvMVC.UI.Models.Entity;
using YesilEvMVC.UI.Models.VMs;

namespace YesilEvMVC.UI.Controllers
{
    
    public class DenemeController : Controller 
    {
        public ActionResult deneme() 
        
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                ViewBag.ornek = db.Categories.ToList();
                TempData["ornek"] = db.Categories.ToList();
             
            }

            return View();
        }

        public ActionResult deneme2() 
        {

            return View();
        }
        public ActionResult deneme3()
        {


            return View();
        }


    }
    
    
    public class OrderController : Controller 
    {
        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {

            using (CustomerOrderDAL dal = new CustomerOrderDAL())
            {
                TempData["Data"] = dal.FindData();
                //TempData["company"] = dal.FindCustomers();
                //TempData["employee"] = dal.FindEmployee();
                //TempData["shipper"] = dal.FindShippers();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerOrderFilterVMs vm)
        {
            if (vm.CompanyName !=null && vm.EmplooyeName==null&&vm.ShipperName==null)
            {
                using (CustomerOrderDAL dal = new CustomerOrderDAL())
                {
                    TempData["Data"] = dal.FindDateFilteredbyCompany(vm.CompanyName);
                }

            }
            else if(vm.CompanyName == null && vm.EmplooyeName != null && vm.ShipperName == null)
            {
                using (CustomerOrderDAL dal = new CustomerOrderDAL())
                {
                    TempData["Data"] = dal.FindDataFilteredbyEmployee(vm.EmplooyeName);
                }
            }
            else if (vm.CompanyName == null && vm.EmplooyeName == null && vm.ShipperName != null)
            {
                using (CustomerOrderDAL dal = new CustomerOrderDAL())
                {
                    TempData["Data"] = dal.FindDataFİlteredbyShipper(vm.ShipperName);
                }
            }
            else
            {
                using (CustomerOrderDAL dal = new CustomerOrderDAL())
                {
                    TempData["Data"] = dal.FindData();
                }

            }
            return View();
        }



        [HttpGet]
        public ActionResult NewOrder() 
        {
            using (CustomerOrderDAL dal = new CustomerOrderDAL())
            {
                ViewBag.customer = dal.FindCustomers();
                ViewBag.product = dal.FindProduct();
                ViewBag.shipper = dal.FindShippers();
                ViewBag.employee   = dal.FindEmployee();
                //TempData["Data"] = dal.FindData();

            }
            return View();
        }
        [HttpPost]
        public ActionResult NewOrder(Tuple<Orders,Order_Details>ord) 
        {
            using (CustomerOrderDAL dal = new CustomerOrderDAL())
            {
                dal.NewOrder(ord);
            }

            return RedirectToAction("Index");

            
        }
    }
}