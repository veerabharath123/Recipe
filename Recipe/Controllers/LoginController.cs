using Microsoft.AspNetCore.Mvc;

namespace Recipe.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login(bool? signin)
        {
            var session = HttpContext.Session.GetString("LoginPage");
            if (session != null && signin == null) ViewData["signin"] = Convert.ToBoolean(session);
            else
            {
                signin = signin??false;
                ViewData["signin"] = signin;
                HttpContext.Session.SetString("LoginPage", Convert.ToString(signin));
            }
            return View();
        }
        public IActionResult VerifyUser(string email,string password)
        {

            return RedirectToAction("Login");
        }
    }
}
