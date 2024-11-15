using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class ProductController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Products = Provider.Product.GetProductsForDashboard();
        ViewBag.Categories = Provider.Category.GetCategorysForDashboard();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product, IFormFile fileUpload)
    {
        if (fileUpload != null && fileUpload.Length > 0)
        {
            var fileName = Path.GetFileName(fileUpload.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product/", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            product.ImageURL = "/img/product/" + fileName;

            Provider.Product.Add(product);

            return Redirect("/dashboard/product");
        }
        return Redirect("/dashboard/product");
    }

    public IActionResult Edit(byte id)
    {
        ViewBag.Categories = Provider.Category.GetCategorysForDashboard();
        return View(Provider.Product.GetProduct(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(byte id, Product product, IFormFile fileUpload)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }

        var existingProduct = Provider.Product.GetProduct(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        if (fileUpload != null && fileUpload.Length > 0)
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProduct.ImageURL.TrimStart('/'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            var fileName = Path.GetFileName(fileUpload.FileName);
            var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product/", fileName);

            using (var stream = new FileStream(newImagePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            product.ImageURL = "/img/product/" + fileName;
        }
        else
        {
            product.ImageURL = existingProduct.ImageURL;
        }

        Provider.Product.Edit(product);

        return Redirect("/dashboard/product");
    }


    public IActionResult Delete(short id)
    {
        int ret = Provider.Product.Delete(id);
        if (ret > 0)
        {
            return Redirect("/dashboard/product");
        }
        return Redirect("/dashboard/product/error");
    }

}