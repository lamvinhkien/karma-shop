using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.SalesMonthly = Provider.Invoice.GetSalesMonthly(DateTime.Now.Year);
        return View();
    }

    public IActionResult SalesCategory(int year)
    {
        return Json(Provider.Invoice.GetSalesByCategory(year));
    }

    public IActionResult SalesByMonth(int year)
    {
        return Json(Provider.Invoice.GetSalesByMonth(year));
    }
}