using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class CategoryController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Products = Provider.Product.GetProducts();
        ViewBag.Categorys = Provider.Category.GetCategorys();
        return View();
    }
}