using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Dashboard.Controllers;
[Area("dashboard")]
[Authorize(Roles = "Admin")]

public class MemberController : BaseController
{
    public IActionResult Index()
    {
        ViewBag.Members = Provider.Member.GetMembers();
        return View();
    }
    [HttpPost]
    public IActionResult AddMember(Member obj)
    {
        obj.MemberId = Guid.NewGuid().ToString().Replace("-", "");
        obj.Password = Helper.Hash(obj.Password);
        int ret = Provider.Member.Add(obj);
        if (ret > 0)
        {
            return Redirect("/dashboard/member");
        }
        ModelState.AddModelError("Error", "Insert Failed!");
        return View(obj);
    }

    public IActionResult Edit(string id)
    {
        return View(Provider.Member.GetMember(id));
    }
    [HttpPost]
    public IActionResult EditMember(Member obj)
    {
        obj.Role = obj.Role == null || obj.Role == "" ? "Customer" : "Admin";
        int ret = Provider.Member.Edit(obj);
        if (ret > 0)
        {
            return Redirect("/dashboard/member");
        }

        ModelState.AddModelError("Error", "Update Failed!");
        return View(obj);
    }

    public IActionResult Delete(string id)
    {
        int ret = Provider.Member.Delete(id);
        if (ret > 0)
        {
            return Redirect("/dashboard/member");
        }
        return Redirect("/dashboard/member/error");
    }
}