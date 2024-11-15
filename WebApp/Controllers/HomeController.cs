using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class HomeController : BaseController
{
    static IEnumerable<Product>[] ToArray(IEnumerable<Product> list, int size)
    {
        int len = list.Count() / size;
        IEnumerable<Product>[] arr = new IEnumerable<Product>[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = list.Skip(i * len).Take(len);
        }
        return arr;
    }
    public IActionResult Index()
    {
        ViewBag.LastestProducts = ToArray(Provider.Product.GetLastestProducts(), 2);
        ViewBag.ExclusiveDeal = ToArray(Provider.Product.GetExclusiveDeal(), 2);
        ViewBag.DealOfWeek = ToArray(Provider.Product.GetDealOfWeek(), 2);
        ViewBag.Categorys = Provider.Category.GetCategorys();
        ViewBag.Brands = Provider.Brand.GetBrands();
        ViewBag.Banners = Provider.Banner.GetBanners();
        return View();
    }

    public IActionResult Details(int id)
    {
        ViewBag.CategoryName = Provider.Category.GetCategoryName(id);
        return View(Provider.Product.GetProductClient(id));
    }
}