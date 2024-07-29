using System.Linq;
using System.Web.Mvc;
using WebApp.Models; // Ensure this namespace matches your project

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBMarketEntities _context;

        public AdminController()
        {
            _context = new DBMarketEntities(); // Initialize your DbContext
        }

        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()M
        {
            return View(new LoginViewModel()); // Return the view with an empty ViewModel
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if user exists in the database
                var user = _context.tbl_admin
                    .FirstOrDefault(u => u.ad_username == model.Username && u.ad_password == model.Password);

                if (user != null)
                {
                    // User authenticated
                    // You can set session variables or perform other actions here
                    return RedirectToAction("Index", "Home"); // Redirect to home or another action
                }

                // Authentication failed
                ViewBag.LoginFailed = "Invalid username or password.";
            }

            // Return the view with the model if validation fails
            return View(model);
        }
    }
}
