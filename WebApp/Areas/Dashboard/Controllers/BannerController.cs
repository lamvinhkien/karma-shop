using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class BannerController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Banners = Provider.Banner.GetBannersForDashboard();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Banner banner, IFormFile fileUpload)
    {
        if (fileUpload != null && fileUpload.Length > 0)
        {
            var fileName = Path.GetFileName(fileUpload.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/banner/", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            banner.ImageURL = "/img/banner/" + fileName;

            Provider.Banner.Add(banner);

            return Redirect("/dashboard/banner");
        }
        return Redirect("/dashboard/banner");
    }

    public IActionResult Edit(byte id)
    {
        return View(Provider.Banner.GetBanner(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(byte id, Banner banner, IFormFile fileUpload)
    {
        if (id != banner.BannerId)
        {
            return BadRequest();
        }

        var existingBanner = Provider.Banner.GetBanner(id);
        if (existingBanner == null)
        {
            return NotFound();
        }

        if (fileUpload != null && fileUpload.Length > 0)
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBanner.ImageURL.TrimStart('/'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            var fileName = Path.GetFileName(fileUpload.FileName);
            var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/banner/", fileName);

            using (var stream = new FileStream(newImagePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            banner.ImageURL = "/img/banner/" + fileName;
        }
        else
        {
            banner.ImageURL = existingBanner.ImageURL;
        }

        Provider.Banner.Edit(banner);

        return Redirect("/dashboard/banner");
    }


    public IActionResult Delete(short id)
    {
        int ret = Provider.Banner.Delete(id);
        if (ret > 0)
        {
            return Redirect("/dashboard/banner");
        }
        return Redirect("/dashboard/banner/error");
    }

}