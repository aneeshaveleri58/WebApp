using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Data.Entity; // Ensure this namespace is included

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private DBMarketEntities _context = new DBMarketEntities();

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = _context.tbl_admin
                                    .FirstOrDefault(a => a.ad_username == model.Username && a.ad_password == model.Password);
                if (admin != null)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(model);
        }

        // GET: Admin/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Admin/ManageProducts
        public ActionResult ManageProducts()
        {
            var products = _context.tbl_product.ToList();
            return View(products);
        }

        // GET: Admin/EditProduct/5
        public ActionResult EditProduct(int id)
        {
            var product = _context.tbl_product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/EditProduct/5
        [HttpPost]
        public ActionResult EditProduct(tbl_product product)
        {
            if (ModelState.IsValid)
            {
                //_context.Entry(product).State = EntityState.Modified; // Ensure this line is correct
                //_context.SaveChanges();
                return RedirectToAction("ManageProducts");
            }
            return View(product);
        }

        // GET: Admin/DeleteProduct/5
        public ActionResult DeleteProduct(int id)
        {
            var product = _context.tbl_product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/DeleteProduct/5
        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.tbl_product.Find(id);
            _context.tbl_product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("ManageProducts");
        }
    }
}
