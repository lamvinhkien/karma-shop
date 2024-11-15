using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class BlogController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Blogs = Provider.Blog.GetBlogs();
        return View();
    }

    public IActionResult Details(int id)
    {
        return View(Provider.Blog.GetBlogClient(id));

    }
}