using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Recipe.Helpers;
using Recipe.Models;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Recipe.Dtos.RequestDto;
using System.Security.Claims;

namespace Recipe.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IRequest _request;

        public HomeController(IConfiguration config, IRequest request)
        {
            _request = request;
        }
        private IActionResult ClearSession()
        {
            return RedirectToAction("LogOut", "Login");
        }
        private bool VerifySession()
        {
            var user = JsonConvert.DeserializeObject<UserResponse>(HttpContext?.Session.GetString("UserDetails")??"null");
            return user == null;
        }
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Index()
        {
            if (VerifySession()) return ClearSession();
            ViewData["section"] = "Index";
            var request = new DecimalPageRequest();
            request.id = Convert.ToDecimal(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var session = HttpContext.Session.GetString("PagerRequest");
            if(session != null) request = JsonConvert.DeserializeObject<DecimalPageRequest>(session);
            var data = await _request.ApiCallPost<PagerResponse<RecipeDetailResponse>>("Food", "GetRecipeList", request,true);
            if (data.Result != null) data.Result.View = request?.pager.view;
            return View(data.Result);
        }
        public async Task<IActionResult> Details(decimal id=0)
        {
            var result = new RecipeDetailResponse();
            if (id > 0)
            {
                var response = await _request.ApiCallPost<RecipeDetailResponse>("Food", "GetRecipeById", new DecimalRequest { id = id },true);
                result = response.Result;
            }
            return View("Save",result);
        }
        public async Task<IActionResult> SaveRecipe(RecipeDetailResponse request)
        {
            if (ModelState.IsValid)
            {
                var data = await _request.ApiCallPost<bool>("Food", "SaveRecipe", request,true);
                return Json(data);
            }
            return Json(new { sucess = false, message = "failed" });
        }
        public IActionResult LoadIngredients(string data)
        {
            var ing = JsonConvert.DeserializeObject<List<IngredientResponse>>(data);
            return PartialView("IngredientPartial",ing);
        }
        public async Task<IActionResult> DeleteRecipe(decimal id)
        {
            if(id > 0)
            {
                var res = await _request.ApiCallPost<bool>("Food", "DeleteRecipeById", new DecimalRequest { id = id }, true);
                return Json(res);
            }
            return Json(new
            {
                Success= false,
                Message= "Failed"
            });
        }
        public async Task<IActionResult> DeleteIngredient(decimal id=0)
        {
            if (id > 0)
            {
                var res = await _request.ApiCallPost<bool>("Food", "DeleteIngredientById", new DecimalRequest { id = id },true);
                return Json(res);
            }
            return Json(new
            {
                Success = false,
                Message = "Failed"
            });
        }
        public async Task<IActionResult> View(decimal id)
        {
            var result = new RecipeDetailResponse();
            if (id > 0)
            {
                var response = await _request.ApiCallPost<RecipeDetailResponse>("Food", "GetRecipeById", new DecimalRequest { id = id }, true);
                result = response.Result;
            }
            return PartialView("View", result);
        }
        public async Task<IActionResult> ChangeFav(decimal? id,bool? change)
        {
            if (id != null && change != null)
            {
                var response = await _request.ApiCallPost<bool>("Food", "ChangeFav", new KeyValue { value = id,key= change.Value ? "Y" : "N" },true);
                return Json(response);
            }
            return Json(new {sucess=false,message="failed"});
        }
        public async Task<IActionResult> Favourites(DecimalPageRequest request)
        {
            request.id = Convert.ToDecimal(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            ViewData["section"] = "Favourites";
            HttpContext.Session.SetString("PagerRequest", JsonConvert.SerializeObject(request));
            var response = await _request.ApiCallPost<PagerResponse<RecipeDetailResponse>>("Food", "GetFavourites", request??new DecimalPageRequest(), true);
            if (response.Result != null) response.Result.View = request?.pager.view;
            return View("Index", response.Result);
        }
        public async Task<IActionResult> SaveImage(ImageRequest imgDetails)
        {
            var response = await _request.ApiCallPost<Guid?>("Documents", "SaveImage", imgDetails,true);
            return Json(response);
        }
        public async Task<IActionResult> LoadPages(DecimalPageRequest request)
        {
            request.id = Convert.ToDecimal(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            HttpContext.Session.SetString("PagerRequest", JsonConvert.SerializeObject(request));
            var response = await _request.ApiCallPost<PagerResponse<RecipeDetailResponse>>("Food", "GetRecipeList", request ?? new DecimalPageRequest(), true);
            if(response.Result != null) response.Result.View = request?.pager.view;
            return PartialView("Pages",response.Result);
        }
        public IActionResult GeneratePdf()
        {
            var pdf = new PdfGenerator();
            return File(pdf.Generate(), "application/pdf");
        }
    }
}
