using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Recipe.Helpers;
using Recipe.Models;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore;

namespace Recipe.Controllers
{
    public class HomeController : Controller
    {
        private readonly Request _request;

        public HomeController(IConfiguration config)
        {
            _request = new Request(config);
        }
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Index()
        {
            ViewData["section"] = "Index";
            var request = new PagerRequest();
            var session = HttpContext.Session.GetString("PagerRequest");
            if(session != null) request = JsonConvert.DeserializeObject<PagerRequest>(session);
            var data = await _request.ApiCallPost<PagerResponse<RecipeDetails>>("Food", "GetRecipeList", request);
            if (data.Result != null) data.Result.VIEW = request.view;
            return View(data.Result);
        }
        public async Task<IActionResult> Details(decimal? id)
        {
            var result = new RecipeDetails();
            if (id != null)
            {
                var response = await _request.ApiCallPost<RecipeDetails>("Food", "GetRecipeById", new KeyValue { value = id });
                result = response.Result;
            }
            return View("Save",result);
        }
        public async Task<IActionResult> SaveRecipe(RecipeDetails request)
        {
            if (ModelState.IsValid)
            {
                var data = await _request.ApiCallPost<bool>("Food", "SaveRecipe", request);
                if (data.Result)
                    return RedirectToAction("Index");
            }
            return View("Save", request);
        }
        public IActionResult LoadIngredients(string data)
        {
            var ing = JsonConvert.DeserializeObject<List<Ingredient>>(data);
            return PartialView("IngredientPartial",ing);
        }
        public async Task<IActionResult> DeleteRecipe(decimal? id)
        {
            if(id != null && id > 0)
            {
                var res = await _request.ApiCallPost<bool>("Food", "DeleteRecipeById", new KeyValue { value = id });
                return Json(res);
            }
            return Json(new
            {
                Success= false,
                Message= "Failed"
            });
        }
        public async Task<IActionResult> DeleteIngredient(decimal? id)
        {
            if (id != null && id > 0)
            {
                var res = await _request.ApiCallPost<bool>("Food", "DeleteIngredientById", new KeyValue { value = id });
                return Json(res);
            }
            return Json(new
            {
                Success = false,
                Message = "Failed"
            });
        }
        public async Task<IActionResult> View(decimal? id)
        {
            var result = new RecipeDetails();
            if (id != null)
            {
                var response = await _request.ApiCallPost<RecipeDetails>("Food", "GetRecipeById", new KeyValue { value = id });
                result = response.Result;
            }
            return View("View", result);
        }
        public async Task<IActionResult> ChangeFav(decimal? id,bool? change)
        {
            if (id != null && change != null)
            {
                var response = await _request.ApiCallPost<bool>("Food", "ChangeFav", new KeyValue { value = id,key= change.Value ? "Y" : "N" });
                return Json(response);
            }
            return Json(new {sucess=false,message="failed"});
        }
        public async Task<IActionResult> Favourites(PagerRequest request)
        {
            ViewData["section"] = "Favourites";
            HttpContext.Session.SetString("PagerRequest", JsonConvert.SerializeObject(request));
            var response = await _request.ApiCallPost<PagerResponse<RecipeDetails>>("Food", "GetFavourites", new PagerRequest());
            if (response.Result != null) response.Result.VIEW = request.view;
            return View("Index", response.Result);
        }
        public async Task<IActionResult> SaveImage(ImageDetails imgDetails)
        {
            var response = await _request.ApiCallPost<Guid?>("Documents", "SaveImage", imgDetails);
            return Json(response);
        }
        public async Task<IActionResult> LoadPages(PagerRequest request)
        {
            HttpContext.Session.SetString("PagerRequest", JsonConvert.SerializeObject(request));
            var response = await _request.ApiCallPost<PagerResponse<RecipeDetails>>("Food", "GetRecipeList", request);
            if(response.Result != null) response.Result.VIEW = request.view;
            return PartialView("Pages",response.Result);
        }
    }
}
