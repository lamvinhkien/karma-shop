using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class BrandController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Brands = Provider.Brand.GetBrandsForDashboard();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Brand brand, IFormFile fileUpload)
    {
        if (fileUpload != null && fileUpload.Length > 0)
        {
            var fileName = Path.GetFileName(fileUpload.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/brand/", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            brand.ImageURL = "/img/brand/" + fileName;

            Provider.Brand.Add(brand);

            return Redirect("/dashboard/brand");
        }
        return Redirect("/dashboard/brand");
    }

    public  IActionResult Edit(byte id)
    {
        return View(Provider.Brand.GetBrand(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(byte id, Brand brand, IFormFile fileUpload)
    {
        if (id != brand.BrandId)
        {
            return BadRequest();
        }

        var existingBrand = Provider.Brand.GetBrand(id);
        if (existingBrand == null)
        {
            return NotFound();
        }

        if (fileUpload != null && fileUpload.Length > 0)
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBrand.ImageURL.TrimStart('/'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            var fileName = Path.GetFileName(fileUpload.FileName);
            var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/brand/", fileName);

            using (var stream = new FileStream(newImagePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            brand.ImageURL = "/img/brand/" + fileName;
        }
        else
        {
            brand.ImageURL = existingBrand.ImageURL;
        }

        Provider.Brand.Edit(brand);

        return Redirect("/dashboard/brand");
    }


    public IActionResult Delete(short id)
    {
        int ret = Provider.Brand.Delete(id);
        if (ret > 0)
        {
            return Redirect("/dashboard/brand");
        }
        return Redirect("/dashboard/brand/error");
    }

}