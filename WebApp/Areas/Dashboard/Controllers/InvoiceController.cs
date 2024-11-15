using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class InvoiceController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Invoices = Provider.Invoice.GetInvoices();
        return View();
    }

    public IActionResult Detail(string id)
    {
        ViewBag.ProductByInvoice = Provider.InvoiceDetail.GetProductByInvoiceDetail(id);
        return View(Provider.Invoice.GetInvoice(id));
    }

    public IActionResult Delete(string id)
    {
        int ret = Provider.Invoice.Delete(id);
        if (ret > 0)
        {
            return Redirect("/dashboard/invoice");
        }
        return Redirect("/dashboard/invoice/error");
    }
}