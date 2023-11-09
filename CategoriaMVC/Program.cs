using CatalogoMVC.Services;
using CatalogoMVC.Services.Interfaces;
using CategoriaMVC.Services;
using CategoriaMVC.Services.Interfaces;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("CategoryApi", C =>
{
    C.BaseAddress = new Uri(builder.Configuration["ServiceUri:CategoryApi"]);
});
builder.Services.AddHttpClient("ProductApi", C =>
{
    C.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]);
});
builder.Services.AddHttpClient("AutenticaApi", C =>
{
    C.BaseAddress = new Uri(builder.Configuration["ServiceUri:AutenticaApi"]);
    C.DefaultRequestHeaders.Accept.Clear();
    C.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IAuthentication, AuthenticationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
