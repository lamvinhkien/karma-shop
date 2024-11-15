using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class CategoryController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Categorys = Provider.Category.GetCategorysForDashboard();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Category category, IFormFile fileUpload)
    {
        if (fileUpload != null && fileUpload.Length > 0)
        {
            var fileName = Path.GetFileName(fileUpload.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/category/", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            category.ImageURL = "/img/category/" + fileName;

            Provider.Category.Add(category);

            return Redirect("/dashboard/category");
        }
        return Redirect("/dashboard/category");
    }

    public IActionResult Edit(byte id)
    {
        return View(Provider.Category.GetCategory(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(byte id, Category category, IFormFile fileUpload)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        var existingCategory = Provider.Category.GetCategory(id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        if (fileUpload != null && fileUpload.Length > 0)
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCategory.ImageURL.TrimStart('/'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            var fileName = Path.GetFileName(fileUpload.FileName);
            var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/category/", fileName);

            using (var stream = new FileStream(newImagePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            category.ImageURL = "/img/category/" + fileName;
        }
        else
        {
            category.ImageURL = existingCategory.ImageURL;
        }

        Provider.Category.Edit(category);

        return Redirect("/dashboard/category");
    }


    public IActionResult Delete(short id)
    {
        int ret = Provider.Category.Delete(id);
        if (ret > 0)
        {
            return Redirect("/dashboard/category");
        }
        return Redirect("/dashboard/category/error");
    }

}