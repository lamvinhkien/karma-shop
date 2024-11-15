using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;
public class AuthController : BaseController
{
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(Member obj)
    {
        obj.MemberId = Guid.NewGuid().ToString().Replace("-", "");
        obj.Password = Helper.Hash(obj.Password);
        int ret = Provider.Member.Add(obj);
        if (ret > 0)
        {
            return Redirect("/auth/login");
        }
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/auth/login");
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel obj)
    {
        if (ModelState.IsValid)
        {
            Member? member = Provider.Member.Login(obj);
            if (member != null)
            {
                List<Claim> list = new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, member.MemberId),
                    new Claim(ClaimTypes.Email, member.Email),
                    new Claim(ClaimTypes.GivenName, member.GivenName),
                    new Claim(ClaimTypes.Surname, member.SurName),
                    new Claim(ClaimTypes.Name, member.Name),
                    new Claim(ClaimTypes.Role, member.Role)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(list, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    IsPersistent = obj.Rem
                });

                TempData["Msg"] = "Login success";
                return Redirect("/");
            }
            ModelState.AddModelError("Error", "Login failed");
        }
        return View(obj);
    }

    public IActionResult GoogleLogin()
    {
        string? url = Url.Action("googleresponse", "auth", null, protocol: Request.Scheme);
        if (string.IsNullOrEmpty(url))
        {
            return Redirect("/auth/login");
        }
        AuthenticationProperties properties = new AuthenticationProperties { RedirectUri = url };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result != null && result.Principal != null)
        {
            var claims = result.Principal.Claims;
            var member = new Member
            {
                MemberId = claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value,
                Email = claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value,
                Name = claims.FirstOrDefault(p => p.Type == ClaimTypes.Name).Value,
                GivenName = claims.FirstOrDefault(p => p.Type == ClaimTypes.GivenName).Value,
                SurName = claims.FirstOrDefault(p => p.Type == ClaimTypes.Surname).Value,
                Password = Helper.Hash("asdasnldajeoidanjskldsnlkdnlsa")
            };

            Provider.Member.Add(member);
            return Redirect("/");
        }
        return View();
    }

}