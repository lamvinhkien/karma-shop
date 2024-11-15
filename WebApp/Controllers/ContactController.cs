using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class ContactController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}