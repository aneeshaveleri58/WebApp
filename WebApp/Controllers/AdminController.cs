using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

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
        [HttpGet]
        public ActionResult CreateProduct()
        {
            ViewBag.Categories = _context.tbl_category.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(tbl_product product)
        {
            if (ModelState.IsValid)
            {
                _context.tbl_product.Add(product);
                _context.SaveChanges();
                return RedirectToAction("ManageProducts");
            }
            ViewBag.Categories = _context.tbl_category.ToList();
            return View(product);
        }

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
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(tbl_product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.tbl_product.Find(id);
            _context.tbl_product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("ManageProducts");
        }

        // GET: Admin/ManageCategories
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(tbl_category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if category already exists
                    bool categoryExists = _context.tbl_category.Any(c => c.cat_name == category.cat_name);
                    if (categoryExists)
                    {
                        ModelState.AddModelError("", "A category with this name already exists.");
                        return View(category);
                    }

                    _context.tbl_category.Add(category);
                    _context.SaveChanges();
                    return RedirectToAction("ManageCategories");
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception and handle specific constraint violation
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UQ__tbl_cate__FA8C1790195E278D"))
                    {
                        ModelState.AddModelError("", "A category with this name already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while saving the category.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An unexpected error occurred.");
                }
            }
            return View(category);
        }





        public ActionResult ManageCategories()
        {
            var categories = _context.tbl_category.ToList();
            return View(categories);
        }


        // GET: Admin/EditCategory/5
        public ActionResult EditCategory(int id)
        {
            var category = _context.tbl_category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/EditCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(tbl_category category)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ManageCategories");
            }
            return View(category);
        }

        // GET: Admin/DeleteCategory/5
        public ActionResult DeleteCategory(int id)
        {
            var category = _context.tbl_category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/DeleteCategory/5
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            var category = _context.tbl_category.Find(id);
            _context.tbl_category.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("ManageCategories");
        }
    }
}
