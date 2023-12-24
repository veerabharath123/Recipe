using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipe.Helpers;
using Recipe.Models;
using System.Security.Claims;

namespace Recipe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRequest _request;

        public LoginController(IConfiguration config,IRequest request)
        {
            _request = request;
        }
        [ResponseCache(NoStore = true)]
        public IActionResult LoginPage()
        {
            var UserLogin = HttpContext.Session.GetString("UserLogin");
            if (Convert.ToBoolean(UserLogin??"false"))
                return RedirectToAction("Index", "Home");
            return View("Login");
        }
        [ResponseCache(NoStore = true)]
        public IActionResult SigninPage()
        {
            var UserLogin = HttpContext.Session.GetString("UserLogin");
            if (Convert.ToBoolean(UserLogin ?? "false"))
                return RedirectToAction("Index", "Home");
            return View("Signin");
        }
        public async Task<IActionResult> CreateAccount(LoginRequest login)
        {
            if (ModelState.IsValid)
            {
                var data = await _request.ApiCallPost<UserResponse>("Users", "CreateUser", login);
                if (data.Success && data.Result != null)
                {
                    await SignIn(data.Result);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("LoginPage");
        }
        [HttpPost]
        public async Task<IActionResult> VerifyUser(LoginRequest login)
        {
            if (ModelState.IsValid)
            {
                var data = await _request.ApiCallPost<UserResponse>("Users", "Login", login);
                if (data.Success && data.Result != null)
                {
                    await SignIn(data.Result);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("LoginPage");
        }
        private async Task SignIn(UserResponse user)
        {
            HttpContext.Session.SetString("UserLogin", Convert.ToString(true));
            HttpContext.Session.SetString("UserDetails", JsonConvert.SerializeObject(user));
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
                            new Claim(ClaimTypes.Name, user.username),
                        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginPage");
        }
    }
}
