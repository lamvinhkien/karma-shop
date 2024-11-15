using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class BlogController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Blogs = Provider.Blog.GetBlogsForDashboard();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Blog blog, IFormFile fileUpload)
    {
        if (fileUpload != null && fileUpload.Length > 0)
        {
            var fileName = Path.GetFileName(fileUpload.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/blog/", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            blog.ImageURL = "/img/blog/" + fileName;

            Provider.Blog.Add(blog);

            return Redirect("/dashboard/blog");
        }
        return Redirect("/dashboard/blog");
    }

    public IActionResult Edit(byte id)
    {
        return View(Provider.Blog.GetBlog(id));
    }
    [HttpPost]
    public async Task<IActionResult> Edit(byte id, Blog blog, IFormFile fileUpload)
    {
        if (id != blog.BlogId)
        {
            return BadRequest();
        }

        var existingBlog = Provider.Blog.GetBlog(id);
        if (existingBlog == null)
        {
            return NotFound();
        }

        if (fileUpload != null && fileUpload.Length > 0)
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBlog.ImageURL.TrimStart('/'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            var fileName = Path.GetFileName(fileUpload.FileName);
            var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/blog/", fileName);

            using (var stream = new FileStream(newImagePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            blog.ImageURL = "/img/blog/" + fileName;
        }
        else
        {
            blog.ImageURL = existingBlog.ImageURL;
        }

        Provider.Blog.Edit(blog);

        return Redirect("/dashboard/blog");
    }


    public IActionResult Delete(short id)
    {
        int ret = Provider.Blog.Delete(id);
        if (ret > 0)
        {
            return Redirect("/dashboard/blog");
        }
        return Redirect("/dashboard/blog/error");
    }

}