using Microsoft.AspNetCore.Mvc;
using Recipe.Helpers;
using Recipe.Models;

namespace Recipe.Controllers
{
    public class LoginController : Controller
    {
        private readonly Request _request;

        public LoginController(IConfiguration config)
        {
            _request = new Request(config);
        }
        public IActionResult Login(bool? signin)
        {
            var UserLogin = HttpContext.Session.GetString("UserLogin");
            if (Convert.ToBoolean(UserLogin??"false"))
                return RedirectToAction("Index", "Home");
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
        [HttpPost]
        public async Task<IActionResult> VerifyUser(LoginUser login)
        {
            if (ModelState.IsValid)
            {
                if(login.emailOrUsername == "abc" || login.password == "Ua5KeKeHOoFoLSysnaM27w==")
                {
                    var data = await _request.ApiCallPost<bool>("Users", "Login", login);
                    HttpContext.Session.SetString("UserLogin", Convert.ToString(true));
                    return RedirectToAction("Index", "Home");
                }  
            }
            HttpContext.Session.SetString("UserLogin", Convert.ToString(false));
            return RedirectToAction("Login");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserLogin", Convert.ToString(false));
            return RedirectToAction("Login");
        }
    }
}
