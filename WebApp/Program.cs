using WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddScoped<VnPayService>();
builder.Services.Configure<VnPay>(builder.Configuration.GetSection("Payment:VnPay"));
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(p =>
    {
        p.LoginPath = "/auth/login";
        p.LogoutPath = "/auth/logout";
        p.AccessDeniedPath = "/auth/denied";
        p.ExpireTimeSpan = TimeSpan.FromDays(30);
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new Exception("Not found ClientId");
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new Exception("Not found ClientSecret");
    });

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(name: "dashboard",
    pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();
