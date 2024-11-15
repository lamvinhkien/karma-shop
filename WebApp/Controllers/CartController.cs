using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class CartController : BaseController
{
    VnPayService service;
    public CartController(VnPayService service)
    {
        this.service = service;
    }
    const string Cart = "cart";
    public IActionResult Index()
    {
        string? code = Request.Cookies[Cart];
        if (string.IsNullOrEmpty(code))
        {
            return Redirect("/");
        }
        return View(Provider.Cart.GetCarts(code));
    }

    [HttpPost]
    public IActionResult Edit(Cart obj)
    {
        string? code = Request.Cookies[Cart];
        if (string.IsNullOrEmpty(code))
        {
            return Json(0);
        }
        obj.CartCode = code;
        return Json(Provider.Cart.Edit(obj));
    }

    public IActionResult Add(Cart obj)
    {
        string? code = Request.Cookies[Cart];
        if (string.IsNullOrEmpty(code))
        {
            code = Guid.NewGuid().ToString().Replace("-", string.Empty);
            Response.Cookies.Append(Cart, code, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
        }

        obj.CartCode = code;
        int ret = Provider.Cart.Add(obj);
        if (ret > 0)
        {
            return Redirect("/cart");
        }
        return Redirect("/cart/error");
    }

    public IActionResult Delete(int id)
    {
        int ret = Provider.Cart.Delete(id);
        if (ret > 0)
        {
            return Redirect("/cart");
        }
        return Redirect("/cart/error");
    }

    public IActionResult Checkout()
    {
        string? code = Request.Cookies[Cart];
        if (string.IsNullOrEmpty(code))
        {
            return Redirect("/");
        }
        return View(Provider.Cart.GetCarts(code));
    }
    [HttpPost]
    public IActionResult Checkout(Invoice obj)
    {
        string? code = Request.Cookies[Cart];
        if (string.IsNullOrEmpty(code))
        {
            return Redirect("/");
        }
        obj.CartCode = code;
        int ret = Provider.Invoice.Add(obj);
        if (ret > 0)
        {
            decimal amount = Provider.Invoice.GetAmount(obj.InvoiceId);
            obj.Amount = amount * 1000;
            string url = service.ToUrl(obj);
            return Redirect(url);
        }

        ModelState.AddModelError("Error", "Insert Failed");
        return View(obj);
    }

    public IActionResult VnPaymentResponse(VnPayResponse obj)
    {
        int ret = Provider.VnPay.Add(obj);
        if (ret > 0)
        {
            return Redirect("/cart/success");
        }
        return Redirect("/cart/error");

    }

    public IActionResult Success(){
        return View();
    }
}
